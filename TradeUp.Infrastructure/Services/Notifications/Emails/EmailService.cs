using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TradeUp.Application.Abstractions.Notifications.Email;

namespace TradeUp.Infrastructure.Services.Notifications.Emails
{
    internal sealed class EmailService : IEmailService
    {  
        private readonly MailGunOptions _mailgunOptions;
        public EmailService(IOptions<MailGunOptions> mailGunOptions)
        {
            _mailgunOptions = mailGunOptions.Value;
        }
        public RestResponse SendEmailAsync(string to, string subject, string body)
        {
            RestClient client = new RestClient(new Uri(_mailgunOptions.BaseUrl));

            // Add Basic Authentication to the request as a header using AddDefaultParameter
            client.AddDefaultParameter("Authorization",
                "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes($"api:{_mailgunOptions.ApiKey}")),
                ParameterType.HttpHeader);
            RestRequest request = new RestRequest();
            request.AddParameter("domain", _mailgunOptions.SandBoxDomain, ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", $"Excited User <mailgun@{_mailgunOptions.SandBoxDomain}>");
            request.AddParameter("to", _mailgunOptions.SandboxTo);
            request.AddParameter("subject", subject);
            request.AddParameter("text", body);
            request.Method = Method.Post;
            return client.Execute(request);
        }

        public RestResponse SendSimpleMessage()
        {
           
            RestClient client = new RestClient(new Uri(_mailgunOptions.BaseUrl));
            
            // Add Basic Authentication to the request as a header using AddDefaultParameter
            client.AddDefaultParameter("Authorization",
                "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes($"api:{_mailgunOptions.ApiKey}")),
                ParameterType.HttpHeader);
            RestRequest request = new RestRequest();
            request.AddParameter("domain", _mailgunOptions.SandBoxDomain, ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", $"Excited User <mailgun@{_mailgunOptions.SandBoxDomain}>");
            request.AddParameter("to", _mailgunOptions.SandboxTo);
            request.AddParameter("subject", "Hello");
            request.AddParameter("text", "Testing some Mailgun awesomeness!");
            request.Method = Method.Post;
            return client.Execute(request);
        }


    }
}
