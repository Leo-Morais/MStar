import axios from 'axios';

const API_URL = 'https://localhost:7116/api/v1/Movimentação';

const movimentacaoService = {
  // Adicionar nova movimentação
  add: async (movimentacaoDTO) => {
    try {
      const response = await axios.post(`${API_URL}/add`, movimentacaoDTO);
      return response.data;
    } catch (error) {
      console.error("Erro ao adicionar movimentação", error);
      throw error;
    }
  },

  // Deletar movimentação pelo ID
  delete: async (id) => {
    try {
      await axios.delete(`${API_URL}/delete/${id}`);
    } catch (error) {
      console.error(`Erro ao deletar movimentação com ID: ${id}`, error);
      throw error;
    }
  },

  // Buscar todas as movimentações
  getAll: async () => {
    try {
      const response = await axios.get(`${API_URL}/getAll`);
      return response.data;
    } catch (error) {
      console.error("Erro ao buscar movimentações", error);
      throw error;
    }
  },

  // Buscar movimentação pelo ID
  getById: async (id) => {
    try {
      const response = await axios.get(`${API_URL}/getById/${id}`);
      return response.data;
    } catch (error) {
      console.error(`Erro ao buscar movimentação com ID: ${id}`, error);
      throw error;
    }
  },

  // Atualizar movimentação
  update: async (id, movimentacaoDTO) => {
    try {
      const response = await axios.put(`${API_URL}/update/${id}`, movimentacaoDTO);
      return response.data;
    } catch (error) {
      console.error(`Erro ao atualizar movimentação com ID: ${id}`, error);
      throw error;
    }
  }
};

export default movimentacaoService;
