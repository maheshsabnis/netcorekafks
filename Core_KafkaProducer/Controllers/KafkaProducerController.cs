using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Core_KafkaProducer.Controllers
{
    public class KafkaProducerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}