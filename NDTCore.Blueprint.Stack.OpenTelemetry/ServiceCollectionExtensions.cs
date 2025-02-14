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
        const string OTEL_EXPORTER_OTLP_ENDPOINT = "http://localhost:4318";
        const string OTEL_EXPORTER_OTLP_TRACES = "true";
        const string OTEL_EXPORTER_OTLP_METRICS = "true";
        const string OTEL_EXPORTER_OTLP_LOGS = "true";

        static readonly string SERVICE_NAME = Assembly.GetEntryAssembly()?.GetName().Name ?? "NDTCore.Blueprint";
        static readonly string SERVICE_VERSION = Assembly.GetEntryAssembly()?.GetName().Version?.ToString() ?? "1.0.0.0";

        public static IServiceCollection AddStackOpentelemetry(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddLogging(logging => logging
                    .AddOpenTelemetry(options => options
                        .SetResourceBuilder(
                            ResourceBuilder
                            .CreateDefault()
                            .AddService(serviceName: SERVICE_NAME, serviceVersion: SERVICE_VERSION))
                        .AddConsoleExporter()
                        .AddOtlpExporter()
                    )
            );

            services.AddOpenTelemetry()
                    .ConfigureResource(configure => configure
                        .AddService(serviceName: SERVICE_NAME))
                    .WithTracing(tracerBuilder => tracerBuilder
                        .AddSource("InsightSource")
                        .AddAspNetCoreInstrumentation()
                        .AddHttpClientInstrumentation()
                        .AddSqlClientInstrumentation()
                        .AddConsoleExporter()
                        .AddOtlpExporter(options =>
                        {
                            options.Endpoint = new Uri(OTEL_EXPORTER_OTLP_ENDPOINT);
                            options.Protocol = OtlpExportProtocol.HttpProtobuf;
                        })
                    );

            services.AddOpenTelemetry()
                    .ConfigureResource(configure => configure
                        .AddService(serviceName: SERVICE_NAME))
                    .WithMetrics(metricBuilder => metricBuilder
                        .AddAspNetCoreInstrumentation()
                        .AddHttpClientInstrumentation()
                        .AddRuntimeInstrumentation()
                        .AddConsoleExporter()
                        .AddOtlpExporter(options =>
                        {
                            options.Endpoint = new Uri(OTEL_EXPORTER_OTLP_ENDPOINT);
                            options.Protocol = OtlpExportProtocol.HttpProtobuf;
                        })
                    );

            return services;
        }
    }
}
