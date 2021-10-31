using Confluent.Kafka;
using KafkaDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KafkaDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly ProducerConfig _config;
        private readonly IOptions<Settings> _options;
        private readonly ILogger<MessageController> _logger;

        public MessageController(ProducerConfig config, IOptions<Settings> options,ILogger<MessageController> logger)
        {
            _config = config;
            _logger = logger;
            _options = options;
        }

       
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromQuery]string  message)
        {
           
            MessageRequest messageRequest = new MessageRequest();
            messageRequest.id = Guid.NewGuid().ToString();
            messageRequest.message = message;
            string messagereq = JsonConvert.SerializeObject(messageRequest);
            _logger.LogInformation("MessageController - Recieved a new message" + messagereq);

            var producer = new ProducerWrapper(_config, _options.Value.topicname);
            await producer.writeMessage(messagereq);

            return Created("", "message added to kafka");
        }
    }
}

