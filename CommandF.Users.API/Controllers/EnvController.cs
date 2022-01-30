using CommandF.Users.API.Models;
using CommandF.Users.API.Repositories.Users;
using CommandF.Users.API.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandF.Users.API.Controllers
{
    [Route("Env/")]
    [ApiController]
    public class EnvController : Controller
    {
        private readonly ILogger<EnvController> logger;
        private readonly IUsersRepository repository;

        public EnvController(ILogger<EnvController> logger, IUsersRepository repository)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpPost("{environment}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<object>> AddEnvData([FromBody] EnvironmentDataBody @body, [FromRoute] string environment)
        {
            var requestId = Guid.NewGuid();
            try
            {
                var result = await repository.AddEnvironment(environment, @body.Data);
                this.logger.LogInformation($"[{requestId} - {BrazilDateTime.GetCurrentDate()}] POST /Env/{environment} - | Body: {JsonConvert.SerializeObject(body)} | Response: {JsonConvert.SerializeObject(result)}");
                return Ok(new {
                    Id = result
                });
            }
            catch (Exception e)
            {
                var response = new ErrorResponse(e.Message, e.StackTrace);
                this.logger.LogError($"[{requestId} - {BrazilDateTime.GetCurrentDate()}] POST /Env/{environment} - | Body: {JsonConvert.SerializeObject(body)} | Response: {JsonConvert.SerializeObject(response)}");
                return BadRequest(response);
            }
        }


        [HttpGet("{environment}/{id}")]
        [ProducesResponseType(typeof(EnvironmentOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EnvironmentOutput>> GetEnvData([FromRoute] string id, [FromRoute] string environment)
        {
            var requestId = Guid.NewGuid();
            try
            {
                var result = await repository.GetEnvironmentData(environment, id);
                this.logger.LogInformation($"[{requestId} - {BrazilDateTime.GetCurrentDate()}] GET /Env/{environment}/{id} - | Response: {JsonConvert.SerializeObject(result)}");
                return Ok(new EnvironmentOutput(result));
            }
            catch (Exception e)
            {
                var response = new ErrorResponse(e.Message, e.StackTrace);
                this.logger.LogError($"[{requestId} - {BrazilDateTime.GetCurrentDate()}] GET /Env/{environment}/{id}  - | Response: {JsonConvert.SerializeObject(response)}");
                return BadRequest(response);
            }
        }
    }
}
