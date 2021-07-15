using System;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using Core_KafkaConsumer.Models;
namespace Core_KafkaConsumer.BakgroundService
{
    public class KafkaConsumerService: IHostedService
    {
        // the tipic name must match thatb is created in then  Kafka
        private readonly string topic = "messag_exchange_topic";
        CitusTrainingContext context;
        public KafkaConsumerService()
        {
            context = new CitusTrainingContext();
        }

        /// <summary>
        /// define the consumer
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            var consumerConfig = new ConsumerConfig()
            {
                GroupId = "mex_consumer_group",
              // <Use when app runnung fronm VS>  BootstrapServers = "localhost:9092",
               // BootstrapServers = "localhost:9092",
                 BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            // This handler must run in a separate thread since it will eternally watch for
            //incoming messages within a while loop.
            // Therefore, it’s making use of Async Tasks in this class.


            using (var builder = new ConsumerBuilder<Ignore,string>(consumerConfig).Build())
            {
                // subscribe to the topic
                builder.Subscribe(topic);
                var cancelToken = new CancellationTokenSource();
                try
                {
                    while (true)
                    {
                        // start consuming messages
                        var consumer = builder.Consume(cancelToken.Token);
                        Console.WriteLine($"Message: {consumer.Message.Value} received from {consumer.TopicPartitionOffset}");
                        Test1 t = new Test1()
                        {
                             Message = consumer.Message.Value
                        };
                        context.Test1.Add(t);
                        context.SaveChanges();
                    }
                }
                catch (Exception)
                {
                    builder.Close();
                }
            }
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
