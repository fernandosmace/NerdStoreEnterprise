using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NSE.Identidade.API.Models.InputModel;

namespace NSE.Identidade.API.Controllers;

[ApiController]
[Route("account")]
public class AuthController : Controller
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegisterInputModel userRegister)
    {
        if (!ModelState.IsValid) return BadRequest();

        var user = new IdentityUser
        {
            UserName = userRegister.Email,
            Email = userRegister.Email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, userRegister.Password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, false);
            return Created();
        }

        return BadRequest();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginInputModel userRegister)
    {
        if (!ModelState.IsValid) return BadRequest();

        var result = await _signInManager.PasswordSignInAsync(userRegister.Email, userRegister.Password, false, true);

        if (result.Succeeded)
        {
            return Ok();
        }

        return BadRequest();
    }
}
