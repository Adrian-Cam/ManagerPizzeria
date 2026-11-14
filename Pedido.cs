using System;
using System.Collections.Generic;

class Pedido
{
    public int Id { get; }
    public Cliente Cliente { get; }
    public string Estado { get; set; }
    public List<ItemPedido> Items { get; } = new List<ItemPedido>();

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

    public Pedido(int id, Cliente cliente)
    {
        Id = id;
        Cliente = cliente;
        Estado = "Pendiente";
    }

    public void AgregarProducto(Producto producto, int cantidad)
    {
        Items.Add(new ItemPedido(producto, cantidad));
    }

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
