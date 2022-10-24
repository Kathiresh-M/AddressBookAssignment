using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Response
{
    public class UserResponse : MessageResponse
    {
        public User user { get; protected set; }
        public UserResponse(bool isSuccess, string message, User user) : base(isSuccess, message)
        {
            this.user = user;
        }
    }
}
