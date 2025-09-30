using AutoMapper;
using Libreria.Application.DTOs;
using Libreria.Application.Services.Interfaces;
using Libreria.Infraestructure.Models;
using Libreria.Infraestructure.Repository.Implementations;
using Libreria.Infraestructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Libreria.Application.Services.Implementations
{
    public class ServiceLibro : IServiceLibro
    {
        private readonly IRepositoryLibro _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<ServiceLibro> _logger;
        public ServiceLibro(IRepositoryLibro repository, IMapper mapper,
                            ILogger<ServiceLibro> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<int> AddAsync(LibroDTO dto, string[] selectedCategorias)
        {
            // Map LibroDTO to Libro
            var objectMapped = _mapper.Map<Libro>(dto);

            // Return
            return await _repository.AddAsync(objectMapped, selectedCategorias);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<ICollection<LibroDTO>> FindByNameAsync(string nombre)
        {
            var list = await _repository.FindByNameAsync(nombre);

            var collection = _mapper.Map<ICollection<LibroDTO>>(list);

            return collection;

        }

        public async Task<LibroDTO> FindByIdAsync(int id)
        {
            var @object = await _repository.FindByIdAsync(id);
            var objectMapped = _mapper.Map<LibroDTO>(@object);
            return objectMapped;
        }

        public async Task<ICollection<LibroDTO>> ListAsync()
        {
            // Get data from Repository
            var list = await _repository.ListAsync();
            // Map List<Libro> to ICollection<LibroDTO>
            var collection = _mapper.Map<ICollection<LibroDTO>>(list);
            // Return Data
            return collection;
        }

        public async Task UpdateAsync(int id, LibroDTO dto, string[] selectedCategorias)
        {
            //Obtenga el modelo original a actualizar
            var @object = await _repository.FindByIdAsync(id);
            //       source, destination
            var entity = _mapper.Map(dto, @object!);


            await _repository.UpdateAsync(entity, selectedCategorias);
        }


        public async Task<ICollection<LibroDTO>> GetLibroByCategoria(int IdCategoria)
        {
            // Get data from Repository
            var list = await _repository.GetLibroByCategoria(IdCategoria);
            // Map List<Libro> to ICollection<LibroDTO>
            var collection = _mapper.Map<ICollection<LibroDTO>>(list);
            // Return Data
            return collection;
        }
       
    }
}
