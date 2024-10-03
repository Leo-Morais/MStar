using MStar.DTO;
using MStar.Model;

namespace MStar.Service
{
    public interface IMercadoriaService
    {
        Task<Mercadoria> Add(MercadoriaDTO mercadoriaDTO);
        Task<Mercadoria> Update(int id, MercadoriaDTO mercadoriaDTO);

        Task Delete(int id);
        Task <Mercadoria> GetById(int id);
        Task<List<Mercadoria>> GetAll();
    }
}
