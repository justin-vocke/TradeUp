﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeUp.Domain.Core.Entities
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
    }
}
