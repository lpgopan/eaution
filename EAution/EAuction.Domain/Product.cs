using EAuction.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EAuction.Domain
{
    public class Product : BaseDomainEntity
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ShortDescription { get; set; }
        public string DetailedDescription { get; set; }
        public string Category { get; set; }
        public int Startingprice { get; set; }
        public DateTime BidEndDate { get; set; }
        public Seller SellerInfo { get; set; }
       public List<int> Buyers { get; set; }
    }
}
