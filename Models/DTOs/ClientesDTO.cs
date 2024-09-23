using Cadeteria2024MD.Models.Interfaces;

namespace Cadeteria2024MD.Models.DTOs
{
    public class ClientesDTO:Icliente
    {
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string DatosReferenciaDrieccion { get; set; }
    }
}
