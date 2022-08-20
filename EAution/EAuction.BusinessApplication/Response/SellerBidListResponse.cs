using System;
using System.Collections.Generic;
using System.Text;

namespace EAuction.BusinessApplication.Response
{
    public class SellerBidListResponse
    {

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ShortDescription { get; set; }
        public string DetailedDescription { get; set; }
        public string Category { get; set; }
        public int Startingprice { get; set; }
        public DateTime BidEndDate { get; set; }
        public List<ProductBid> ProductBids { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
    }
}
