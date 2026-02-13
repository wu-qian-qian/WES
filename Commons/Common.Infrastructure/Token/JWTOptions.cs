namespace Common.Infrastructure.Token;

public sealed class JWTOptions
{
    public JWTOptions()
    {

        //默认30天过期时间
        ExpireSeconds = 2592000;
    }

    /// <summary>
    ///     令牌颁发者
    /// </summary>
    public string Issuer { get; set; }

    /// <summary>
    ///     受众
    /// </summary>
    public string Audience { get; set; }

    /// <summary>
    ///     编码密钥
    /// </summary>
    public string Key { get; set; }

    /// <summary>
    ///     过期时间
    /// </summary>
    public int ExpireSeconds { get; set; }
}