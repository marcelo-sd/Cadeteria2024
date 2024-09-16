using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]


public class CadeteriaController : ControllerBase
{
    // GET: api/productos
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

      

}