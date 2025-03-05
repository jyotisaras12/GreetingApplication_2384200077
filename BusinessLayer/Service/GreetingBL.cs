using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using RepositoryLayer.Interface;
using ModelLayer.Model;
using RepositoryLayer.Entity;

namespace BusinessLayer.Service
{
    public class GreetingBL : IGreetingBL
    {
        private readonly IGreetingRL _greetingRL;

        public GreetingBL(IGreetingRL greetingRL)
        {
            _greetingRL = greetingRL;
        }

        // UC2: Method to get simple greeting message "Hello World!"
        public string GreetingMessage()
        {
            return "Hello World!";
        }

        // UC3: Method to get greeting message with name
        public string GreetingMessageWithName(string? firstName, string? lastName)
        {
            if (!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
            {
                return $"Hello {firstName} {lastName}!";
            }
            else if (!string.IsNullOrWhiteSpace(firstName))
            {
                return $"Hello {firstName}!";
            }
            else if (!string.IsNullOrWhiteSpace(lastName))
            {
                return $"Hello {lastName}!";
            }
            else
            {
                return "Hello World!";
            }
        }

        // UC4: Method to save greeting messages in the repository
        public RequestDTO GreetingMessageBL(RequestDTO requestDTO)
        {
            var result = _greetingRL.SaveGreetingRL(requestDTO);
            if (result == null)
                return null;

            return new RequestDTO
            {
                Message = result.Message
            };
        }

        // UC5: Method to find greeting message by Id
        public GreetingEntity GreetingMessageByIdBL(int Id)
        {
            return _greetingRL.GreetingByIdRL(Id);
        }

        // UC6: Method to list all the greeting messages in the repository
        public List<GreetingEntity> ListGreetingMessagesBL()
        {
            return _greetingRL.ListGreetingsRL();
        }

        // UC7: Method to edit the greeting message in the repository
        public GreetingEntity EditGreetingBL(int Id, GreetingEntity newGreeting)
        {
            return _greetingRL.EditGreetingRL(Id, newGreeting);
        }

        // UC8: Method to delete greeting message by Id from the repository
        public bool DeleteGreetingBL(int Id)
        {
            return _greetingRL.DeleteGreetingRL(Id);
        }
    }
}
