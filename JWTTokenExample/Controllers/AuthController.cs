using Microsoft.AspNetCore.Mvc;
 
[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    // Kullanıcı giriş yapar ve Access + Refresh Token alır
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        if (request.Role != "Admin" && request.Role != "User")
        {
            return BadRequest("Geçersiz rol");
        }

        var accessToken = _authService.GenerateJwtToken(request.Role);
        var refreshToken = _authService.GenerateRefreshToken();

        // Refresh token'ı güvenli bir şekilde sakla (örneğin veritabanında)
        //_authService.SaveRefreshToken(refreshToken, request.Role);

        return Ok(new { AccessToken = accessToken, RefreshToken = refreshToken });
    }

    // Refresh Token ile yeni Access Token al
    [HttpPost("refresh-token")]
    public IActionResult RefreshToken([FromBody] RefreshTokenRequest request)
    {
        var role = _authService.ValidateRefreshToken(request.RefreshToken);
        if (role == null) return Unauthorized("Geçersiz veya süresi dolmuş refresh token");

        var newAccessToken = _authService.GenerateJwtToken(role);
        var newRefreshToken = _authService.GenerateRefreshToken();
        _authService.UpdateRefreshToken(request.RefreshToken, newRefreshToken);

        return Ok(new { AccessToken = newAccessToken, RefreshToken = newRefreshToken });
    }

    // Refresh Token iptal ederek logout işlemi
    [HttpPost("logout")]
    public IActionResult Logout([FromBody] RefreshTokenRequest request)
    {
        //_authService.DeleteRefreshToken(request.RefreshToken);
        return Ok(new { Message = "Çıkış yapıldı" });
    }
}