using System;
using EAuction.Domain.Common;

namespace EAuction.Domain
{
    public class Seller
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Pin { get; set; }
        public long Phone { get; set; }
        public string Email { get; set; }
    }
}
