using Libreria.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Libreria.Web.Controllers
{
    public class AutorController : Controller
    {
        private readonly IServiceAutor _serviceAutor;

        public AutorController(IServiceAutor serviceAutor)
        {
            _serviceAutor = serviceAutor;
        }

        // GET: AutorController
        public async Task<ActionResult> Index()
        {
            //Recibir Mensaje
            if (TempData.ContainsKey("Mensaje")){
                ViewBag.NotificationMessage = TempData["Mensaje"];
            }
            ViewData["Title"] = "Autores";
            ViewBag.Titulo = "Lista Autores";
            var collection= await _serviceAutor.ListAsync();
            return View(collection);
        }
        public async Task<ActionResult> Enviar()
        {
            //Procesamiento
            TempData["Mensaje"] = Util.SweetAlertHelper.Mensaje("Enviar","Mensaje enviado",Util.SweetAlertMessageType.success);
            //return View();
            return RedirectToAction("Index");
        }

    }
}
