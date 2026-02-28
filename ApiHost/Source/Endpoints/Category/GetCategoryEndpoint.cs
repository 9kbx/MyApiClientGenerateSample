// namespace MyApiClientGenerateSample.Endpoints.Category;
//
// public class GetCategoryEndpoint : Endpoint<GetCategoryRequest, ResponseData<string>>
// {
//     public override void Configure()
//     {
//         Get("category/{CategoryId}");
//         Description(x => x.AutoTagOverride("Category"));
//         AllowAnonymous();
//     }
//
//     public override async Task HandleAsync(GetCategoryRequest r, CancellationToken c)
//     {
//         var categoryName = "hello";
//         await Send.OkAsync($"categoryId={r.CategoryId}, categoryName={categoryName}".AsResponseData(), c);
//     }
// }
//
// public sealed class GetCategoryRequest
// {
//     [RouteParam]
//     public CategoryId CategoryId { get; set; }
// }
//
// public record CategoryId : IInt64StronglyTypedId;