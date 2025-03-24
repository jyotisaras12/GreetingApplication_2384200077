using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Model;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {
        string RegisterBL(UserDTO user); 
        UserDTO? LoginBL(string username, string password);
    }
}