namespace MyApiClientGenerateSample.Endpoints.User;

public class UpdateUserEndpoint : Endpoint<UpdateUserRequest, ResponseData<string>>
{
    public override void Configure()
    {
        Post("update/{UserId}");
        Group<UserEndpointGroup>();
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateUserRequest r, CancellationToken c)
    {
        await Send.OkAsync($"userId={r.UserId}".AsResponseData(), c);
    }
}

public sealed class UpdateUserRequest
{
    [RouteParam]
    public string UserId { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
}