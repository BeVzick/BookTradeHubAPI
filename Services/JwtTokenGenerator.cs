using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BookTradeHubAPI.Enums;
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

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.NameId, student.Id),
            new Claim(JwtRegisteredClaimNames.Email, student.Email),
            new Claim("firstName", student.FirstName),
            new Claim("lastName", student.LastName)
        };

        foreach (Roles role in Enum.GetValues(typeof(Roles)))
        {
            if (student.Role.HasFlag(role))
                claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
        }

        claims.Add(new Claim("rolesValue", ((int)student.Role).ToString()));

        var token = new JwtSecurityToken(
            issuer: _settings.Issuer,
            audience: _settings.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(_settings.AccessTokenExpirationMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefresh()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Secretkey));

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _settings.Issuer,
            ValidAudience = _settings.Audience,
            IssuerSigningKey = key,
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                return null;

            return principal;
        }
        catch
        {
            return null;
        }
    }
}
