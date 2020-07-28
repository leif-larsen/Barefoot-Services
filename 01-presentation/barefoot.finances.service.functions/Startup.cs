using System;
using MediatR;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using barefoot.finances.service.core.storehouseholdinfo;
using barefoot.finances.service.core.behaviors;
using barefoot.finances.service.core.interfaces;
using barefoot.finances.service.firebase;

[assembly: FunctionsStartup(typeof(barefoot.finances.service.functions.Startup))]
namespace barefoot.finances.service.functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            // builder.Services
            //     .AddOptions<Configuration>()
            //     .Configure<IConfiguration>((settings, configuration) => { configuration.Bind(settings); }); 
            // builder.Services.AddHttpClient(); 
            builder.Services.AddTransient<IDataPersistance, FirebaseDataPersistence>();              
            builder.Services.AddMediatR(typeof(StoreHouseholdInfoCommand).Assembly);
        
            // builder.Services.AddSingleton<ITopicClient>(serviceProvider => new TopicClient(
            //     connectionString: GetEnvironmentVariable("SBCONNECTIONSTRING"),
            //     entityPath: GetEnvironmentVariable("TOPICNAME")));
            
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            builder.Services.AddSingleton<IValidator<StoreHouseholdInfoCommand>, StoreHouseholdInfoValidator>();
        }

        private string GetEnvironmentVariable(string name) 
        {
            return System.Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);
        }
    }
}