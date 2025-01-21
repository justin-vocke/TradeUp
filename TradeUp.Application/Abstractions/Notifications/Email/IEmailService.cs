﻿using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeUp.Application.Abstractions.Notifications.Email
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);

        RestResponse SendSimpleMessage();

    }
}
