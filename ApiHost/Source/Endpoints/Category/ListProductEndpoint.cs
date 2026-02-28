namespace MyApiClientGenerateSample.Endpoints.Category;

sealed class ListProductEndpoint()
    : Endpoint<ListProductRequest, ResponseData<List<ProductDto>>>
{
    public override void Configure()
    {
        Get("category/list");
        Description(x =>
                x.AutoTagOverride("Category")
                 .WithName("CategoryProductList") // 自定义operation ID，生成api客户端时方法名此名称为准
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
        new(1, "product1", "p1", DateTimeOffset.UtcNow),
        new(2, "product2", "p2", DateTimeOffset.UtcNow),
        new(3, "product3", "p3", DateTimeOffset.UtcNow),
        new(4, "product4", "p4", DateTimeOffset.UtcNow),
        new(5, "product5", "p5", DateTimeOffset.UtcNow),
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