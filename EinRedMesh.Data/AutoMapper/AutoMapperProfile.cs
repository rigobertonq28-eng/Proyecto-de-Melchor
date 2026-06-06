using AutoMapper;
using Ein.Dtos;
using Ein.Entidades;
using Microsoft.Extensions.Options;

namespace EinRedMesh.Data.AutoMapper
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<GeneracionSetDto, GeneracionEntity>()
                .ForMember(campo => campo.EstaActivo, asignar => asignar.MapFrom(valor => true));
            CreateMap<GeneracionEntity, GeneracionGetDto>();

            CreateMap<GrupoSetDto, GrupoEntity>()
                .ForMember(campo => campo.EstaActivo, asignar => asignar.MapFrom(valor => true));
            CreateMap<GrupoEntity, GrupoGetDto>()
            .ForMember(campo => campo.NombreGeneracion, asignar => asignar.MapFrom(valor => valor.Generacion.Nombre));

        }
    }
}
