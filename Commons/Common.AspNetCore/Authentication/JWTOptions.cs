namespace Common.AspNetCore.Authentication;

public class JWTOptions
{
    public JWTOptions()
    {
        Issuer = nameof(Issuer);
        Audience = nameof(Audience);
        Key = Guid.NewGuid().ToString("N");
        ExpireSeconds = 86400;
    }

    /// <summary>
    /// 令牌颁发者
    /// </summary>
    public string Issuer { get; set; }

    /// <summary>
    /// 受众
    /// </summary>
    public string Audience { get; set; }

    /// <summary>
    /// 编码密钥
    /// </summary>
    public string Key { get; set; }

    /// <summary>
    /// 过期时间
    /// </summary>
    public int ExpireSeconds { get; set; }
}