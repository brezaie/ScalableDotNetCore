using System;
using System.Threading.Tasks;
using Domain.Command;
using Domain.Event;
using MassTransit;

namespace Receiver
{
    public class CreateUserCommandConsumer : IConsumer<ICreateUserCommand>
    {
        public async Task Consume(ConsumeContext<ICreateUserCommand> context)
        {
            try
            {
                await context.RespondAsync<ICreateUserEvent>(new
                {
                    Email = context.Message.Email,
                    UserId = Guid.NewGuid().ToString(),
                    IsSuccessful = true
                });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}