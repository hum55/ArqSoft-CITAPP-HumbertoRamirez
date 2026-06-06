using Microsoft.AspNetCore.Mvc;
using CitasApp.Data;
using CitasApp.Models;

namespace CitasApp.Controllers
{
    public class PacienteController : Controller
    {
        private readonly JsonData _data;

        public PacienteController(JsonData data)
        {
            _data = data;
        }

        public IActionResult Index()
        {
            var pacientes = _data.GetPacientes();
            return View(pacientes);
        }

        public IActionResult Create()
        {
            return View(new Paciente());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                _data.AddPaciente(paciente);
                TempData["Mensaje"] = "Paciente registrado exitosamente";
                return RedirectToAction(nameof(Index));
            }
            return View(paciente);
        }

        public IActionResult Edit(int id)
        {
            var paciente = _data.GetPacienteById(id);
            if (paciente == null) return NotFound();
            return View(paciente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                _data.UpdatePaciente(paciente);
                TempData["Mensaje"] = "Paciente actualizado exitosamente";
                return RedirectToAction(nameof(Index));
            }
            return View(paciente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _data.DeletePaciente(id);
            TempData["Mensaje"] = "Paciente eliminado exitosamente";
            return RedirectToAction(nameof(Index));
        }
    }
}