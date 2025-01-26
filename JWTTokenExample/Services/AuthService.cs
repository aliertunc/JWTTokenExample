using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
 
public class AuthService
{
    private readonly IConfiguration _config;

    public AuthService(IConfiguration config)
    {
        _config = config;
    }

    public string GenerateJwtToken(string role)
    {
        var key = Encoding.UTF8.GetBytes(_config["JwtSettings:Secret"]);
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, "TestUser"),
                new Claim(ClaimTypes.Role, role) // Role ata
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = _config["JwtSettings:Issuer"],
            Audience = _config["JwtSettings:Audience"],
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature
            )
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var refreshToken = Guid.NewGuid().ToString();
        return refreshToken;
    }

    // Refresh token'ı doğrula
    public string ValidateRefreshToken(string refreshToken)
    {
        // Burada refresh token'ı veritabanı veya güvenli bir mekanizmada kontrol etmelisin
        // Örneğin: token'ın veritabanında olup olmadığı ve geçerlilik süresi kontrol edilebilir
        return "Admin"; // Örnek olarak "Admin" döndürüyoruz
    }

    // Refresh token'ı güncelle
    public void UpdateRefreshToken(string oldRefreshToken, string newRefreshToken)
    {
        // Eski refresh token'ı veritabanından sil ve yeni olanı kaydet
        // Bu işlemi gerçek veritabanı veya güvenli bir mekanizmada yapmalısınız
    }
}
  