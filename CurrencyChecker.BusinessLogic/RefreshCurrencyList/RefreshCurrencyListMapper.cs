using AutoMapper;
using CurrencyChecker.Data;
using CurrencyChecker.External.Api.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyChecker.BusinessLogic.RefreshCurrencyList
{
	public class RefreshCurrencyListMapper : Profile
	{
		public RefreshCurrencyListMapper()
		{
			this.CreateMap<CurrencyModel, Currency>();
		}
	}
}
