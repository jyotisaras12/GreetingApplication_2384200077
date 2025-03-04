using BusinessLayer.Interface;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Model;
using RepositoryLayer.Entity;

namespace HelloGreetingApplication.Controllers
{
    /// <summary>
    /// Class providing API for HelloGreeting
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class HelloGreetingController : ControllerBase
    {
        private readonly ILogger<HelloGreetingController> _logger;
        private readonly IGreetingBL _greetingBL;
        ResponseModel<RequestDTO> response;

        public HelloGreetingController(ILogger<HelloGreetingController> logger, IGreetingBL greetingBL)
        {
            _logger = logger;
            _greetingBL = greetingBL;
        }

        /// <summary>
        /// Get method to fetch the greeting message from Business Layer
        /// </summary>
        /// <returns>response model</returns>
        [HttpGet]
        [Route("greeting")]
        public IActionResult GetGreetingMessage()
        {
            _logger.LogInformation("GET request received to fetch greeting message from Business Layer.");
            string greetingMessage = _greetingBL.GreetingMessage();
            ResponseModel<string> responseModel = new ResponseModel<string>();
            responseModel.Success = true;
            responseModel.Message = "Greeting message retrieved successfully!";
            responseModel.Data = greetingMessage;
            return Ok(responseModel);
        }

        /// <summary>
        /// Get method to fetch the personal greeting message with name
        /// </summary>
        /// <returns>response model</returns>
        [HttpGet]
        [Route("personalGreeting")]
        public IActionResult GetGreetingMessageWithName([FromQuery] string? firstName, [FromQuery] string? lastName)
        {
            _logger.LogInformation("GET request received to fetch personal greeting message with name.");
            string greetingMessageWithName = _greetingBL.GreetingMessageWithName(firstName, lastName);
            ResponseModel<string> responseModel = new ResponseModel<string>();
            responseModel.Success = true;
            responseModel.Message = "Greeting message retrieved successfully!";
            responseModel.Data = greetingMessageWithName;
            return Ok(responseModel);
        }

        [HttpGet("{Id}")]
        public IActionResult GetGreetingMessageById(int Id)
        {
            _logger.LogInformation($"GET request received to fetch greeting by ID: {Id}");
            var greeting = _greetingBL.GreetingMessageByIdBL(Id);
            ResponseModel<GreetingEntity> greetingResponse = new ResponseModel<GreetingEntity>(); ;
            if (greeting == null)
            {
                greetingResponse.Success = false;
                greetingResponse.Message = "Greeting not found!";
                greetingResponse.Data = null;
                return NotFound(greetingResponse); 
            }

            greetingResponse.Success = true;
            greetingResponse.Message = "Greeting message retrieved successfully!";
            greetingResponse.Data = greeting;
            return Ok(greetingResponse);
        }

        [HttpGet]
        [Route("AllGreetings")]
        public IActionResult GetGreetingsList()
        {
            _logger.LogInformation($"GET request received to fetch all the greeting messages in the repository.");
            var greetings = _greetingBL.ListGreetingMessagesBL();
            ResponseModel<List<GreetingEntity>> greetingResponse = new ResponseModel<List<GreetingEntity>>(); ;
            if (greetings == null || !greetings.Any())
            {
                greetingResponse.Success = false;
                greetingResponse.Message = "Greeting not found!";
                greetingResponse.Data = null;
                return NotFound(greetingResponse);
            }

            greetingResponse.Success = true;
            greetingResponse.Message = "Greeting messages retrieved successfully!";
            greetingResponse.Data = greetings;
            return Ok(greetingResponse);
        }

        /// <summary>
        /// Post method to add greeting message in the Repository
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns>reponse model</returns>
        [HttpPost]
        public IActionResult SaveGreetingMessage(RequestDTO requestDTO)
        {
            _logger.LogInformation("POST request to save greeting message in the repository.");
            var result = _greetingBL.GreetingMessageBL(requestDTO);
            response = new ResponseModel<RequestDTO>();
            response.Success = true;
            response.Message = "Greeting message saved!";
            response.Data = result;
            return Ok(response);
        }
    }
}
