using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiEmpresa.Models;

namespace ApiEmpresa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly Conexiones _context;

        public VentasController(Conexiones context)
        {
            _context = context;
        }

        // GET: api/Ventas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ventas>>> GetVentas()
        {
            return await _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Empleado)
                .Include(v => v.VentasDetalle)
                    .ThenInclude(d => d.Producto)
                .ToListAsync();
        }

        // GET: api/Ventas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ventas>> GetVenta(int id)
        {
            var venta = await _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Empleado)
                .Include(v => v.VentasDetalle)
                    .ThenInclude(d => d.Producto)
                .FirstOrDefaultAsync(v => v.id_venta == id);

            if (venta == null)
            {
                return NotFound();
            }

            return venta;
        }

        // POST: api/Ventas (Validar stock, descontar existencia y registrar Maestro/Detalle)
        [HttpPost]
        public async Task<ActionResult<Ventas>> PostVenta(Ventas venta)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // 1. Validar existencias antes de procesar la venta
                foreach (var detalle in venta.VentasDetalle)
                {
                    var producto = await _context.Productos.FindAsync(detalle.id_producto);
                    if (producto == null)
                    {
                        return BadRequest($"El producto con ID {detalle.id_producto} no existe.");
                    }

                    int stockActual = producto.existencia ?? 0;
                    int cantidadSolicitada = detalle.cantidad ?? 0;

                    if (cantidadSolicitada > stockActual)
                    {
                        return BadRequest(new { 
                            mensaje = "No se puede realizar la venta. Existencia insuficiente.",
                            producto = producto.producto,
                            existenciaDisponibles = stockActual,
                            cantidadSolicitada = cantidadSolicitada
                        });
                    }

                    // 2. Disminuir la existencia del producto
                    producto.existencia = stockActual - cantidadSolicitada;
                    _context.Productos.Update(producto);
                }

                // 3. Registrar la venta (Maestro y Detalle)
                _context.Ventas.Add(venta);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return CreatedAtAction("GetVenta", new { id = venta.id_venta }, venta);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al registrar la venta: {ex.Message}");
            }
        }

        // DELETE: api/Ventas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVenta(int id)
        {
            var venta = await _context.Ventas
                .Include(v => v.VentasDetalle)
                .FirstOrDefaultAsync(v => v.id_venta == id);

            if (venta == null)
            {
                return NotFound();
            }

            _context.Ventas_Detalle.RemoveRange(venta.VentasDetalle);
            _context.Ventas.Remove(venta);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}