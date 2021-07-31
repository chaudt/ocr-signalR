using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prometheus;
using RCMS.Commons.Consul;
using RCMS.Commons.Dispatchers;
using RCMS.Commons.Jaeger;
using RCMS.Commons.Kafka.v2;
using RCMS.Commons.Mvc;
using RCMS.Commons.SignalR;
using OCR_SignalR.Hubs;
using OCR_SignalR.Registration;
using OCR_SignalR.Configurations;

namespace OCR_SignalR
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
            services.AddAutoMapper(typeof(Startup))
                    .AddConsul()
                    .AddErrorHandler()
                    .AddCorsStar()
                    .AddCors()// add signalR
                    .AddJaeger()
                    .AddOpenTracing();
            services.AddSignalR();// thay thế

            services.AddHealthChecks();

        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.AddDispatchers()
                   .AddLogging(Configuration)
                   .AddSignalR()
                   .AddInternalCommunication()
                   .AddKafkaConfigures(ServiceConfigure.Instance.Alls)
                   .AddKafkaConsumers(ServiceConsumer.Instance.Alls);
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseErrorHandler();
            app.UseCors(x => x
               .AllowAnyMethod()
               .AllowAnyHeader()
               .SetIsOriginAllowed(origin => true) // allow any origin
               .AllowCredentials()); // allow credential

            app.UseKafkaReceiveConsumer(ServiceConsumer.Instance.Listeners);
            
            app.UseConsul(Configuration);
            app.UseRouting();
            app.UseHttpMetrics();
            // app.UseAuthentication();
            app.UseAuthorization();
            app.UseHealthChecks("/health");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<RCMSHub>("/ocr");
                endpoints.MapMetrics();
            });
        }
    }
}
