<template>
  <form @submit.prevent="handleSubmit">
    <input type="file" @change="handleFileChange" ref="fileInput" />
    <button type="submit" :disabled="status.loading">
      {{ status.loading ? "Loading..." : "Submit" }}
    </button>
  </form>
</template>
<script>
import { FILE } from "../../services/FileService";
export default {
  data() {
    return {
      file: null,
      status: {
        loading: false,
      },
    };
  },
  methods: {
    handleFileChange(event) {
      this.file = event.target.files[0];
    },
    async handleSubmit() {
      this.status.loading = true;
      try {
        const formData = new FormData();
        formData.append("file", this.file);
        debugger;
        const { data } = await FILE.uploadFile(formData, {
          "Content-Type": "multipart/form-data",
        });
        console.log(data);
      } catch (error) {
        console.error(error);
      } finally {
        this.status.loading = false;
      }
    },
  },
};
</script>
