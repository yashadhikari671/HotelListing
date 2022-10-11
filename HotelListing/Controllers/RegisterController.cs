using HotelListing.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace HotelListing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IunitOfWork _unitOfWork;
        private readonly ILogger<RegisterController> _logger;

        public RegisterController(IunitOfWork unitOfWork, ILogger<RegisterController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var Registered = await _unitOfWork.RegisterRepo.GetAll();
                return Ok(Registered);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex ,$"SOMETHING WENT WRONG {nameof(Get)}");
                return StatusCode(500, "Internal server error");

            }
        }
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var Registered = await _unitOfWork.RegisterRepo.Get(n => n.Id == id);
                return Ok(Registered);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"SOMETHING WENT WRONG {nameof(Get)}");
                return StatusCode(500, "Internal server error");

            }
        }

    }
}
