using Confluent.Kafka;
using EAuction.BusinessApplication.Configuration;
using EAuction.BusinessApplication.Exceptions;
using EAuction.BusinessApplication.Features.Seller.Requests.Queries;
using EAuction.BusinessApplication.Persistance;
using EAuction.BusinessApplication.Response;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace EAuction.BusinessApplication.Features.Seller.Handlers.Queries
{
    public class ListAllBidsByProductQueriesHandler : IRequestHandler<ListAllBidsByProductQueries, SellerBidListResponse>
    {
        private readonly IBuyerRepository _buyerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOptions<EAuctionConfiguration> _configurations;
        private readonly ILogger _logger;
        public ListAllBidsByProductQueriesHandler(IBuyerRepository buyerRepository, IProductRepository productRepository, 
            IOptions<EAuctionConfiguration> configurations, ILogger<ListAllBidsByProductQueriesHandler> logger)
        {
            this._buyerRepository = buyerRepository;
            this._productRepository = productRepository;
            this._configurations = configurations;
            _logger = logger;
        }
        public async Task<SellerBidListResponse> Handle(ListAllBidsByProductQueries request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"ListAllBids Started at {DateTime.Now}");
            var response = new SellerBidListResponse();
            const int commitPeriod = 5;
            try
            {
                Domain.Product product = await _productRepository.getProductByProductId(request.ProductId);
                response.ProductId = request.ProductId;
                if (product != null)
                {
                    response.ProductName = product.ProductName;
                    response.ShortDescription = product.ShortDescription;
                    response.Startingprice = product.Startingprice;
                    response.DetailedDescription = product.DetailedDescription;
                    response.Category = product.Category;
                    response.BidEndDate = product.BidEndDate;

                    List<Domain.Buyer> buyers = await _buyerRepository.getBuyerDetailsByProductId(product.ProductId);

                    List<ProductBid> productBids = getProductBids(buyers);
                    if (_configurations.Value.EnableKafka)
                    {
                        productBids = getProductBidsFromKafka(request, commitPeriod, ref productBids);
                    }
                    response.ProductBids = productBids;
                }
            }
            catch (Exception ex)
            {
                String error = String.Format("Listing product bids failed. %s", ex.Message);
                _logger.LogError(error);
                throw new BadRequestException(error);

            }
            _logger.LogInformation($"ListAllBids Ended at {DateTime.Now}");
            return response;
        }

        private static List<ProductBid> getProductBidsFromKafka(ListAllBidsByProductQueries request, int commitPeriod, ref List<ProductBid> productBids)
        {
            List<Domain.Buyer> kafkaBuyers = new List<Domain.Buyer>();
            var conf = new ConsumerConfig
            {
                GroupId = "test-consumer-group",
                BootstrapServers = "localhost:9092",
                // AutoOffsetReset = AutoOffsetReset.Earliest,
                //  EnableAutoOffsetStore = false
                EnableAutoCommit = false,
                StatisticsIntervalMs = 5000,
                SessionTimeoutMs = 6000,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnablePartitionEof = true,
                // A good introduction to the CooperativeSticky assignor and incremental rebalancing:
                // https://www.confluent.io/blog/cooperative-rebalancing-in-kafka-streams-consumer-ksqldb/
                PartitionAssignmentStrategy = PartitionAssignmentStrategy.CooperativeSticky
            };
            var c = new ConsumerBuilder<int, string>(conf).Build();
            c.Subscribe("product_bid");

            // Because Consume is a blocking call, we want to capture Ctrl+C and use a cancellation token to get out of our while loop and close the consumer gracefully.
            var cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
            };

            try
            {
                while (true)
                {
                    // Consume a message from the test topic. Pass in a cancellation token so we can break out of our loop when Ctrl+C is pressed
                    var cr = c.Consume(cts.Token); //TimeSpan.FromMilliseconds(10000));// cts.Token);
                    if (cr == null)
                    {
                        break;
                    }
                    if (cr.IsPartitionEOF)
                    {
                        Console.WriteLine(
                            $"Reached end of topic { cr.Topic}, partition {cr.Partition}, offset {cr.Offset}.");

                        break;
                    }

                    Console.WriteLine($"Consumed message '{cr.Message.Value}' from topic {cr.Topic}, partition {cr.Partition}, offset {cr.Offset}");
                    Domain.Buyer kafkaBuyer = JsonSerializer.Deserialize<Domain.Buyer>(cr.Message.Value);
                    if (kafkaBuyer.ProductId == request.ProductId)
                    {
                        kafkaBuyers.Add(kafkaBuyer);
                    }
                    if (cr.Offset % commitPeriod == 0)
                    {
                        // The Commit method sends a "commit offsets" request to the Kafka
                        // cluster and synchronously waits for the response. This is very
                        // slow compared to the rate at which the consumer is capable of
                        // consuming messages. A high performance application will typically
                        // commit offsets relatively infrequently and be designed handle
                        // duplicate messages in the event of failure.
                        try
                        {
                            c.Commit(cr);
                        }
                        catch (KafkaException e)
                        {
                            Console.WriteLine($"Commit error: {e.Error.Reason}");
                        }
                    }
                }
            }
            catch (OperationCanceledException ex)
            {
                throw new BadRequestException(String.Format("Listing product bids by consuming from kafka failed. %s", ex.Message));
            }
            finally
            {
                c.Close();
            }
            if (kafkaBuyers.Count > 0)
            {
                productBids = getProductBids(kafkaBuyers);
            }

            return productBids;
        }

        private static List<ProductBid> getProductBids(List<Domain.Buyer> buyers)
        {
            List<ProductBid> productBids = new List<ProductBid>();
            foreach (Domain.Buyer buyer in buyers)
            {
                productBids.Add(new ProductBid()
                {
                    BidId = buyer.BidId,
                    FirstName = buyer.FirstName,
                    LastName = buyer.LastName,
                    Address = buyer.Address,
                    City = buyer.City,
                    State = buyer.State,
                    Pin = buyer.Pin,
                    Phone = buyer.Phone,
                    Email = buyer.Email,
                    BidAmount = buyer.BidAmount,
                    ProductId = buyer.ProductId

                });
            }
            productBids.Sort((x, y) => x.BidAmount.CompareTo(y.BidAmount));
            productBids.Reverse();
            return productBids;
        }
    }
}
