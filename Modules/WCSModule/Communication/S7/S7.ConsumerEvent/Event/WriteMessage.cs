/// <summary>
/// 
/// </summary>
/// <param name="EventId"></param>
/// <param name="DeviceName"></param>
/// <param name="Values">字段名 数据</param>
public record WriteMessage(Guid EventId, string DeviceName, IReadOnlyDictionary<string, string> Values);