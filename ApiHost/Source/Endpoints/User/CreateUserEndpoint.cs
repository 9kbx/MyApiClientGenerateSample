namespace MyApiClientGenerateSample.Endpoints.User;

public class CreateUserEndpoint : Endpoint<CreateUserRequest, CreateUserResponse>
{
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

    public override async Task HandleAsync(CreateUserRequest r, CancellationToken c)
    {
        await Send.OkAsync(new($"Hello {r.FirstName} {r.LastName}..."), c);
    }
}

public sealed record CreateUserRequest(string FirstName, string LastName);

public sealed record CreateUserResponse(string Message);