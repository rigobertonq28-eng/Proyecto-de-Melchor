using AutoMapper;
using Ein.Dtos;
using Ein.Entidades;
using EinRedMesh.API.Models;
using EinRedMesh.Data.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace EinRedMesh.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneracionesController : ControllerBase
    {
        private readonly EinDataContext _context;
        private readonly IMapper _mapper;
        public GeneracionesController(EinDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<RespuestaModel>> Get()
        {

            try
            {
                var lista = await _context.Generacion
    .Where(x => x.EstaActivo == true)
    .ToListAsync(); // ← primero trae los datos

                var listaDto = lista
                    .Select(x => _mapper.Map<GeneracionGetDto>(x)) // ← luego mapea
                    .ToList();

                if (listaDto.Count == 0)
                    return new RespuestaModel(StatusCodes.Status204NoContent, "No hay datos");

                return new RespuestaModel(StatusCodes.Status200OK,
                    "Se ejecutó correctamente", listaDto);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RespuestaModel>> GetById(int id)
        {

            try
            {
                var generacion = await _context.Generacion.Where(x=> x.EstaActivo==true && x.Id==id).FirstOrDefaultAsync();
                if (generacion == null)
                    return new RespuestaModel(StatusCodes.Status204NoContent,"No existe contenido");

                var obj = _mapper.Map<GeneracionGetDto>(generacion);
                return new RespuestaModel(StatusCodes.Status200OK, "Se ejecuto correctamente", obj);
            }
            catch (Exception ex)
            {

                return new RespuestaModel(StatusCodes.Status400BadRequest, ex.Message);
            }

        }




        [HttpPost]
        public async Task <ActionResult<RespuestaModel>> Post(int id, GeneracionSetDto newobj)
        {
            try
            {

                if (!ModelState.IsValid)
                    return new RespuestaModel(StatusCodes.Status400BadRequest,"Modelo invalido");



                var obj = _mapper.Map<GeneracionEntity>(newobj);
                await _context.Generacion.AddAsync(obj);
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
                var generacion = await _context.Generacion.Where(x => x.EstaActivo && x.Id == id).FirstOrDefaultAsync();

                if (generacion == null)
                    return new RespuestaModel(StatusCodes.Status404NotFound, "No se encontraron");

               // _context.Generacion.Remove(generacion);
               generacion.EstaActivo = false;
                _context.Generacion.Update(generacion);
                await _context.SaveChangesAsync();
                return new RespuestaModel(StatusCodes.Status200OK, "Se elimino correctamente",generacion);
            }
            catch (Exception ex)
            {

                return new RespuestaModel(400, ex.Message);
            }

        }

        [HttpPut]
        public async Task<ActionResult<RespuestaModel>> Put(int id, GeneracionSetDto updateobj)
        {
            try
            {
                if (!ModelState.IsValid)
                    return new RespuestaModel(StatusCodes.Status400BadRequest, "Modelo invalido");

                var generacion =await  _context.Generacion.FindAsync(id);
                if (generacion == null)
                    return new RespuestaModel(StatusCodes.Status404NotFound, "No se encontraron");

                generacion.Nombre = updateobj.Nombre;
                _context.Generacion.Update(generacion);
                await _context.SaveChangesAsync();
                return new RespuestaModel(StatusCodes.Status200OK, "Actualizado correctamente", generacion);
            }
            catch (Exception ex)
            {

                return new RespuestaModel(400, ex.Message);
            }
        }
    }
}