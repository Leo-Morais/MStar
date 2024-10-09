using Microsoft.AspNetCore.Mvc;
using MStar.CustomException;
using MStar.DTO;
using MStar.Service;

namespace MStar.Controllers
{
    [ApiController]
    [Route("api/v1/Estoque")]
    public class EstoqueController : Controller
    {
        private readonly IEstoqueService _estoqueService;

        public EstoqueController(IEstoqueService service)
        {
            _estoqueService = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(EstoqueDTO estoqueDTO)
        {
            var estoque = await _estoqueService.Add(estoqueDTO);
            return Ok(estoque);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var estoque = await _estoqueService.GetAll();
            return Ok(estoque);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var estoque = await _estoqueService.GetById(id);
            return Ok(estoque);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _estoqueService.Delete(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EstoqueDTO estoqueDTO)
        {
            try
            {
                var updateTipo = await _estoqueService.Update(id, estoqueDTO);
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
