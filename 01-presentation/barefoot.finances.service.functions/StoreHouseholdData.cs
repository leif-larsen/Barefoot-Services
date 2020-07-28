using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using FluentValidation;
using MediatR;
using barefoot.finances.service.core.storehouseholdinfo;
using barefoot.finances.service.models;

namespace barefoot.finances.services.function
{
    public class StoreHouseholdData
    {
        private readonly IMediator _mediator;

        public StoreHouseholdData(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [FunctionName("StoreHouseholdData")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            bool result = false;

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            HouseholdInfo data = JsonConvert.DeserializeObject<HouseholdInfo>(requestBody);

            try 
            {
                result = await _mediator.Send(new StoreHouseholdInfoCommand 
                {
                    Info = data  
                });
            }
            catch(ValidationException vex)
            {

            }
            catch(Exception ex)
            {

            }

            log.LogInformation("C# HTTP trigger function processed a request.");

            return new OkObjectResult(result);
        }
    }
}
