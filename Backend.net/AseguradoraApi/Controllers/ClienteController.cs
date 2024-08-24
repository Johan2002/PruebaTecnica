using Microsoft.AspNetCore.Mvc;

using AseguradoraApi.Data;
using AseguradoraApi.Models;

namespace AseguradoraApi.Controllers
{
    // Define la ruta base para todas las acciones en este controlador.
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        // Instancia de la clase ClienteData que se utiliza para interactuar con la base de datos.
        private readonly ClienteData _clienteData;

        // Constructor que recibe una instancia de ClienteData.
        public ClienteController(ClienteData clienteData)
        {
            _clienteData = clienteData;
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            // Llama al método Lista de ClienteData para obtener la lista de clientes.
            List<Cliente> Lista = await _clienteData.Lista();
            return StatusCode(StatusCodes.Status200OK, Lista);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Obtener(int id)
        {
            // Llama al método Obtener de ClienteData para obtener el cliente por ID.
            Cliente cliente = await _clienteData.Obtener(id);
            return StatusCode(StatusCodes.Status200OK, cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Cliente cliente)
        {
            // Llama al método Crear de ClienteData para insertar el nuevo cliente en la base de datos.
            bool respuesta  = await _clienteData.Crear(cliente);
            return StatusCode(StatusCodes.Status200OK, new  {isSuccess = respuesta });
        }

        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] Cliente cliente)
        {
            // Llama al método Editar de ClienteData para actualizar el cliente en la base de datos.
            bool respuesta = await _clienteData.Editar(cliente);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            // Llama al método Eliminar de ClienteData para eliminar el cliente por ID.
            bool respuesta = await _clienteData.Eliminar(id);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }
    }
}
