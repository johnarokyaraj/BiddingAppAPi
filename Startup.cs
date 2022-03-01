using BidingAPPAPI.Utility;
using BidingAPPAPI.Repository;
using BidingAPPAPI.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace BidingAPPAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
            });
            services.AddControllers().AddNewtonsoftJson();
            //Add Swagger relates setting  
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Bidding API",
                    Version = "v1",
                    Description = "API to unerstand request and response schema.",
                });
                swagger.SchemaFilter<SwaggerSkipPropertyFilter>();
            });
            services.AddTransient<SellerService>();
            services.AddTransient<SellerRepo>();
            services.AddTransient<BuyerService>();
            services.AddTransient<BuyerRepo>();
            services.AddSingleton(Configuration);
            var producerConfig = new ProducerConfig(new ClientConfig
            {
                BootstrapServers = Configuration["Kafka:ClientConfigs:BootstrapServers"]
            });

            services.AddSingleton(producerConfig);
            services.AddSingleton(typeof(IKafkaProducer<,>), typeof(KafkaProducer<,>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x
            .SetIsOriginAllowed(origin => true)
           .AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bidding API v1");
            });

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
           
        }
    }
}
