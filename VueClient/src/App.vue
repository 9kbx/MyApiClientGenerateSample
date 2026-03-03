<script setup lang="ts">
import { ref, reactive , onMounted} from 'vue';
import { api } from './api';
import { ProductApi } from './api/v1';

// 1. 定义表单数据模型
const createProductForm = reactive({
  name: 'iphone 17 pro max',
  code: 'ip17promax',
});

const loading = ref(false);
const result = ref<any>(null);
const helloResult = ref<any>(null);

onMounted(async () => {
  try {
    const { data } = await api.v1.hello.hello({request:{
      firstName: "John",
      lastName: "Doe111"
    }});
    helloResult.value = data;
    console.log("SayHello 成功！", data);
  } catch (err: any) {
    console.log("SayHello 失败！", err);
  }

  const proApi = new ProductApi();
  proApi.listProduct().then(res => {
    console.log("ListProducts 成功！", res.data);
  }).catch(err => {
    console.log("ListProducts 失败！", err);
  });

  // query product v1
  api.v1.product.getProduct({
    productId:1111
  }).then(res => {
    console.log("getProductV1 成功！", res.data);
  }).catch(err => {
    console.log("getProductV1 失败！", err);
  });

  // query product v2
  api.v2.product.getProductV2({
    productId:2222
  }).then(res => {
    console.log("getProductV2 成功！", res.data);
  }).catch(err => {
    console.log("getProductV2 失败！", err);
  });
});

// 创建商品
const onSubmit = async () => {
  if (!createProductForm.name || !createProductForm.code) {
    alert("请输入产品名称和编码");
    return;
  }

  loading.value = true;
  result.value = null;

  try {
    // 调用生成的 ProductApi
    const { data } = await api.v1.product.createProduct({
      createProductRequest: {
        name: createProductForm.name,
        code: createProductForm.code
      }
    });

    result.value = data;
    console.log("商品创建成功！", data);
    // alert("商品创建成功！");
    
    // 这里可以执行跳转逻辑，例如：router.push('/dashboard');
  } catch (err: any) {
    // 错误处理已经在 axios 拦截器里通过 alert 或 console 输出了
    // 这里的 catch 主要是为了停止加载状态
    console.log("组件捕获到错误，已停止提交", err);
  } finally {
    loading.value = false;
  }
};
</script>

<template>
<div class="form-container">
  <h2>SayHello</h2>
  <div v-if="helloResult" class="success-box">
      <h3>SayHello 成功返回数据：</h3>
      <pre>{{ JSON.stringify(helloResult, null, 2) }}</pre>
    </div>
</div>

  <div class="form-container">
    <h2>创建商品</h2>
    
    <div class="form-item">
      <label>商品名称：</label>
      <input 
        v-model="createProductForm.name" 
        type="text" 
        placeholder="请输入商品名称"
        :disabled="loading"
      />
    </div>

    <div class="form-item">
      <label>商品编码：</label>
      <input 
        v-model="createProductForm.code" 
        type="text" 
        placeholder="请输入商品编码"
        :disabled="loading"
        @keyup.enter="onSubmit"
      />
    </div>
 
    <button :disabled="loading" @click="onSubmit">
      {{ loading ? '创建中...' : '立即创建' }}
    </button>

    <hr />

    <div v-if="result" class="success-box">
      <h3>创建成功返回数据：</h3>
      <pre>{{ JSON.stringify(result, null, 2) }}</pre>
    </div>
  </div>
</template>

<style scoped>
.form-container {
  max-width: 400px;
  margin: 50px;
  padding: 20px;
  border: 1px solid #ccc;
  border-radius: 8px;
  float: left;
}
.form-item {
  margin-bottom: 15px;
}
.form-item label {
  display: block;
  margin-bottom: 5px;
}
input {
  width: 100%;
  padding: 8px;
  box-sizing: border-box;
}
button {
  width: 100%;
  padding: 10px;
  background-color: #409eff;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}
button:disabled {
  background-color: #a0cfff;
}
.success-box {
  margin-top: 20px;
  background: #f0f9eb;
  padding: 10px;
  border: 1px solid #c2e7b0;
  color:#409eff;
  text-align: left;
}
</style>