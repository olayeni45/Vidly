using AutoMapper;
using Vidly.DTOs;
using Vidly.Models;

namespace Vidly.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Customer, CustomerDTO>();
            Mapper.CreateMap<CustomerDTO, Customer>()
                .ForMember(c => c.Id, opt => opt.Ignore()); ;

            Mapper.CreateMap<Movie, MovieDTO>();
            Mapper.CreateMap<MovieDTO, Movie>()
                .ForMember(m => m.Id, opt => opt.Ignore()); ;
        }
    }
}