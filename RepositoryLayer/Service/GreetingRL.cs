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

namespace RepositoryLayer.Service
{
    public class GreetingRL : IGreetingRL
    {
        private readonly GreetingApplicationContext _dbContext;

        public GreetingRL(GreetingApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        // UC4: Method to save greeting messages in the repository
        public RequestDTO SaveGreetingRL(RequestDTO requestDTO)
        {
            var greeting = new GreetingEntity { Message = requestDTO.Message };
            _dbContext.Greetings.Add(greeting);
            _dbContext.SaveChanges();

            return new RequestDTO
            {
                Message = requestDTO.Message
            };
        }

        // UC5: Method to find greeting message by Id
        public GreetingEntity? GreetingByIdRL(int Id)
        {
            return _dbContext.Greetings.FirstOrDefault(greet => greet.Id == Id);
        }

        // UC6: Method to list all the greeting messages in the repository
        public List<GreetingEntity> ListGreetingsRL()
        {
            return _dbContext.Greetings.ToList();
        }

        // UC7: Method to edit the greeting message in the repository
        public GreetingEntity EditGreetingRL(int Id, GreetingEntity newGreeting)
        {
            var existingGreeting = _dbContext.Greetings.FirstOrDefault(greet => greet.Id == Id);
            if (existingGreeting == null)
                return null;

            existingGreeting.Message = newGreeting.Message;
            _dbContext.SaveChanges();

            return existingGreeting;
        }

        // UC8: Method to delete greeting message by Id from the repository
        public bool DeleteGreetingRL(int Id)
        {
            var greeting = _dbContext.Greetings.FirstOrDefault(greet => greet.Id == Id);
            if (greeting == null)
                return false;

            _dbContext.Greetings.Remove(greeting);
            _dbContext.SaveChanges();

            return true;
        }

    }
}
