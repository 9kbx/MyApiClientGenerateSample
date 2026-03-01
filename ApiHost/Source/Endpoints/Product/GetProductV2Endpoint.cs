using FastEndpoints.AspVersioning;

namespace MyApiClientGenerateSample.Endpoints.Product;

/// <summary>
/// 获取产品详情的 V2 版本端点。
/// </summary>
public class GetProductV2Endpoint : Endpoint<GetProductRequest, ProductDto>
{
    /// <summary>
    /// 配置端点设置。
    /// </summary>
    public override void Configure()
    {
        Get("{ProductId}");
        Group<ProductEndpointGroup>();
        Version(2); // 指定版本为 2
        AllowAnonymous();

        // Options(x => x
        //              .WithVersionSet(">>Product<<")
        //              .MapToApiVersion(2.0));
    }

    /// <summary>
    /// 处理获取产品请求。
    /// </summary>
    /// <param name="r">包含产品ID的获取产品请求。</param>
    /// <param name="c">取消令牌。</param>
    public override async Task HandleAsync(GetProductRequest r, CancellationToken c)
    {
        // V2 版本返回不同的数据或逻辑
        await Send.OkAsync(new(r.ProductId, "iPhone20 Pro Max", "ip20pm_v2"), c);
    }
}