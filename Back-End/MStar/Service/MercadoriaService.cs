using Microsoft.EntityFrameworkCore;
using MStar.CustomException;
using MStar.DTO;
using MStar.Model;
using MStar.Repository;
using System.Security.Principal;

namespace MStar.Service
{
    public class MercadoriaService : IMercadoriaService
    {
        private readonly ConnectionContext _context;

        public MercadoriaService(ConnectionContext context)
        {
            _context = context;
        }


        public async Task<Mercadoria> Add(MercadoriaDTO mercadoriaDTO)
        {
            var mercadoria = new Mercadoria()
            {
                Nome = mercadoriaDTO.Nome,
                Descricao = mercadoriaDTO.Descricao,
                Fabricante = mercadoriaDTO.Fabricante,
                TipoMercadoria = mercadoriaDTO.TipoMercadoria,
                DataCriacao = DateTime.Now
            };
            await _context.Mercadoria.AddAsync(mercadoria);
            await _context.SaveChangesAsync();

            return mercadoria;
        }

        public async Task Delete(int id)
        {
            var mercadoria = await _context.Mercadoria.FindAsync(id);
            if (mercadoria == null)
            {
                throw new IdentityNotMappedException($"Mercadoria com Id: {id} não encontrado.");
            }
            _context.Mercadoria.Remove(mercadoria);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Mercadoria>> GetAll()
        {
            return await _context.Mercadoria.ToListAsync();
        }

        public async Task<Mercadoria> GetById(int id)
        {
            var mercadoriaEncontrada = await _context.Mercadoria.FindAsync(id);
            if(mercadoriaEncontrada == null)
            {
                throw new IdNotFoundException($"Mercadoria com Id: {id} não encontrado.");
            }
            return mercadoriaEncontrada;
        }

        public async Task<Mercadoria> Update(int id, MercadoriaDTO mercadoriaDTO)
        {
            if(id <= 0)
            {
                throw new IdNotFoundException("Id inválido");
            }
            if(mercadoriaDTO == null)
            {
                throw new WrongPropertyException("Existe campo nulo.");
            }

            var mercadoriaEncontrada = await _context.Mercadoria.FindAsync(id);
            if(mercadoriaEncontrada == null)
            {
                throw new IdNotFoundException($"Mercadoria com o Id:{id} não encontrada.");
            }
            mercadoriaEncontrada.TipoMercadoria = mercadoriaDTO.TipoMercadoria;
            mercadoriaEncontrada.Fabricante = mercadoriaDTO.Fabricante;
            mercadoriaEncontrada.Descricao = mercadoriaDTO.Descricao;
            mercadoriaEncontrada.Nome = mercadoriaDTO.Nome;
            await _context.SaveChangesAsync();
            return mercadoriaEncontrada;

        }
    }
}
