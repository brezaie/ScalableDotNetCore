using System.Collections.Generic;

namespace Domain.Event
{
    public interface ICreateUserEvent
    {
        string UserId { get; set; }

        string Email { get; set; }

        bool IsSuccessful { get; set; }

        IEnumerable<string> Errors { get; set; }
    }
}