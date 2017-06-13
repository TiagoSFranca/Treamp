using AutoMapper;
using WebService.Models;
using WebService.Models.ViewModels;

namespace WebService.Mappers
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            //<ORIGEM, DESTINO>
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<StateViewItem, State>().ReverseMap();


                cfg.CreateMap<CityViewModel, City>().ReverseMap();
                cfg.CreateMap<CityViewItem, City>().ReverseMap();
            });
        }
    }
}