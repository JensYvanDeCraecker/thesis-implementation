using System.Linq;
using AutoMapper;
using ParcelTracker.Models.Database;
using ParcelTracker.Models.Response;

namespace ParcelTracker.Models
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<State, StateResponseModel>();
            CreateMap<ParcelState, ParcelStateResponseModel>()
                .ForMember(s => s.Name, opts => opts.MapFrom(s => s.State.Name))
                .ForMember(s => s.Description, opts => opts.MapFrom(s => s.State.Description));
            CreateMap<Address, AddressResponseModel>();
            CreateMap<Parcel, ParcelResponseModel>().ForMember(p => p.States, opts => opts.MapFrom(p => p.ParcelStates.OrderBy(s => s.Created)));
        }
    }
}