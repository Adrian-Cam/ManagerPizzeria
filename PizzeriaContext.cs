using Microsoft.EntityFrameworkCore;


class PizzeriaContext : DbContext
{
    public DbSet<Producto> Productos { get; set; } = null!;
    public DbSet<Cliente> Clientes { get; set; } = null!;
    public DbSet<Pedido> Pedidos { get; set; } = null!;
    public DbSet<ItemPedido> DetallePedido { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=LAPROP_DE_AC\\SQLEXPRESS;Database=ManagerPizzeriaDB;Trusted_Connection=True;TrustServerCertificate=True;");
    }

   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Producto>().Property(p => p.Precio).HasPrecision(10, 2);
    }

    public List<Pedido> ObtenerPedidos()
    {
        return Pedidos
            .Include(p => p.Cliente)
            .Include(p => p.Items)
                .ThenInclude(i => i.Producto)
            .OrderBy(p => p.Id)
            .ToList();
    }
}
