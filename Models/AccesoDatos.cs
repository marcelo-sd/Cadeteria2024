
public class AccesoDatos
{

    protected static string rutaCadeteFile = @"C:\Users\diazs\Desktop\proyectos-c#\c#-vstudio\Cadeteria2024MD\Data\cadetes.csv";

    protected static string rutaPedidos_csv = @"C:\Users\diazs\Desktop\proyectos-c#\c#-vstudio\Cadeteria2024MD\Data\pedidos.csv";

    protected static string rutaClientes_csv = @"C:\Users\diazs\Desktop\proyectos-c#\c#-vstudio\Cadeteria2024MD\Data\clientes.csv";

     protected static string rutaPedidos_json = @"C:\Users\diazs\Desktop\proyectos-c#\c#-vstudio\Cadeteria2024MD\Data\Json\pedidos.json";

    public AccesoDatos()
    {

    }


public static int LectorIds(string rutaArchivo)
{
    int ultimoId = 0;

    // Verificar si la ruta del archivo es válida
    if (string.IsNullOrEmpty(rutaArchivo))
    {
        throw new ArgumentException("La ruta del archivo no puede ser nula o vacía.");
    }

    try
    {
        string[] lineas = File.ReadAllLines(rutaArchivo);

        // Verificar si el archivo no está vacío
        if (lineas.Length > 0)
        {
            // Obtener la última línea no vacía del archivo
            for (int i = lineas.Length - 1; i >= 0; i--)
            {
                string ultimaLinea = lineas[i];

                if (!string.IsNullOrWhiteSpace(ultimaLinea))
                {
                    // Dividir la última línea en valores
                    string[] valores = ultimaLinea.Split(',');

                    // Verificar si el primer valor es un número válido
                    if (valores.Length > 0 && int.TryParse(valores[0], out int id))
                    {
                        ultimoId = id;
                        break;
                    }
                }
            }
        }
    }
    catch (Exception ex)
    {
        // Manejar excepciones (por ejemplo, registrar el error)
        throw new ArgumentException("error",ex);
    }

    return ultimoId;
}




}
