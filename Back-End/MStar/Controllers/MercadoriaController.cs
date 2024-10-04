using Microsoft.AspNetCore.Mvc;
using MStar.CustomException;
using MStar.DTO;
using MStar.Repository;
using MStar.Service;

namespace MStar.Controllers
{
    [ApiController]
    [Route("api/v1/Mercadoria")]
    public class MercadoriaController : ControllerBase
    {
        private readonly IMercadoriaService _mercadoriaService;
        private readonly ConnectionContext _context;

        public MercadoriaController(IMercadoriaService mercadoriaService, ConnectionContext connectionContext)
        {
            _context = connectionContext;
            _mercadoriaService = mercadoriaService ?? throw new ArgumentNullException(nameof(mercadoriaService));
        }
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromForm] MercadoriaDTO mercadoriaDTO)
        {
            var mercadoria = await _mercadoriaService.Add(mercadoriaDTO);
            return Ok(mercadoria);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var mercadoria = await _mercadoriaService.GetAll();
            return Ok(mercadoria);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var mercadoria = await _mercadoriaService.GetById(id);
            return Ok(mercadoria);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mercadoriaService.Delete(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MercadoriaDTO mercadoriaDTO)
        {
            try
            {

                var updateMercadoria = await _mercadoriaService.Update(id, mercadoriaDTO);
                return Ok(updateMercadoria);

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
