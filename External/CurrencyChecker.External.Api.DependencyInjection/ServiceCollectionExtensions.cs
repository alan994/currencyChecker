using CurrencyChecker.External.Api.Abstractions;
using CurrencyChecker.External.Api.Hnb;
using CurrencyChecker.External.Api.Hnb.Api;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestEase;
using System;

namespace CurrencyChecker.External.Api.DependencyInjection
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddCurrencyApi(this IServiceCollection services)
		{
			services.AddSingleton<IHnbApi>((serviceProvider) =>
			{
				var configuration = serviceProvider.GetService<IConfiguration>();
				if (configuration is null)
				{
					throw new Exception("Configuration not defined");
				}
				return RestClient.For<IHnbApi>(configuration["HnbUrl"]);
			});

			services.AddSingleton<ICurrencyApi, HnbCurrencyApi>();

			return services;
		}
	}
}
