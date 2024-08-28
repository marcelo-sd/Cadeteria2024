using System.Runtime.InteropServices;
using static System.Console;
using Colorful;
using System.Drawing;




ForegroundColor = ConsoleColor.Green; // Cambia el color del texto a rojo


/*  Clientes cliente=new Clientes("mimo","lasteia","4234332","casa-fea");
WriteLine("esta hecho supuestamente");
string[] lineas=File.ReadAllLines("C:\\Users\\diazs\\Desktop\\taller-2024\\tps\\taller-001\\clientes.csv"); */

/* foreach(var linea in lineas){
    var valores=linea.Split(",");
    WriteLine("es:"+valores[0]+" lo otro: "+valores[1]+" es "+valores[2]+" es "+valores[4]+" es "+valores[5]);

} 
 */

Cadetes cade1 =new Cadetes("lino","eeuu 44","34545");
cade1.DarDeAltaPedido();






/* bool continuar = true;
while (continuar)
{
    WriteLine("1_ Dar de alta un pedido");
    WriteLine("2_ Lista de clientes");
    WriteLine("3_ Salir");
    WriteLine("Selecciona una opción");
    string? opcion = ReadLine();

    switch (opcion)
    {
        case "1":
            WriteLine("Se seleccionó la opción 1");
            break;
        case "2":
            WriteLine("Se seleccionó la opción 2");
            break;
        case "3":
            WriteLine("Se seleccionó la opción 3");
            continuar = false;
            break;
          default:
            WriteLine("Selecciona una opción correcta");   
         break;
    }
}
 */


public class Interfaz{
Pedidos? PedidoA;


public void DarDeAltaPedido(string obs,string nombreCli, string direccionCli, string telefonoCli,string datosRefCli)
{
PedidoA=new Pedidos(obs,nombreCli,direccionCli,telefonoCli,datosRefCli);    
}

}



