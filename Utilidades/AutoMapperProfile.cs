using AutoMapper;
using Market.DTOs;
using Market.Models;

namespace Market.Utilidades
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            
            CreateMap<OperationDTO, Operation>();
            CreateMap<Operation, OptenerOpDTO>();
        }
    }
}