namespace Cadeteria2024MD.Models.Accesos_ClasesDeaDatos
{
    public class AccesoDatosClientes
    {
        protected static string rutaClientes_csv = @"C:\Users\diazs\Desktop\proyectos-c#\c#-vstudio\Cadeteria2024MD\Data\clientes.csv";

        //leer clientes y devolver lista
        public List<Clientes> ObtenerListaClientes()
        {
            List<Clientes> listaClientes = new List<Clientes>();

            using (StreamReader sr = new StreamReader(rutaClientes_csv))
            {
                string linea;
                while ((linea = sr.ReadLine()) != null)
                {
                    string[] valores = linea.Split(',');
                    if (Guid.TryParse(valores[0], out Guid IDcli))
                    {

                        Guid clienteId = IDcli;
                        Clientes cliente = new Clientes
                        {
                            Id = clienteId,
                            Nombre = valores[1],
                            Direccion = valores[2],
                            Telefono = valores[3]
                        };

                        listaClientes.Add(cliente);
                    }
                    else
                    {
                        throw new ArgumentException("no se pudo reconocer el id del cliente");
                    }



                }
            }

            return listaClientes.Count > 0 ? listaClientes : null;
        }



    }
}
