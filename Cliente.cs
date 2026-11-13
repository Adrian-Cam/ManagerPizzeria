using System;
using System.Collections.Generic;

class Cliente
{
    public string Nombre { get; set; }
    public string Direccion { get; set; }

    public Cliente(string nombre, string direccion)
    {
        Nombre = nombre;
        Direccion = direccion;
    }
}
