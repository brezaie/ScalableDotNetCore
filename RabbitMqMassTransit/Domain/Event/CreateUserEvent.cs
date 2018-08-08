using System.Collections.Generic;

namespace Domain.Event
{
    public class CreateUserEvent : ICreateUserEvent
    {
        private string _userId;
        private string _email;
        private bool _isSuccessful;
        private IEnumerable<string> _errors;

        public string UserId
        {
            get => _userId;
            set => _userId = value;
        }

        public string Email
        {
            get => _email;
            set => _email = value;
        }

        public bool IsSuccessful
        {
            get => _isSuccessful;
            set => _isSuccessful = value;
        }

        public IEnumerable<string> Errors
        {
            get => _errors;
            set => _errors = value;
        }
    }
}