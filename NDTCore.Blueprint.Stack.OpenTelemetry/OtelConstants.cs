using System.Reflection;

namespace NDTCore.Blueprint.Stack.OpenTelemetry
{
    public static class OtelConstants
    {
        public const string OTEL_EXPORTER_OTLP_ENDPOINT = "http://localhost:4318";

        public static readonly string SERVICE_NAME = Assembly.GetEntryAssembly()?.GetName().Name ?? "NDTCore.Blueprint";
        public static readonly string SERVICE_VERSION = Assembly.GetEntryAssembly()?.GetName().Version?.ToString() ?? "1.0.0.0";
    }
}
