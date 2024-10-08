import axios from 'axios';

const API_URL = 'https://localhost:7116/api/v1/Mercadoria';

const mercadoriaService = {
  // Adicionar nova mercadoria
  add: async (mercadoriaDTO) => {
    try {
      const response = await axios.post(`${API_URL}/add`, mercadoriaDTO);
      return response.data;
    } catch (error) {
      console.error("Erro ao adicionar mercadoria", error);
      throw error;
    }
  },

  // Deletar mercadoria pelo ID
  delete: async (id) => {
    try {
      await axios.delete(`${API_URL}/delete/${id}`);
    } catch (error) {
      console.error(`Erro ao deletar mercadoria com ID: ${id}`, error);
      throw error;
    }
  },

  // Buscar todas as mercadorias
  async getAll() {
    const response = await fetch("https://localhost:7116/api/v1/Mercadoria");
    if (!response.ok) {
        throw new Error("Erro ao buscar mercadorias");
    }
    return await response.json();
},

  // Buscar mercadoria pelo ID
  getById: async (id) => {
    try {
      const response = await axios.get(`${API_URL}/getById/${id}`);
      return response.data;
    } catch (error) {
      console.error(`Erro ao buscar mercadoria com ID: ${id}`, error);
      throw error;
    }
  },

  // Atualizar mercadoria
  update: async (id, mercadoriaDTO) => {
    try {
      const response = await axios.put(`${API_URL}/update/${id}`, mercadoriaDTO);
      return response.data;
    } catch (error) {
      console.error(`Erro ao atualizar mercadoria com ID: ${id}`, error);
      throw error;
    }
  }
};

export default mercadoriaService;
