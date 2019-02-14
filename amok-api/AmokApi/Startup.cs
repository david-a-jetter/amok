using AmokApi.Controllers.Dtos;
using AmokApi.ExchangeRates;
using AmokApi.ExchangeRates.Ecb;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Runtime.Caching;

namespace AmokApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSingleton((IServiceProvider _) => new HttpClient());
            services.AddSingleton((IServiceProvider _) => new MemoryCache("ExchangeRates"));

            services.AddTransient<IExchangeRatesDtoFactory>((IServiceProvider _) => new ExchangeRatesDtoFactory());
            services.AddTransient<IExchangeRatesManager>((IServiceProvider sp) =>
                new ExchangeRatesManager(
                    new CachingExchangeRatesAccess(
                        sp.GetRequiredService<MemoryCache>(),
                        new EcbExchangeRatesAccess(
                            new EcbJsonResponseHandler(),
                            sp.GetRequiredService<HttpClient>(),
                            new Uri("https://api.exchangeratesapi.io"))),
                    new ExchangeRatesEngine()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
