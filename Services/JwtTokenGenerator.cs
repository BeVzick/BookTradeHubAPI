using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BookTradeHubAPI.Models.Entity;
using BookTradeHubAPI.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BookTradeHubAPI.Services;

public class JwtTokenGenerator
{
    private readonly JWTSettings _settings;

    public JwtTokenGenerator(IOptions<JWTSettings> options)
    {
        _settings = options.Value;
    }

    public string Generate(Student student)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Secretkey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, student.Id),
            new Claim(JwtRegisteredClaimNames.Email, student.Email),
            new Claim("firstName", student.FirstName),
            new Claim("lastName", student.LastName)
        };

        var token = new JwtSecurityToken(
            issuer: _settings.Issuer,
            audience: _settings.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(_settings.AccessTokenExpirationMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
