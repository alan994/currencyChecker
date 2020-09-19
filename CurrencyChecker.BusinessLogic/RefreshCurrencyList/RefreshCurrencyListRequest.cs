using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyChecker.BusinessLogic.RefreshCurrencyList
{
	public class RefreshCurrencyListRequest : IRequest<RefreshCurrencyListResponse>
	{
	}
}
