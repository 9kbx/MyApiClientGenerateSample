using FluentValidation;

namespace MyApiClientGenerateSample.Endpoints.Product;

sealed class CreateProductEndpoint()
    : Endpoint<CreateProductRequest, ResponseData<ProductDto>>
{
    public override void Configure()
    {
        Post("create");
        Group<ProductEndpointGroup>();
        // Description(x => x.WithName("Create"));
        // Description(x =>
        //         x.AutoTagOverride("Product")
        //          .WithName("Create") // 自定义operation ID，生成api客户端时方法名此名称为准
        // );
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateProductRequest r, CancellationToken c)
    {
        var product = new ProductDto(Random.Shared.Next(100, 999), r.Name, r.Code);
        await Send.OkAsync(product.AsResponseData(), c);
    }
}

public sealed class CreateProductRequest
{
    /// <summary>
    /// 产品名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 产品编码
    /// </summary>
    public string Code { get; set; }
}

sealed class CreateProductValidator : Validator<CreateProductRequest>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Code).NotEmpty();
    }
}

sealed class CreateProductSummary : Summary<CreateProductEndpoint, CreateProductRequest>
{
    public CreateProductSummary()
    {
        Summary = "商品创建";
        Description = "";
    }
}