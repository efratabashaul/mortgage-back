using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interface
{
    public interface ILogin : IRepository<Users>
    {
        public Users getUserByLogin(string email, string password);
    }
}
