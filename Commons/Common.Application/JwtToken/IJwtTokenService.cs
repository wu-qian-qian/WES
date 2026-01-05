namespace Common.Application.Token;

public interface IJwtTokenService
{
    //JWT编码
    string BuildJwtString(string name,List<string> roles, List<string> permissions);
}