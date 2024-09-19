using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Cadeteria2024MD.Controllers
{
    public class CadeteriaController : Controller
    {
        [HttpGet("ListaPedidos")]
        public ActionResult<List<PedidoIntermedio>> GetPedidos()
        {
            // Aquí deberías obtener la lista de pedidos desde tu fuente de datos
            List<PedidoIntermedio> listaPedidos = AccesoJson.LeerDatosPedidosIntermedioJ();

            if (listaPedidos == null || listaPedidos.Count == 0)
            {
                return NotFound("No se encontraron pedidos.");
            }

            return Ok(listaPedidos);
        }



        [HttpGet("ListaCadetes")]
        public ActionResult<List<Cadetes>> GetCadetes() { 
        List<Cadetes> ListaCadetes = AccesoCsv.LeerDatosCadetesC() ;
            return Ok(ListaCadetes);
        }


    }
}
