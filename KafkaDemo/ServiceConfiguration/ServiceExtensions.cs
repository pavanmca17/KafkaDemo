using KafkaDemo.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KafkaDemo.ServiceConfiguration
{
    public static class ServiceExtensions
    {
        public static void ConfigureValues(this IServiceCollection services, IConfiguration Configuration)
        {
            services.Configure<Settings>(options =>
            {
                options.topicname = Configuration.GetSection("topic:name").Value;
                
            });
        }
    }
}
