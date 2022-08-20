using EAuction.BusinessApplication.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAuction.BusinessApplication.DTOs.Buyer
{
    public class UpdateBidAmountDto : BaseDto
    {
        public int BidId { get; set; }
        public int BidAmount { get; set; }
    }
}
