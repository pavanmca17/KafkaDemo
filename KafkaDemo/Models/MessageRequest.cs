using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KafkaDemo.Models
{
    public class MessageRequest
    {
        public string id { get; set; }
        public string message { get; set; }
    }
}
