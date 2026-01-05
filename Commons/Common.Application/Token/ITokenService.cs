namespace Common.Application.Token;

public interface ITokenService
{
    //JWT编码
    string BuildJwtString(string name,List<string> roles, List<string> permissions,JWTOptions options);
}