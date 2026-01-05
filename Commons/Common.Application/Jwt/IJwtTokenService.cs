namespace Common.Application.Jwt;

public interface IJwtTokenService
{
    string BuildJwtString(string name,List<string> roles, List<string> permissions);
}