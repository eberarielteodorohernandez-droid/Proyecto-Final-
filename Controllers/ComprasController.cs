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
    public class ComprasController : ControllerBase
    {
        private readonly Conexiones _context;

        public ComprasController(Conexiones context)
        {
            _context = context;
        }

        // GET: api/Compras
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Compras>>> GetCompras()
        {
            return await _context.Compras
                .Include(c => c.Proveedor)
                .Include(c => c.ComprasDetalle)
                    .ThenInclude(d => d.Producto)
                .ToListAsync();
        }

        // GET: api/Compras/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Compras>> GetCompra(int id)
        {
            var compra = await _context.Compras
                .Include(c => c.Proveedor)
                .Include(c => c.ComprasDetalle)
                    .ThenInclude(d => d.Producto)
                .FirstOrDefaultAsync(c => c.id_compra == id);

            if (compra == null)
            {
                return NotFound();
            }

            return compra;
        }

        // POST: api/Compras (Registrar Maestro/Detalle y aplicar reglas de negocio)
        [HttpPost]
        public async Task<ActionResult<Compras>> PostCompra(Compras compra)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // 1. Recorrer los detalles para aplicar reglas de negocio en los productos
                foreach (var detalle in compra.ComprasDetalle)
                {
                    var producto = await _context.Productos.FindAsync(detalle.id_producto);
                    if (producto != null)
                    {
                        // Aumentar existencia
                        producto.existencia = (producto.existencia ?? 0) + (detalle.cantidad ?? 0);

                        // Actualizar precio de costo con el precio unitario de compra
                        decimal costoUnitario = detalle.precio_costo_unitario ?? 0;
                        producto.precio_costo = costoUnitario;

                        // Calcular precio de venta sumando el 25% al precio de costo
                        producto.precio_venta = costoUnitario * 1.25m;

                        _context.Productos.Update(producto);
                    }
                }

                // 2. Guardar la compra (Maestro y Detalle)
                _context.Compras.Add(compra);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return CreatedAtAction("GetCompra", new { id = compra.id_compra }, compra);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al registrar la compra: {ex.Message}");
            }
        }

        // DELETE: api/Compras/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompra(int id)
        {
            var compra = await _context.Compras
                .Include(c => c.ComprasDetalle)
                .FirstOrDefaultAsync(c => c.id_compra == id);

            if (compra == null)
            {
                return NotFound();
            }

           _context.Compras_Detalle.RemoveRange(compra.ComprasDetalle);
            _context.Compras.Remove(compra);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}