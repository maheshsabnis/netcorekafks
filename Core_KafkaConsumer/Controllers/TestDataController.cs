using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Core_KafkaConsumer.Models;
namespace Core_KafkaConsumer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestDataController : ControllerBase
    {
        CitusTrainingContext context;

        public TestDataController()
        {
            context = new CitusTrainingContext();
        }
        [HttpGet]
        public IActionResult Get()
        {
            var res = context.Test1.ToList();
            return Ok(res);
        }
    }
}