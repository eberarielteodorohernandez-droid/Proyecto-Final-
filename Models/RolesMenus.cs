namespace ApiEmpresa.Models;
using System.ComponentModel.DataAnnotations.Schema;

public class RolesMenus
{
    public short id_rol { get; set; }
    [ForeignKey("id_rol")]
    public Roles? Rol { get; set; }

    public int id_menu { get; set; }
    [ForeignKey("id_menu")]
    public Menus? Menu { get; set; }
}