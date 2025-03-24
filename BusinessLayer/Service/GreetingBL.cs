using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using RepositoryLayer.Interface;
using ModelLayer.Model;
using RepositoryLayer.Entity;
using Microsoft.Extensions.Logging;
using RepositoryLayer.Service;

namespace BusinessLayer.Service
{
    public class GreetingBL : IGreetingBL
    {
        private readonly IGreetingRL _greetingRL;
        private readonly ILogger<GreetingBL> _logger;

        public GreetingBL(IGreetingRL greetingRL, ILogger<GreetingBL> logger)
        {
            _greetingRL = greetingRL;
            _logger = logger;
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
            if (requestDTO == null || string.IsNullOrEmpty(requestDTO.Message))
            {
                _logger.LogError("Invalid greeting data provided.");
                throw new ArgumentException("Greeting message cannot be empty.");
            }

            _logger.LogInformation($"Saving Greeting: {requestDTO.Message}");
            var result = _greetingRL.SaveGreetingRL(requestDTO);
            
            return new RequestDTO
            {
                Message = result.Message
            };
        }

        // UC5: Method to find greeting message by Id
        public GreetingEntity GreetingMessageByIdBL(int Id)
        {
            _logger.LogInformation("BL: Fetching greeting with ID: {Id}", Id);
            return _greetingRL.GreetingByIdRL(Id);
        }

        // UC6: Method to list all the greeting messages in the repository
        public List<GreetingEntity> ListGreetingMessagesBL()
        {
            _logger.LogInformation("BL: Fetching all greetings.");
            return _greetingRL.ListGreetingsRL();
        }

        // UC7: Method to edit the greeting message in the repository
        public GreetingEntity EditGreetingBL(int Id, GreetingEntity newGreeting)
        {
            if (newGreeting == null || string.IsNullOrEmpty(newGreeting.Message))
            {
                _logger.LogError("BL: Invalid greeting data for update.");
                return null; // Return false instead of throwing an exception
            }

            _logger.LogInformation("BL: Updating greeting with ID: {Id}", Id);
            return _greetingRL.EditGreetingRL(Id, newGreeting);
        }

        // UC8: Method to delete greeting message by Id from the repository
        public bool DeleteGreetingBL(int Id)
        {
            _logger.LogInformation("BL: Deleting greeting with ID: {Id}", Id);
            return _greetingRL.DeleteGreetingRL(Id);
        }
    }
}
