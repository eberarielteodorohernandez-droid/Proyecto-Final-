namespace ApiEmpresa.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Usuarios
{
    [Key]
    public int id_usuario { get; set; }
    public string usuario { get; set; } = null!;
    public string contrasena { get; set; } = null!;

    public int id_empleado { get; set; }
    [ForeignKey("id_empleado")]
    public Empleados? Empleado { get; set; }

    public short id_rol { get; set; }
    [ForeignKey("id_rol")]
    public Roles? Rol { get; set; }

    public bool activo { get; set; } = true;
    public DateTime? fecha_ingreso { get; set; }
}