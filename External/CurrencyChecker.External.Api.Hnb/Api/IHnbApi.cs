using CurrencyChecker.External.Api.Abstractions.Models;
using RestEase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyChecker.External.Api.Hnb.Api
{
	public interface IHnbApi
	{
		[Get]
		[AllowAnyStatusCode]
		Task<Response<List<CurrencyModel>>> GetCurrencyListAsync();
	}
}
