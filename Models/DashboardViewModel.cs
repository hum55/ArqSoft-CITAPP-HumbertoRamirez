namespace CitasApp.Models
{
    public class DashboardViewModel
    {
        public int TotalCitas { get; set; }
        public int TotalPacientes { get; set; }
        public int MedicosActivos { get; set; }
        public int CitasHoy { get; set; }
        public List<CitaDetalle> ProximasCitas { get; set; } = new();
        public List<ActividadReciente> Actividad { get; set; } = new();
    }

    public class CitaDetalle
    {
        public int Id { get; set; }
        public string NombrePaciente { get; set; } = string.Empty;
        public string NombreMedico { get; set; } = string.Empty;
        public string Especialidad { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public string Hora { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string Iniciales { get; set; } = string.Empty;
    }

    public class ActividadReciente
    {
        public string Tipo { get; set; } = string.Empty;  // cita, paciente, cancelacion, medico
        public string Texto { get; set; } = string.Empty;
        public string Tiempo { get; set; } = string.Empty;
    }
}