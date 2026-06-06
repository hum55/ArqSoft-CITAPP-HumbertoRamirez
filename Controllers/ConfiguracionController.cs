using CitasApp.Data;
using CitasApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CitasApp.Controllers
{
    public class ConfiguracionController : Controller
    {
        private readonly JsonData _data;

        public ConfiguracionController(JsonData data)
        {
            _data = data;
        }

        public IActionResult Index()
        {
            ViewBag.Admins = _data.GetAdmins().OrderBy(a => a.Nombre).ToList();
            return View(_data.GetSettings());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(SystemSettings settings)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Admins = _data.GetAdmins().OrderBy(a => a.Nombre).ToList();
                return View(settings);
            }

            _data.SaveSettings(settings);

            var currentEmail = HttpContext.Session.GetString("AdminEmail");
            if (!string.IsNullOrWhiteSpace(currentEmail))
            {
                var admins = _data.GetAdmins();
                var current = admins.FirstOrDefault(a => a.Email.Equals(currentEmail, StringComparison.OrdinalIgnoreCase));
                if (current != null && current.Nombre != settings.AdministradorPrincipal)
                {
                    current.Nombre = settings.AdministradorPrincipal;
                    _data.SaveAdmins(admins);
                    HttpContext.Session.SetString("AdminName", current.Nombre);
                }
            }

            TempData["Mensaje"] = "Configuracion guardada correctamente.";
            return RedirectToAction(nameof(Index));
        }
    }
}
