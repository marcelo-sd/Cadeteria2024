using System.IO;

public class Clientes
{
   public Guid  Id { get; set; }
   public string Nombre { get; set; }
   public string Direccion { get; set; }
   public string Telefono {  get; set; }
   public string DatosReferenciaDrieccion { get; set; }


   public Clientes(string nombre, string direccion, string telefono, string datosRef)
   {
     Id=Guid.NewGuid();
      Nombre = nombre;
      Direccion = direccion;
      Telefono = telefono;
      DatosReferenciaDrieccion = datosRef;
   GuardarCliente(nombre,direccion,telefono,datosRef);
   }
   public Clientes()
   {
      
   }
   









   public void GuardarCliente(string nombre,string direccion, string telefono, string datoDeREferencia)
   {

         string path = @"C:\Users\diazs\Desktop\proyectos-c#\c#-vstudio\Cadeteria2024MD\Data\clientes.csv";

try
     {
        using (StreamWriter file = new StreamWriter(path, true))
        {
            file.WriteLine($"{Id},{nombre},{direccion},{telefono},{datoDeREferencia}");
        }
    }
      catch (Exception ex)
      {
         throw new ApplicationException("there is a problem", ex);

      }
   }

}




