using Microsoft.EntityFrameworkCore;

PizzeriaContext context = new PizzeriaContext();

if (!context.Database.CanConnect())
{
    Console.WriteLine("No se pudo conectar a la base de datos.");
    Console.WriteLine("Presione una tecla para salir...");
    Console.ReadKey();
    return;
}

int options = 0;

do
{
    Console.Clear();

    Console.WriteLine("=================");
    Console.WriteLine("Control de Pedidos Pizzería");
    Console.WriteLine("=================");

    Console.WriteLine("Ingrese Una Opción:");
    Console.WriteLine("1. Gestionar productos");
    Console.WriteLine("2. Gestionar clientes");
    Console.WriteLine("3. Crear pedido");
    Console.WriteLine("4. Modificar pedido");
    Console.WriteLine("5. Consultar pedidos");
    Console.WriteLine("6. Pedidos pendientes");
    Console.WriteLine("7. Salir");

    Console.Write("Opción: ");
    string input = Console.ReadLine();
    if (!int.TryParse(input, out options))
    {
        Console.WriteLine("Debes escribir un número del 1 al 7");
        options = 0;
        Console.ReadKey();
        continue;
    }

    switch (options)
    {
        case 1:
            {
                int subOpcion = 0;

                do
                {
                    Console.Clear();
                    Console.WriteLine("=== Gestión de productos ===");
                    Console.WriteLine("1. Agregar producto");
                    Console.WriteLine("2. Ver productos");
                    Console.WriteLine("3. Volver");
                    Console.Write("Opción: ");

                    try
                    {
                        subOpcion = int.Parse(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Debes escribir un número.");
                        subOpcion = 0;
                        Console.ReadKey();
                        continue;
                    }

                    switch (subOpcion)
                    {
                        case 1:
                            try
                            {
                                Console.Write("Nombre del producto: ");
                                string nombre = Console.ReadLine();

                                if (string.IsNullOrWhiteSpace(nombre))
                                {
                                    throw new Exception("El nombre no puede estar vacío.");
                                }

                                Console.Write("Precio: ");
                                decimal precio = decimal.Parse(Console.ReadLine());

                                if (precio <= 0)
                                {
                                    throw new Exception("El precio debe ser mayor que 0.");
                                }

                                context.Productos.Add(new Producto(nombre, precio));
                                context.SaveChanges();
                                Console.WriteLine("Producto agregado.");
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("El precio debe ser un número.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error: " + ex.Message);
                            }
                            break;

                        case 2:
                            try
                            {
                                List<Producto> lista = context.Productos.ToList();

                                if (lista.Count == 0)
                                {
                                    Console.WriteLine("No hay productos registrados.");
                                }
                                else
                                {
                                    for (int i = 0; i < lista.Count; i++)
                                    {
                                        Console.WriteLine((i + 1) + ". " + lista[i].Nombre + " - $" + lista[i].Precio);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error al consultar productos: " + ex.Message);
                            }
                            break;

                        case 3:
                            Console.WriteLine("Volviendo al menú principal...");
                            break;

                        default:
                            Console.WriteLine("Opción no válida.");
                            break;
                    }

                    if (subOpcion != 3)
                    {
                        Console.WriteLine("\nPresione una tecla para continuar...");
                        Console.ReadKey();
                    }

                } while (subOpcion != 3);

                break;
            }

        case 2:
            {
                int subOpcion = 0;

                do
                {
                    Console.Clear();
                    Console.WriteLine("=== Gestión de clientes ===");
                    Console.WriteLine("1. Agregar cliente");
                    Console.WriteLine("2. Ver clientes");
                    Console.WriteLine("3. Volver");
                    Console.Write("Opción: ");

                    try
                    {
                        subOpcion = int.Parse(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Debes escribir un número.");
                        subOpcion = 0;
                        Console.ReadKey();
                        continue;
                    }

                    switch (subOpcion)
                    {
                        case 1:
                            try
                            {
                                Console.Write("Nombre del cliente: ");
                                string nombre = Console.ReadLine();

                                if (string.IsNullOrWhiteSpace(nombre))
                                {
                                    throw new Exception("El nombre no puede estar vacío.");
                                }

                                Console.Write("Dirección: ");
                                string direccion = Console.ReadLine();

                                if (string.IsNullOrWhiteSpace(direccion))
                                {
                                    throw new Exception("La dirección no puede estar vacía.");
                                }

                                context.Clientes.Add(new Cliente(nombre, direccion));
                                context.SaveChanges();
                                Console.WriteLine("Cliente agregado.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error: " + ex.Message);
                            }
                            break;

                        case 2:
                            try
                            {
                                List<Cliente> lista = context.Clientes.ToList();

                                if (lista.Count == 0)
                                {
                                    Console.WriteLine("No hay clientes registrados.");
                                }
                                else
                                {
                                    for (int i = 0; i < lista.Count; i++)
                                    {
                                        Console.WriteLine((i + 1) + ". " + lista[i].Nombre + " - " + lista[i].Direccion);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error al consultar clientes: " + ex.Message);
                            }
                            break;

                        case 3:
                            Console.WriteLine("Volviendo al menú principal...");
                            break;

                        default:
                            Console.WriteLine("Opción no válida.");
                            break;
                    }

                    if (subOpcion != 3)
                    {
                        Console.WriteLine("\nPresione una tecla para continuar...");
                        Console.ReadKey();
                    }

                } while (subOpcion != 3);

                break;
            }

        case 3:
            {
                try
                {
                    List<Producto> productos = context.Productos.ToList();
                    List<Cliente> clientes = context.Clientes.ToList();

                    if (clientes.Count == 0 || productos.Count == 0)
                    {
                        throw new Exception("Debes registrar al menos un cliente y un producto.");
                    }

                    for (int i = 0; i < clientes.Count; i++)
                    {
                        Console.WriteLine((i + 1) + ". " + clientes[i].Nombre);
                    }
                    Console.Write("Número del cliente: ");
                    int numCliente = int.Parse(Console.ReadLine());

                    if (numCliente < 1 || numCliente > clientes.Count)
                    {
                        throw new Exception("Ese cliente no existe.");
                    }

                    Pedido pedido = new Pedido(clientes[numCliente - 1]);
                    string seguir = "s";

                    while (seguir == "s")
                    {
                        try
                        {
                            for (int i = 0; i < productos.Count; i++)
                            {
                                Console.WriteLine((i + 1) + ". " + productos[i].Nombre + " - $" + productos[i].Precio);
                            }
                            Console.Write("Número del producto: ");
                            int numProducto = int.Parse(Console.ReadLine());

                            if (numProducto < 1 || numProducto > productos.Count)
                            {
                                throw new Exception("Ese producto no existe.");
                            }

                            Console.Write("Cantidad: ");
                            int cantidad = int.Parse(Console.ReadLine());

                            if (cantidad <= 0)
                            {
                                throw new Exception("La cantidad debe ser mayor que 0.");
                            }

                            pedido.AgregarProducto(productos[numProducto - 1], cantidad);
                            Console.WriteLine("Producto agregado al pedido.");
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Debes escribir un número.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                        }

                        Console.Write("¿Agregar otro producto? (s/n): ");
                        seguir = Console.ReadLine().Trim().ToLower();
                    }

                    if (pedido.Items.Count == 0)
                    {
                        Console.WriteLine("El pedido no tiene productos, se cancela.");
                    }
                    else
                    {
                        context.Pedidos.Add(pedido);
                        context.SaveChanges();
                        Console.WriteLine("Pedido #" + pedido.Id + " creado. Total: $" + pedido.Total);
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Debes escribir un número.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                break;
            }

        case 4:
            {
                try
                {
                    List<Pedido> pedidos = context.ObtenerPedidos();

                    if (pedidos.Count == 0)
                    {
                        throw new Exception("No hay pedidos registrados.");
                    }

                    for (int i = 0; i < pedidos.Count; i++)
                    {
                        Console.WriteLine("Pedido #" + pedidos[i].Id + " | " + pedidos[i].Estado + " | $" + pedidos[i].Total);
                    }

                    Console.Write("Número del pedido a modificar: ");
                    int idPedido = int.Parse(Console.ReadLine());

                    int posPedido = -1;
                    for (int i = 0; i < pedidos.Count; i++)
                    {
                        if (pedidos[i].Id == idPedido)
                        {
                            posPedido = i;
                        }
                    }

                    if (posPedido == -1)
                    {
                        throw new Exception("Ese pedido no existe.");
                    }

                    Pedido pedido = pedidos[posPedido];

                    if (pedido.Estado != "Pendiente")
                    {
                        throw new Exception("Solo se pueden modificar pedidos pendientes.");
                    }

                    for (int i = 0; i < pedido.Items.Count; i++)
                    {
                        Console.WriteLine((i + 1) + ". " + pedido.Items[i].Producto.Nombre + " x" + pedido.Items[i].Cantidad);
                    }

                    Console.Write("Número del producto a modificar: ");
                    int numItem = int.Parse(Console.ReadLine());

                    if (numItem < 1 || numItem > pedido.Items.Count)
                    {
                        throw new Exception("Ese producto no está en el pedido.");
                    }

                    Console.Write("Nueva cantidad (0 para quitar el producto): ");
                    int nuevaCantidad = int.Parse(Console.ReadLine());

                    if (nuevaCantidad < 0)
                    {
                        throw new Exception("La cantidad no puede ser negativa.");
                    }

                    ItemPedido item = pedido.Items[numItem - 1];
                    pedido.ModificarCantidad(numItem - 1, nuevaCantidad);

                    if (nuevaCantidad == 0)
                    {
                        context.DetallePedido.Remove(item);
                        Console.WriteLine("Producto eliminado del pedido.");
                    }
                    else
                    {
                        Console.WriteLine("Cantidad actualizada.");
                    }

                    if (pedido.Items.Count == 0)
                    {
                        context.Pedidos.Remove(pedido);
                        context.SaveChanges();
                        Console.WriteLine("El pedido quedó vacío y fue eliminado.");
                    }
                    else
                    {
                        context.SaveChanges();
                        Console.WriteLine("Nuevo total: $" + pedido.Total);
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Debes escribir un número.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                break;
            }

        case 5:
            {
                try
                {
                    List<Pedido> pedidos = context.ObtenerPedidos();

                    if (pedidos.Count == 0)
                    {
                        Console.WriteLine("No hay pedidos registrados.");
                    }
                    else
                    {
                        foreach (Pedido pedido in pedidos)
                        {
                            pedido.MostrarDetalle();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al consultar pedidos: " + ex.Message);
                }
                break;
            }

        case 6:
            {
                try
                {
                    List<Pedido> pedidos = context.ObtenerPedidos();
                    int pendientes = 0;

                    foreach (Pedido pedido in pedidos)
                    {
                        if (pedido.Estado == "Pendiente")
                        {
                            pendientes++;
                            pedido.MostrarDetalle();
                        }
                    }

                    if (pendientes == 0)
                    {
                        Console.WriteLine("No hay pedidos pendientes.");
                    }
                    else
                    {
                        Console.Write("\nNúmero del pedido a marcar como entregado (0 para volver): ");
                        int idEntregado = int.Parse(Console.ReadLine());

                        if (idEntregado != 0)
                        {
                            int pos = -1;
                            for (int i = 0; i < pedidos.Count; i++)
                            {
                                if (pedidos[i].Id == idEntregado)
                                {
                                    pos = i;
                                }
                            }

                            if (pos == -1 || pedidos[pos].Estado != "Pendiente")
                            {
                                throw new Exception("Ese pedido no está pendiente.");
                            }

                            pedidos[pos].Estado = "Entregado";
                            context.SaveChanges();
                            Console.WriteLine("Pedido #" + idEntregado + " marcado como entregado.");
                        }
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Debes escribir un número.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                break;
            }

        case 7:
            Console.WriteLine("Gracias por utilizar el sistema.");
            break;

        default:
            Console.WriteLine("Opcion no valida.");
            break;
    }

    if (options != 7)
    {
        Console.WriteLine("\nPresione una tecla para continuar...");
        Console.ReadKey();
    }

} while (options != 7);

context.Dispose();