using Contract.Response;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    public interface IUserService
    {
        TokenResponse AuthUser(UserDto userData);
        UserResponse CreateUser(UserCreationDto user);
        UserResponse DeleteUser(Guid userId, Guid tokenUserId);
        UserResponse GetUserById(Guid userId, Guid tokenUserId);
        UserResponse GetUserByUserName(string userName);
        UserResponse UpdateUser(Guid userId, Guid tokenUserId, UserUpdationDto userData);
    }
}
