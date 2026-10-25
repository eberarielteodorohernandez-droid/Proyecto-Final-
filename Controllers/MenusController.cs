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
    public class MenusController : ControllerBase
    {
        private readonly Conexiones _context;

        public MenusController(Conexiones context)
        {
            _context = context;
        }

        // GET: api/Menus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Menus>>> GetMenus()
        {
            return await _context.Menus
                .Where(m => m.activo)
                .OrderBy(m => m.orden)
                .ToListAsync();
        }

        // GET: api/Menus/por-rol/1
        // Método especial para cargar el menú dinámico según el Rol del usuario conectado
        [HttpGet("por-rol/{idRol}")]
        public async Task<ActionResult<IEnumerable<Menus>>> GetMenusPorRol(short idRol)
        {
            var menusDelRol = await _context.Roles_Menus
                .Where(rm => rm.id_rol == idRol && rm.Menu!.activo)
                .Select(rm => rm.Menu!)
                .OrderBy(m => m.orden)
                .ToListAsync();

            return Ok(menusDelRol);
        }
    }
}