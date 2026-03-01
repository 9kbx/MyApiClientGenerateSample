namespace MyApiClientGenerateSample.Endpoints.User;

/// <summary>
/// 用户相关端点的分组配置。
/// </summary>
public sealed class UserEndpointGroup : Group
{
    /// <summary>
    /// 初始化用户端点组的新实例。
    /// </summary>
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
