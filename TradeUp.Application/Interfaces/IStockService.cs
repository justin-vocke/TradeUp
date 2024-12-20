﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeUp.Domain.Core.Entities;

namespace TradeUp.Application.Interfaces
{
    public interface IStockService
    {
        void SendStockThresholdNotification(Subscription subscription);


        Task CheckThresholdsAsync();
    }
}
