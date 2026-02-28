using FastEndpoints.ClientGen.Kiota;
using Kiota.Builder;

var bld = WebApplication.CreateBuilder(args);
bld.Services
   .AddAuthenticationJwtBearer(s => s.SigningKey = bld.Configuration["Auth:JwtKey"])
   .AddAuthorization()
   .AddFastEndpoints(o => o.SourceGeneratorDiscoveredTypes = DiscoveredTypes.All)
   .SwaggerDocument(o =>
   {
       o.EnableJWTBearerAuth = false; // 禁用身份认证才能访问SwaggerUI

       // 默认情况下，使用 DTO 类的完整名称（包括命名空间）来生成 swagger 模式名称。
       // 您可以通过以下方式将其更改为仅使用类名称：
       // 注意：同名DTO类会以数字结尾命名，比如: ProductDto 和 ProductDto2
       o.ShortSchemaNames = true;

       o.DocumentSettings = s =>
       {
           s.DocumentName = "v1"; //must match what's being passed in to the map method below
       };
   });

bld.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod()
              .WithExposedHeaders("Content-Disposition"); // 暴露 Content-Disposition 头部以支持前端下载文件时获取服务端提供的文件名
    });
});

var app = bld.Build();
app.UseAuthentication()
   .UseAuthorization()
   .UseCors()
   .UseFastEndpoints(c =>
   {
       c.Binding.ReflectionCache.AddFromMyApiClientGenerateSample();
       c.Errors.UseProblemDetails();

       c.Endpoints.RoutePrefix = "api"; // 自定义路由前缀

       // https://fast-endpoints.com/docs/swagger-support#override-endpoint-name-generation 
       // 如果您想修改默认的端点名称生成逻辑，可以指定如下函数，该函数会为每个端点返回一个唯一的字符串。
       // 在函数中会传递一个 EndpointNameGenerationContext，其中包含所有可用的名称生成信息。
       // c.Endpoints.NameGenerator =
       //     ctx => ctx.EndpointType.Name.TrimEnd("Endpoint").ToString();
       c.Endpoints.NameGenerator = ctx =>
       {
           var name = ctx.EndpointType.Name;
           var suffix = "Endpoint";

           return name.EndsWith(suffix)
                      ? name.AsSpan(0, name.Length - suffix.Length).ToString()
                      : name;
       };
   })
   .UseSwaggerGen(uiConfig: u => u.ShowOperationIDs()); // 在 Swagger UI 中显示OperationID

// 创建客户端代码生成端点（访问此端点可下载打包好的客户带代码）
app.MapApiClientEndpoint(
    "/cs-client",
    c =>
    {
        c.SwaggerDocumentName = "v1"; //must match document name set above
        c.Language = GenerationLanguage.CSharp;
        c.ClientNamespaceName = "MyCompanyName";
        c.ClientClassName = "MyCsClient";
    },
    o => //endpoint customization settings
    {
        o.CacheOutput(p => p.Expire(TimeSpan.FromSeconds(1))); //cache the zip
        o.ExcludeFromDescription();                            //hides this endpoint from swagger docs
    });

// 实现命令行生成客户端代码
// dotnet run --generateclients true
await app.GenerateApiClientsAndExitAsync(
    c =>
    {
        c.SwaggerDocumentName = "v1"; //must match doc name above
        c.Language = GenerationLanguage.CSharp;

        // c.OutputPath = Path.Combine(app.Environment.WebRootPath, "ApiClients", "CSharp");
        c.OutputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApiClients", "CSharp");
        c.ClientNamespaceName = "MyCompanyName";
        c.ClientClassName = "MyCsClient";
        c.CreateZipArchive = true; //if you'd like a zip file as well
    },
    c =>
    {
        c.SwaggerDocumentName = "v1";
        c.Language = GenerationLanguage.TypeScript;
        c.OutputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApiClients", "Typescript");
        c.ClientNamespaceName = "MyCompanyName";
        c.ClientClassName = "MyTsClient";
    });

app.Run();