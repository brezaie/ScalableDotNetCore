using System.Collections.Generic;

namespace Domain.Command
{
    public class CreateUserCommand : ICreateUserCommand
    {
        private string _email;

        public string Email
        {
            get => _email;
            set => _email = value;
        }
    }
}