
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Security.Cryptography.X509Certificates;


public enum Estado
{
    comenzado,
    enProceso,

    terminado
}

public class Pedidos
{
    public int Nro = 0;
    public string Obs = string.Empty;
    public Clientes Cliente = new Clientes();
    public Estado Estado;
    //aqui vamos a contener la lista de clientes
    public static List<Clientes> ListaClientes { get; set; } = new List<Clientes>();
    public Cadetes? Cadete;

    static string rutaPedidos_cvs = @"C:\Users\diazs\Desktop\taller-2024\taller-2024-tps\data\pedidos.csv";


    
    public Pedidos()
    {
        ListaClientes = AccesoCsv.LeerDatosClientesC();
    }


    //este ctor es para cuando el cliente no esta registrado 
public Pedidos(string obs, string nombreCli, string direccionCli, string telefonoCli, string datosRefCli)
{
    Nro = (AccesoDatos.LectorIds(rutaPedidos_cvs)) + 1;
    Obs = obs;
    Estado = Estado.comenzado;

    // Verificar que los parámetros no sean nulos o vacíos
    if (string.IsNullOrEmpty(nombreCli) || string.IsNullOrEmpty(direccionCli) || string.IsNullOrEmpty(telefonoCli) || string.IsNullOrEmpty(datosRefCli))
    {
        throw new ArgumentException("Los parámetros del cliente no pueden ser nulos o vacíos.");
    }

    Cliente = new Clientes(nombreCli, direccionCli, telefonoCli, datosRefCli);

    // Verificar que Cliente no sea nulo antes de agregarlo a la lista
    if (Cliente != null)
    {
        ListaClientes.Add(Cliente);
    }
    else
    {
        throw new NullReferenceException("El objeto Cliente no ha sido inicializado correctamente.");
    }
}



    //este es para los pedidos que si contienen cliente registrados
    public Pedidos(string obs, string parNombre)
    {
        foreach (Clientes cli in ListaClientes)
        {
            if (cli.Nombre == parNombre)
            {
                Nro = (AccesoDatos.LectorIds(rutaPedidos_cvs)) + 1;

                Cliente = cli;

                Obs = obs;
                Estado = Estado.comenzado;
            }
            else
            {
                Console.WriteLine("no es un cliente registrado");
            }
        }


    }









    // Método para guardar el pedido en un archivo CSV/////////////
 public void GuardarPedido()
{
    // Asegúrate de que la ruta del archivo sea correcta y accesible
    if (string.IsNullOrEmpty(rutaPedidos_cvs))
    {
        throw new ArgumentException("La ruta del archivo no puede ser nula o vacía.");
    }

    // Usa un bloque try-catch para manejar posibles excepciones
    try
    {
        using (StreamWriter sw = new StreamWriter(rutaPedidos_cvs, true))
        {
            string cadeteNombre = Cadete != null ? Cadete.Nombre : "00";
            string linea = $"{Nro},{Obs},{Cliente.Id},{Estado},{cadeteNombre}";

            // Escribe la línea en el archivo
            sw.WriteLine(linea);

            // Línea de depuración
            Console.WriteLine($"Escribiendo línea: {linea}");
        }
    }
    catch (Exception ex)
    {
        // Maneja excepciones (por ejemplo, registra el error)
        Console.WriteLine($"Ocurrió un error al escribir en el archivo: {ex.Message}");
    }
}








    // Método para modificar el estado de un pedido en el archivo CSV
   public static void ModificarPedidosEstado(int nroPedido, Estado nuevoEstado)
{
    string[] lineas = File.ReadAllLines(rutaPedidos_cvs);
    for (int i = 0; i < lineas.Length; i++)
    {
        string[] valores = lineas[i].Split(',');

        if (int.TryParse(valores[0], out int nro))
        {
            if (nro == nroPedido)
            {
                valores[3] = nuevoEstado.ToString();
                lineas[i] = string.Join(",", valores);

                AccesoJson.ModificarEstadosDePedidosJson(nro,nuevoEstado.ToString(), int.Parse(valores[4]) );

                System.Console.WriteLine($"estado modificado a: {nuevoEstado}");
                break;
            }
        }
        
    }
    File.WriteAllLines(rutaPedidos_cvs, lineas);
}







    //asignar pedido a cadete
 public static void AsignarPedidoAcadete(int idCadete, int idPedido)
{
    string[] lineas = File.ReadAllLines(rutaPedidos_cvs);
    for (int i = 0; i < lineas.Length; i++)
    {
        string[] valores = lineas[i].Split(',');

        if (int.TryParse(valores[0], out int idPedidoActual))
        {
            if (idPedidoActual == idPedido)
            {
                valores[4] = idCadete.ToString();
                lineas[i] = string.Join(",", valores);
                System.Console.WriteLine("asignado");
                break;
            }
        }
    }
    File.WriteAllLines(rutaPedidos_cvs, lineas);
}




public   void MostrarListaClientes(){
    System.Console.WriteLine("lista de clientes actaul");
    foreach(var cli in ListaClientes){
        System.Console.WriteLine("*********************************************");
        System.Console.WriteLine($"id:{cli.Id}, Nombre: {cli.Nombre}, telefono: {cli.Telefono}, direccion: {cli.Direccion}  ");
          System.Console.WriteLine("*********************************************");
    }
}






//reasignar pedido a cadete
public  static void ReasignarPedidoaCadete(int nroPedido,int idCadete){
    string[] lineas = File.ReadAllLines(rutaPedidos_cvs);
    for (int i = 0; i < lineas.Length; i++)
    {
        string[] valores = lineas[i].Split(',');

        if (int.TryParse(valores[0], out int nro))
        {
            if (nro == nroPedido)
            {
                valores[4] = idCadete.ToString();
                lineas[i] = string.Join(",", valores);
                System.Console.WriteLine($"pedido Reasigando a cadete: {idCadete}");
                break;
            }
        }
        
    }
    File.WriteAllLines(rutaPedidos_cvs, lineas);

}







}
