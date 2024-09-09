public class AccesoDatos{
    
    public static string rutaCadeteFile=@"C:\Users\diazs\Desktop\taller-2024\taller-2024-tps\data\cadetes.csv";

    private static string rutaPedidos_csv=@"C:\Users\diazs\Desktop\taller-2024\taller-2024-tps\data\pedidos.csv";

public AccesoDatos()
{
    
}



//leer pedidos y devolver lista
public static List<Pedidos> LeerDatosPedidos( )
    {
        List<Pedidos> listaPedidos = new List<Pedidos>();

        using (StreamReader sr = new StreamReader(rutaPedidos_csv))
        {
            string linea;
            while ((linea = sr.ReadLine()) != null)
            {
                string[] valores = linea.Split(',');

                Pedidos pedido = new Pedidos
                {
                    
                    Nro = Convert.ToInt32(valores[0]),
                    Obs = valores[1],
                    Cliente = new Clientes { Id = Guid.Parse(valores[2]) }, 
                    Estado = (Estado)Enum.Parse(typeof(Estado), valores[3]),
                    Cadete = valores.Length > 4 ? new Cadetes { Id = Convert.ToInt32(valores[4])} : null 
                };

                listaPedidos.Add(pedido);
            }
        }

        return listaPedidos;
    }




    //leer cadetes y devolver lista
public static  List<Cadetes> LeerDatosCadetes()
    {
        List<Cadetes> listaCadetes = new List<Cadetes>();

        using (StreamReader sr = new StreamReader(AccesoDatos.rutaCadeteFile))
        {
            string linea;
            while ((linea = sr.ReadLine()) != null)
            {
                string[] valores = linea.Split(',');

                Cadetes ca = new Cadetes
                {
                    Id = Convert.ToInt32(valores[0]),
                    Nombre = valores[1],
                    Direccion = valores[2], 
                    Telefono = valores[3]
                 
                };

                listaCadetes.Add(ca);
            }
        }

        return listaCadetes;
    }


 public static int LectorIds(string rutaArchivo)
{
    int ultimoId = 0;
    string[] lineas = File.ReadAllLines(rutaArchivo);

    // Verificar si el archivo no está vacío
    if (lineas.Length > 0)
    {
        // Obtener la última línea del archivo
        string ultimaLinea = lineas[lineas.Length - 1];

        // Verificar si la última línea no está vacía
        if (!string.IsNullOrWhiteSpace(ultimaLinea))
        {
            // Dividir la última línea en valores
            string[] valores = ultimaLinea.Split(',');

            // Verificar si el primer valor es un número válido
            if (valores.Length > 0 && int.TryParse(valores[0], out int id))
            {
                ultimoId = id;
            }
        }
    }

    return ultimoId;
}




}