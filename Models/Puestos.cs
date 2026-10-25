namespace ApiEmpresa.Models;
using System.ComponentModel.DataAnnotations;

public class Puestos
{
    [Key]
    public short id_puesto { get; set; }
    public string puesto { get; set; } = null!;
}