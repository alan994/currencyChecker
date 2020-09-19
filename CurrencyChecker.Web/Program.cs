using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace CurrencyChecker.Web
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.ConfigureLogging((host, logging) =>
					{
						logging.ClearProviders();
					});

					webBuilder.UseSerilog((webHostBuilderConfiguration, loggerConfiguration) =>
					{
						loggerConfiguration.ReadFrom.Configuration(webHostBuilderConfiguration.Configuration.GetSection("All"));
					});

					webBuilder.UseStartup<Startup>();
				});
	}
}
