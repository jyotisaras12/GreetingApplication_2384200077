using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Model;
using RepositoryLayer.Entity;

namespace RepositoryLayer.Interface
{
    public interface IGreetingRL
    {
        RequestDTO SaveGreetingRL(RequestDTO requestDTO);   // UC4 method
        GreetingEntity GreetingByIdRL(int Id);  // UC5 method
        List<GreetingEntity> ListGreetingsRL(); //// UC6 method
        GreetingEntity EditGreetingRL(int Id, GreetingEntity newGreeting);  // UC7 method
        bool DeleteGreetingRL(int Id);  // UC8 method
    }
}
