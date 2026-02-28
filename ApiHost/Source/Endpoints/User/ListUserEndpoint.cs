namespace MyApiClientGenerateSample.Endpoints.User;

public class ListUserEndpoint : EndpointWithoutRequest<List<UserDto>>
{
    public override void Configure()
    {
        Get("list");
        Group<UserEndpointGroup>();
        // Description(x => x.WithName("List"));
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken c)
    {
        await Send.OkAsync(
            Enumerable.Range(1, 10)
                      .Select(x => new UserDto(x, $"user_{x}"))
                      .ToList(),
            c);
    }
}