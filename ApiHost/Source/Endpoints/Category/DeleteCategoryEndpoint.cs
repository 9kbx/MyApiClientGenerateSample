namespace MyApiClientGenerateSample.Endpoints.Category;

/// <summary>
/// 删除 Category 的端点。
/// </summary>
public class DeleteCategoryEndpoint : Endpoint<DeleteCategoryRequest, DeleteCategoryResponse>
{
    /// <summary>
    /// 配置端点设置。
    /// </summary>
    public override void Configure()
    {
        Delete("{CategoryId}");
        Group<CategoryEndpointGroup>();
        AllowAnonymous();
    }

    /// <summary>
    /// 处理删除 Category 请求。
    /// </summary>
    /// <param name="req">包含 CategoryID 的删除 Category 请求。</param>
    /// <param name="ct">取消令牌。</param>
    public override async Task HandleAsync(DeleteCategoryRequest req, CancellationToken ct)
    {
        // 删除 Category 的逻辑将在这里
        await Send.OkAsync(new DeleteCategoryResponse($"Category {req.CategoryId} deleted successfully"), ct);
    }
}

/// <summary>
/// 删除 Category 的请求模型。
/// </summary>
public sealed class DeleteCategoryRequest
{
    /// <summary>
    /// 获取或设置要删除的 Category 的唯一标识符。
    /// </summary>
    [RouteParam]
    public string CategoryId { get; set; }
}

/// <summary>
/// 删除 Category 操作的响应模型。
/// </summary>
/// <param name="Message">结果消息。</param>
public sealed record DeleteCategoryResponse(string Message);
