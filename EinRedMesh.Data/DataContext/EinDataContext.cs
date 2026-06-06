using Ein.Entidades;
using Microsoft.EntityFrameworkCore;
namespace EinRedMesh.Data.DataContext
{
    public class EinDataContext: DbContext
    {

        public EinDataContext(DbContextOptions<EinDataContext> options) : base(options)
        {
        }

        public DbSet<AlumnoEntity> Alumno { get; set; }
        public DbSet<GeneracionEntity> Generacion { get; set; }

        public DbSet<GrupoEntity> Grupo { get; set; }

    }
}
