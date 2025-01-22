using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeUp.Domain.Core.Entities.Users;

namespace TradeUp.Domain.UnitTests.Users
{
    public class UserTests
    {
        [Fact]
        public void Create_Should_SetPropertyValues()
        {
            //Act
            var user = User.Create(UserData.Email, UserData.FirstName, UserData.LastName);

            //Assert
            user.FirstName.Should().Be(UserData.FirstName);
            user.LastName.Should().Be(UserData.LastName);
            user.Email.Should().Be(UserData.Email);
        }

        [Fact]
        public void Create_Should_AddRegisteredRoleToUser()
        {
            //Act
            var user = User.Create(UserData.Email, UserData.FirstName, UserData.LastName);

            //Assert
            user.Roles.Should().Contain(Role.Registered);
        }
    }
}
