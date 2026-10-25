namespace ApiEmpresa.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Menus
{
    [Key]
    public int id_menu { get; set; }
    public string nombre { get; set; } = null!;
    public string? ruta { get; set; }
    public string? icono { get; set; }
    public int? id_menu_padre { get; set; }

    [ForeignKey("id_menu_padre")]
    public Menus? MenuPadre { get; set; }

    public short orden { get; set; }
    public bool activo { get; set; } = true;

    public ICollection<RolesMenus> RolesMenus { get; set; } = new List<RolesMenus>();
}