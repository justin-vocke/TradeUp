using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeUp.Domain.Core.Abstractions;

namespace TradeUp.Domain.Core.Entities.Users
{
    public sealed class User : Entity
    {
        private readonly List<Role> _roles = new();
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
        public string IdentityId { get; private set; } = string.Empty;

        public IReadOnlyCollection<Role> Roles => _roles.ToList();
        public static User Create(string email, string firstName, string lastName)
        {
            var user = new User(Guid.NewGuid(), email, firstName, lastName);

            user._roles.Add(Role.Registered);

            return user;
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        public void SetIdentityId(string identityId)
        {
            IdentityId = identityId;
        }
    }
}
