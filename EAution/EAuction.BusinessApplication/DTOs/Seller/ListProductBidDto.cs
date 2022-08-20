using EAuction.BusinessApplication.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAuction.BusinessApplication.DTOs
{
    public class ListProductBidDto : BaseDto
    {
        public string ProductName { get; set; }
        public string ShortDescription { get; set; }
        public string DetailedDescription { get; set; }
        public string Category { get; set; }
        public int Startingprice { get; set; }
        public List<BuyerDto> PlacedBids { get; set; }
}
}
