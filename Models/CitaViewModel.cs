namespace CitasApp.Models
{
    public class CitaViewModel
    {
        public int Id { get; set; }
        public DateOnly Fecha { get; set; }
        public TimeOnly Hora { get; set; }
        public string? Paciente { get; set; }
        public string? Medico { get; set; }
        public string? Motivo { get; set; }
        public string? Estado { get; set; }
    }
}
