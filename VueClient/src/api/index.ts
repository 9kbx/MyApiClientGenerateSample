// src/api/index.ts
//import axios from "axios";
import axios, { type AxiosResponse, type AxiosError } from "axios";
import { ProductApi, HelloApi, Configuration } from "./generated";

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

// 2. 配置
const config = new Configuration();

// 3. 导出实例
export const productApi = new ProductApi(config, config.basePath, axiosInstance);
export const helloApi = new HelloApi(config, config.basePath, axiosInstance);