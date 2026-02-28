namespace MyApiClientGenerateSample.Endpoints.Product;

public class GetProductEndpoint : Endpoint<GetProductRequest, ProductDto>
{
    public override void Configure()
    {
        Get("{ProductId}");
        Group<ProductEndpointGroup>();
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetProductRequest r, CancellationToken c)
    {
        await Send.OkAsync(new (r.ProductId, "iPhone20", "ip20"),c);
    }
}

public sealed record GetProductRequest(int ProductId);