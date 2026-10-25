namespace ApiEmpresa.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Productos
{
    [Key]
    public int id_producto { get; set; }
    public string producto { get; set; } = null!;
    
    public short id_marca { get; set; }

    [ForeignKey("id_marca")]
    public Marcas? Marca { get; set; }

    public string? descripcion { get; set; }
    public string? imagen { get; set; }
    
    [Column(TypeName = "decimal(8,2)")]
    public decimal? precio_costo { get; set; }
    
    [Column(TypeName = "decimal(8,2)")]
    public decimal? precio_venta { get; set; }
    
    public int? existencia { get; set; }
    public DateTime? fecha_ingreso { get; set; }
}