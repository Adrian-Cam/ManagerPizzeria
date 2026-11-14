class Pedido
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; } = null!;
    public string Estado { get; set; } = "Pendiente";
    public List<ItemPedido> Items { get; set; } = new List<ItemPedido>();

    // Solo tiene get, así que EF Core no lo guarda en la base de datos: se calcula
    public decimal Total
    {
        get
        {
            decimal total = 0;
            foreach (ItemPedido item in Items)
            {
                total += item.Subtotal;
            }
            return total;
        }
    }

    public Pedido()
    {
    }

    public Pedido(Cliente cliente)
    {
        Cliente = cliente;
        Estado = "Pendiente";
    }

    public void AgregarProducto(Producto producto, int cantidad)
    {
        Items.Add(new ItemPedido(producto, cantidad));
    }

    // Si la nueva cantidad es 0, el producto se quita del pedido.
    public void ModificarCantidad(int indice, int nuevaCantidad)
    {
        if (nuevaCantidad == 0)
        {
            Items.RemoveAt(indice);
        }
        else
        {
            Items[indice].Cantidad = nuevaCantidad;
        }
    }

    public void MostrarDetalle()
    {
        Console.WriteLine("Pedido #" + Id + " | Cliente: " + Cliente.Nombre + " | " + Estado);
        foreach (ItemPedido item in Items)
        {
            Console.WriteLine("   " + item.Producto.Nombre + " x" + item.Cantidad);
        }
        Console.WriteLine("   Total: $" + Total);
    }
}