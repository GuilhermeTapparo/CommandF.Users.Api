using CommandF.Users.API.Exceptions;
using CommandF.Users.API.Models;
using CommandF.Users.API.Repositories.Users;
using CommandF.Users.API.Services.Users;
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
    [Route("User/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> logger;
        private readonly IUsersRepository repository;
        private readonly IUserService userService;

        public UserController(ILogger<UserController> logger, IUsersRepository repository, IUserService userService)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet("{username}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<User>> GetByUsername([FromRoute] string username)
        {
            var requestId = Guid.NewGuid();
            try
            {
                var result = await repository.GetByUsername(username);
                this.logger.LogInformation($"[{requestId} - {BrazilDateTime.GetCurrentDate()}] GET /User/{username} - | Response: {JsonConvert.SerializeObject(result)}");
                return Ok(result);
            }
            catch (NotFoundException e)
            {
                var response = new ErrorResponse(e.Message, e.StackTrace);
                this.logger.LogWarning($"[{requestId} - {BrazilDateTime.GetCurrentDate()}] GET /User/{username} - | Response: NOT_FOUND {JsonConvert.SerializeObject(response)}");
                return NotFound(response);
            }
            catch (Exception e)
            {
                var response = new ErrorResponse(e.Message, e.StackTrace);
                this.logger.LogError($"[{requestId} - {BrazilDateTime.GetCurrentDate()}] GET /User/{username} - | Response: {JsonConvert.SerializeObject(response)}");
                return BadRequest(response);
            }
        }

        [HttpGet("Search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<User>> Search([FromQuery] string email)
        {
            var requestId = Guid.NewGuid();

            try
            {
                var result = await repository.GetByEmail(email);
                this.logger.LogInformation($"[{requestId} - {BrazilDateTime.GetCurrentDate()}] GET /User/Search/?email={email} - | Response: {JsonConvert.SerializeObject(result)}");
                return Ok(result);
            }
            catch (NotFoundException e)
            {
                var response = new ErrorResponse(e.Message, e.StackTrace);
                this.logger.LogWarning($"[{requestId} - {BrazilDateTime.GetCurrentDate()}] GET /User/Search/?email={email} - | Response: NOT_FOUND {JsonConvert.SerializeObject(response)}");
                return NotFound(response);
            }
            catch (Exception e)
            {
                var response = new ErrorResponse(e.Message, e.StackTrace);
                this.logger.LogError($"[{requestId} - {BrazilDateTime.GetCurrentDate()}] GET /User/Search/?email={email} - | Response: {JsonConvert.SerializeObject(response)}");
                return BadRequest(response);
            }
        }

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<User>> CreateUser([FromBody] User user)
        {
            var requestId = Guid.NewGuid();
            try
            {
                var result = await userService.CreateUser(requestId, user);
                this.logger.LogInformation($"[{requestId} - {BrazilDateTime.GetCurrentDate()}] POST /User - | Body: {JsonConvert.SerializeObject(user)} | Response: {JsonConvert.SerializeObject(result)}");
                return Ok(result);
            }
            catch (NotFoundException e)
            {
                var response = new ErrorResponse(e.Message, e.StackTrace);
                this.logger.LogWarning($"[{requestId} - {BrazilDateTime.GetCurrentDate()}] POST /User - | Body: {JsonConvert.SerializeObject(user)} | Response: NOT_FOUND {JsonConvert.SerializeObject(response)}");
                return NotFound(response);
            }
            catch (Exception e)
            {
                var response = new ErrorResponse(e.Message, e.StackTrace);
                this.logger.LogError($"[{requestId} - {BrazilDateTime.GetCurrentDate()}] POST /User - | Body: {JsonConvert.SerializeObject(user)} | Response: {JsonConvert.SerializeObject(response)}");
                return BadRequest(response);
            }
        }

        [HttpPut("{username}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<User>> UpdateUser([FromRoute] string username, [FromBody] User user)
        {
            var requestId = Guid.NewGuid();
            try
            {
                var result = await repository.UpdateUser(username, user);
                this.logger.LogInformation($"[{requestId} - {BrazilDateTime.GetCurrentDate()}] PUT /User/{username} - | Body: {JsonConvert.SerializeObject(user)} | Response: {JsonConvert.SerializeObject(result)}");
                return Ok(result);
            }
            catch (NotFoundException e)
            {
                var response = new ErrorResponse(e.Message, e.StackTrace);
                this.logger.LogWarning($"[{requestId} - {BrazilDateTime.GetCurrentDate()}] PUT /User/{username} - | Body: {JsonConvert.SerializeObject(user)} | Response: NOT_FOUND {JsonConvert.SerializeObject(response)}");
                return NotFound(response);
            }
            catch (Exception e)
            {
                var response = new ErrorResponse(e.Message, e.StackTrace);
                this.logger.LogError($"[{requestId} - {BrazilDateTime.GetCurrentDate()}] PUT /User/{username} - | Body: {JsonConvert.SerializeObject(user)} | Response: {JsonConvert.SerializeObject(response)}");
                return BadRequest(response);
            }
        }

        [HttpPatch("{username}/Status")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<User>> UpdateUser([FromRoute] string username, [FromBody] UpdateStatusBody updateStatus)
        {
            var requestId = Guid.NewGuid();
            try
            {
                await repository.UpdateStatus(username, updateStatus.NewStatus);
                this.logger.LogInformation($"[{requestId} - {BrazilDateTime.GetCurrentDate()}] PATCH /User/{username}/Status - | Body: {JsonConvert.SerializeObject(updateStatus)} | Response: OK-NO_CONTENT");
                return NoContent();
            }
            catch (NotFoundException e)
            {
                var response = new ErrorResponse(e.Message, e.StackTrace);
                this.logger.LogWarning($"[{requestId} - {BrazilDateTime.GetCurrentDate()}] PATCH /User/{username}/Status - | Body: {JsonConvert.SerializeObject(updateStatus)} | Response: NOT_FOUND {JsonConvert.SerializeObject(response)}");
                return NotFound(response);
            }
            catch (Exception e)
            {
                var response = new ErrorResponse(e.Message, e.StackTrace);
                this.logger.LogError($"[{requestId} - {BrazilDateTime.GetCurrentDate()}] PATCH /User/{username}/Status - | Body: {JsonConvert.SerializeObject(updateStatus)}  | Response: {JsonConvert.SerializeObject(response)}");
                return BadRequest(response);
            }
        }
    }
}
