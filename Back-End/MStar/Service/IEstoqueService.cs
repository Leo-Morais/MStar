using MStar.DTO;
using MStar.Model;

namespace MStar.Service
{
    public interface IEstoqueService
    {
        Task<Estoque> Add(int id, EstoqueDTO estoqueDTO);
        Task<Estoque> Update(int id, EstoqueDTO estoqueDTO);

        Task Delete(int id);
        Task<Estoque> GetById(int id);
        Task<List<Estoque>> GetAll();
    }
}
