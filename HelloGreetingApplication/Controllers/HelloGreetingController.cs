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
        /// Method for test Exception API
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpGet("exception")]
        public IActionResult ThrowException()
        {
            throw new Exception("This is a test exception");
        }

        /// <summary>
        /// Method to fetch the greeting message from Business Layer
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
        /// Method to fetch the personal greeting message with name
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

        /// <summary>
        /// Method to fetch greeting message by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>response model</returns>
        [HttpGet("{Id}")]
        public IActionResult GetGreetingMessageById(int Id)
        {
            _logger.LogInformation($"GET request received to fetch greeting by ID: {Id}");
            var greeting = _greetingBL.GreetingMessageByIdBL(Id);
            ResponseModel<GreetingEntity> greetingResponse = new ResponseModel<GreetingEntity>(); ;
            if (greeting == null)
            {
                _logger.LogWarning($"GET request failed: Greeting with Id = {Id} not found.");
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

        /// <summary>
        /// Method to fetch all the greeting messages in the repository
        /// </summary>
        /// <returns>response model</returns>
        [HttpGet]
        [Route("AllGreetings")]
        public IActionResult GetGreetingsList()
        {
            _logger.LogInformation($"GET request received to fetch all the greeting messages in the repository.");
            var greetings = _greetingBL.ListGreetingMessagesBL();
            ResponseModel<List<GreetingEntity>> greetingResponse = new ResponseModel<List<GreetingEntity>>(); ;
            if (greetings == null || !greetings.Any())
            {
                _logger.LogWarning($"GET request failed: Greetings not found in the repository.");
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
        /// Method to add greeting message in the Repository
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns>reponse model</returns>
        [HttpPost]
        public IActionResult SaveGreetingMessage(RequestDTO requestDTO)
        {
            try
            {
                _logger.LogInformation("POST request to save greeting message in the repository.");
                var result = _greetingBL.GreetingMessageBL(requestDTO);
                response = new ResponseModel<RequestDTO>();
                response.Success = true;
                response.Message = "Greeting message saved!";
                response.Data = result;
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error saving greeting: {ex.Message}");
                return StatusCode(500, "An error occurred while saving the greeting.");
            }
        }

        /// <summary>
        /// Method to edit greeting messages in the repository
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="newGreeting"></param>
        /// <returns>response model</returns>
        [HttpPut]
        [Route("{Id}")]
        public IActionResult EditGreetingMessage(int Id, GreetingEntity newGreeting)
        {
            _logger.LogInformation("PUT request to edit greeting messages in the repository.");
            ResponseModel<GreetingEntity>response = new ResponseModel<GreetingEntity>();
            if (newGreeting == null || Id != newGreeting.Id)
            {
                _logger.LogWarning("PUT request failed: Invalid greeting request made.");
                response.Success = false;
                response.Message = "Invalid greeting message!";
                response.Data = null;
                return BadRequest(response);
            }

            var result = _greetingBL.EditGreetingBL(Id, newGreeting);
            if (result == null)
            {
                _logger.LogWarning($"PUT request failed: Greeting not found in the repository.");
                response.Success = false;
                response.Message = "Greeting not found!";
                response.Data = null;
                return NotFound(response);
            }

            response.Success = true;
            response.Message = "Greeting message updated successfully!";
            response.Data = result;
            return Ok(response);
        }

        /// <summary>
        /// Method to delete greeting messages from the repository
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{Id}")]
        public IActionResult DeleteGreetingMessage(int Id)
        {
            _logger.LogInformation("DELETE request to delete greeting messages in the repository.");
            ResponseModel<string> response = new ResponseModel<string>();
            
            var result = _greetingBL.DeleteGreetingBL(Id);
            if (result == false)
            {
                _logger.LogWarning($"DELETE request failed: Greeting not found in the repository.");
                response.Success = false;
                response.Message = "Greeting not found!";
                response.Data = null;
                return NotFound(response);
            }

            response.Success = true;
            response.Message = "Greeting message deleted successfully!";
            response.Data = $"Deleted Id: {Id}";
            return Ok(response);
        }

    }
}
