// src/api/index.ts
//import axios from "axios";
import axios, { type AxiosResponse, type AxiosError } from "axios";
// 1. 使用命名空间导入，彻底解决重名冲突
import * as ApiV1 from "./v1";
import * as ApiV2 from "./v2";

// 1. 创建 Axios 实例
const axiosInstance = axios.create({
    baseURL: "http://localhost:5000",
    timeout: 5000,
});

// 2. 添加响应拦截器
axiosInstance.interceptors.response.use(
    (response: AxiosResponse) => {
        // 如果后端 200 状态码但业务逻辑有自定义 code，可以在这里处理
        return response;
    },
    (error: AxiosError) => {
        // 重点：处理 400 等非 2xx 状态码
        if (error.response) {
            const data = error.response.data as any;

            // 根据你提供的后端返回结构提取错误信息
            const errorMessage = data?.message || "系统未知错误";

            // 这里你可以根据 UI 框架换成 ElMessage 或 message.error
            console.error("【API 错误回执】:", errorMessage);
            alert(`操作失败: ${errorMessage}`);

            // 如果有更详细的 errorData，可以循环遍历
            if (data.errorData && data.errorData.length > 0) {
                data.errorData.forEach((err: any) => {
                    console.warn(`字段 ${err.propertyName}: ${err.errorMessage}`);
                });
            }

            // 将整个后端错误对象抛出，以便组件内细化处理
            return Promise.reject(data);
        } else {
            console.error("网络异常或服务器无响应");
        }

        return Promise.reject(error);
    }
);

// 2. 分别创建各版本的配置实例
// 如果两个版本的 basePath 不同，可以在此处区分
const configV1 = new ApiV1.Configuration();
const configV2 = new ApiV2.Configuration();

// // 3. 导出实例
// export const productApi = new ProductApi(config, config.basePath, axiosInstance);
// export const helloApi = new HelloApi(config, config.basePath, axiosInstance);

// 3. 导出 API 实例
export const api = {
    // V1 命名空间
    v1: {
        product: new ApiV1.ProductApi(configV1, configV1.basePath, axiosInstance),
        hello: new ApiV1.HelloApi(configV1, configV1.basePath, axiosInstance),
        // 如果 V1 还有其他 API 类，继续在这里添加
    },

    // V2 命名空间
    v2: {
        product: new ApiV2.ProductApi(configV2, configV2.basePath, axiosInstance),
        // 假设 V2 新增了一个订单接口
        // order: new ApiV2.OrderApi(configV2, configV2.basePath, axiosInstance),
    }
};

// 导出类型定义（可选，方便在组件中使用类型）
export { ApiV1, ApiV2 };