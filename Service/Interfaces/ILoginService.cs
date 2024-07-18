using Common.Entities;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ILoginService:IService<UsersDto>
    {
        public UsersDto Login(string password, string Email);
        public UsersDto SetPassword(string email);


    }
}
