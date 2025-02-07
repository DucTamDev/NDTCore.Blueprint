using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace NDTCore.Blueprint.Stack.Kafka
{
    public class KafkaProducer
    {
        private readonly IServiceProvider _serviceProvider;

        public KafkaProducer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task PublishAsync<T>(T message) where T : class
        {
            var producer = _serviceProvider.GetRequiredService<ITopicProducer<string, T>>();

            return producer.Produce(Guid.NewGuid().ToString(), message);
        }
    }
}
