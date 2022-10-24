using Contract;
using Contract.IHelper;
using Contract.Response;
using Entities;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly IPassword _password;
        private readonly IJWTService _jwtService;

        public UserService(IUserRepo userRepo, 
            IPassword passwordHasher, IJWTService jwtService)
        {
            _userRepo = userRepo;
            _password = passwordHasher;
            _jwtService = jwtService;
        }

        public UserResponse GetUserById(Guid userId, Guid tokenUserId)
        {

            if (!userId.Equals(tokenUserId))
                return new UserResponse(false, "User not found", null);

            var user = _userRepo.GetUser(userId);

            if (user == null)
                return new UserResponse(false, "User not found", null);

            return new UserResponse(true, null, user);
        }

        public UserResponse GetUserByUserName(string userName)
        {
            var user = _userRepo.GetUser(userName);

            if (user == null)
                return new UserResponse(false, "User not found", null);

            return new UserResponse(true, null, user);
        }

        public TokenResponse AuthUser(UserDto userData)
        {
            var user = _userRepo.GetUser(userData.UserName);

            if (user == null)
                return new TokenResponse(false, "User not authenticated", null, null);

            if (_password.PasswordMatches(userData.Password, user.Password))
                return new TokenResponse(true, "", _jwtService.GenerateSecurityToken(user), "bearer");

            return new TokenResponse(false, "User not authenticated", null, null);
        }

        public UserResponse CreateUser(UserCreationDto userData)
        {

            var user = _userRepo.GetUser(userData.UserName);

            if (user != null)
                return new UserResponse(false, "Username already exists", null);

            var userToSave = new User()
            {
                UserName = userData.UserName,
                FirstName = userData.FirstName,
                LastName = userData.LastName,
                Password = _password.HashPassword(userData.Password),
            };

            _userRepo.CreateUser(userToSave);
            _userRepo.Save();

            return new UserResponse(true, "", userToSave);
        }

        public UserResponse UpdateUser(Guid userId, Guid tokenUserId, UserUpdationDto userData)
        {
            if (!userId.Equals(tokenUserId))
                return new UserResponse(false, "User not found", null);

            var user = _userRepo.GetUser(userId);

            if (user == null)
                return new UserResponse(false, "User not found", null);

            if (!string.IsNullOrEmpty(userData.UserName))
                user.UserName = userData.UserName;

            if (!string.IsNullOrEmpty(userData.FirstName))
                user.FirstName = userData.FirstName;

            if (!string.IsNullOrEmpty(userData.LastName))
                user.LastName = userData.LastName;

            if (!string.IsNullOrEmpty(userData.Password))
                user.Password = _password.HashPassword(userData.Password);

            _userRepo.UpdateUser(user);
            _userRepo.Save();

            return new UserResponse(true, "Updated user data successfully", user);
        }

        public UserResponse DeleteUser(Guid userId, Guid tokenUserId)
        {
            if (!userId.Equals(tokenUserId))
                return new UserResponse(false, "User not found", null);

            var user = _userRepo.GetUser(userId);

            if (user == null)
                return new UserResponse(false, "User not found", null);

            _userRepo.DeleteUser(user);
            _userRepo.Save();

            return new UserResponse(true, null, user);
        }
    }
}
