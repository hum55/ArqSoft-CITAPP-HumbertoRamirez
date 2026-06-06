using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CitasApp.Models
{
    public class AdminUser
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Display(Name = "Nombre completo")]
        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Ingresa un correo valido")]
        [Display(Name = "Correo")]
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("passwordHash")]
        public string PasswordHash { get; set; } = string.Empty;

        [JsonPropertyName("rol")]
        public string Rol { get; set; } = "Administrador";

        [JsonPropertyName("activo")]
        public bool Activo { get; set; } = true;
    }
}
