namespace MyApiClientGenerateSample.Endpoints.User;

/// <summary>
/// 用于列出所有用户的端点。
/// </summary>
public class ListUserEndpoint : EndpointWithoutRequest<List<UserDto>>
{
    /// <summary>
    /// 配置端点设置。
    /// </summary>
    public override void Configure()
    {
        Get("list");
        Group<UserEndpointGroup>();
        // Description(x => x.WithName("List"));
        AllowAnonymous();
    }

    /// <summary>
    /// 处理列出用户请求。
    /// </summary>
    /// <param name="c">取消令牌。</param>
    public override async Task HandleAsync(CancellationToken c)
    {
        await Send.OkAsync(
            Enumerable.Range(1, 10)
                      .Select(x => new UserDto(x, $"user_{x}"))
                      .ToList(),
            c);
    }
}
