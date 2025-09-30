using AutoMapper;
using Libreria.Application.DTOs;
using Libreria.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Application.Profiles
{
    public class LibroProfile: Profile
    {
        public LibroProfile() {
            CreateMap<LibroDTO, Libro>().ReverseMap();

            CreateMap<LibroDTO, Libro>()
           .ForMember(dest => dest.IdLibro, orig => orig.MapFrom(o => o.IdLibro))
           .ForMember(dest => dest.Isbn, orig => orig.MapFrom(o => o.Isbn))
           .ForMember(dest => dest.IdAutor, orig => orig.MapFrom(o => o.IdAutor))
           .ForMember(dest => dest.Nombre, orig => orig.MapFrom(o => o.Nombre))
           .ForMember(dest => dest.Precio, orig => orig.MapFrom(o => o.Precio))
           .ForMember(dest => dest.Cantidad, orig => orig.MapFrom(o => o.Cantidad))
           .ForMember(dest => dest.Imagen, orig => orig.MapFrom(o => o.Imagen))
            .ForMember(dest => dest.IdAutorNavigation, orig => orig.MapFrom(o => o.IdAutorNavigation))
            .ForMember(dest => dest.OrdenDetalle, orig => orig.MapFrom(o => o.OrdenDetalle))
            .ForMember(dest => dest.IdCategoria, orig => orig.MapFrom(o => o.IdCategoria));


        }
        
    }
}
