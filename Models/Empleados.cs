namespace ApiEmpresa.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Empleados
{
    [Key]
    public int id_empleado { get; set; }
    public string nombres { get; set; } = null!;
    public string apellidos { get; set; } = null!;
    public string? direccion { get; set; }
    public string? telefono { get; set; }
    public string? dpi { get; set; }
    public bool? genero { get; set; }
    public DateTime? fecha_nacimiento { get; set; }
    
    public short id_puesto { get; set; }

    [ForeignKey("id_puesto")]
    public Puestos? Puesto { get; set; }

    public DateTime? fecha_inicio_labores { get; set; }
    public DateTime? fecha_ingreso { get; set; }
}