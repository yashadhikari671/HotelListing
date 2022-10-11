using AutoMapper;
using HotelListing.Data;
using HotelListing.Dtos;
using HotelListing.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace HotelListing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApiUser> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;
        private readonly IAuthServices _authServices;

        public AccountController(UserManager<ApiUser> userManager,
            ILogger<AccountController> logger,
            IMapper mapper,
            IAuthServices authServices)
        {
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
            _authServices = authServices;
            

        }
        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            _logger.LogInformation($"Register Attempt for {userDto.EmailAddress}");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = _mapper.Map<ApiUser>(userDto);
                user.UserName = userDto.EmailAddress;
                user.Email = userDto.EmailAddress;
                var result = await _userManager.CreateAsync(user ,userDto.Password);

                if (!result.Succeeded)
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);
                }
                await _userManager.AddToRoleAsync(user, "Admin");

                return Accepted();

            }catch(Exception ex)
            {
                _logger.LogError(ex, $"Something went Wrong {nameof(Register)}");
                return Problem($"Something went wrong in Server", statusCode: 500);
            }
        }
        [HttpPost]
        [Route("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] UserDto userDto)
        {
            _logger.LogInformation($"Login Attempt for {userDto.EmailAddress}");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                
                if (!await _authServices.ValidateUser(userDto))
                {
                    return Unauthorized();
                }
                return Accepted(new {token = await _authServices.CreateToken()});

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went Wrong {nameof(Login)}");
                return Problem($"Something went wrong in Server", statusCode: 500);
            }
        }
    }
}
