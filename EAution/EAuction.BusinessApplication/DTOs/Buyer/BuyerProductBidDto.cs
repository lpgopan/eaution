using EAuction.BusinessApplication.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAuction.BusinessApplication.DTOs.Buyer
{
    public class BuyerProductBidDto : BaseDto
    {
        public int BidId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Pin { get; set; }
        public long Phone { get; set; }
        public string Email { get; set; }
        public int BidAmount { get; set; }
        public int ProductId{ get; set; }
        public string ProductName { get; set; }
      /*  public string ShortDescription { get; set; }
        public string DetailedDescription { get; set; }
        public string Category { get; set; }
        public int Startingprice { get; set; }
        public DateTime BidEndDate { get; set; }
        public string productId { get; set; }*/
    }
}
