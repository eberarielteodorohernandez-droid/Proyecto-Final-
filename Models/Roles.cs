namespace ApiEmpresa.Models;
using System.ComponentModel.DataAnnotations;

public class Roles
{
    [Key]
    public short id_rol { get; set; }
    public string rol { get; set; } = null!;
    public string? descripcion { get; set; }

    public ICollection<RolesMenus> RolesMenus { get; set; } = new List<RolesMenus>();
    public ICollection<Usuarios> Usuarios { get; set; } = new List<Usuarios>();
}