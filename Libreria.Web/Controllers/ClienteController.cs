using Libreria.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Libreria.Web.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IServiceCliente _serviceCliente;
        public ClienteController(IServiceCliente serviceCliente)
        {
            _serviceCliente = serviceCliente;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetClienteByName(string filtro)
        {

            var collections = _serviceCliente.FindByDescriptionAsync(filtro).GetAwaiter().GetResult();

            return Json(collections);
        }
    }
}
