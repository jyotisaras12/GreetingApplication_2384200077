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
        string GreetingMessage();   // UC2 method
        string GreetingMessageWithName(string? firstName, string? lastName);    // UC3 method
        RequestDTO GreetingMessageBL(RequestDTO requestDTO);    //// UC4 method
        GreetingEntity GreetingMessageByIdBL(int Id);   // UC5 method
        List<GreetingEntity> ListGreetingMessagesBL();  // UC6 method
        GreetingEntity EditGreetingBL(int Id, GreetingEntity newGreeting);  // UC7 method
        bool DeleteGreetingBL(int Id);  // UC8 method
    }
}
