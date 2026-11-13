using System;
using System.Collections.Generic;

class ItemPedido
{
    public Producto Producto { get; set; }
    public int Cantidad { get; set; }

    public decimal Subtotal
    {
        get { return Producto.Precio * Cantidad; }
    }

    public ItemPedido(Producto producto, int cantidad)
    {
        Producto = producto;
        Cantidad = cantidad;
    }
}
