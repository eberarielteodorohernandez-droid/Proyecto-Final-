namespace ApiEmpresa.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class VentasDetalle
{
    [Key]
    public long id_venta_detalle { get; set; }

    public int id_venta { get; set; }
    [ForeignKey("id_venta")]
    public Ventas? Venta { get; set; }

    public int id_producto { get; set; }
    [ForeignKey("id_producto")]
    public Productos? Producto { get; set; }

    public int? cantidad { get; set; }

    [Column(TypeName = "decimal(8,2)")]
    public decimal? precio_unitario { get; set; }
}