import { api } from "@/boot/axios";

const FILE = {
  uploadFile: (formData, headers) =>
    api
      .post("file/upload", formData, headers)
      .then((response) => response.data),
  //   getMyNotes: (l = 10, p = 1) =>
  //     api.get(`/user/note?limit=${l}&page=${p}`).then((r) => r.data),
  //   createNote: (p) => api.post("/note", { content: p }).then((r) => r.data),
  //   updateNote: (id, p) =>
  //     api.patch(`/note/${id}`, { content: p }).then((r) => r.data),
  //   deleteNote: (id, hard = false) =>
  //     api.delete(`/note/${id}?hard=${hard}`).then((r) => r.data),
  //   recoverNote: (id) => api.patch(`/note/${id}/recover`).then((r) => r.data),
};

export { FILE };
