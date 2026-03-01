namespace MyApiClientGenerateSample.Endpoints.User;

/// <summary>
/// 更新现有用户的端点。
/// </summary>
public class UpdateUserEndpoint : Endpoint<UpdateUserRequest, ResponseData<string>>
{
    /// <summary>
    /// 配置端点设置。
    /// </summary>
    public override void Configure()
    {
        Post("update/{UserId}");
        Group<UserEndpointGroup>();
        AllowAnonymous();
    }

    /// <summary>
    /// 处理更新用户请求。
    /// </summary>
    /// <param name="r">包含更新详细信息的更新用户请求。</param>
    /// <param name="c">取消令牌。</param>
    public override async Task HandleAsync(UpdateUserRequest r, CancellationToken c)
    {
        await Send.OkAsync($"userId={r.UserId}".AsResponseData(), c);
    }
}

/// <summary>
/// 更新用户的请求模型。
/// </summary>
public sealed class UpdateUserRequest
{
    /// <summary>
    /// 获取或设置要更新的用户的唯一标识符。
    /// </summary>
    [RouteParam]
    public string UserId { get; set; }

    /// <summary>
    /// 获取或设置用户的名字。
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// 获取或设置用户的姓氏。
    /// </summary>
    public string LastName { get; set; }
}
