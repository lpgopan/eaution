﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EAuction.BusinessApplication.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string name, object key) : base($"{name} ({key}) was not found")
        {

        }
    }
}
