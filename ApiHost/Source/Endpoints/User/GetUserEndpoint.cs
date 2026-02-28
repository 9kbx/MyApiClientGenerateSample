namespace MyApiClientGenerateSample.Endpoints.User;

public class GetUserEndpoint : Endpoint<GetUserRequest, ResponseData<string>>
{
    public override void Configure()
    {
        Get("{UserId}");
        Group<UserEndpointGroup>();
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetUserRequest r, CancellationToken c)
    {
        var firstName = "hello";
        var lastName = "world";
        await Send.OkAsync($"userId={r.UserId}, userName={firstName} {lastName}".AsResponseData(), c);
    }
}

public sealed class GetUserRequest
{
    [RouteParam]
    public string UserId { get; set; }
}