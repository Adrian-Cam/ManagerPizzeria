class ItemPedido
{
    public int Id { get; set; }
    public int PedidoId { get; set; }
    public int ProductoId { get; set; }
    public Producto Producto { get; set; } = null!;
    public int Cantidad { get; set; }

    public decimal Subtotal
    {
        get { return Producto.Precio * Cantidad; }
    }

    public ItemPedido()
    {
    }

    public ItemPedido(Producto producto, int cantidad)
    {
        Producto = producto;
        Cantidad = cantidad;
    }
}