namespace MyApiClientGenerateSample.Endpoints.Product;

sealed class ListProductEndpoint()
    : Endpoint<ListProductRequest, ResponseData<List<ProductDto>>>
{
    public override void Configure()
    {
        Get("product/list");
        Description(x =>
                x.AutoTagOverride("Product")
                 .WithName("List") // 自定义OpratorId，生成api客户端时方法名此名称为准
        );
        AllowAnonymous();
    }

    public override async Task HandleAsync(ListProductRequest r, CancellationToken c)
    {
        List<ProductDto> products = [];

        if (!string.IsNullOrEmpty(r.Name))
            products = Products.Where(p => p.Name.Contains(r.Name)).ToList();

        await Send.OkAsync(products.AsResponseData(), c);
    }

    static List<ProductDto> Products =
    [
        new(1, "product1", "p1"),
        new(2, "product2", "p2"),
        new(3, "product3", "p3"),
        new(4, "product4", "p4"),
        new(5, "product5", "p5"),
    ];
}

public sealed class ListProductRequest
{
    /// <summary>
    /// 产品名称
    /// </summary>
    public string? Name { get; set; }
}

sealed class ListProductValidator : Validator<ListProductRequest>
{
    public ListProductValidator()
    {
        // RuleFor(x => x.Property).NotEmpty();
    }
}

sealed class ListProductSummary : Summary<ListProductEndpoint, ListProductRequest>
{
    public ListProductSummary()
    {
        Summary = "商品查询";
        Description = "";
    }
}