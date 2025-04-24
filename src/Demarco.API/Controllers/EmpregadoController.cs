using Demarco.Application.Interfaces;
using Demarco.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Demarco.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class EmpregadoController : Controller
    {
        private readonly IEmpregadoApp _app;
        public EmpregadoController(IEmpregadoApp app)
        {
            this._app = app;
        }

        [HttpGet]
        [ProducesResponseType(typeof(EmpregadoDTO[]), (int)HttpStatusCode.OK)]
        public IActionResult Get()
        {
            try
            {
                return Ok(_app.RecuperarTodos());
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EmpregadoDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            try
            {
                var empregado = _app.Recuperar(id);
                if (empregado == null)
                {
                    return NotFound("Empregado não encontrado.");
                }
                return Ok(empregado);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(EmpregadoDTO), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Post(EmpregadoDTO empregadoDTO)
        {
            try
            {
                if (await _app.Incluir(empregadoDTO))
                {
                    return this.StatusCode(StatusCodes.Status201Created, "Empregado Criado");
                }
                return BadRequest();
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }

        [HttpPut()]
        [ProducesResponseType(typeof(EmpregadoDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(EmpregadoDTO empregadoDTO)
        {
            try
            {
                if (await _app.Atualizar(empregadoDTO))
                {
                    return this.StatusCode(StatusCodes.Status200OK, "Empregado Atualizado");
                }
                return BadRequest();
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


    }
}
