using System;
using System.Collections.Generic;
using System.Text;
using EAuction.Domain.Common;

namespace EAuction.Domain
{
    public class Buyer : BaseDomainEntity
    {
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Pin { get; set; } 
        public long Phone { get; set; }
        public string Email { get; set; }
        public int BidAmount { get; set; }
        public int ProductId { get; set; }
        public int BidId { get; set; }
    }
}
