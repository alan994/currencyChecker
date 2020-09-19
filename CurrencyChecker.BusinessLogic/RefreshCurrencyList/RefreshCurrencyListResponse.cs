using CurrencyChecker.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyChecker.BusinessLogic.RefreshCurrencyList
{
	public class RefreshCurrencyListResponse
	{
		public bool UpdateSuccessful { get; set; } = true;
		public List<Currency> Currencies { get; set; }
	}
}
