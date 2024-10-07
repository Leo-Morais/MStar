using Microsoft.AspNetCore.Mvc;
using MStar.CustomException;
using MStar.DTO;
using MStar.Repository;
using MStar.Service;

namespace MStar.Controllers
{
    [ApiController]
    [Route("api/v1/TipoMercadoria")]
    public class TipoMercadoriaController : ControllerBase
    {
        private readonly ITipoMercadoriaService _tipoService;

        public TipoMercadoriaController(ITipoMercadoriaService _tipoService)
        {
            this._tipoService = _tipoService ?? throw new ArgumentNullException(nameof(_tipoService));
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(TipoMercadoriaDTO tipoMercadoriaDTO)
        {
            var tipo = await _tipoService.Add(tipoMercadoriaDTO);
            return Ok(tipo);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tipo = await _tipoService.GetAll();
            return Ok(tipo);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var tipo = await _tipoService.GetById(id);
            return Ok(tipo);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _tipoService.Delete(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TipoMercadoriaDTO tipoMercadoriaDTO)
        {
            try
            {

                var updateTipo = await _tipoService.Update(id, tipoMercadoriaDTO);
                return Ok(updateTipo);

            }
            catch (IdNotFoundException infe)
            {
                return NotFound(infe.Message);
            }
            catch (WrongPropertyException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
