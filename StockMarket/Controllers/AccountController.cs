using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using StockMarket.DTO.Account;
using StockMarket.Model;

namespace StockMarket.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signinManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signinManager)
        {
            this.userManager = userManager;
            this.signinManager = signinManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDTO register)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState); 
                }
                var appUser = new AppUser
                {
                    UserName = register.UserName,
                    Email = register.Email,
                };
                var createUser = await userManager.CreateAsync(appUser, register.Password);
                if(createUser.Succeeded)
                {
                    var roleResult = await userManager.AddToRoleAsync(appUser, "User");
                    if(roleResult.Succeeded)
                    {
                        return Ok("User Created Successfully");
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createUser.Errors);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Retrieving Data from Database");
            }
        }


        [HttpPost("login")]
        public async Task<IActionResult> LoginUser (LoginDTO login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await userManager.Users.FirstOrDefaultAsync(x => x.UserName == login.UserName);
            if(user == null)
            {
                return Unauthorized("Invalid UserName ");
            }
            var result = await signinManager.CheckPasswordSignInAsync(user, login.Password, false);
            if (!result.Succeeded)
            {
                return Unauthorized("UserName or Password Not Found");
            }
            return Ok(user);

        }
    }
}
