using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace NDTCore.Blueprint.Stack.Kafka
{
    public static class ServiceCollectionExtensions
    {
        private const string TOPIC_NAME = "kafka-topic-example";
        private const string GROUP_ID = "consumer-group";
        private const string BOOTSTRAP_SERVER = "localhost:9092";
        private const string SCHEMA_REGISTRY_URL = "localhost:8081";

        public static IServiceCollection AddStackKafkaWithoutSchemaRegistry(this IServiceCollection services)
        {
            services.AddMassTransit(busConfig =>
            {
                busConfig.UsingInMemory();

                busConfig.AddRider(rider =>
                {
                    rider.AddProducer<string, KafkaMessage>(TOPIC_NAME, configure => configure.Message.Id);

                    rider.AddConsumer<KafkaConsumer>();

                    rider.UsingKafka((context, k) =>
                    {
                        k.Host(BOOTSTRAP_SERVER);

                        k.TopicEndpoint<KafkaMessage>(TOPIC_NAME, GROUP_ID, e =>
                        {
                            e.ConfigureConsumer<KafkaConsumer>(context);
                            e.CreateIfMissing();
                        });
                    });
                });
            });

            services.AddScoped<KafkaServiceExample>();

            return services;
        }

        public static IServiceCollection AddStackKafkaUseSchemaRegistry(this IServiceCollection services)
        {
            // Configuration for Avro Serializer
            var serializerConfig = new AvroSerializerConfig
            {
                AutoRegisterSchemas = true, // Enable auto-registration
                UseLatestVersion = false   // Disable latest version usage
            };

            // Schema Registry Client configuration
            var schemaRegistryConfig = new SchemaRegistryConfig
            {
                Url = SCHEMA_REGISTRY_URL,  // Make sure this is the correct URL of your Schema Registry
            };

            // Create Schema Registry client instance
            var schemaRegistryClient = new CachedSchemaRegistryClient(schemaRegistryConfig);
            services.AddSingleton<ISchemaRegistryClient>(schemaRegistryClient);  // Register it as a singleton

            // MassTransit configuration
            services.AddMassTransit(busConfig =>
            {
                // Configuring MassTransit to use InMemory transport (for development or testing)
                busConfig.UsingInMemory();

                // Adding a Kafka rider to produce and consume messages
                busConfig.AddRider(rider =>
                {
                    rider.AddProducer<string, KafkaMessage>(TOPIC_NAME, (context, configure) =>
                    {
                        // Setting up serializers for both key (string) and value (KafkaMessage)
                        configure.SetKeySerializer(new AvroSerializer<string>(schemaRegistryClient, serializerConfig).AsSyncOverAsync());
                        configure.SetValueSerializer(new AvroSerializer<KafkaMessage>(context.GetRequiredService<ISchemaRegistryClient>(), serializerConfig));
                    });

                    // Register Kafka consumer
                    rider.AddConsumer<KafkaConsumer>();

                    // Configuring Kafka host and topic settings
                    rider.UsingKafka((context, k) =>
                    {
                        // Set the Kafka host (bootstrap server)
                        k.Host(BOOTSTRAP_SERVER);

                        // Set up the topic and its configuration for consumption
                        k.TopicEndpoint<string, KafkaMessage>(TOPIC_NAME, GROUP_ID, configure =>
                        {
                            // Optionally create the topic if it does not exist (ensure Kafka is configured to allow this)
                            configure.CreateIfMissing();
                            configure.AutoOffsetReset = AutoOffsetReset.Earliest;

                            // Setting up deserializer for both key (string) and value (KafkaMessage)
                            configure.SetKeyDeserializer(new AvroDeserializer<string>(schemaRegistryClient, null).AsSyncOverAsync());
                            configure.SetValueDeserializer(new AvroDeserializer<KafkaMessage>(context.GetRequiredService<ISchemaRegistryClient>()).AsSyncOverAsync());

                            // Configure consumer for the topic
                            configure.ConfigureConsumer<KafkaConsumer>(context);
                        });
                    });
                });
            });

            // Register the KafkaServiceExample for DI (Dependency Injection)
            services.AddScoped<KafkaServiceExample>();

            return services;
        }
    }
}
