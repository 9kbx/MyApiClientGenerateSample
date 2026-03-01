namespace MyApiClientGenerateSample.Endpoints.Category;

/// <summary>
/// Category 相关端点的分组配置。
/// </summary>
public sealed class CategoryEndpointGroup : Group
{
    /// <summary>
    /// 初始化 CategoryEndpointGroup 的新实例。
    /// </summary>
    public CategoryEndpointGroup()
    {
        Configure(
            "category",
            ep =>
            {
                ep.Description(x => x.AutoTagOverride("Category"));
            });
    }
}
