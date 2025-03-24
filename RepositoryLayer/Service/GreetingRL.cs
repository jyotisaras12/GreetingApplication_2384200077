using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer.Interface;
using RepositoryLayer.Context;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entity;
using ModelLayer.Model;
using Microsoft.Extensions.Logging;

namespace RepositoryLayer.Service
{
    public class GreetingRL : IGreetingRL
    {
        private readonly GreetingApplicationContext _dbContext;
        private readonly ILogger<GreetingRL> _logger;

        public GreetingRL(GreetingApplicationContext dbContext, ILogger<GreetingRL> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        // UC4: Method to save greeting messages in the repository
        public RequestDTO SaveGreetingRL(RequestDTO requestDTO)
        {
            try
            {
                _logger.LogInformation("Saving new greeting to the database.");

                var greeting = new GreetingEntity { Message = requestDTO.Message };
                _dbContext.Greetings.Add(greeting);
                _dbContext.SaveChanges();

                return new RequestDTO
                {
                    Message = requestDTO.Message
                };
                _logger.LogInformation("Greeting saved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while saving the greeting.");
                throw;
            }
        }

        // UC5: Method to find greeting message by Id
        public GreetingEntity? GreetingByIdRL(int Id)
        {
            _logger.LogInformation("Fetching greeting with ID: {Id}", Id);
            var greeting = _dbContext.Greetings.FirstOrDefault(greet => greet.Id == Id);
            if (greeting == null)
            {
                _logger.LogWarning("Greeting with ID {Id} not found.", Id);
                return null;
            }
            return greeting;
        }

        // UC6: Method to list all the greeting messages in the repository
        public List<GreetingEntity> ListGreetingsRL()
        {
            _logger.LogInformation("Fetching all greetings from the database.");
            return _dbContext.Greetings.ToList();
        }

        // UC7: Method to edit the greeting message in the repository
        public GreetingEntity EditGreetingRL(int Id, GreetingEntity newGreeting)
        {
            var existingGreeting = _dbContext.Greetings.FirstOrDefault(greet => greet.Id == Id);
            if (existingGreeting == null)
            {
                _logger.LogWarning("RL: Greeting with ID {Id} not found.", Id);
                return null;
            }

            existingGreeting.Message = newGreeting.Message;
            _dbContext.SaveChanges();

            _logger.LogInformation("RL: Greeting with ID {Id} updated successfully.", Id);
            return existingGreeting;
        }

        // UC8: Method to delete greeting message by Id from the repository
        public bool DeleteGreetingRL(int Id)
        {
            _logger.LogInformation("Attempting to delete greeting with ID: {Id}", Id);
            var greeting = _dbContext.Greetings.FirstOrDefault(greet => greet.Id == Id);
            if (greeting == null)
            {
                _logger.LogWarning("Greeting with ID {Id} not found for deletion.", Id);
                return false;
            }
                
            _dbContext.Greetings.Remove(greeting);
            _dbContext.SaveChanges();
            _logger.LogInformation("Greeting with ID {Id} deleted successfully.", Id);
            return true;
        }

    }
}
