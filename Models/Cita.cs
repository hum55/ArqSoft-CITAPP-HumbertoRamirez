using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CitasApp.Models
{
    public class Cita
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El paciente es obligatorio")]
        [Display(Name = "Paciente")]
        [JsonPropertyName("pacienteId")]
        public int PacienteId { get; set; }

        [Required(ErrorMessage = "El medico es obligatorio")]
        [Display(Name = "Medico")]
        [JsonPropertyName("medicoId")]
        public int MedicoId { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha")]
        [JsonPropertyName("fecha")]
        public DateTime Fecha { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "La hora es obligatoria")]
        [Display(Name = "Hora")]
        [JsonPropertyName("hora")]
        public string Hora { get; set; } = string.Empty;

        [Display(Name = "Estado")]
        [JsonPropertyName("estado")]
        public string Estado { get; set; } = "Pendiente";

        [Display(Name = "Motivo")]
        [JsonPropertyName("motivo")]
        public string? Motivo { get; set; }

        [JsonIgnore]
        public Paciente? Paciente { get; set; }

        [JsonIgnore]
        public Medico? Medico { get; set; }
    }
}
