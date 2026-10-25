namespace ApiEmpresa.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Compras
{
    [Key]
    public int id_compra { get; set; }
    public int no_orden_compra { get; set; }
    
    public int id_proveedor { get; set; }
    [ForeignKey("id_proveedor")]
    public Proveedores? Proveedor { get; set; }

    public DateTime? fecha_orden { get; set; }
    public DateTime? fecha_ingreso { get; set; }

    // Relación Maestro/Detalle
    public ICollection<ComprasDetalle> ComprasDetalle { get; set; } = new List<ComprasDetalle>();
}