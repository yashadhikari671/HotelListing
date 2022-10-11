using HotelListing.Data;
using AutoMapper.Configuration;
using HotelListing.Dtos;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Collections.Generic;
using System.Security.Claims;

namespace HotelListing.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly UserManager<ApiUser> _userManager;
        private readonly IConfiguration _configuration;
        private ApiUser _user;
        public AuthServices(UserManager<ApiUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<string> CreateToken()
        {
           
            var signingCredentials = GetSigninCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var JwtSetting = _configuration.GetSection("Jwt");
            var expiration = DateTime.Now.AddMinutes(Convert.ToDouble(JwtSetting.GetSection("Lifetime").Value));
            var token = new JwtSecurityToken(
                issuer: JwtSetting.GetSection("Issuer").Value,
                claims: claims,
                expires: expiration,
                signingCredentials: signingCredentials
                );
            return token;
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,_user.UserName)
            };
            var roles = await _userManager.GetRolesAsync(_user);
            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        private SigningCredentials GetSigninCredentials()
        {
            var JwtSetting = _configuration.GetSection("Jwt");
            var key = JwtSetting.GetSection("Key").Value;
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            return new SigningCredentials(secret,SecurityAlgorithms.HmacSha256);
        }

        public async Task<bool> ValidateUser(UserDto userDto)
        {
            _user = await  _userManager.FindByNameAsync(userDto.EmailAddress);
            return (_user != null && await _userManager.CheckPasswordAsync(_user,userDto.Password));
        }
    }
}
