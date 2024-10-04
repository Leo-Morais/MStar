using Microsoft.AspNetCore.Mvc;
using MStar.CustomException;
using MStar.DTO;
using MStar.Repository;
using MStar.Service;

namespace MStar.Controllers
{
    [ApiController]
    [Route("api/v1/Movimentação")]
    public class MovimentacaoController : ControllerBase
    {
        private readonly ConnectionContext _context;
        private readonly IMovimentacaoService _movimentacaoService;

        public MovimentacaoController(ConnectionContext context, IMovimentacaoService movimentacaoService)
        {
            _context = context;
            _movimentacaoService = movimentacaoService ?? throw new ArgumentNullException(nameof(movimentacaoService));
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromForm] MovimentacaoDTO movimentacaoDTO)
        {
            var movimentacao = await _movimentacaoService.Add(movimentacaoDTO);
            return Ok(movimentacao);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var movimentacao = await _movimentacaoService.GetAll();
            return Ok(movimentacao);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var movimentacao = await _movimentacaoService.GetById(id);
            return Ok(movimentacao);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _movimentacaoService.Delete(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MovimentacaoDTO movimentacaoDTO)
        {
            try
            {

                var updateMercadoria = await _movimentacaoService.Update(id, movimentacaoDTO);
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
