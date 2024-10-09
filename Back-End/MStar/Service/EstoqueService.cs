using Microsoft.EntityFrameworkCore;
using MStar.CustomException;
using MStar.DTO;
using MStar.Model;
using MStar.Repository;

namespace MStar.Service
{
    public class EstoqueService : IEstoqueService
    {
        private readonly ConnectionContext _context;
        private readonly IMercadoriaService _mercadoriaService;

        public EstoqueService(ConnectionContext context, IMercadoriaService mercadoriaService)
        {
            _context = context;
            _mercadoriaService = mercadoriaService;
        }

        public async Task<Estoque> Add(EstoqueDTO estoqueDTO)
        {
            var mercadoriaEncontrada = await _mercadoriaService.GetById(estoqueDTO.IdMercadoria);
            var estoque = new Estoque()
            {
                Quantidade = estoqueDTO.Quantidade,
                DataAtualizacao = DateTime.Now,
                IdMercadoria = mercadoriaEncontrada.Id
            };
            await _context.Estoque.AddAsync(estoque);
            await _context.SaveChangesAsync();
            return estoque;
        }

        public async Task Delete(int id)
        {
            var estoqueEcontrado = await _context.Estoque.FindAsync(id);
            if (estoqueEcontrado == null)
            {
                throw new IdNotFoundException($"Estoque com Id: {id} não encontrado.");
            }
            _context.Estoque.Remove(estoqueEcontrado);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Estoque>> GetAll()
        {
            return await _context.Estoque.ToListAsync();
        }

        public async Task<Estoque> GetById(int id)
        {
            var estoqueEcontrado = await _context.Estoque.FindAsync(id);
            if (estoqueEcontrado == null)
            {
                throw new IdNotFoundException($"Mercadoria com Id: {id} não encontrado.");
            }
            return estoqueEcontrado;
        }

        public async Task<Estoque> Update(int id, EstoqueDTO estoqueDTO)
        {
            if (id <= 0)
            {
                throw new IdNotFoundException("Id inválido");
            }
            if (estoqueDTO == null)
            {
                throw new WrongPropertyException("Existe campo nulo.");
            }

            var estoqueEcontrado = await _context.Estoque.FindAsync(id);

            if (estoqueEcontrado == null)
            {
                throw new IdNotFoundException($"Estoque com o Id:{id} não encontrada.");
            }

            estoqueEcontrado.Quantidade = estoqueDTO.Quantidade;
            estoqueEcontrado.DataAtualizacao = DateTime.Now;

            if (estoqueDTO.IdMercadoria > 0)
            {
                estoqueEcontrado.IdMercadoria = estoqueDTO.IdMercadoria;
            }

            await _context.SaveChangesAsync();
            return estoqueEcontrado;
        }
    }
}
