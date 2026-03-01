using System.Text.Json;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using MyCompanyName;
using MyCompanyName.Models;

Console.WriteLine("Hello, World!");

// API requires no authentication, so use the anonymous
// authentication provider
var authProvider = new AnonymousAuthenticationProvider();
// Create request adapter using the HttpClient-based implementation
var adapter = new HttpClientRequestAdapter(authProvider);
adapter.BaseUrl = "http://localhost:5000";
// Create the API client
var client = new MyCsClient(adapter);


// get 请求，路由参数 /api/user/{userId:string}
{
    Console.WriteLine("============= get 请求，路由参数 /api/user/{userId:string} ==================");

    var userId = "abc112233";

    var res = await client.Api.V1.User[userId].GetAsync();;
    Console.WriteLine(JsonSerializer.Serialize(res));
    Console.WriteLine();
}
// get 请求，路由参数 /api/user/{userId:guid}
{
    Console.WriteLine("============= get 请求，路由参数 /api/user/{userId:string} ==================");

    var userId = "abc112233";

    var res = await client.Api.V2.User[userId].GetAsync();;
    Console.WriteLine(JsonSerializer.Serialize(res));
    Console.WriteLine();
}

// get 请求，路由参数 /api/product/{productId:int}
{
    Console.WriteLine("============= get 请求，路由参数 /api/product/{productId:int} ==================");
    var productId = 123;
    var product = await client.Api.V2.Product[productId].GetAsync();
    Console.WriteLine(JsonSerializer.Serialize(product));
    Console.WriteLine();
}

// get 请求，多个参数 /api/product/list?name=xx&code=123
{
    Console.WriteLine("============= get 请求，多个参数 /api/product/list?name=xx&code=123 ==================");

    var likeProductName = "1";

    var res = await client.Api.V1.Product.List.GetAsync(c => { c.QueryParameters.Name = likeProductName; });

    Console.WriteLine(JsonSerializer.Serialize(res));
    Console.WriteLine();
}


// Post请求
{
    Console.WriteLine("============= Post请求 ==================");

    var request = new CreateProductRequest()
    {
        Name = "iphone 20",
        Code = "ip20"
    };

    var res = await client.Api.V1.Product.Create.PostAsync(request);
    Console.WriteLine(JsonSerializer.Serialize(res));
    Console.WriteLine();
}

// Post请求2
{
    Console.WriteLine("============= Post请求2 ==================");

    var productId = 112233;
    var request = new UpdateProductRequest()
    {
        Name = "iphone 200",
        Code = "ip200"
    };

    var res = await client.Api.V1.Product[productId].PostAsync(request);
    Console.WriteLine(JsonSerializer.Serialize(res));
    Console.WriteLine();
}

// Post请求3
{
    Console.WriteLine("============= Post请求3 ==================");

    var userId = "abc112233";
    var request = new UpdateUserRequest()
    {
        FirstName = "iphone 200",
        LastName = "ip200"
    };

    var res = await client.Api.V1.User.Update[userId].PostAsync(request);
    Console.WriteLine(JsonSerializer.Serialize(res));
    Console.WriteLine();
}

Console.WriteLine("Press any key to exit...");
Console.ReadKey();