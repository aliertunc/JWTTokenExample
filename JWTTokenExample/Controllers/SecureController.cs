using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api")]
[ApiController]
public class SecureController : ControllerBase
{
    [Authorize(Roles = "Admin")]
    [HttpPost("admin/post")]
    public IActionResult AdminPost() 
    {
        return Ok("Admin tarafından post işlemi başarılı!");
    }

    [Authorize(Roles = "User")]
    [HttpPost("user/post")]
    public IActionResult UserPost()
    {
        return Ok("User tarafından post işlemi başarılı!");
    }
}
