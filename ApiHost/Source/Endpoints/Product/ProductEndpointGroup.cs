namespace MyApiClientGenerateSample.Endpoints.Product;

public sealed class ProductEndpointGroup : Group
{
    public ProductEndpointGroup()
    {
        Configure(
            "product",
            ep =>
            {
                ep.Description(x => x.AutoTagOverride("Product"));
            });
    }
}