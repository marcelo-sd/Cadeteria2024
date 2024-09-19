using AutoMapper;
using Cadeteria2024MD.Models.DTOs;

namespace Cadeteria2024MD.Profiles
{
    public class ProfilesDto : Profile
    {
        protected ProfilesDto()
        {
            CreateMap<Pedidos,PedidosDTO> ().ReverseMap();
        }
    }
}
