import axios from 'axios';

const API_URL = 'https://localhost:7116/api/v1/TipoMercadoria';

const tipoMercadoriaService = {
  // Adicionar novo tipo de mercadoria
  add: async (tipoMercadoriaDTO) => {
    try {
      const response = await axios.post(`${API_URL}/add`, tipoMercadoriaDTO);
      return response.data;
    } catch (error) {
      console.error("Erro ao adicionar tipo de mercadoria", error);
      throw error;
    }
  },

  // Deletar tipo de mercadoria pelo ID
  delete: async (id) => {
    try {
      await axios.delete(`${API_URL}/delete/${id}`);
    } catch (error) {
      console.error(`Erro ao deletar tipo de mercadoria com ID: ${id}`, error);
      throw error;
    }
  },

  // Buscar todos os tipos de mercadoria
  getAll: async () => {
    try {
      const response = await axios.get(`${API_URL}/getAll`);
      return response.data;
    } catch (error) {
      console.error("Erro ao buscar tipos de mercadoria", error);
      throw error;
    }
  },

  // Buscar tipo de mercadoria pelo ID
  getById: async (id) => {
    try {
      const response = await axios.get(`${API_URL}/getById/${id}`);
      return response.data;
    } catch (error) {
      console.error(`Erro ao buscar tipo de mercadoria com ID: ${id}`, error);
      throw error;
    }
  },

  // Atualizar tipo de mercadoria
  update: async (id, tipoMercadoriaDTO) => {
    try {
      const response = await axios.put(`${API_URL}/update/${id}`, tipoMercadoriaDTO);
      return response.data;
    } catch (error) {
      console.error(`Erro ao atualizar tipo de mercadoria com ID: ${id}`, error);
      throw error;
    }
  }
};

export default tipoMercadoriaService;
