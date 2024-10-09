import axios from 'axios';

const API_URL = 'https://localhost:7116/api/v1/Estoque';

const estoqueService = {
  // Adicionar novo estoque
  add: async (estoqueDTO) => {
    try {
      const response = await axios.post(API_URL, estoqueDTO); // A URL está correta
      return response.data; // Retorna o estoque adicionado
    } catch (error) {
      console.error("Erro ao adicionar estoque", error);
      throw error;
    }
  },

  // Deletar estoque pelo ID
  delete: async (id) => {
    try {
      await axios.delete(`${API_URL}/${id}`); // A URL está correta
    } catch (error) {
      console.error(`Erro ao deletar estoque com ID: ${id}`, error);
      throw error;
    }
  },

  // Buscar todos os estoques
  getAll: async () => {
    try {
      const response = await axios.get(API_URL); // A URL está correta
      return response.data; // Retorna a lista de estoques
    } catch (error) {
      console.error("Erro ao buscar estoques", error);
      throw error;
    }
  },

  // Buscar estoque pelo ID
  getById: async (id) => {
    try {
      const response = await axios.get(`${API_URL}/${id}`); // A URL está correta
      return response.data; // Retorna o estoque encontrado
    } catch (error) {
      console.error(`Erro ao buscar estoque com ID: ${id}`, error);
      throw error;
    }
  },

  // Atualizar estoque
  update: async (id, estoqueDTO) => {
    try {
      const response = await axios.put(`${API_URL}/${id}`, estoqueDTO); // A URL está correta
      return response.data; // Retorna o estoque atualizado
    } catch (error) {
      console.error(`Erro ao atualizar estoque com ID: ${id}`, error);
      throw error;
    }
  }
};

export default estoqueService;
