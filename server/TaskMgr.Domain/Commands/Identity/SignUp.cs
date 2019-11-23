using Framework.CQRS.Commands;
using System;


namespace TaskMgr.Domain.Commands.Identity
{
    public class SignUp : ICommand
    {
        public Guid Id { get;  }
        public string Email { get;  }
        public string FirstName { get;  }
        public string LastName { get;  }
        public string Password { get;  }


        public SignUp(Guid id, string email, string password, string firstName, string lastName)
        {
            Id = id;
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
