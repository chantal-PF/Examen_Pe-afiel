using Examen_Peñafiel.Datos;
using Examen_Peñafiel.Entidades;
using Examen_Peñafiel.Model.Request;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace Examen_Peñafiel.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class UsaurioController : ControllerBase
    {
        private readonly IUsuarioRepo _usuarioRepo;
        public UsaurioController(IUsuarioRepo usuarioRepo)
        {
            _usuarioRepo = usuarioRepo;
        }

        [HttpGet]
        public IActionResult ObtenerTodas()
        {
            var usuarios = _usuarioRepo.ObtenerTodas();
            return Ok(usuarios);
        }

        [HttpGet("/{id}")]
        public IActionResult ObtenerPorId(int id)
        {
            var usuarios = _usuarioRepo.ObtenerPorId(id);
            return usuarios is null ? NotFound() : Ok(usuarios);
        }

        [HttpPost]
        public IActionResult CrearTarea(CrearRequestUsuarios request)
        {
            var nuevoUsuarios = new Usuarios
            {
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                Email = request.Email,
            };
            var result = _usuarioRepo.Crear(nuevoUsuarios);
            return Ok(result);
        }

    }
}
