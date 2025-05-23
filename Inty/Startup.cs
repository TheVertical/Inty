using System;
using Microsoft.OpenApi.Models;
using System.IO;
using Inty.Currencies.Services;
using Inty.RussianBank;
using Inty.RussianBank.Adapters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Inty
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
            services.AddControllers();

            services.AddMvc()
                .AddNewtonsoftJson(
                    options =>
                    {
                        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                        options.SerializerSettings.Converters.Add(new StringEnumConverter(new CamelCaseNamingStrategy()));
                        options.SerializerSettings.Error = (_, eventArgs) => throw eventArgs.ErrorContext.Error;
                    }
                );

            services.AddSwaggerGen(
                c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "Russian Bank Gateway",
                        Version = "v1",
                        Description = "Russian bank API gateway to get currency exchange rate."
                    });

                    c.DescribeAllParametersInCamelCase();

                    var xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly);
                    foreach (var xmlFile in xmlFiles)
                    {
                        c.IncludeXmlComments(xmlFile);
                    }
                }
            );

            services.AddScoped<IRussianBankAdapter, RussianBankAdapter>();
            services.AddScoped<ICurrencyExchangeRateInfoService, CurrencyExchangeRateInfoService>();

            services.AddOptions<RussianBankIntegrationOptions>()
                .BindConfiguration("RussianBank")
                .ValidateDataAnnotations()
                .ValidateOnStart();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c => {c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");});
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseStaticFiles();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}