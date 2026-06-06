using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CitasApp.Data;
using CitasApp.Models;

namespace CitasApp.Controllers
{
    public class CitaController : Controller
    {
        private readonly JsonData _data;

        public CitaController(JsonData data)
        {
            _data = data;
        }

        public IActionResult Index()
        {
            var citas = _data.GetCitas();
            var pacientes = _data.GetPacientes();
            var medicos = _data.GetMedicos();

            // Attach navigation objects
            foreach (var c in citas)
            {
                c.Paciente = pacientes.FirstOrDefault(p => p.Id == c.PacienteId);
                c.Medico = medicos.FirstOrDefault(m => m.Id == c.MedicoId);
            }

            return View(citas.OrderByDescending(c => c.Fecha).ThenBy(c => c.Hora).ToList());
        }

        public IActionResult Create()
        {
            CargarDropdowns();
            return View(new Cita { Fecha = DateTime.Today });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Cita cita)
        {
            if (ModelState.IsValid)
            {
                cita.Estado = "Pendiente";
                _data.AddCita(cita);
                TempData["Mensaje"] = "Cita creada exitosamente";
                return RedirectToAction(nameof(Index));
            }
            CargarDropdowns();
            return View(cita);
        }

        public IActionResult Edit(int id)
        {
            var cita = _data.GetCitaById(id);
            if (cita == null) return NotFound();
            CargarDropdowns();
            return View(cita);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Cita cita)
        {
            if (ModelState.IsValid)
            {
                _data.UpdateCita(cita);
                TempData["Mensaje"] = "Cita actualizada exitosamente";
                return RedirectToAction(nameof(Index));
            }
            CargarDropdowns();
            return View(cita);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _data.DeleteCita(id);
            TempData["Mensaje"] = "Cita eliminada exitosamente";
            return RedirectToAction(nameof(Index));
        }

        private void CargarDropdowns()
        {
            ViewBag.Pacientes = _data.GetPacientes()
                .Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Nombre })
                .ToList();

            ViewBag.Medicos = _data.GetMedicos()
                .Where(m => m.Activo)
                .Select(m => new SelectListItem { Value = m.Id.ToString(), Text = $"{m.Nombre} - {m.Especialidad}" })
                .ToList();

            ViewBag.Estados = new List<SelectListItem>
            {
                new("Pendiente", "Pendiente"),
                new("Confirmada", "Confirmada"),
                new("Completada", "Completada"),
                new("Cancelada", "Cancelada")
            };
        }
    }
}