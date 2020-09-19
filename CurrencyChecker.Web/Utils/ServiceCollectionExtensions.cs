using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CurrencyChecker.Web.Utils
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddBusinessLogic(this IServiceCollection services, List<Assembly> assembliesToScan)
		{
			services.AddMediatR(assembliesToScan.ToArray());
			return services;
		}

		public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services, List<Assembly> assembliesToScan)
		{
			services.AddAutoMapper(assembliesToScan);
			return services;
		}
	}
}
