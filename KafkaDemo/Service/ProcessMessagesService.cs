using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using KafkaDemo.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace KafkaDemo.Service
{
    public class ProcessMessagesService : BackgroundService
    {
        private readonly ConsumerConfig consumerConfig;
        private readonly ProducerConfig producerConfig;
        private readonly ILogger<ProcessMessagesService> _logger;
        private readonly IOptions<Settings> _options;

        public ProcessMessagesService(ILogger<ProcessMessagesService> logger,
            ConsumerConfig consumerConfig, ProducerConfig producerConfig,
            IOptions<Settings> options)
        {
            this.producerConfig = producerConfig;
            this.consumerConfig = consumerConfig;
            this._logger = logger;
            this._options = options;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("ProcessMessagesService Service Started");

            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    var consumerHelper = new ConsumerHelper(consumerConfig, _options);
                    string messageRequest = consumerHelper.readMessage();
                    var messagereq = JsonConvert.DeserializeObject<MessageRequest>(messageRequest);

                    //TODO:: Process Order
                    _logger.LogDebug($"message with id {messagereq.id} and content {messagereq.message}");
                    _logger.LogInformation($"message with id {messagereq.id} and content {messagereq.message}");


                    //Write to ReadyToShip Queue

                    var producerWrapper = new ProducerWrapper(producerConfig, _options.Value.topicname);
                    await producerWrapper.writeMessage(JsonConvert.SerializeObject(messagereq));
                }
            }
            catch(Exception ex)
            {

                _logger.LogError(ex.Message);
            }
           
          

          
        }
    }
}
