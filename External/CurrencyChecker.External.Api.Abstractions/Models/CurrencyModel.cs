using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyChecker.External.Api.Abstractions.Models
{
	public class CurrencyModel
	{
		public Guid Id { get; set; }
		public string Code { get; set; }
		public string Mark { get; set; }
		public DateTime UpdatedOn { get; set; }
		public DateTime LastChanged { get; set; }
		public int Quantity { get; set; }
		public decimal Buy { get; set; }
		public decimal Middle { get; set; }
		public decimal Sell { get; set; }
		public string Country { get; set; }
	}
}
