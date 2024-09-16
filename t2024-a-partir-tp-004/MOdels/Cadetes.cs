

public class Cadetes
{

    public int Id = 0;
    public string Nombre;
     public string Direccion;
    public string Telefono;

    private string rutaCadete_cvs = @"C:\Users\diazs\Desktop\taller-2024\taller-2024-tps\data\cadetes.csv";




    public Cadetes(string nombre, string direccion, string telefono)
    {
        Id = (AccesoDatos.LectorIds(rutaCadete_cvs))+1;
        Nombre = nombre;
        Direccion = direccion;
        Telefono = telefono;

    }

    public Cadetes()
    {

    }

    // Método para guardar el Cadete en un archivo CSV
    public void GuardarCadeteCsv()
    {
        using (StreamWriter sw = new StreamWriter(rutaCadete_cvs, true))
        {
            string linea = $"{Id},{Nombre},{Direccion},{Telefono}";
            sw.WriteLine(linea);
        }
    }



}
