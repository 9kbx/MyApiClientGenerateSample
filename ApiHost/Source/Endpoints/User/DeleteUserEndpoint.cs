namespace MyApiClientGenerateSample.Endpoints.User;

/// <summary>
/// 删除用户的端点。
/// </summary>
public class DeleteUserEndpoint : Endpoint<DeleteUserRequest, DeleteUserResponse>
{
    /// <summary>
    /// 配置端点设置。
    /// </summary>
    public override void Configure()
    {
        Delete("{UserId}");
        Group<UserEndpointGroup>();
        Description(x => x.WithName("DeleteUser"));
        AllowAnonymous();
    }

    /// <summary>
    /// 处理删除用户请求。
    /// </summary>
    /// <param name="req">包含用户ID的删除用户请求。</param>
    /// <param name="ct">取消令牌。</param>
    public override async Task HandleAsync(DeleteUserRequest req, CancellationToken ct)
    {
        // 删除用户的逻辑将在这里
        await Send.OkAsync(new DeleteUserResponse($"User {req.UserId} deleted successfully"), ct);
    }
}

/// <summary>
/// 删除用户的请求模型。
/// </summary>
public sealed class DeleteUserRequest
{
    /// <summary>
    /// 获取或设置要删除的用户的唯一标识符。
    /// </summary>
    [RouteParam]
    public string UserId { get; set; }
}

/// <summary>
/// 删除用户操作的响应模型。
/// </summary>
/// <param name="Message">结果消息。</param>
public sealed record DeleteUserResponse(string Message);
