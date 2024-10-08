using Microsoft.EntityFrameworkCore;
using MStar.CustomException;
using MStar.DTO;
using MStar.Model;
using MStar.Repository;
using System.Security.Principal;

namespace MStar.Service
{
    public class MovimentacaoService : IMovimentacaoService
    {
        private readonly ConnectionContext _context;
        private readonly IMercadoriaService _mercadoriaService;

        public MovimentacaoService(ConnectionContext context, IMercadoriaService mercadoriaService)
        {
            _context = context;
            _mercadoriaService = mercadoriaService;
        }

        public async Task<Movimentacao> Add(MovimentacaoDTO movimentacaoDTO)
        {
            var mercadoria = await _mercadoriaService.GetById(movimentacaoDTO.IdMercadoria);
            var movimentacao = new Movimentacao()
            {
                IdMercadoria = movimentacaoDTO.IdMercadoria,
                DataCriacao = DateTime.Now,
                LocalMovimentacao = movimentacaoDTO.LocalMovimentacao,
                Quantidade = movimentacaoDTO.Quantidade,
                TipoMovimentacao = movimentacaoDTO.TipoMovimentacao.ToUpper(),
            };
            await _context.Movimentacao.AddAsync(movimentacao);
            await _context.SaveChangesAsync();
            return movimentacao;

        }

        public async Task Delete(int id)
        {
            var movimentacaoEncontrada = await _context.Movimentacao.FindAsync(id);
            if (movimentacaoEncontrada == null)
            {
                throw new IdNotFoundException($"Movimentação com Id: {id} não encontrado.");
            }
            _context.Movimentacao.Remove(movimentacaoEncontrada);
            _context.SaveChanges();
        }

        public async Task<List<Movimentacao>> GetAll()
        {
            return await _context.Movimentacao.ToListAsync();
        }

        public async Task<Movimentacao> GetById(int id)
        {
            var movimentacaoEncontrada = await _context.Movimentacao.FindAsync(id);
            if (movimentacaoEncontrada == null)
            {
                throw new IdNotFoundException($"Movimentação com Id: {id} não encontrado.");
            }
            return movimentacaoEncontrada;
        }

        public async Task<Movimentacao> Update(int id, MovimentacaoDTO movimentacaoDTO)
        {
            if (id <= 0)
            {
                throw new IdNotFoundException("Id inválido");
            }

            if (movimentacaoDTO == null)
            {
                throw new WrongPropertyException("Existe campo nulo.");
            }

            var movimentacaoEncontrada = await _context.Movimentacao.FindAsync(id);
            if (movimentacaoEncontrada == null)
            {
                throw new IdNotFoundException($"Movimentação com o Id:{id} não encontrada.");
            }

            // Atualizando os campos da movimentação
            movimentacaoEncontrada.TipoMovimentacao = movimentacaoDTO.TipoMovimentacao;
            movimentacaoEncontrada.LocalMovimentacao = movimentacaoDTO.LocalMovimentacao;
            movimentacaoEncontrada.Quantidade = movimentacaoDTO.Quantidade;

            // Verificando se a mercadoria existe antes de atualizar a FK
            var mercadoria = await _mercadoriaService.GetById(movimentacaoDTO.IdMercadoria);
            if (mercadoria == null)
            {
                throw new IdNotFoundException($"Mercadoria com o Id: {movimentacaoDTO.IdMercadoria} não encontrada.");
            }

            // Atualizando a FK
            movimentacaoEncontrada.IdMercadoria = movimentacaoDTO.IdMercadoria;

            await _context.SaveChangesAsync();
            return movimentacaoEncontrada;
        }
    }
}
