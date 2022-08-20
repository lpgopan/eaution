using System;
using System.Collections.Generic;
using System.Text;

namespace EAuction.BusinessApplication.Exceptions
{
    public class BadRequestException : ApplicationException
    {
        public BadRequestException(string message) : base(message)
        {

        }
    }
}
