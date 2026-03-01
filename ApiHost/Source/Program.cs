using Asp.Versioning;
using Asp.Versioning.Conventions;
using FastEndpoints.AspVersioning;
using FastEndpoints.ClientGen.Kiota;
using Kiota.Builder;

var bld = WebApplication.CreateBuilder(args);
bld.Services
   .AddAuthenticationJwtBearer(s => s.SigningKey = bld.Configuration["Auth:JwtKey"])
   .AddAuthorization()
   .AddFastEndpoints(o =>
   {
       o.SourceGeneratorDiscoveredTypes = DiscoveredTypes.All;
   })
   // .AddVersioning(o =>
   // {
   //     o.DefaultApiVersion = new(1.0);
   //     o.AssumeDefaultVersionWhenUnspecified = true;
   //     o.ApiVersionReader = new UrlSegmentApiVersionReader();
   // })
   .SwaggerDocument(o =>
   {
       o.EnableJWTBearerAuth = false;
       o.ShortSchemaNames = true;
       o.DocumentSettings = s =>
       {
           s.DocumentName = "v1";
           s.Title = "My API";
           s.Version = "v1.0";
           // s.ApiVersion(new(1.0));
       };
       o.MaxEndpointVersion = 1;
   })
   .SwaggerDocument(o =>
   {
       o.EnableJWTBearerAuth = false;
       o.ShortSchemaNames = true;
       o.DocumentSettings = s =>
       {
           s.DocumentName = "v2";
           s.Title = "My API";
           s.Version = "v2.0";
           // s.ApiVersion(new(2.0));
       };

       // o.AutoTagPathSegmentIndex = 0;

       o.MinEndpointVersion = 2; 
       o.MaxEndpointVersion = 2;
   });

// VersionSets.CreateApi(
//     ">>Product<<",
//     v => v
//          .HasApiVersion(1.0)
//          .HasApiVersion(2.0));
//
// VersionSets.CreateApi(
//     ">>Inventory<<",
//     v =>
//     {
//         v.HasApiVersion(1.0);
//         v.HasApiVersion(1.1);
//     });

bld.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod()
              .WithExposedHeaders("Content-Disposition");
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

       c.Versioning.DefaultVersion = 1; // 默认版本号
       c.Versioning.Prefix = "v";
       c.Versioning.PrependToRoute = true;
       c.Endpoints.RoutePrefix = "api";

       c.Endpoints.NameGenerator = ctx =>
       {
           var name = ctx.EndpointType.Name;
           var suffix = "Endpoint";

           return name.EndsWith(suffix)
                      ? name.AsSpan(0, name.Length - suffix.Length).ToString()
                      : name;
       };
   })
   .UseSwaggerGen(uiConfig: u => u.ShowOperationIDs());

// 创建客户端代码生成端点
app.MapApiClientEndpoint(
    "/cs-client",
    c =>
    {
        c.SwaggerDocumentName = "all"; // 使用包含所有版本的文档
        c.Language = GenerationLanguage.CSharp;
        c.ClientNamespaceName = "MyCompanyName";
        c.ClientClassName = "MyCsClient";
    },
    o =>
    {
        o.CacheOutput(p => p.Expire(TimeSpan.FromSeconds(1)));
        o.ExcludeFromDescription();
    });

// 实现命令行生成客户端代码
// dotnet run --generateclients true
await app.GenerateApiClientsAndExitAsync(
    c =>
    {
        c.SwaggerDocumentName = "all"; // 使用包含所有版本的文档
        c.Language = GenerationLanguage.CSharp;
        c.OutputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApiClients", "CSharp");
        c.ClientNamespaceName = "MyCompanyName";
        c.ClientClassName = "MyCsClient";
        c.CreateZipArchive = true;
    },
    c =>
    {
        c.SwaggerDocumentName = "all"; // 使用包含所有版本的文档
        c.Language = GenerationLanguage.TypeScript;
        c.OutputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ApiClients", "Typescript");
        c.ClientNamespaceName = "MyCompanyName";
        c.ClientClassName = "MyTsClient";
    });

app.Run();