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
			try
			{
				//Try to fetch data from API
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


			var newCurrencies = this.mapper.Map<List<Currency>>(apiResponse);
			var oldCurrencies = await this.GetCurrenciesFromDatabaseAsync();

			var oldcurrenciesForDelete = new List<Currency>();

			foreach(var oldCurrency in oldCurrencies)
			{
				var targetNewCurrency = newCurrencies.FirstOrDefault(x => x.Code == oldCurrency.Code);
				if(targetNewCurrency is null)
				{
					oldcurrenciesForDelete.Add(oldCurrency);
					continue;
				}

				this.mapper.Map(targetNewCurrency, oldCurrency);				
			}

			//Check if there is something to delete
			if (oldcurrenciesForDelete.Any())
			{
				this.db.Currencies.RemoveRange(oldcurrenciesForDelete);
			}

			//Check if there is something to add
			var toAdd = newCurrencies.Select(x => x.Code).Except(oldCurrencies.Select(x => x.Code).ToList()).ToList();
			if (toAdd.Any())
			{
				this.db.Currencies.AddRange(newCurrencies.Where(x => toAdd.Contains(x.Code)).ToList());
			}
									
			await this.db.SaveChangesAsync();

			//TODO: Candiate for optimization
			response.Currencies = await this.GetCurrenciesFromDatabaseAsync();

			return response;
		}

		private async Task<List<Currency>> GetCurrenciesFromDatabaseAsync()
		{
			return await this.db.Currencies.ToListAsync();
		}
	}
}
