using Enities.DTOs;
using Enities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECOM.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class _AccountsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _jwtSettings;
        public _AccountsController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
            _jwtSettings = _configuration.GetSection("JWTSettings");
        }
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDTO userRegistration)
        {
            if (userRegistration == null || !ModelState.IsValid)
            {
                return BadRequest("Cannot Register user");
            }
            var user = new ApplicationUser()
            {
                UserName = userRegistration.Name,
                Email = userRegistration.Email,
                PhoneNumber = userRegistration.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, userRegistration.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new RegistrationResponseDTO { Errors = errors });
            }
            return StatusCode(201);
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            

            if (user == null || !await _userManager.CheckPasswordAsync(user, login.Password))
            {
                return Unauthorized(new LoginResponseDTO { Errors = new[] { "Faild to login" } });
            }
            var signinCredentials = GetSigningCredentials();
            var Claims = GetClaims(user);
            var tokenOptions = GenerateTokenOptions(signinCredentials, Claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return Ok(new LoginResponseDTO { IsLogInSuccessful = true, Token = token });
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes("Dd2AR9zxfUHmtu9yyHgcDgTXJYy26k211VGeU0Gw2T8AX");
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        private List<Claim> GetClaims(ApplicationUser user)
        {
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.Email)
    };

            return claims;
        }
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken(
                issuer: _jwtSettings["validIssuer"],
                audience: _jwtSettings["validAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings["expiryInMinutes"])),
                signingCredentials: signingCredentials);

            return tokenOptions;
        }
    }
}

