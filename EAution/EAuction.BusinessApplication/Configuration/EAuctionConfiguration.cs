using System;
using System.Collections.Generic;
using System.Text;

namespace EAuction.BusinessApplication.Configuration
{
    public class EAuctionConfiguration
    {
        public bool EnableKafka { get; set; }
        public string SellerCollectionName { get; set; }
        public string BuyerCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        
    }
}
