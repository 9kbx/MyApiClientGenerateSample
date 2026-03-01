namespace MyApiClientGenerateSample.Endpoints.User;

/// <summary>
/// 表示用户的数据传输对象。
/// </summary>
/// <param name="Id">用户的唯一标识符。</param>
/// <param name="Name">用户的名称。</param>
public record UserDto(int Id, string Name);
