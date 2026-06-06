using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CitasApp.Models
{
    public class Paciente
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Display(Name = "Nombre")]
        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Display(Name = "Fecha de Nacimiento")]
        [DataType(DataType.Date)]
        [JsonPropertyName("fechaNacimiento")]
        public DateTime? FechaNacimiento { get; set; }

        [Display(Name = "Teléfono")]
        [JsonPropertyName("telefono")]
        public string? Telefono { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [Display(Name = "Dirección")]
        [JsonPropertyName("direccion")]
        public string? Direccion { get; set; }
    }
}