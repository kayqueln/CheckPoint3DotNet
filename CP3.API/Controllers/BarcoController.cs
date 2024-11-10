using CP3.Application.Dtos;
using CP3.Domain.Entities;
using CP3.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CP3.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarcoController : ControllerBase
    {
        private readonly IBarcoApplicationService _applicationService;

        public BarcoController(IBarcoApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        [Produces<IEnumerable<BarcoEntity>>]
        public IActionResult Get()
        {
            var categorias = _applicationService.ObterTodosBarcos();

            if (categorias is not null)
                return Ok(categorias);

            return BadRequest("Não foi possivel obter os dados");
        }


        [HttpGet("{id}")]
        [Produces<BarcoEntity>]
        public IActionResult GetPorId(int id)
        {
            var categorias = _applicationService.ObterBarcoPorId(id);

            if (categorias is not null)
                return Ok(categorias);

            return BadRequest("Não foi possivel obter os dados");
        }

        [HttpPost]
        [Produces<BarcoEntity>]
        public IActionResult Post(BarcoDto entity)
        {
            try
            {
                var categorias = _applicationService.AdicionarBarco(entity);

                if (categorias is not null)
                    return Ok(categorias);

                return BadRequest("Não foi possivel salvar os dados");
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Error = ex.Message,
                    Status = HttpStatusCode.BadRequest,
                });
            }
        }

        [HttpPut("{id}")]
        [Produces<BarcoEntity>]
        public IActionResult Put(int id, BarcoDto entity)
        {
            try
            {
                var categorias = _applicationService.EditarBarco(id, entity);

                if (categorias is not null)
                    return Ok(categorias);

                return BadRequest("Não foi possivel editar os dados");
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Error = ex.Message,
                    Status = HttpStatusCode.BadRequest,
                });
            }
        }

        [HttpDelete("{id}")]
        [Produces<BarcoEntity>]
        public IActionResult Delete(int id)
        {
            var categorias = _applicationService.RemoverBarco(id);

            if (categorias is not null)
                return Ok(categorias);

            return BadRequest("Não foi possivel deletar os dados");
        }

    }
}
