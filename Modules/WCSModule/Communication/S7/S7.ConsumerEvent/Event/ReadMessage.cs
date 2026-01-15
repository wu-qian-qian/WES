/// <summary>
/// 
/// </summary>
/// <param name="EventId">幂等标识/param>
/// <param name="DeviceName"></param>
/// <param name="CacheKey"></param>
public record ReadMessage(Guid EventId, string DeviceName,Guid CacheKey);

