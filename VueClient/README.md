# Vue 3 + TypeScript + Vite

This template should help get you started developing with Vue 3 and TypeScript in Vite. The template uses Vue 3 `<script setup>` SFCs, check out the [script setup docs](https://v3.vuejs.org/api/sfc-script-setup.html#sfc-script-setup) to learn more.

Learn more about the recommended Project Setup and IDE Support in the [Vue Docs TypeScript Guide](https://vuejs.org/guide/typescript/overview.html#project-setup).

创建项目

```shell
# 创建项目（选 Vue, TypeScript 即可）
npm create vite@latest api-test -- --template vue-ts
cd api-test

# 安装必要的运行时依赖（必须安装，否则生成的代码会报错）
npm install axios
```

手动生成api客户端代码

```shell
openapi-generator generate \
  -i http://localhost:5000/swagger/v1/swagger.json \
  -g typescript-axios \
  -o ./src/api/v1 \
  --additional-properties=useSingleRequestParameter=true,withInterfaces=true


openapi-generator generate \
  -i http://localhost:5000/swagger/v2/swagger.json \
  -g typescript-axios \
  -o ./src/api/v2 \
  --additional-properties=useSingleRequestParameter=true,withInterfaces=true

```

为了方便维护，建议在 `package.json` 中配置如下脚本，每次后端更新 Swagger 后跑一下：

```json
"scripts": {
  "gen:api": "npm run gen:v1 && npm run gen:v2",
  "gen:v1": "openapi-generator generate -i http://localhost:5000/swagger/v1/swagger.json -g typescript-axios -o ./src/api/v1 --additional-properties=useSingleRequestParameter=true,withInterfaces=true",
  "gen:v2": "openapi-generator generate -i http://localhost:5000/swagger/v2/swagger.json -g typescript-axios -o ./src/api/v2 --additional-properties=useSingleRequestParameter=true,withInterfaces=true"
}
```

> 注意，这里使用 `openapi-generator` 是因为我的系统是 macOS 通过 brew 安装的CLI，其它系统可能是 `openapi-generator-cli`

然后执行以下命令更新 API 客户端

```shell
npm run gen:api
```