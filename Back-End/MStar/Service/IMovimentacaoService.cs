using MStar.DTO;
using MStar.Model;

namespace MStar.Service
{
    public interface IMovimentacaoService
    {
        Task<Movimentacao> Add(MovimentacaoDTO movimentacaoDTO);
        Task<Movimentacao> Update(int id, MovimentacaoDTO movimentacaoDTO);

        Task Delete(int id);
        Task<Movimentacao> GetById(int id);
        Task<List<Movimentacao>> GetAll();
    }
}
