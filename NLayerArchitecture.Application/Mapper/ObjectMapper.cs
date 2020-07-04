using AutoMapper;
using NLayerArchitecture.Application.Models;
using NLayerArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NLayerArchitecture.Application.Mapper
{
    public static class ObjectMapper
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                // This line ensures that internal properties are also mapped over.
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<AspnetRunDtoMapper>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });
        public static IMapper Mapper => Lazy.Value;
    }

    public class AspnetRunDtoMapper : Profile
    {
        public AspnetRunDtoMapper()
        {
            CreateMap<Product, ProductModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name)).ReverseMap();

            CreateMap<Category, CategoryModel>().ReverseMap();
        }
    }
}
