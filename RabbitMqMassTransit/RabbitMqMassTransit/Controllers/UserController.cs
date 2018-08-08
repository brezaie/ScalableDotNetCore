using System;
using System.Threading.Tasks;
using Domain;
using Domain.Command;
using Domain.Event;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using RabbitMqMassTransit;

namespace Requester.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IBus Bus;

        public UserController(IBus _bus)
        {
            try
            {
                Bus = _bus;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost("createUser")]
        public async Task<IActionResult> CreateUser([FromBody] PostRequest request)
        {
            try
            {
                var _userCreationService = BusConfigurator.CreateRequestClient<ICreateUserCommand, ICreateUserEvent>(Bus);

                var response = await _userCreationService.Request(new CreateUserCommand
                {
                    Email = request.Email
                });

                return Ok(response.IsSuccessful);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}