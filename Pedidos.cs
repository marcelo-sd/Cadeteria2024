
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
    public static List<Clientes> ListaClientes { get; set; } = new List<Clientes>();
    public Cadetes? Cadete;
    //aqui vamos a contener la lista de clientes

    static string rutaPedidos_cvs = @"C:\Users\diazs\Desktop\taller-2024\taller-2024-tps\data\pedidos.csv";


    //este ctor es para cuando el cliente no esta registrado 
    public Pedidos(string obs, string nombreCli, string direccionCli, string telefonoCli, string datosRefCli)
    {
        Nro = (AccesoDatos.LectorIds(rutaPedidos_cvs)) + 1;
        Obs = obs;
        Estado = Estado.comenzado;
        Cliente = new Clientes(nombreCli, direccionCli, telefonoCli, datosRefCli);
        ListaClientes.Add(Cliente);

    }

    public Pedidos()
    {

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









    // Método para guardar el pedido en un archivo CSV
    public void GuardarPedido()
    {
        using (StreamWriter sw = new StreamWriter(rutaPedidos_cvs, true))
        {
            string cadeteNombre = Cadete != null ? Cadete.Nombre : "00";
            string linea = $"{Nro},{Obs},{Cliente.Id},{Estado},{cadeteNombre}";
            sw.WriteLine(linea);
        }
    }
    

    // Método para modificar el estado de un pedido en el archivo CSV
    public static void ModificarPedidosEstado( int nroPedido, Estado nuevoEstado)
    {
        string[] lineas = File.ReadAllLines(rutaPedidos_cvs);
        for (int i = 0; i < lineas.Length; i++)
        {
            string[] valores = lineas[i].Split(',');
            if (Convert.ToInt32(valores[0]) == nroPedido)
            {
                valores[3] = nuevoEstado.ToString();
                lineas[i] = string.Join(",", valores);
                break;
            }
        }
        File.WriteAllLines(rutaPedidos_cvs, lineas);
    }
    //metodo para borrar pedido

    //asignar pedido a cadete
public  static void AsignarPedidoAcadete(int idCadete,int idPedido){
    string[] lineas = File.ReadAllLines(rutaPedidos_cvs);
      for (int i = 0; i < lineas.Length; i++)
        {
            string[] valores = lineas[i].Split(',');
            if (Convert.ToInt32(valores[0]) == idCadete)
            {
                valores[4] = idPedido.ToString();
                lineas[i] = string.Join(",", valores);
                break;
            }
        }
                File.WriteAllLines(rutaPedidos_cvs, lineas);




}













}
