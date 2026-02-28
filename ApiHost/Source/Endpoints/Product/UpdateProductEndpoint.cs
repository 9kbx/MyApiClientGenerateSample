namespace MyApiClientGenerateSample.Endpoints.Product;

public class UpdateProductEndpoint : Endpoint<UpdateProductRequest, ProductDto>
{
    public override void Configure()
    {
        Post("{ProductId}");
        Group<ProductEndpointGroup>();
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateProductRequest r, CancellationToken c)
    {
        await Send.OkAsync(new(r.ProductId, r.Name, r.Code), c);
    }
}

public sealed class UpdateProductRequest
{
    [RouteParam]
    public int ProductId { get; set; }

    public string Name { get; set; }
    public string Code { get; set; }
}