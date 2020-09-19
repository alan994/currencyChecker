using CurrencyChecker.External.Api.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyChecker.External.Api.Abstractions
{
	public interface ICurrencyApi
	{
		Task<List<CurrencyModel>> GetCurrencyListAsync();
	}
}
