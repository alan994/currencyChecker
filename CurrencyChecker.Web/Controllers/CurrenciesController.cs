using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurrencyChecker.BusinessLogic.RefreshCurrencyList;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CurrencyChecker.Web.Controllers
{
	[ApiController]
	[Route("currencies")]
	public class CurrenciesController : Controller
	{		
		private readonly ILogger<CurrenciesController> logger;
		private readonly IMediator mediator;

		public CurrenciesController(ILogger<CurrenciesController> logger, IMediator mediator)
		{
			this.logger = logger;
			this.mediator = mediator;
		}

		[HttpGet]
		public async Task<ActionResult<RefreshCurrencyListResponse>> GetAsync()
		{
			var response = await this.mediator.Send(new RefreshCurrencyListRequest());
			return this.Ok(response);
		}
	}
}
