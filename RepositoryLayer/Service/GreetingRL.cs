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
    }
}
