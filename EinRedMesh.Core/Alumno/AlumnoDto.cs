using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EinRedMesh.Core.Alumno
{
    public class AlumnoSetDto
    {
        [Required] public int IdGeneracion { get; set; }
        [Required]
        [StringLength(30)] public string Nombre { get; set; }

    }


    public class AlumnoGetDto
    {
        [Key] public int Id { get; set; }
        [StringLength(30), Required] public string Nombre { get; set; } = "";
        [StringLength(30), Required] public string ApellidoPaterno { get; set; } = "";

        [StringLength(30), Required] public string ApellidoMaterno { get; set; } = "";

        [Required] public int IdGeneracion { get; set; }




    }


}
