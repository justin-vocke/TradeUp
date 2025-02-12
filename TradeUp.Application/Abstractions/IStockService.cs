﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeUp.Application.DTO.FinnHub;
using TradeUp.Domain.Core.Entities;

namespace TradeUp.Application.Abstractions
{
    public interface IStockService
    {

        Task CheckThresholdsAsync();
        //IAsyncEnumerable<FinnHubStockDto> GetStockUpdates(CancellationToken stoppingToken);
    }
}
