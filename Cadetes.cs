


public class Cadetes
{

    public int Id = 0;
    public string Nombre;
    string Direccion;
    public string Telefono;




    public Cadetes(string nombre, string direccion, string telefono, List<Cadetes> lisCadetes)
    {
        if (lisCadetes == null || lisCadetes.Count == 0)
        {
            Id = 1;
        }
        else
        {
            Id = lisCadetes.Count + 1;
        }
        Nombre = nombre;
        Direccion = direccion;
        Telefono = telefono;

    }

    

    
}
