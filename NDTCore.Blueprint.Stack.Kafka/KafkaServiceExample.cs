using Microsoft.Extensions.Logging;

namespace NDTCore.Blueprint.Stack.Kafka
{
    public class KafkaServiceExample
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<KafkaServiceExample> _logger;

        public KafkaServiceExample(IServiceProvider serviceProvider,
            ILogger<KafkaServiceExample> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public void SendMessage()
        {
            try
            {
                KafkaProducer kafkaProducer = new KafkaProducer(_serviceProvider);

                KafkaMessage message = new KafkaMessage();
                message.Id = Guid.NewGuid().ToString();
                message.DataContent = "This is a message tessting kafka";

                kafkaProducer.PublishAsync(message);

                _logger.LogInformation("Message published successfully.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Message published failed {@ERROR}", e.Message);
            }
        }
    }
}
