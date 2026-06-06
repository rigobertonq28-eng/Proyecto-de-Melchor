using Ein.Entidades;
using EinRedMesh.Data.DataContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace EinRedMesh.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneracionesController : ControllerBase
    {
        private readonly EinDataContext _context;
        public GeneracionesController(EinDataContext newobj)
        {
            _context = newobj;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var generaciones = _context.Generacion.ToList();
            return Ok(generaciones);

        }

        [HttpPost]
        public IActionResult Post([FromBody] GeneracionEntity newobj)
        {
            _context.Generacion.Add(newobj);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Get), newobj);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var generacion = _context.Generacion.Find(id);
            _context.Generacion.Remove(generacion);
            _context.SaveChanges();
            return Ok("Se elimino correctamente");
        }

        [HttpPut]
        public IActionResult Put(int id, [FromBody] GeneracionEntity updateobj)
        {
            var generaciones = _context.Generacion.Find(id);
            generaciones.Nombre = updateobj.Nombre;
            generaciones.EstaActivo = updateobj.EstaActivo;
            _context.Generacion.Update(generaciones);
            _context.SaveChanges();
            return Ok("Actulizado correctamente");
        }

        [HttpPatch]
        public IActionResult Patch(int id, [FromBody] GeneracionEntity updateobj)
        {
            return Ok("Actulizado correctamente");
        }
    }
}