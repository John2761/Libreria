using AutoMapper;
using Libreria.Application.DTOs;
using Libreria.Application.Services.Interfaces;
using Libreria.Infraestructure.Models;
using Libreria.Infraestructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Libreria.Application.Services.Implementations
{
    public  class ServiceOrden: IServiceOrden
    {
        private readonly IRepositoryOrden _repositoryOrden;
        private readonly IRepositoryLibro _repositoryLibro;
        private readonly IMapper _mapper;
        public ServiceOrden(IRepositoryOrden repositoryOrden, IRepositoryLibro repositoryLibro, IMapper mapper) { 
            _repositoryOrden= repositoryOrden;
            _repositoryLibro = repositoryLibro;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(OrdenDTO dto)
        {
            // Validar Stock disponible
            foreach (var item in dto.OrdenDetalle)
            {
                var Libro = await _repositoryLibro.FindByIdAsync(item.IdLibro);

                if (Libro.Cantidad - item.Cantidad < 0)
                {
                    throw new Exception($"No hay stock para el Libro {Libro.Nombre}, cantidad en stock {Libro.Cantidad} ");
                }
            }

            var @object = _mapper.Map<Orden>(dto);
            return await _repositoryOrden.AddAsync(@object);
        }

        public async Task<OrdenDTO> FindByIdAsync(int id)
        {
            var @object = await _repositoryOrden.FindByIdAsync(id);
            var objectMapped = _mapper.Map<OrdenDTO>(@object);
            return objectMapped;
        }

        public async Task<OrdenDTO> FindByIdChangeAsync(int id)
        {
            var @object = await _repositoryOrden.FindByIdChangeAsync(id);
            var objectMapped = _mapper.Map<OrdenDTO>(@object);
            return objectMapped;
        }

        public async Task<int> GetNextNumberOrden()
        {
            int nextReceipt = await _repositoryOrden.GetNextNumberOrden();
            return nextReceipt + 1;
        }

        public async Task<ICollection<OrdenDTO>> ListAsync()
        {
            var list = await _repositoryOrden.ListAsync();
            var collection = _mapper.Map<ICollection<OrdenDTO>>(list);
            return collection;
        }
        

    }
}
