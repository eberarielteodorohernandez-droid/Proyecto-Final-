namespace ApiEmpresa.Models;
using System.ComponentModel.DataAnnotations;

public class Proveedores
{
    [Key]
    public int id_proveedor { get; set; }
    public string proveedor { get; set; } = null!;
    public string? nit { get; set; }
    public string? direccion { get; set; }
    public string? telefono { get; set; }
}