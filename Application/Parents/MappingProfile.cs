using System.Linq;
using Application.Parents;
using AutoMapper;
using Domain;

namespace Application.Parents
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Parent, ParentDTO>();
        }
    }
}