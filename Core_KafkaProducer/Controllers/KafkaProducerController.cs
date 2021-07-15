using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;

namespace Core_KafkaProducer.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class KafkaProducerController : ControllerBase
    {
        private readonly ProducerConfig config;
        private readonly string topic;

        public KafkaProducerController()
        {
            
            config = new ProducerConfig()
            {
              // <Use for app running from VS>   BootstrapServers="localhost:9092",
                BootstrapServers= "localhost:9092"

            };
            topic = "messag_exchange_topic";
        }
         
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hi");
        } 
        [HttpPost]   
        // public IActionResult Post([FromQuery] string message)
        public IActionResult Post(string message)
        {
            return Created(string.Empty, SendToKafkaTopic(topic, message));
        }

        private Object SendToKafkaTopic(string topic, string message)
        {
            using (var producer  =new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    return producer.ProduceAsync(topic, new Message<Null, string>()
                    { Value = message }).GetAwaiter().GetResult();
                }
                catch (Exception ex)
                {
                    return BadRequest($"Something went wrong while sending data {ex.Message}");
                }
            }
          
        }
    }
}