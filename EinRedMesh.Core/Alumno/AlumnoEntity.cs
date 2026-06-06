using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;



namespace Ein.Entidades
{
    [Table("Alumno")]
    public class AlumnoEntity
    {
        [Key] public int Id { get; set; }
        [StringLength(8), Required] public string NumeroCuenta { get; set; } = "";
        [StringLength(30), Required] public string Nombre { get; set; } = "";
        [StringLength(30), Required] public string ApellidoPaterno { get; set; } = "";

        [StringLength(30), Required] public string ApellidoMaterno { get; set; } = "";

        [Required] public DateTime FechaNacimiento { get; set; }
        [Required] public required SexoEnum Sexo { get; set; }
        [Required] public int IdGrupo { get; set; }
        [Required] public bool EstaActivo { get; set; }

        [ForeignKey("IdGrupo")] public virtual required GrupoEntity Grupo { get; set; }
        [ForeignKey("IdGeneracion")] public virtual GeneracionEntity Generacion { get; set;}

    }
}
