using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Model;
using RepositoryLayer.Entity;

namespace BusinessLayer.Interface
{
    public interface IGreetingBL
    {
        string GreetingMessage();
        string GreetingMessageWithName(string? firstName, string? lastName);
        RequestDTO GreetingMessageBL(RequestDTO requestDTO);
        GreetingEntity GreetingMessageByIdBL(int Id);
        List<GreetingEntity> ListGreetingMessagesBL();
    }
}
