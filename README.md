# MyApiClientGenerateSample
通过OpenAPI Generator生成typescript api客户端示例，后端fast-endpoint，前端vue3 axios

## 安装 OpenAPI Generator

在 macOS 安装 OpenAPI Generator CLI
```shell
brew install openapi-generator
```

其它环境参考官方文档：https://openapi-generator.tech/docs/installation

## WebApi设置

Program.cs
```csharp
bld.Services
   .SwaggerDocument(o =>
   {
       // 默认情况下，使用 DTO 类的完整名称（包括命名空间）来生成 swagger 模式名称。
       // 您可以通过以下方式将其更改为仅使用类名称：
       // 注意：同名DTO类会以数字结尾命名，比如: ProductDto 和 ProductDto2
       o.ShortSchemaNames = true;
   });

app.UseFastEndpoints(c =>
   {
       ...
       
       // https://fast-endpoints.com/docs/swagger-support#override-endpoint-name-generation 
       // 如果您想修改默认的端点名称生成逻辑，可以指定如下函数，该函数会为每个端点返回一个唯一的字符串。
       // 在函数中会传递一个 EndpointNameGenerationContext，其中包含所有可用的名称生成信息。
       c.Endpoints.NameGenerator = ctx =>
       {
           var name = ctx.EndpointType.Name;
           var suffix = "Endpoint";
           return name.EndsWith(suffix)
                      ? name.AsSpan(0, name.Length - suffix.Length).ToString()
                      : name;
       };
   });
```

修改默认端点名称生成逻辑
- 好处：生成简洁的Api客户端代码（不修改情况api调用方法名以命名空间和类名命名导致方法特别长）
- 坏处：需要关注类名称（或OperationId是否存在重名）

或者在 Endpoint.cs 手动修改

```csharp
public override void Configure()
{
    Post("user/create");
    AllowAnonymous();
    
    // 则默认按命名空间+类名对operationId命名
    // "operationId": "MyApiClientGenerateSampleEndpointsUserCreateUserEndpoint",
    // 生成Api客户端调用方法也会这么长，所以建议自定义operationId
    // 注意：operationId重名将导致openapi文档生成失败
    // 建议：通过全局变量进行定义
    Description(x => x.WithName("CreateUser"));
}
````

## 生成 Typescript 客户端代码

```shell
openapi-generator generate \
  -i http://localhost:5000/swagger/v1/swagger.json \
  -g typescript-axios \
  -o ./VueClient/src/api/generated \
  --additional-properties=useSingleRequestParameter=true,withInterfaces=true
```
参数说明：
- -i 指定openapi文档URL或文件路径
- -g 生成器类型：typescript-axios、csharp
- -o 代码输出位置
- --additional-properties
  - 此参数用于传递生成器的具体配置选项（附加属性）
    - useSingleRequestParameter=true
      - 这是一个非常重要的配置。如果 API 接口有多个参数，它会将这些参数封装成一个单独的请求对象（Request Object）传入函数
      - 这使得调用函数时参数顺序不重要，且更方便维护代码
    - withInterfaces=true
      - 该属性确保生成代码时包含 TypeScript 的接口定义 (Interfaces)
      - 这提供了更好的类型安全和 IDE 的自动补全功能

## Vue3客户端使用

### 自定义api实例

src/api/index.ts

```typescript
import axios from "axios";
import { ProductApi, HelloApi, Configuration } from "./generated";

// 1. 创建 Axios 实例
const axiosInstance = axios.create({
    baseURL: "http://localhost:5000",
    timeout: 5000,
});

// 2. 配置
const config = new Configuration();

// 3. 导出实例
export const productApi = new ProductApi(config, config.basePath, axiosInstance);
export const helloApi = new HelloApi(config, config.basePath, axiosInstance);
```

App.vue

```vue
<script setup lang="ts">
import { ref, reactive , onMounted} from 'vue';
import { productApi } from './api';

// 1. 定义表单数据模型
const createProductForm = reactive({
  name: 'iphone 17 pro max',
  code: 'ip17promax',
});

// 调用生成的 ProductApi
const { data } = await productApi.createProduct({
  createProductRequest: {
    name: createProductForm.name,
    code: createProductForm.code
  }
});
</script>
```

### 或直接使用

```typescript
import { ProductApi } from './api/generated';

const productApi = new ProductApi();

productApi.createProduct({
        createProductRequest: {
            name: createProductForm.name,
            code: createProductForm.code
        }
    })
    .then(res => {
        console.log("ListProducts 成功！", res.data);
    }).catch(err => {
        console.log("ListProducts 失败！", err);
    });
```

