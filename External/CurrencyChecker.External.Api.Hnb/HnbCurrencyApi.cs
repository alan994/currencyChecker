using CurrencyChecker.External.Api.Abstractions;
using CurrencyChecker.External.Api.Abstractions.Models;
using CurrencyChecker.External.Api.Hnb.Api;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyChecker.External.Api.Hnb
{
	public class HnbCurrencyApi : ICurrencyApi
	{
		private readonly IHnbApi api;

		public HnbCurrencyApi(IHnbApi api)
		{
			this.api = api;
		}
		public async Task<List<CurrencyModel>> GetCurrencyListAsync()
		{
			var response = await this.api.GetCurrencyListAsync();
			if (!response.ResponseMessage.IsSuccessStatusCode)
			{
				throw new ApiUnavailable();
			}

			return response.GetContent();
		}
	}
}
