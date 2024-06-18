using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IServicsExtention<T>:IService<T>
    {
        public Task<T> GetUserbyUserMail(string Email, string Password);
    }
}
