namespace Cadeteria2024MD.Models.Accesos_ClasesDeaDatos
{
    public class AccesoDatosCadetes
    {
        private string rutaCadeteCsv = @"C:\Users\diazs\Desktop\proyectos-c#\c#-vstudio\Cadeteria2024MD\Data\cadetes.csv";













        //leer cadetes y devolver lista
        public List<Cadetes> ObtenerListaCadetes()
        {
            List<Cadetes> listaCadetes = new List<Cadetes>();

            using (StreamReader sr = new StreamReader(rutaCadeteCsv))
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






     
    }
}
