using Confluent.Kafka;
using KafkaDemo.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KafkaDemo
{
    public class ConsumerHelper
    {
        private readonly IConsumer<string, string> _consumer;
        private readonly ConsumerConfig _consumerConfig;
        private readonly IOptions<Settings> _options;

        public ConsumerHelper(ConsumerConfig config,IOptions<Settings> options)
        {
            this._options = options;
            this._consumerConfig = config;
            this._consumer = new ConsumerBuilder<string, string>(this._consumerConfig).Build();           
            this._consumer.Subscribe(_options.Value.topicname);

        }
        public string readMessage()
        {
            var consumeResult = this._consumer.Consume();
            return consumeResult.Message.Value;
        }
    }
}
