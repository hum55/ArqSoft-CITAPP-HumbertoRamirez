using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CitasApp.Models
{
    public class SystemSettings
    {
        [Required(ErrorMessage = "El nombre del sistema es obligatorio")]
        [Display(Name = "Nombre del sistema")]
        [JsonPropertyName("nombreSistema")]
        public string NombreSistema { get; set; } = "CitasApp";

        [Required(ErrorMessage = "El nombre de la clinica es obligatorio")]
        [Display(Name = "Clinica")]
        [JsonPropertyName("nombreClinica")]
        public string NombreClinica { get; set; } = "Clinica Ramirez";

        [Required(ErrorMessage = "El administrador principal es obligatorio")]
        [Display(Name = "Administrador principal")]
        [JsonPropertyName("administradorPrincipal")]
        public string AdministradorPrincipal { get; set; } = "Humberto Ramirez";

        [EmailAddress(ErrorMessage = "Ingresa un correo valido")]
        [Display(Name = "Correo de contacto")]
        [JsonPropertyName("correoContacto")]
        public string CorreoContacto { get; set; } = "admin@citasapp.local";

        [Display(Name = "Telefono")]
        [JsonPropertyName("telefono")]
        public string Telefono { get; set; } = "999-000-0000";

        [Display(Name = "Direccion")]
        [JsonPropertyName("direccion")]
        public string Direccion { get; set; } = "Merida, Yucatan";

        [Range(10, 180, ErrorMessage = "La duracion debe estar entre 10 y 180 minutos")]
        [Display(Name = "Duracion de cita")]
        [JsonPropertyName("duracionCitaMinutos")]
        public int DuracionCitaMinutos { get; set; } = 30;

        [Display(Name = "Inicio de jornada")]
        [JsonPropertyName("horaInicio")]
        public string HoraInicio { get; set; } = "08:00";

        [Display(Name = "Fin de jornada")]
        [JsonPropertyName("horaFin")]
        public string HoraFin { get; set; } = "18:00";

        [Display(Name = "Permitir registro de administradores")]
        [JsonPropertyName("permitirRegistroAdministradores")]
        public bool PermitirRegistroAdministradores { get; set; } = true;
    }
}
