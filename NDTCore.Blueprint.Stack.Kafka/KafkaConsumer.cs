using MassTransit;
using Microsoft.Extensions.Logging;

namespace NDTCore.Blueprint.Stack.Kafka
{
    class KafkaConsumer : IConsumer<KafkaMessage>
    {
        private readonly ILogger<KafkaConsumer> _logger;

        public KafkaConsumer(ILogger<KafkaConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<KafkaMessage> context)
        {
            _logger.LogInformation("Received Kafka message: {DataContent}", context.Message.DataContent);

            // Handle message

            return Task.CompletedTask;
        }
    }
}
