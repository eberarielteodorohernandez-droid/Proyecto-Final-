namespace ApiEmpresa.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ComprasDetalle
{
    [Key]
    public long id_compra_detalle { get; set; }
    
    public int id_compra { get; set; }
    [ForeignKey("id_compra")]
    public Compras? Compra { get; set; }

    public int id_producto { get; set; }
    [ForeignKey("id_producto")]
    public Productos? Producto { get; set; }

    public int? cantidad { get; set; }

    [Column(TypeName = "decimal(8,2)")]
    public decimal? precio_costo_unitario { get; set; }
}