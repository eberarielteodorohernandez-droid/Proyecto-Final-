namespace ApiEmpresa.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Ventas
{
    [Key]
    public int id_venta { get; set; }
    public int no_factura { get; set; }
    public string serie { get; set; } = null!;
    public DateTime? fecha_factura { get; set; }

    public int id_cliente { get; set; }
    [ForeignKey("id_cliente")]
    public Clientes? Cliente { get; set; }

    public int id_empleado { get; set; }
    [ForeignKey("id_empleado")]
    public Empleados? Empleado { get; set; }

    public DateTime? fecha_ingreso { get; set; }

    // Relación Maestro/Detalle
    public ICollection<VentasDetalle> VentasDetalle { get; set; } = new List<VentasDetalle>();
}