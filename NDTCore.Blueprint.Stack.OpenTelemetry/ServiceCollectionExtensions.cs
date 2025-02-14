using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Reflection;
namespace NDTCore.Blueprint.Stack.OpenTelemetry
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddStackOpenTelemetry(this IServiceCollection services, IConfiguration configuration)
        {
            var resource = ResourceBuilder
                            .CreateDefault()
                            .AddService(OtelConstants.SERVICE_NAME, OtelConstants.SERVICE_NAME, OtelConstants.SERVICE_VERSION);

            services.AddLogging(logging => logging
                    .AddOpenTelemetry(options => options
                        .SetResourceBuilder(resource)
                        .AddConsoleExporter()
                        .AddOtlpExporter()
                    )
            );

            services.AddOpenTelemetry()
                    .WithTracing(tracerBuilder => tracerBuilder
                        .SetResourceBuilder(resource)
                        .AddAspNetCoreInstrumentation()
                        .AddHttpClientInstrumentation()
                        .AddSqlClientInstrumentation()
                        .AddConsoleExporter()
                        .AddOtlpExporter(options =>
                        {
                            options.Endpoint = new Uri(OtelConstants.OTEL_EXPORTER_OTLP_ENDPOINT);
                            options.Protocol = OtlpExportProtocol.HttpProtobuf;
                        })
                    );

            services.AddOpenTelemetry()
                    .WithMetrics(metricBuilder => metricBuilder
                        .SetResourceBuilder(resource)
                        .AddAspNetCoreInstrumentation()
                        .AddHttpClientInstrumentation()
                        .AddRuntimeInstrumentation()
                        .AddConsoleExporter()
                        .AddOtlpExporter(options =>
                        {
                            options.Endpoint = new Uri(OtelConstants.OTEL_EXPORTER_OTLP_ENDPOINT);
                            options.Protocol = OtlpExportProtocol.HttpProtobuf;
                        })
                    );

            return services;
        }
    }
}
