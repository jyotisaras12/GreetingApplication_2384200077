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

        public string GreetingMessage()
        {
            return "Hello World!";
        }

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

        public GreetingEntity GreetingMessageByIdBL(int Id)
        {
            return _greetingRL.GreetingByIdRL(Id);
        }

        public List<GreetingEntity> ListGreetingMessagesBL()
        {
            return _greetingRL.ListGreetingsRL();
        }
    }
}
