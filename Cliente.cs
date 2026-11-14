class Cliente
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public string Direccion { get; set; } = "";

    public Cliente()
    {
    }

    public Cliente(string nombre, string direccion)
    {
        Nombre = nombre;
        Direccion = direccion;
    }
}