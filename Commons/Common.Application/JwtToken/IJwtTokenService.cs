namespace Common.Application.JwtToken;

public interface IJwtTokenService
{
    //JWT编码
    string BuildJwtString(string name, List<string> roles, List<string> permissions);
}