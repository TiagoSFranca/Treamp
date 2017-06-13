using AutoMapper;
using Presentation.Models;
using Presentation.Models.ViewModels;

namespace WebService.Mappers
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            //<ORIGEM, DESTINO>
            Mapper.Initialize(cfg =>
            {



                cfg.CreateMap<AddressViewModel, Address>().ReverseMap();
                cfg.CreateMap<AddressViewItem, Address>().ReverseMap();
                cfg.CreateMap<AddressViewRegister, Address>().ReverseMap();

                cfg.CreateMap<DestinationViewModel, Destination>().ReverseMap();
                cfg.CreateMap<DestinationViewItem, Destination>().ReverseMap();

                cfg.CreateMap<TypeCostViewModel, TypeCost>().ReverseMap();
                cfg.CreateMap<TypeCostViewItem, TypeCost>().ReverseMap();

                cfg.CreateMap<CostViewModel, Cost>().ReverseMap();
                cfg.CreateMap<CostViewItem, Cost>().ReverseMap();

                cfg.CreateMap<UserViewModel, User>().ReverseMap();
                cfg.CreateMap<UserViewItem, User>().ReverseMap();
                cfg.CreateMap<UserViewRegister, User>().ReverseMap();
                cfg.CreateMap<UserViewEdit, User>().ReverseMap();

                cfg.CreateMap<TypeAccountViewModel, TypeAccount>().ReverseMap();
                cfg.CreateMap<TypeAccountViewItem, TypeAccount>().ReverseMap();

                cfg.CreateMap<BankAccountViewModel, BankAccount>().ReverseMap();
                cfg.CreateMap<BankAccountViewItem, BankAccount>().ReverseMap();

                cfg.CreateMap<PhoneViewModel, Phone>().ReverseMap();
                cfg.CreateMap<PhoneViewItem, Phone>().ReverseMap();

                cfg.CreateMap<NotificationViewModel, Notification>().ReverseMap();
                cfg.CreateMap<NotificationViewItem, Notification>().ReverseMap();

                cfg.CreateMap<ContactViewModel, Contact>().ReverseMap();
                cfg.CreateMap<ContactViewItem, Contact>().ReverseMap();

                cfg.CreateMap<TravelViewCreate, Travel>().ReverseMap();
                cfg.CreateMap<TravelViewModel, Travel>().ReverseMap();
                cfg.CreateMap<TravelViewItem, Travel>().ReverseMap();

                cfg.CreateMap<TravelUserViewCreate, TravelUser>().ReverseMap();
                cfg.CreateMap<TravelUserViewItem, TravelUser>().ReverseMap();
                cfg.CreateMap<TravelUserViewModel, TravelUser>().ReverseMap();

            });
        }
    }
}