using AutoMapper;
using CurrencyChecker.Data;
using CurrencyChecker.External.Api.Abstractions;
using CurrencyChecker.External.Api.Abstractions.Models;
using CurrencyChecker.External.Api.Hnb;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CurrencyChecker.BusinessLogic.RefreshCurrencyList
{
	public class RefreshCurrencyListHandler : IRequestHandler<RefreshCurrencyListRequest, RefreshCurrencyListResponse>
	{
		private readonly ICurrencyApi currencyApi;
		private readonly CurrencyContext db;
		private readonly ILogger<RefreshCurrencyListHandler> logger;
		private readonly IMapper mapper;

		public RefreshCurrencyListHandler(ICurrencyApi currencyApi, CurrencyContext db, ILogger<RefreshCurrencyListHandler> logger, IMapper mapper)
		{
			this.currencyApi = currencyApi;
			this.db = db;
			this.logger = logger;
			this.mapper = mapper;
		}

		public async Task<RefreshCurrencyListResponse> Handle(RefreshCurrencyListRequest request, CancellationToken cancellationToken)
		{
			var response = new RefreshCurrencyListResponse();

			List<CurrencyModel> apiResponse = null;
			//Try to fetch data from API
			try
			{
				apiResponse = await this.currencyApi.GetCurrencyListAsync();
			}
			catch(ApiUnavailable ex)
			{
				this.logger.LogError(ex, "Currency API unavailable.");
				response.UpdateSuccessful = false;
				response.Currencies = await this.GetCurrenciesFromDatabaseAsync();		
				return response;
			}
			catch(Exception ex)
			{
				this.logger.LogError(ex, "Fatching from API failed.");
				response.UpdateSuccessful = false;
				response.Currencies = await this.GetCurrenciesFromDatabaseAsync();
				return response;
			}


			var currencies = this.mapper.Map<List<Currency>>(apiResponse);

			this.db.Currencies.AddRange(currencies);
			await this.db.SaveChangesAsync();

			response.Currencies = currencies;

			return response;
		}

		private async Task<List<Currency>> GetCurrenciesFromDatabaseAsync()
		{
			return await this.db.Currencies.ToListAsync();
		}
	}
}
