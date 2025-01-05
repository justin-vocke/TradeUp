using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeUp.Domain.Core.Abstractions;

namespace TradeUp.Domain.Core.Entities
{
    public sealed class User : Entity
    {
        private User(Guid id, string email, string firstName, string lastName) : base(id)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
        }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; private set; } = true;    

        public static User Create(string email, string firstName, string lastName)
        {
            var user = new User(Guid.NewGuid(), email, firstName, lastName);
            return user;
        }

        public void Deactivate()
        {
            IsActive = false;
        }
    }
}
