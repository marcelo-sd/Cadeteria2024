using AutoMapper;
using Cadeteria2024MD.Models.DTOs;
using Cadeteria2024MD.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Cadeteria2024MD.Controllers
{
    public class CadeteriaController : Controller
    {

        private readonly IMapper _mapper;
        private readonly Cadeteria _cade;

        public CadeteriaController(IMapper mapper, ILogger<Cadeteria> logger)
        {
            _mapper = mapper;
            _cade = new Cadeteria(logger);
        }




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
        public ActionResult<List<Cadetes>> GetCadetes()
        {
            List<Cadetes> ListaCadetes = AccesoCsv.LeerDatosCadetesC();
            return Ok(ListaCadetes);
        }




        [HttpPost("GuardarPedido")]
        public ActionResult AgregarPedido([FromBody] PedidosDTO ped)
        {
            if (ped == null)
            {
                return BadRequest("El pedido es nulo");
            }

            Cadeteria Cad = new Cadeteria();

            // Verifica si ped.Cliente es de la instancia ClientesDTO
            if (ped.Cliente is ClientesDTO clienteCompleto)
            {
                bool result = Cad.DarDeAltaPedido(ped.Obs, clienteCompleto.Nombre, clienteCompleto.Direccion,
                                                  clienteCompleto.Telefono, clienteCompleto.DatosReferenciaDrieccion);

                if (result)
                {
                    return Ok("El pedido se guardó correctamente");
                }
                else
                {
                    return BadRequest("No se pudo guardar el pedido");
                }
            }
            else
            {
                return BadRequest("El cliente no tiene la información completa.");
            }
        }



        // result = Cad.DarDeAltaPedido(ped.Obs, ped.Cliente.Nombre, ped.Cliente.Direccion,
        //                                 ped.Cliente.Telefono, ped.Cliente.DatosReferenciaDrieccion);



        [HttpPost("GuardarPedidoConClienteRegistrado")]
        public ActionResult AgregarPedidoConCliRe([FromBody] PedidosDTO ped)
        {
            bool result = false;

            if (ped == null)
            {
                return BadRequest("El pedido es nulo");
            }


            result = _cade.DarDeAltaPedido(ped.Obs, ped.Cliente.Nombre);



            if (result)
            {

                return Ok("el pedido se guardo correctamente");
            }
            else
            {
                return BadRequest("no se pudo guardar el pedidos");
            }


        }





        [HttpPut("AsiganarCadete")]
        public ActionResult AsignarCadeteAPedidos(int idcadete, int idPedido)
        {
            bool res = _cade.AsignarCadeteAPedido(idcadete, idPedido);



            if (res)
            {
                return Ok("se asigno el pedido al cadete");
            }
            else
            {
                return BadRequest("no se puedo asignar el cadete al pedido");
            }
        }


        [HttpPut("CambiarEstadoPedido")]
        public ActionResult CambiarEstadoPedido(int idPedido)
        {
            bool res = _cade.CambiarEstadoPedido(idPedido);



            if (res)
            {
                return Ok("se cambio el estado del pedidos  correctamente");
            }
            else
            {
                return BadRequest("no ses pudo cambiar el estado del pedido");
            }
        }



        [HttpPut("ReasignarCadete")]
        public ActionResult ReasignarCadete(int idPedido, int idcadete, int idCadAnterior)
        {
            bool res = _cade.ResignarPedidoAcadete(idPedido, idcadete, idCadAnterior);



            if (res)
            {
                return Ok("el pedido se reasigno correctamente");
            }
            else
            {
                return BadRequest("no se pudo reasignar el pedido");
            }
        }




        [HttpGet("Informe")]
        public ActionResult Informe()
        {
            var ObjRes = _cade.InformeIntegral();

            return Ok(ObjRes);

        }


    }
}
