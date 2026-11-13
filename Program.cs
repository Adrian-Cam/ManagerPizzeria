List<string> nameProducts = new List<string>();
List<decimal> priceProducts = new List<decimal>();

List<string> nameCustomers = new List<string>();
List<string> addressCustomers = new List<string>();

List<int> idsOrders = new List<int>();
List<int> customerOrders = new List<int>();
List<string> statusOrders = new List<string>();
List<decimal> totalOrders = new List<decimal>();

List<int> detOrderIds = new List<int>();
List<int> detProductIdx = new List<int>();
List<int> detQuantities = new List<int>();

int nextOrderId = 1;
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

                                nameProducts.Add(nombre);
                                priceProducts.Add(precio);
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
                            if (nameProducts.Count == 0)
                            {
                                Console.WriteLine("No hay productos registrdos.");
                            }
                            else
                            {
                                for (int i = 0; i < nameProducts.Count; i++)
                                {
                                    Console.WriteLine((i + 1) + ". " + nameProducts[i] + " - $" + priceProducts[i]);
                                }
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
                        Console.WriteLine("\nPresione una tecla para continaur...");
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

                                nameCustomers.Add(nombre);
                                addressCustomers.Add(direccion);
                                Console.WriteLine("Cliente agregado.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error: " + ex.Message);
                            }
                            break;

                        case 2:
                            if (nameCustomers.Count == 0)
                            {
                                Console.WriteLine("No hay clientes registrados.");
                            }
                            else
                            {
                                for (int i = 0; i < nameCustomers.Count; i++)
                                {
                                    Console.WriteLine((i + 1) + ". " + nameCustomers[i] + " - " + addressCustomers[i]);
                                }
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
                    if (nameCustomers.Count == 0 || nameProducts.Count == 0)
                    {
                        throw new Exception("Debes registrar al menos un cliente y un producto.");
                    }

                    for (int i = 0; i < nameCustomers.Count; i++)
                    {
                        Console.WriteLine((i + 1) + ". " + nameCustomers[i]);
                    }
                    Console.Write("Número del cliente: ");
                    int cliente = int.Parse(Console.ReadLine());

                    if (cliente < 1 || cliente > nameCustomers.Count)
                    {
                        throw new Exception("Ese cliente no existe.");
                    }

                    decimal total = 0;
                    int items = 0;
                    string seguir = "s";

                    while (seguir == "s")
                    {
                        try
                        {
                            for (int i = 0; i < nameProducts.Count; i++)
                            {
                                Console.WriteLine((i + 1) + ". " + nameProducts[i] + " - $" + priceProducts[i]);
                            }
                            Console.Write("Número del producto: ");
                            int prod = int.Parse(Console.ReadLine());

                            if (prod < 1 || prod > nameProducts.Count)
                            {
                                throw new Exception("Ese producto no existe.");
                            }

                            Console.Write("Cantidad: ");
                            int cantidad = int.Parse(Console.ReadLine());

                            if (cantidad <= 0)
                            {
                                throw new Exception("La cantidad debe ser mayor que 0.");
                            }

                            detOrderIds.Add(nextOrderId);
                            detProductIdx.Add(prod - 1);
                            detQuantities.Add(cantidad);
                            total += priceProducts[prod - 1] * cantidad;
                            items++;
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

                    if (items == 0)
                    {
                        Console.WriteLine("El pedido no tiene productos, se cancela.");
                    }
                    else
                    {
                        idsOrders.Add(nextOrderId);
                        customerOrders.Add(cliente - 1);
                        statusOrders.Add("Pendiente");
                        totalOrders.Add(total);
                        Console.WriteLine("Pedido #" + nextOrderId + " creado. Total: $" + total);
                        nextOrderId++;
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
                    if (idsOrders.Count == 0)
                    {
                        throw new Exception("No hay pedidos registrados.");
                    }

                    for (int i = 0; i < idsOrders.Count; i++)
                    {
                        Console.WriteLine("Pedido #" + idsOrders[i] + " | " + statusOrders[i] + " | $" + totalOrders[i]);
                    }

                    Console.Write("Número del pedido a modificar: ");
                    int idPedido = int.Parse(Console.ReadLine());
                    int posPedido = idsOrders.IndexOf(idPedido);

                    if (posPedido == -1)
                    {
                        throw new Exception("Ese pedido no existe.");
                    }
                    if (statusOrders[posPedido] != "Pendiente")
                    {
                        throw new Exception("Solo se pueden modificar pedidos pendientes.");
                    }

                    List<int> posiciones = new List<int>();
                    for (int i = 0; i < detOrderIds.Count; i++)
                    {
                        if (detOrderIds[i] == idPedido)
                        {
                            posiciones.Add(i);
                            Console.WriteLine(posiciones.Count + ". " + nameProducts[detProductIdx[i]] + " x" + detQuantities[i]);
                        }
                    }

                    Console.Write("Número del producto a modificar: ");
                    int numItem = int.Parse(Console.ReadLine());

                    if (numItem < 1 || numItem > posiciones.Count)
                    {
                        throw new Exception("Ese producto no está en el pedido.");
                    }

                    Console.Write("Nueva cantidad (0 para quitar el producto): ");
                    int nuevaCantidad = int.Parse(Console.ReadLine());

                    if (nuevaCantidad < 0)
                    {
                        throw new Exception("La cantidad no puede ser negativa.");
                    }

                    int posDetalle = posiciones[numItem - 1];

                    if (nuevaCantidad == 0)
                    {
                        detOrderIds.RemoveAt(posDetalle);
                        detProductIdx.RemoveAt(posDetalle);
                        detQuantities.RemoveAt(posDetalle);
                        Console.WriteLine("Producto eliminado del pedido.");
                    }
                    else
                    {
                        detQuantities[posDetalle] = nuevaCantidad;
                        Console.WriteLine("Cantidad actualizada.");
                    }

                    decimal nuevoTotal = 0;
                    int itemsRestantes = 0;
                    for (int i = 0; i < detOrderIds.Count; i++)
                    {
                        if (detOrderIds[i] == idPedido)
                        {
                            nuevoTotal += priceProducts[detProductIdx[i]] * detQuantities[i];
                            itemsRestantes++;
                        }
                    }

                    if (itemsRestantes == 0)
                    {
                        idsOrders.RemoveAt(posPedido);
                        customerOrders.RemoveAt(posPedido);
                        statusOrders.RemoveAt(posPedido);
                        totalOrders.RemoveAt(posPedido);
                        Console.WriteLine("El pedido quedó vacío y fue eliminado.");
                    }
                    else
                    {
                        totalOrders[posPedido] = nuevoTotal;
                        Console.WriteLine("Nuevo total: $" + nuevoTotal);
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
                if (idsOrders.Count == 0)
                {
                    Console.WriteLine("No hay pedidos registrados.");
                }
                else
                {
                    for (int i = 0; i < idsOrders.Count; i++)
                    {
                        Console.WriteLine("Pedido #" + idsOrders[i] + " | Cliente: " + nameCustomers[customerOrders[i]] + " | " + statusOrders[i]);
                        for (int j = 0; j < detOrderIds.Count; j++)
                        {
                            if (detOrderIds[j] == idsOrders[i])
                            {
                                Console.WriteLine("   " + nameProducts[detProductIdx[j]] + " x" + detQuantities[j]);
                            }
                        }
                        Console.WriteLine("   Total: $" + totalOrders[i]);
                    }
                }
                break;
            }

        case 6:
            {
                int pendientes = 0;

                for (int i = 0; i < idsOrders.Count; i++)
                {
                    if (statusOrders[i] == "Pendiente")
                    {
                        pendientes++;
                        Console.WriteLine("Pedido #" + idsOrders[i] + " | Cliente: " + nameCustomers[customerOrders[i]]);
                        for (int j = 0; j < detOrderIds.Count; j++)
                        {
                            if (detOrderIds[j] == idsOrders[i])
                            {
                                Console.WriteLine("   " + nameProducts[detProductIdx[j]] + " x" + detQuantities[j]);
                            }
                        }
                        Console.WriteLine("   Total: $" + totalOrders[i]);
                    }
                }

                if (pendientes == 0)
                {
                    Console.WriteLine("No hay pedidos pendientes.");
                }
                else
                {
                    try
                    {
                        Console.Write("\nNúmero del pedido a marcar como entregado (0 para volver): ");
                        int idEntregado = int.Parse(Console.ReadLine());

                        if (idEntregado != 0)
                        {
                            int pos = idsOrders.IndexOf(idEntregado);
                            if (pos == -1 || statusOrders[pos] != "Pendiente")
                            {
                                throw new Exception("Ese pedido no está pendiente.");
                            }
                            statusOrders[pos] = "Entregado";
                            Console.WriteLine("Pedido #" + idEntregado + " marcado como entregado.");
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