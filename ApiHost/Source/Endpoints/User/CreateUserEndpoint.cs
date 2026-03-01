namespace MyApiClientGenerateSample.Endpoints.User;

/// <summary>
/// 创建新用户的端点。
/// </summary>
public class CreateUserEndpoint : Endpoint<CreateUserRequest, CreateUserResponse>
{
    /// <summary>
    /// 配置端点设置。
    /// </summary>
    public override void Configure()
    {
        Post("create");
        Group<UserEndpointGroup>();
        AllowAnonymous();
        
        // 则默认按命名空间+类名对operationId命名
        // "operationId": "MyApiClientGenerateSampleEndpointsUserCreateUserEndpoint",
        // 生成Api客户端调用方法也会这么长
        // 所以建议自定义operationId
        // 注意：operationId重名将导致openapi文档生成失败
        // 建议：通过全局变量进行定义
        // Description(x => x.WithName("CreateUser"));
    }

    /// <summary>
    /// 处理创建用户请求。
    /// </summary>
    /// <param name="r">包含用户详细信息的创建用户请求。</param>
    /// <param name="c">取消令牌。</param>
    public override async Task HandleAsync(CreateUserRequest r, CancellationToken c)
    {
        await Send.OkAsync(new($"Hello {r.FirstName} {r.LastName}..."), c);
    }
}

/// <summary>
/// 创建用户的请求模型。
/// </summary>
/// <param name="FirstName">用户的名字。</param>
/// <param name="LastName">用户的姓氏。</param>
public sealed record CreateUserRequest(string FirstName, string LastName);

/// <summary>
/// 创建用户操作的响应模型。
/// </summary>
/// <param name="Message">结果消息。</param>
public sealed record CreateUserResponse(string Message);
