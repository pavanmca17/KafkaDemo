using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KafkaDemo
{
    public class ProducerWrapper
    {
        private string _topicName;
        private IProducer<string, string> _producer;
        private ProducerConfig _config;
       

        public ProducerWrapper(ProducerConfig config, string topicName)
        {
            this._topicName = topicName;
            this._config = config;           
            this._producer = new ProducerBuilder<string, string>(this._config).Build(); ;
            
        }
        public async Task writeMessage(string message)
        {
            var task = await _producer.ProduceAsync(_topicName, new Message<string, string>()
            {  Value = message
            });

            _producer.Flush(TimeSpan.FromSeconds(10));
            return;
        }
    }
}
