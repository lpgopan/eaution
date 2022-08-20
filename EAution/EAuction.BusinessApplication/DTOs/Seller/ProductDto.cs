using EAuction.BusinessApplication.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAuction.BusinessApplication.DTOs.Seller
{
    public class ProductDto : BaseDto
    {
        public string ProductName { get; set; }
        public string ShortDescription { get; set; }
        public string DetailedDescription { get; set; }
        public string Category { get; set; }
        public int Startingprice { get; set; }
        public DateTime BidEndDate { get; set; }
    }
}
