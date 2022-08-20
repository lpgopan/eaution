using EAuction.BusinessApplication.DTOs.Common;
using EAuction.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAuction.BusinessApplication.DTOs
{
    public class BuyerDto : BaseDto
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
    }
}
