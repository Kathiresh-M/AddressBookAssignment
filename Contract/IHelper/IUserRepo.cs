using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.IHelper
{
    public interface IUserRepo
    {
        void CreateUser(User user);
        void DeleteUser(User user);
        User GetUser(Guid userId);
        User GetUser(string userName);
        void UpdateUser(User user);
        void Save();
    }
}
