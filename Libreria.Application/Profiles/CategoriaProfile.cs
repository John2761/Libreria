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
    public class CategoriaProfile:Profile
    {
        public CategoriaProfile() {
            CreateMap<CategoriaDTO, Categoria>().ReverseMap();
        }
    }
}
