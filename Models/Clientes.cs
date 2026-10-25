namespace ApiEmpresa.Models;
using System.ComponentModel.DataAnnotations;

public class Clientes
{
    [Key]
    public int id_cliente { get; set; }
    public string nombres { get; set; } = null!;
    public string apellidos { get; set; } = null!;
    public string? nit { get; set; }
    public bool? genero { get; set; }
    public string? telefono { get; set; }
    public string? correo_electronico { get; set; }
    public DateTime? fecha_ingreso { get; set; }
}