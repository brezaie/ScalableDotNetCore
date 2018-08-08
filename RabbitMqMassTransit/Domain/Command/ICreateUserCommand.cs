using System.Collections.Generic;

namespace Domain.Command
{
    public interface ICreateUserCommand
    {
        string Email { get; set; }
    }
}