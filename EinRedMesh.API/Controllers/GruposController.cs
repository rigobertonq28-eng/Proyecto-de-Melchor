using AutoMapper;
using Ein.Dtos;
using Ein.Entidades;
using EinRedMesh.API.Models;
using EinRedMesh.Data.DataContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EinRedMesh.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GruposController : ControllerBase
    {
        private readonly EinDataContext _context;
        private readonly IMapper _mapper;

        public GruposController(EinDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var grupos = _context.Grupo
                    .Include(x => x.Generacion)
                    .Select(x => _mapper.Map<GrupoGetDto>(x))
                    .ToList();
                if (grupos.Count == 0)
                    return NoContent();

                return Ok(grupos);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
        [HttpPost]
        public IActionResult Post(GrupoSetDto newobj)
        {
            try
            {

                var obj = _mapper.Map<GrupoEntity>(newobj);
                _context.Grupo.Add(obj);
                _context.SaveChanges();
                return Ok(new RespuestaModel(StatusCodes.Status200OK, "Grupo creado correctamente", obj));


            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        public IActionResult Put( int id, GrupoSetDto dto)
        {
            try
            {
                var grupo = _context.Grupo.Find(id);
                if(grupo==null)
                    return NotFound();

                grupo.Nombre = dto.Nombre;
                grupo.IdGeneracion = dto.IdGeneracion;
                _context.Grupo.Update(grupo);
                _context.SaveChanges();

                return Ok(_mapper.Map<GrupoGetDto>(grupo));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                var grupo = _context.Grupo.Find(id);
                if(grupo==null)
                    return NotFound();
                
                _context.Grupo.Remove(grupo);
                _context.SaveChanges();
                return Ok(_mapper.Map<GrupoGetDto>(grupo));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
