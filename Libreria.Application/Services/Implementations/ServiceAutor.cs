using AutoMapper;
using Libreria.Application.DTOs;
using Libreria.Application.Services.Interfaces;
using Libreria.Infraestructure.Models;
using Libreria.Infraestructure.Repository.Implementations;
using Libreria.Infraestructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Application.Services.Implementations
{
    public class ServiceAutor : IServiceAutor
    {
        private readonly IRepositoryAutor _repository;
        private readonly IMapper _mapper;

        public ServiceAutor(IRepositoryAutor repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AutorDTO> FindByIdAsync(int id)
        {
            var @object = await _repository.FindByIdAsync(id);
            var objectMapped = _mapper.Map<AutorDTO>(@object);
            return objectMapped;
        }

        public async Task<ICollection<AutorDTO>> ListAsync()
        {
            //Obtener datos del repositorio
            var list = await _repository.ListAsync();
            // Map List<Autor> a ICollection<BodegaDTO>
            var collection = _mapper.Map<ICollection<AutorDTO>>(list);
            // Return lista
            return collection;
        }
    }
    
}
