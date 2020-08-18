using GraphDotCms.Api.Middleware;
using GraphDotCms.Application.Extensions;
using GraphDotCms.Persistence.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace GraphDotCms.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }


        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
            services.AddMetrics();
            services.AddControllers();
            services.AddApplication();
            services.AddPersistence(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<CustomExceptionHandlerMiddleware>();
            app.UseSerilogRequestLogging();

            //var counter = Metrics.CreateCounter("values_api_counter", "Counts requests to the values API endpoint",
            //    new CounterConfiguration
            //    {
            //        LabelNames = new[] {"Method", "endpoint"}
            //    });

            //app.Use((context, next) =>
            //{
            //    counter.WithLabels(context.Request.Method, context.Request.Path).Inc();

            //    return next;
            //});

            //app.UseMetricsServer();
            //app.UseHttpMetrics();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
