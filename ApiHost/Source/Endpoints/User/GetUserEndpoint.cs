namespace MyApiClientGenerateSample.Endpoints.User;

/// <summary>
/// 用于检索单个用户的端点。
/// </summary>
public class GetUserEndpoint : Endpoint<GetUserRequest, ResponseData<string>>
{
    /// <summary>
    /// 配置端点设置。
    /// </summary>
    public override void Configure()
    {
        Get("{UserId}");
        Group<UserEndpointGroup>();
        AllowAnonymous();
    }

    /// <summary>
    /// 处理获取用户请求。
    /// </summary>
    /// <param name="r">包含用户ID的获取用户请求。</param>
    /// <param name="c">取消令牌。</param>
    public override async Task HandleAsync(GetUserRequest r, CancellationToken c)
    {
        var firstName = "hello";
        var lastName = "world";
        await Send.OkAsync($"userId={r.UserId}, userName={firstName} {lastName}".AsResponseData(), c);
    }
}

/// <summary>
/// 用于检索用户的请求模型。
/// </summary>
public sealed class GetUserRequest
{
    /// <summary>
    /// 获取或设置要检索的用户的唯一标识符。
    /// </summary>
    [RouteParam]
    public string UserId { get; set; }
}
