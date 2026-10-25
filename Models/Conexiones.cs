using Microsoft.EntityFrameworkCore;

namespace ApiEmpresa.Models
{
    public class Conexiones : DbContext
    {
        public Conexiones(DbContextOptions<Conexiones> options) : base(options)
        {
        }

        public DbSet<Puestos> Puestos { get; set; } = null!;
        public DbSet<Empleados> Empleados { get; set; } = null!;
        public DbSet<Clientes> Clientes { get; set; } = null!;
        public DbSet<Proveedores> Proveedores { get; set; } = null!;
        public DbSet<Marcas> Marcas { get; set; } = null!;
        public DbSet<Productos> Productos { get; set; } = null!;
        public DbSet<Compras> Compras { get; set; } = null!;
        public DbSet<ComprasDetalle> Compras_Detalle { get; set; } = null!;
        public DbSet<Ventas> Ventas { get; set; } = null!;
        public DbSet<VentasDetalle> Ventas_Detalle { get; set; } = null!;
        
        // Nuevas tablas agregadas
        public DbSet<Menus> Menus { get; set; } = null!;
        public DbSet<Roles> Roles { get; set; } = null!;
        public DbSet<RolesMenus> Roles_Menus { get; set; } = null!;
        public DbSet<Usuarios> Usuarios { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar la clave primaria compuesta para la tabla intermedia Roles_Menus
            modelBuilder.Entity<RolesMenus>()
                .HasKey(rm => new { rm.id_rol, rm.id_menu });
        }
    }
}