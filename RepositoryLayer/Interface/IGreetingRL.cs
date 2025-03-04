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
        RequestDTO SaveGreetingRL(RequestDTO requestDTO);
        GreetingEntity GreetingByIdRL(int Id);
        List<GreetingEntity> ListGreetingsRL();
        GreetingEntity EditGreetingRL(int Id, GreetingEntity newGreeting);
    }
}
