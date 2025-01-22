using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeUp.Infrastructure
{
    public class MailGunOptions
    {
        public string BaseUrl { get; set; }
        public string SandBoxDomain { get; set; }
        public string ApiKey { get; set; }
        public string SandboxTo { get; set; }
    }
}
