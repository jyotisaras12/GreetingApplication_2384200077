using BusinessLayer.Interface;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Model;

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
        private static int newId = 1;
        private static Dictionary<int, RequestDTO> _greetings = new Dictionary<int, RequestDTO>
        {
            { newId++, new RequestDTO { Key = "Formal", Value = "Good Morning!" } },
            { newId++, new RequestDTO { Key = "Informal", Value = "Hey!" } },
            { newId++, new RequestDTO { Key = "Cultural", Value = "Namaste!" } }
        };

        public HelloGreetingController(ILogger<HelloGreetingController> logger, IGreetingBL greetingBL)
        {
            _logger = logger;
            _greetingBL = greetingBL;
        }

        /// <summary>
        /// Get method to get the greeting messages
        /// </summary>
        /// <returns>response model</returns>
        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("GET request received to fetch all greetings.");
            ResponseModel<Dictionary<int, RequestDTO>> responseModel = new ResponseModel<Dictionary<int, RequestDTO>>();
            responseModel.Success = true;
            responseModel.Message = "Get() method executed successfully!";
            responseModel.Data = _greetings;
            return Ok(responseModel);
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
        /// Post method to add a key-value pair greeting
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns>response model</returns>
        [HttpPost]
        public IActionResult Post(RequestDTO requestDTO)
        {
            ResponseModel<string> response = new ResponseModel<string>();
            if (requestDTO == null)
            {
                _logger.LogWarning("POST request failed: Invalid data request made.");
                response.Success = false;
                response.Message = "Invalid data request!";
                response.Data = null;
                return BadRequest(response);
            }
            // If the key-value pair already exists
            if (_greetings.Values.Any(g => g.Key == requestDTO.Key && g.Value == requestDTO.Value))
            {
                response.Success = false;
                response.Message = "Greeting already exists!";
                response.Data = null;
                return BadRequest(response);
            }
            // If key-value pair doesn't already exist in '_greetings' dictionary
            _greetings[newId++] = requestDTO;
            _logger.LogInformation($"New greeting added.");
            response.Success = true;
            response.Message = "Greeting added successfully";
            response.Data = requestDTO.Value;
            return Ok(response);
        }

        /// <summary>
        /// Put method to update greeting value by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="requestDTO"></param>
        /// <returns>response model</returns>
        [HttpPut("{Id}")]
        public IActionResult Put(int Id,RequestDTO requestDTO) 
        {
            ResponseModel<string> response = new ResponseModel<string>();
            if (!_greetings.ContainsKey(Id))
            {
                _logger.LogWarning($"PUT request failed: Record with ID = {Id} not found.");
                response.Success = false;
                response.Message = "Greeting not found!";
                response.Data = null;
                return NotFound(response);
            }

            _greetings[Id] = requestDTO;
            _logger.LogInformation($"Greeting updated: ID = {Id}, Key = {requestDTO.Key}, Value = {requestDTO.Value}");
            response.Success = true;
            response.Message = "Greeting updated successfully";
            response.Data = requestDTO.Value;
            return Ok(response);
        }

        /// <summary>
        /// Patch method to patch key-value pair
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="requestDTO"></param>
        /// <returns>response model</returns>
        [HttpPatch("{Id}")]
        public IActionResult Patch(int Id,RequestDTO requestDTO) 
        {
            ResponseModel<string> response = new ResponseModel<string>();
            if (!_greetings.ContainsKey(Id))
            {
                _logger.LogWarning($"PATCH request failed: Record with ID = {Id} not found.");
                response.Success = false;
                response.Message = "Greeting not found!";
                response.Data = null;
                return NotFound(response);
            }

            var existingGreeting = _greetings[Id];
            if (!string.IsNullOrWhiteSpace(requestDTO.Key))
                existingGreeting.Key = requestDTO.Key;

            if (!string.IsNullOrWhiteSpace(requestDTO.Value))
                existingGreeting.Value = requestDTO.Value;

            _greetings[Id] = existingGreeting;
            _logger.LogInformation($"Greeting patched: ID = {Id}, Key = {existingGreeting.Key}, Value = {existingGreeting.Value}");

            response.Success = true;
            response.Message = "Greeting patched successfully";
            response.Data = requestDTO.Value;
            return Ok(response);
        }

        /// <summary>
        /// Delete method to delete a greeting by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id) 
        {
            ResponseModel<string> response = new ResponseModel<string>();
            if (!_greetings.ContainsKey(Id))
            {
                _logger.LogWarning($"DELETE request failed: Record with ID = {Id} not found.");
                response.Success = false;
                response.Message = "Greeting not found!";
                response.Data = null;
                return NotFound(response);
            }

            _greetings.Remove(Id);
            _logger.LogInformation($"Greeting deleted: ID = {Id}");
            response.Success = true;
            response.Message = "Greeting deleted successfully";
            response.Data = $"Deleted Id: {Id}";
            return Ok(response);
        }
    }
}
