using Microsoft.EntityFrameworkCore;
using MStar.CustomException;
using MStar.DTO;
using MStar.Model;
using MStar.Repository;
using System.Security.Principal;

namespace MStar.Service
{
    public class TipoMercadoriaService : ITipoMercadoriaService
    {
        private readonly ConnectionContext _context;

        public TipoMercadoriaService(ConnectionContext context)
        {
            _context = context;
        }

        public async Task<TipoMercadoria> Add(TipoMercadoriaDTO tipoMercadoriaDTO)
        {
            var tipo = new TipoMercadoria()
            {
                Tipo = tipoMercadoriaDTO.Tipo
            };
            await _context.TipoMercadoria.AddAsync(tipo);
            await _context.SaveChangesAsync();
            return tipo;
        }

        public async Task Delete(int id)
        {
            var tipoAchado = await _context.TipoMercadoria.FindAsync(id);
            if (tipoAchado == null)
            {
                throw new IdNotFoundException($"Tipo com Id: {id} não encontrado.");
            }
            _context.TipoMercadoria.Remove(tipoAchado);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TipoMercadoria>> GetAll()
        {
            return await _context.TipoMercadoria.ToListAsync();
        }

        public async Task<TipoMercadoria> GetById(int id)
        {
            var tipoAchado = await _context.TipoMercadoria.FindAsync(id);
            if (tipoAchado == null)
            {
                throw new IdNotFoundException($"Tipo com Id: {id} não encontrado.");
            }
            return tipoAchado;
        }

        public async Task<TipoMercadoria> Update(int id, TipoMercadoriaDTO tipoMercadoriaDTO)
        {
            if (id <= 0)
            {
                throw new IdNotFoundException("Id inválido");
            }
            if (tipoMercadoriaDTO == null)
            {
                throw new WrongPropertyException("Existe campo nulo.");
            }

            var tipoAchado = await _context.TipoMercadoria.FindAsync(id);
            tipoAchado.Tipo = tipoMercadoriaDTO.Tipo;
            await _context.SaveChangesAsync();
            return tipoAchado;
        }
    }
}
