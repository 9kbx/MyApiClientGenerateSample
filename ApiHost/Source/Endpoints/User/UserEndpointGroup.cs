namespace MyApiClientGenerateSample.Endpoints.User;

public sealed class UserEndpointGroup : Group
{
    public UserEndpointGroup()
    {
        Configure(
            "user",
            ep =>
            {
                ep.Description(x => x.AutoTagOverride("User"));
            });
    }
}