using Microsoft.AspNetCore.Mvc;
using CitasApp.Data;
using CitasApp.Models;

namespace CitasApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly JsonData _data;

        public HomeController(JsonData data)
        {
            _data = data;
        }

        public IActionResult Index()
        {
            var citas = _data.GetCitas();
            var pacientes = _data.GetPacientes();
            var medicos = _data.GetMedicos();
            var hoy = DateTime.Today;

            var proximasCitas = citas
                .Where(c => c.Fecha >= hoy && c.Estado != "Cancelada")
                .OrderBy(c => c.Fecha)
                .ThenBy(c => c.Hora)
                .Take(8)
                .Select(c =>
                {
                    var pac = pacientes.FirstOrDefault(p => p.Id == c.PacienteId);
                    var med = medicos.FirstOrDefault(m => m.Id == c.MedicoId);
                    var nombre = pac?.Nombre ?? "Sin asignar";
                    var partes = nombre.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    var iniciales = partes.Length >= 2
                        ? $"{partes[0][0]}{partes[1][0]}"
                        : nombre.Length >= 2 ? nombre[..2] : nombre;

                    return new CitaDetalle
                    {
                        Id = c.Id,
                        NombrePaciente = nombre,
                        NombreMedico = med?.Nombre ?? "Sin asignar",
                        Especialidad = med?.Especialidad ?? "",
                        Fecha = c.Fecha,
                        Hora = c.Hora,
                        Estado = c.Estado,
                        Iniciales = iniciales.ToUpper()
                    };
                })
                .ToList();

            var model = new DashboardViewModel
            {
                TotalCitas = citas.Count,
                TotalPacientes = pacientes.Count,
                MedicosActivos = medicos.Count(m => m.Activo),
                CitasHoy = citas.Count(c => c.Fecha.Date == hoy),
                ProximasCitas = proximasCitas,
                Actividad = new List<ActividadReciente>
                {
                    new() { Tipo = "cita", Texto = "Nueva cita agendada para María García", Tiempo = "Hace 5 min" },
                    new() { Tipo = "paciente", Texto = "Paciente Roberto Sánchez actualizado", Tiempo = "Hace 12 min" },
                    new() { Tipo = "cancelacion", Texto = "Cita de José Hernández cancelada", Tiempo = "Hace 28 min" },
                    new() { Tipo = "medico", Texto = "Dr. Mendoza confirmó disponibilidad", Tiempo = "Hace 1 h" },
                    new() { Tipo = "cita", Texto = "Cita completada: Laura Martínez", Tiempo = "Hace 2 h" }
                }
            };

            return View(model);
        }
    }
}