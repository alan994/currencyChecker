using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyChecker.External.Api.Abstractions.Models
{
	public class CurrencyModel
	{
		[JsonProperty("Šifra valute")]
		public string Code { get; set; }

		[JsonProperty("Valuta")]
		public string Mark { get; set; }

		[JsonProperty("Datum primjene")]
		public DateTime LastChanged { get; set; }

		[JsonProperty("Jedinica")]
		public int Quantity { get; set; }

		[JsonProperty("Kupovni za devize")]
		public decimal Buy { get; set; }

		[JsonProperty("Srednji za devize")]
		public decimal Middle { get; set; }

		[JsonProperty("Prodajni za devize")]
		public decimal Sell { get; set; }

		[JsonProperty("Država")]
		public string Country { get; set; }

	}
}
