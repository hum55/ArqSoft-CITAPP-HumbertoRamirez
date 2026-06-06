using CitasApp.Data;
using CitasApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CitasApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly JsonData _data;

        public AuthController(JsonData data)
        {
            _data = data;
        }

        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("AdminEmail") != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(new LoginViewModel { Email = "admin@citasapp.local" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var admin = _data.GetAdminByEmail(model.Email);
            var passwordHash = JsonData.HashPassword(model.Password);

            if (admin == null || !admin.Activo || admin.PasswordHash != passwordHash)
            {
                ModelState.AddModelError(string.Empty, "Correo o contrasena incorrectos.");
                return View(model);
            }

            HttpContext.Session.SetString("AdminName", admin.Nombre);
            HttpContext.Session.SetString("AdminEmail", admin.Email);
            HttpContext.Session.SetString("AdminRole", admin.Rol);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            if (!_data.GetSettings().PermitirRegistroAdministradores)
            {
                TempData["Error"] = "El registro de administradores esta desactivado.";
                return RedirectToAction(nameof(Login));
            }

            return View(new RegisterViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!_data.GetSettings().PermitirRegistroAdministradores)
            {
                TempData["Error"] = "El registro de administradores esta desactivado.";
                return RedirectToAction(nameof(Login));
            }

            if (!ModelState.IsValid) return View(model);

            if (_data.GetAdminByEmail(model.Email) != null)
            {
                ModelState.AddModelError(nameof(model.Email), "Ya existe un administrador con ese correo.");
                return View(model);
            }

            _data.AddAdmin(new AdminUser
            {
                Nombre = model.Nombre,
                Email = model.Email,
                PasswordHash = JsonData.HashPassword(model.Password),
                Rol = "Administrador",
                Activo = true
            });

            TempData["Mensaje"] = "Administrador registrado. Ya puedes iniciar sesion.";
            return RedirectToAction(nameof(Login));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }
    }
}
