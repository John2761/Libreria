using Libreria.Application.DTOs;
using Libreria.Application.Services.Implementations;
using Libreria.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using X.PagedList.Extensions;

namespace Libreria.Web.Controllers
{
    public class LibroController : Controller
    {
        private readonly IServiceLibro _serviceLibro;
        private readonly IServiceAutor _serviceAutor;
        private readonly IServiceCategoria _serviceCategoria;
        private readonly ILogger<ServiceLibro> _logger;

        public LibroController(IServiceLibro servicioLibro,
            IServiceAutor serviceAutor,
            IServiceCategoria serviceCategoria,
            ILogger<ServiceLibro> logger)
        {
            _serviceLibro = servicioLibro;
            _serviceAutor = serviceAutor;
            _serviceCategoria = serviceCategoria;
            _logger = logger;
        }


        // GET: LibroController
        public async Task<ActionResult> Index()
        {
            //Recibir el mensaje de TempData
            if (TempData.ContainsKey("Mensaje"))
            {
                ViewBag.NotificationMessage = TempData["Mensaje"];
                
            }
            ViewBag.ListCategorias=await _serviceCategoria.ListAsync();

            var collection = await _serviceLibro.ListAsync();
            return View(collection);
        }
        public async Task<IActionResult> buscarxCategoria(int IdCategoria)
        {
            var collection = await _serviceLibro.ListAsync();
            if(IdCategoria != 0)
            {
                collection = await _serviceLibro.GetLibroByCategoria(IdCategoria) ?? new List<LibroDTO>(); ;

            }
            return PartialView("_ListLibros",collection);
        }

        public async Task<ActionResult> IndexAdmin(int? page)
        {
            var collection = await _serviceLibro.ListAsync();
            return View(collection.ToPagedList(page ?? 1, 5));
        }

        // GET: LibroController/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("IndexAdmin");
                }
                var @object = await _serviceLibro.FindByIdAsync(id.Value);
                if (@object == null)
                {
                    throw new Exception("Libro no existente");

                }

                return View(@object);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: LibroController/Create
        [Authorize(Roles = "Admnistrador")]
        public async Task<IActionResult> Create()
        {
            ViewBag.ListAutor = await _serviceAutor.ListAsync();
            var categorias = await _serviceCategoria.ListAsync();
            ViewBag.ListCategorias = new MultiSelectList(categorias, "IdCategoria", "Nombre");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LibroDTO dto, IFormFile? imageFile, string[] selectedCategorias)
        {
            MemoryStream target = new MemoryStream();

            // Cuando es Insert Image viene en null porque se pasa diferente
            if (dto.Imagen == null)
            {
                if (imageFile != null)
                {
                    imageFile.OpenReadStream().CopyTo(target);

                    dto.Imagen = target.ToArray();
                    ModelState.Remove("Imagen");
                }
                else
                {
                    var rutaImagenDefault = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "nophoto.jpg");

                    if (System.IO.File.Exists(rutaImagenDefault))
                    {
                        byte[] imagenDefault = System.IO.File.ReadAllBytes(rutaImagenDefault);
                        dto.Imagen = imagenDefault;
                        ModelState.Remove("Imagen");
                    }

                }
            }

            if (!ModelState.IsValid)
            {
                // Lee del ModelState todos los errores que
                // vienen para el lado del server
                string errors = string.Join("; ", ModelState.Values
                                   .SelectMany(x => x.Errors)
                                   .Select(x => x.ErrorMessage));
                // Response errores
                //return BadRequest(errors);
                ViewBag.NotificationMessage = Util.SweetAlertHelper.Mensaje(
                "Crear Libro",
                "Errores: " + errors.ToString(),
                Util.SweetAlertMessageType.error);
                //Recargar el mismo formulario
                ViewBag.ListAutor = await _serviceAutor.ListAsync();
                var categorias = await _serviceCategoria.ListAsync();
                ViewBag.ListCategorias = new MultiSelectList(categorias, "IdCategoria", "Nombre");
                return View(dto);
            }

            var idLibro = await _serviceLibro.AddAsync(dto, selectedCategorias);
            TempData["mensaje"] = Util.SweetAlertHelper.Mensaje(
               "Crear Libro",
                "Libro creado " + idLibro.ToString(),
                Util.SweetAlertMessageType.success);
            return RedirectToAction("Index");

        }

        // GET: LibroController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var @object = await _serviceLibro.FindByIdAsync(id);
            ViewBag.ListAutor = await _serviceAutor.ListAsync();
            var categorias = await _serviceCategoria.ListAsync();
            var catSelected = @object.IdCategoria.Select(x => x.IdCategoria.ToString()).ToList();

            ViewBag.ListCategorias = new MultiSelectList(
                    items: categorias,
                    dataValueField: nameof(CategoriaDTO.IdCategoria),
                    dataTextField: nameof(CategoriaDTO.Nombre),
                    selectedValues: catSelected
                );


            return View(@object);
        }


        // POST: LibroController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LibroDTO dto, string[] selectedCategorias)
        {
            if (!ModelState.IsValid)
            {
                // Lee del ModelState todos los errores que
                // vienen para el lado del server
                string errors = string.Join("; ", ModelState.Values
                                   .SelectMany(x => x.Errors)
                                   .Select(x => x.ErrorMessage));
                ViewBag.ErrorMessage = errors;
                return View();
            }
            else
            {

                await _serviceLibro.UpdateAsync(id, dto, selectedCategorias);
                return RedirectToAction("IndexAdmin");
            }


        }

        // GET: LibroController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var @object = await _serviceLibro.FindByIdAsync(id);
            return View(@object);
        }

        // POST: LibroController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            await _serviceLibro.DeleteAsync(id);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> GetLibroByName(string filtro)
        {

            var collection = await _serviceLibro.FindByNameAsync(filtro);
            return Json(collection);

        }

    }
}
