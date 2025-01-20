using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tradeup.Domain.Core.Bus;
using Tradeup.Infrastructure.Bus;
using TradeUp.Infrastructure;

namespace TradeUp.Application.IntegrationTests.Infrastructure
{
    public abstract class BaseIntegrationTest: IClassFixture<IntegrationTestWebAppFactory>
    {
        private readonly IServiceScope _scope;
        protected readonly ISender Sender;
        protected readonly ApplicationDbContext DbContext;
        protected readonly IEventBus EventBus;
        protected BaseIntegrationTest(IntegrationTestWebAppFactory factory) 
        {
            _scope = factory.Services.CreateScope();
            Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
            DbContext = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            EventBus = _scope.ServiceProvider.GetRequiredService<RabbitMQBus>();
        }
    }
}
