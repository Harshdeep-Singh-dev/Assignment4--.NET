using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

public class LoginController : Controller
{
    // LOGIN WITH GITHUB
    [HttpGet("login/github")]
    public IActionResult GitHubLogin()
    {
        var props = new AuthenticationProperties { RedirectUri = "/" };
        return Challenge(props, "GitHub");
    }

    // LOGIN WITH GOOGLE
    [HttpGet("login/google")]
    public IActionResult GoogleLogin()
    {
        var props = new AuthenticationProperties { RedirectUri = "/" };
        return Challenge(props, "Google");
    }
}
