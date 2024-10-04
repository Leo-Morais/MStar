using MStar.DTO;
using MStar.Model;

namespace MStar.Service
{
    public interface ITipoMercadoriaService
    {
        Task<TipoMercadoria> Add(TipoMercadoriaDTO tipoMercadoriaDTO);
        Task<TipoMercadoria> Update(int id, TipoMercadoriaDTO tipoMercadoriaDTO);

        Task Delete(int id);
        Task<TipoMercadoria> GetById(int id);
        Task<List<TipoMercadoria>> GetAll();
    }
}
