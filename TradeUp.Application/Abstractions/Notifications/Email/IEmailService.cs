using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeUp.Application.Abstractions.Notifications.Email
{
    public interface IEmailService
    {
        RestResponse SendEmailAsync(string to, string subject, string body);

        RestResponse SendSimpleMessage();

    }
}
