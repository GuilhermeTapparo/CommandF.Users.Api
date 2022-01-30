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
    [Route("Newsletter/")]
    [ApiController]
    public class NewsletterController : Controller
    {
        private readonly ILogger<NewsletterController> logger;
        private readonly IUsersRepository repository;

        public NewsletterController(ILogger<NewsletterController> logger, IUsersRepository repository)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpPost("")]
        [ProducesResponseType(typeof(NewsletterUser),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<NewsletterUser>> AddNewsletterUser([FromBody] NewsletterUserBody @body)
        {
            var requestId = Guid.NewGuid();
            try
            {
                var result = await repository.AddToNewsletter(@body.Email, @body.Name);
                this.logger.LogInformation($"[{requestId} - {BrazilDateTime.GetCurrentDate()}] POST /Newsletter - | Body: {JsonConvert.SerializeObject(body)} | Response: {JsonConvert.SerializeObject(result)}");
                return Ok(result);
            }
            catch (Exception e)
            {
                var response = new ErrorResponse(e.Message, e.StackTrace);
                this.logger.LogError($"[{requestId} - {BrazilDateTime.GetCurrentDate()}] POST /Newsletter - | Body: {JsonConvert.SerializeObject(body)} | Response: {JsonConvert.SerializeObject(response)}");
                return BadRequest(response);
            }
        }
    }
}
