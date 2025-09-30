using Libreria.Application.DTOs;
using Libreria.Application.Services.Interfaces;
using Libreria.Infraestructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Libreria.Web.Controllers
{
    [Authorize]
    public class OrdenController : Controller
    {
        private readonly IServiceLibro _serviceLibro;
        private readonly IServiceOrden _serviceOrden;
        private readonly IServiceCliente _serviceCliente;
        public OrdenController(IServiceLibro serviceLibro,
                             IServiceOrden serviceOrden,
                             IServiceCliente serviceCliente)
        {
            _serviceLibro = serviceLibro;
            _serviceOrden = serviceOrden;
            _serviceCliente = serviceCliente;
        }
        // GET: OrdenController
        public async Task<ActionResult> Index()
        {
            var collection = await _serviceOrden.ListAsync();
            return View(collection);
        }
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                var @object = await _serviceOrden.FindByIdAsync(id.Value);
                if (@object == null)
                {
                    throw new Exception("Orden no existente");

                }

                return View(@object);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        // GET: OrdenController/Create
        public async Task<ActionResult> Create()
        {

            var nextReceiptNumber = await _serviceOrden.GetNextNumberOrden();
            ViewBag.CurrentReceipt = nextReceiptNumber;

            // Clear CarShopping
            TempData["CartShopping"] = null;
            TempData.Keep();

            return View();
        }

        // POST: OrdenController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(OrdenDTO orden)
        {
            string json;
            try
            {

                // IdClient exist?
                var cliente = await _serviceCliente.FindByIdAsync(orden.IdCliente);

                if (cliente == null)
                {
                    TempData.Keep();
                    return BadRequest("Cliente No existe");
                }


                json = (string)TempData["CartShopping"]!;

                if (string.IsNullOrEmpty(json))
                {
                    return BadRequest("No hay datos por facturar");
                }

                var lista = JsonSerializer.Deserialize<List<OrdenDetalleDTO>>(json!)!;
                //Agregar datos faltantes a la orden
                orden.IdOrden = 0;
                orden.IdUsuario = 2;
                orden.FechaOrden = DateTime.Today;
                orden.OrdenDetalle = lista;
                orden.Total = orden.OrdenDetalle.Sum(p => p.Subtotal);

                await _serviceOrden.AddAsync(orden);


                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Keep Cache data
                TempData.Keep();
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> AddLibro(int id,  int cantidad)
        {
            OrdenDetalleDTO detalle = new OrdenDetalleDTO();
            var lista = new List<OrdenDetalleDTO>();
            string json = "";

            var Libro = await _serviceLibro.FindByIdAsync(id);
            OrdenDetalleDTO item = new OrdenDetalleDTO();
            //Cantidad de item a guardar
            detalle.Cantidad = cantidad;
            if (TempData["CartShopping"] != null)
            {
                json = (string)TempData["CartShopping"]!;
                lista = JsonSerializer.Deserialize<List<OrdenDetalleDTO>>(json!)!;
                //Buscar si existe en la compra
                item = lista.FirstOrDefault(o => o.IdLibro == id);
                if (item!=null)
                {
                    detalle.Cantidad+= cantidad;
                    
                }
            }

            // Stock ??
            if (detalle.Cantidad > Libro.Cantidad)
            {
                return BadRequest("No hay inventario suficiente!");
            }
            else
            {
                if (item != null && item.Cantidad != 0)
                {
                    //Actualizar cantidad de libro existente
                    item.Cantidad += cantidad;
                    item.Subtotal = item.Cantidad * Libro.Precio;
                }
                else
                {
                    detalle.IdLibro = Libro.IdLibro;
                    detalle.NombreLibro = Libro.Nombre;
                    detalle.Cantidad = cantidad;
                    detalle.Precio = Libro.Precio;
                    //Falta impuesto
                    detalle.Subtotal = (detalle.Precio * detalle.Cantidad);
                    //Agregar al carrito de compras
                    lista.Add(detalle);
                }
                json = JsonSerializer.Serialize(lista);
                TempData["CartShopping"] = json;
            }

            TempData.Keep();
            return PartialView("_DetailOrden", lista);
        }

        public IActionResult GetDetailOrden()
        {
            List<OrdenDetalleDTO> lista = new List<OrdenDetalleDTO>();
            string json = "";
            json = (string)TempData["CartShopping"]!;
            lista = JsonSerializer.Deserialize<List<OrdenDetalleDTO>>(json!)!;
            
            json = JsonSerializer.Serialize(lista);
            TempData["CartShopping"] = json;
            TempData.Keep();

            return PartialView("_DetailOrden", lista);
        }

        public IActionResult DeleteLibro(int idLibro)
        {
            OrdenDetalleDTO OrdenDetalleDTO = new OrdenDetalleDTO();
            List<OrdenDetalleDTO> lista = new List<OrdenDetalleDTO>();
            string json = "";

            if (TempData["CartShopping"] != null)
            {
                json = (string)TempData["CartShopping"]!;
                lista = JsonSerializer.Deserialize<List<OrdenDetalleDTO>>(json!)!;
                //Eliminar de la lista segun el indice
                int idx = lista.FindIndex(p => p.IdLibro == idLibro);
                lista.RemoveAt(idx);
                json = JsonSerializer.Serialize(lista);
                TempData["CartShopping"] = json;
            }

            TempData.Keep();

            // return Content("Ok");
            return PartialView("_DetailOrden", lista);

        }
    }
}
