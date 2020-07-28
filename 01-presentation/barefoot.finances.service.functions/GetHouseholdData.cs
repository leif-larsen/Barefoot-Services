using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MediatR;
using barefoot.finances.service.core.gethouseholdinfo;
using barefoot.finances.service.models;
using FluentValidation;

namespace barefoot.finances.services.function
{
    public class GetHouseholdData
    {
        private readonly IMediator _mediator;

        public GetHouseholdData(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [FunctionName("GetHouseholdData")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            HouseholdInfo result = null;

            try 
            {
                result = await _mediator.Send(new GetHouseholdInfoCommand 
                { 
                    UserId = req.Query["userid"]
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
