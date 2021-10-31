using Confluent.Kafka;
using KafkaDemo.Service;
using KafkaDemo.ServiceConfiguration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using HostedServices = Microsoft.Extensions.Hosting;

namespace KafkaDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            Configuration = configuration;
            _logger = logger;
        }

        public IConfiguration Configuration { get; }
        private readonly ILogger _logger;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            _logger.LogInformation("ConfigureServices");
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.ConfigureValues(Configuration);
            services.AddSingleton<HostedServices.IHostedService, ProcessMessagesService>();

            var producerConfig = new ProducerConfig();
            var consumerConfig = new ConsumerConfig();

            Configuration.Bind("producer", producerConfig);
            Configuration.Bind("consumer", consumerConfig);

            services.AddSingleton<ProducerConfig>(producerConfig);
            services.AddSingleton<ConsumerConfig>(consumerConfig);

          //  services();

          
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            _logger.LogInformation("Configure");
            _logger.LogInformation(env.EnvironmentName);
            if (env.IsDevelopment())
            {
              
                app.UseDeveloperExceptionPage();
            }
            else
            {
               
                app.UseHsts();
            }

          
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
