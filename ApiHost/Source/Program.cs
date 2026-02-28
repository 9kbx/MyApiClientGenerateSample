var bld = WebApplication.CreateBuilder(args);
bld.Services
   .AddAuthenticationJwtBearer(s => s.SigningKey = bld.Configuration["Auth:JwtKey"])
   .AddAuthorization()
   .AddFastEndpoints(o => o.SourceGeneratorDiscoveredTypes = DiscoveredTypes.All)
   .SwaggerDocument(o =>
   {
       o.EnableJWTBearerAuth = false; // 禁用身份认证才能访问SwaggerUI

       // 默认情况下，使用 DTO 类的完整名称（包括命名空间）来生成 swagger 模式名称。您可以通过以下方式将其更改为仅使用类名称：
       o.ShortSchemaNames = true;
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
   })
   .UseSwaggerGen();
app.Run();