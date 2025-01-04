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
        private User(Guid id, string email) : base(id)
        {
            Email = email;
        }
        public string Email { get; set; }
        public bool IsActive { get; private set; } = true;    

        public static User Create(string email)
        {
            var user = new User(Guid.NewGuid(), email);
            return user;
        }

        public void Deactivate()
        {
            IsActive = false;
        }
    }
}
