using System.Collections.Generic;
using System.Reflection;
using CurrencyChecker.BusinessLogic.RefreshCurrencyList;
using CurrencyChecker.Data;
using CurrencyChecker.External.Api.DependencyInjection;
using CurrencyChecker.Web.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CurrencyChecker.Web
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			var cs = this.Configuration["ConnectionString"];
			services.AddDbContext<CurrencyContext>(options =>
			{
				options.UseSqlServer(cs, sqlOptions =>
				{
					sqlOptions.MigrationsAssembly("CurrencyChecker.Data");
				});
			});

			var assemblies = new List<Assembly>()
			{
				typeof(RefreshCurrencyListHandler).Assembly
			};

			services.AddCurrencyApi();
			services.AddBusinessLogic(assemblies);
			services.AddAutoMapperConfiguration(assemblies);
			services.AddControllers();
			services.AddSwaggerGen();
		}
				
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, CurrencyContext db)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				db.Database.Migrate();
			}

			app.UseHttpsRedirection();

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Currency API V1");
			});

			app.UseRouting();
						
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
