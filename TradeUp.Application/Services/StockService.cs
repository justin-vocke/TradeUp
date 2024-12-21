using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tradeup.Domain.Core.Bus;
using TradeUp.Application.Commands;
using TradeUp.Application.Interfaces;
using TradeUp.Domain.Core.Entities;
using TradeUp.Domain.Core.Events;
using TradeUp.Domain.Core.Interfaces.Repositories;

namespace TradeUp.Application.Services
{
    public class StockService : IStockService
    {
        private readonly IEventBus _eventBus;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IStockApiClient _stockApiClient;

        public StockService(IEventBus eventBus, ISubscriptionRepository subscriptionRepository, IStockApiClient stockApiClient)
        {
            _eventBus = eventBus;
            _subscriptionRepository = subscriptionRepository;
            _stockApiClient = stockApiClient;
        }

       
        public async Task CheckThresholdsAsync()
        {
            //get unique tickers in subscriptions to know what stock api calls to make
            var tickers = await _subscriptionRepository.GetAllDistinctTickersFromSubscriptions();
            //get subscriptions from database
            var subscriptions = await _subscriptionRepository.GetAllSubscriptions();
            //only taking one for now as there's a limit to free version of stock api
            var firstTicker = tickers.FirstOrDefault();
            //get stock prices from external apip
            var stockInformation = await _stockApiClient.GetStockInfoAsync(firstTicker);
            var subsToCheck = subscriptions.Where(x => x.TickerSymbol == stockInformation.GlobalQuote.Symbol).ToList();
            var subsToCheckAboveThreshold = subsToCheck.Where(x => x.Position == Subscription.ThresholdPosition.Above);
            var subsToCheckBelowThreshold = subsToCheck.Where(x => x.Position == Subscription.ThresholdPosition.Below);
            foreach(var sub in subsToCheckAboveThreshold)
            {
                if(Convert.ToDecimal(stockInformation.GlobalQuote.High) >= sub.Threshold)
                {
                    //send message to queue to notify user
                    _eventBus.Publish(new PriceThresholdReachedEvent(sub.UserId, sub.Email, sub.TickerSymbol, sub.Threshold,
                        sub.Position));
                }
            }
            foreach(var sub in subsToCheckBelowThreshold)
            {
                if (Convert.ToDecimal(stockInformation.GlobalQuote.Low) <= sub.Threshold)
                {
                    //send message to queue to notify user
                    _eventBus.Publish(new PriceThresholdReachedEvent(sub.UserId, sub.Email, sub.TickerSymbol, sub.Threshold,
                        sub.Position));
                }
            }
        }


    }
}
