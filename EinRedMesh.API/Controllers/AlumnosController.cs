using AutoMapper;
using Ein.Dtos;
using Ein.Entidades;
using EinRedMesh.API.Models;
using EinRedMesh.Core.Alumno;
using EinRedMesh.Data.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EinRedMesh.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnosController : ControllerBase
    {
        private readonly EinDataContext _context;
        private readonly IMapper _mapper;
        public AlumnosController(EinDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<RespuestaModel>> Get()
        {
            try
            {
                var alumnos = await _context.Alumno.Where(x => x.EstaActivo == true)
                    .Select(x => _mapper.Map<AlumnoGetDto>(x))
                    .ToListAsync();

                if (alumnos.Count == 0)
                    return new RespuestaModel(StatusCodes.Status204NoContent, "No existe contenido en los alumnos");

                return new RespuestaModel(StatusCodes.Status200OK, "Se ejecuto correctamente", alumnos);

            }
            catch (Exception ex)
            {

                return new RespuestaModel(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult<RespuestaModel>> Post(int id, AlumnoSetDto newobj)
        {
            try
            {

                if (!ModelState.IsValid)
                    return new RespuestaModel(StatusCodes.Status400BadRequest, "Modelo invalido");
                var alumnos = await _context.Alumno.Where(x => x.EstaActivo && x.Id == id).FirstOrDefaultAsync();

                if (alumnos == null)
                    return NotFound();


                var obj = _mapper.Map<AlumnoEntity>(newobj);
                await _context.Alumno.AddAsync(obj);
                await _context.SaveChangesAsync();
                return new RespuestaModel(200, "Se ejecuto correctamente", obj);

            }
            catch (Exception ex)
            {

                return new RespuestaModel(400, ex.Message);
            }

        }
        [HttpDelete]
        public async Task<ActionResult<RespuestaModel>> Delete(int id)
        {

            try
            {
                var alumnos = await _context.Alumno.Where(x => x.EstaActivo && x.Id == id).FirstOrDefaultAsync();

                if (alumnos == null)
                    return new RespuestaModel(StatusCodes.Status404NotFound, "No se encontraron");

                // _context.Alumno.Remove(alumnos);
                alumnos.EstaActivo = false;
                _context.Alumno.Update(alumnos);
                await _context.SaveChangesAsync();
                return new RespuestaModel(StatusCodes.Status200OK, "Se elimino correctamente", alumnos);
            }
            catch (Exception ex)
            {

                return new RespuestaModel(400, ex.Message);
            }



        }

        [HttpPut]
        public async Task<ActionResult<RespuestaModel>> Put(int id, AlumnoSetDto updateobj)
        {
            try
            {
                if (!ModelState.IsValid)
                    return new RespuestaModel(StatusCodes.Status400BadRequest, "Modelo invalido");

                var alumnos = await _context.Alumno.FindAsync(id);
                if (alumnos == null)
                    return new RespuestaModel(StatusCodes.Status404NotFound, "No se encontraron");

                alumnos.Nombre = updateobj.Nombre;
                _context.Alumno.Update(alumnos);
                await _context.SaveChangesAsync();
                return new RespuestaModel(StatusCodes.Status200OK, "Actualizado correctamente", alumnos);
            }
            catch (Exception ex)
            {

                return new RespuestaModel(400, ex.Message);
            }
        }

    }


}
