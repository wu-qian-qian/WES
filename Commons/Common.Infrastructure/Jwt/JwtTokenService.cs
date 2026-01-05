using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Common.Application.Encodings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Common.Infrastructure.Authentication;

public sealed class TokenService(JWTOptions options) : IJwtTokenService
{
    public string BuildJwtString(string name,List<string> roles, List<string> permissions)
    {
        // 创建Claims
        var claims = new List<Claim>();
        var ts = TimeSpan.FromSeconds(options.ExpireSeconds);
        // 添加角色
        roles.ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));

        // 添加权限（自定义声明）
        permissions.ForEach(permission => claims.Add(new Claim("permission", permission)));
        claims.Add(new Claim(ClaimTypes.Name, name));

        //构建密钥
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        // 创建令牌
        var tokenDescriptor = new JwtSecurityToken(options.Issuer, options.Audience, claims,
            expires: DateTime.Now.Add(ts), signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
}