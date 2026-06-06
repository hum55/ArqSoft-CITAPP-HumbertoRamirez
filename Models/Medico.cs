using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CitasApp.Models
{
    public class Medico
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Display(Name = "Nombre")]
        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "La especialidad es obligatoria")]
        [Display(Name = "Especialidad")]
        [JsonPropertyName("especialidad")]
        public string Especialidad { get; set; } = string.Empty;

        [Display(Name = "Teléfono")]
        [JsonPropertyName("telefono")]
        public string? Telefono { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [Display(Name = "Activo")]
        [JsonPropertyName("activo")]
        public bool Activo { get; set; } = true;
    }
}