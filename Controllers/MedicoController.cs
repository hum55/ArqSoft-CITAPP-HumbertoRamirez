using Microsoft.AspNetCore.Mvc;
using CitasApp.Data;
using CitasApp.Models;

namespace CitasApp.Controllers
{
    public class MedicoController : Controller
    {
        private readonly JsonData _data;

        public MedicoController(JsonData data)
        {
            _data = data;
        }

        public IActionResult Index()
        {
            var medicos = _data.GetMedicos();
            return View(medicos);
        }

        public IActionResult Create()
        {
            return View(new Medico());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Medico medico)
        {
            if (ModelState.IsValid)
            {
                _data.AddMedico(medico);
                TempData["Mensaje"] = "Médico registrado exitosamente";
                return RedirectToAction(nameof(Index));
            }
            return View(medico);
        }

        public IActionResult Edit(int id)
        {
            var medico = _data.GetMedicoById(id);
            if (medico == null) return NotFound();
            return View(medico);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Medico medico)
        {
            if (ModelState.IsValid)
            {
                _data.UpdateMedico(medico);
                TempData["Mensaje"] = "Médico actualizado exitosamente";
                return RedirectToAction(nameof(Index));
            }
            return View(medico);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _data.DeleteMedico(id);
            TempData["Mensaje"] = "Médico eliminado exitosamente";
            return RedirectToAction(nameof(Index));
        }
    }
}