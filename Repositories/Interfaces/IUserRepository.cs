using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prueba.Models;

namespace Prueba.Repositories.Interfaces
{
    public interface IUserRepository
    {  
        List<User> GetAllUsers();
        void CreateUser(User user);
        void UpdateUser(int userId, User user);
        void DeleteUser(int userId);
        
    }
}