namespace ApiEmpresa.Models;
using System.ComponentModel.DataAnnotations;

public class Marcas
{
    [Key]
    public short id_marca { get; set; }
    public string marca { get; set; } = null!;
}