using CitasApp.Models;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace CitasApp.Data
{
    public class JsonData
    {
        private readonly string _dataPath;
        private readonly JsonSerializerOptions _options;

        public JsonData(IWebHostEnvironment env)
        {
            _dataPath = Path.Combine(env.ContentRootPath, "Data");
            Directory.CreateDirectory(_dataPath);
            _options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };
            _options.Converters.Add(new DateOnlyConverter());
            _options.Converters.Add(new NullableDateConverter());
            EnsureSeedData();
        }

        private void EnsureSeedData()
        {
            var adminsPath = Path.Combine(_dataPath, "Admins.json");
            if (!File.Exists(adminsPath))
            {
                SaveAdmins(new List<AdminUser>
                {
                    new()
                    {
                        Id = 1,
                        Nombre = "Humberto Ramirez",
                        Email = "admin@citasapp.local",
                        PasswordHash = HashPassword("Admin123"),
                        Rol = "Administrador",
                        Activo = true
                    }
                });
            }

            var settingsPath = Path.Combine(_dataPath, "Settings.json");
            if (!File.Exists(settingsPath))
            {
                SaveSettings(new SystemSettings());
            }
        }

        public static string HashPassword(string password)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            return Convert.ToHexString(bytes);
        }

        public List<Cita> GetCitas()
        {
            var path = Path.Combine(_dataPath, "citas.json");
            if (!File.Exists(path)) return new List<Cita>();
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<Cita>>(json, _options) ?? new List<Cita>();
        }

        public Cita? GetCitaById(int id)
        {
            return GetCitas().FirstOrDefault(c => c.Id == id);
        }

        public void SaveCitas(List<Cita> citas)
        {
            var path = Path.Combine(_dataPath, "citas.json");
            var json = JsonSerializer.Serialize(citas, _options);
            File.WriteAllText(path, json);
        }

        public void AddCita(Cita cita)
        {
            var citas = GetCitas();
            cita.Id = citas.Any() ? citas.Max(c => c.Id) + 1 : 1;
            citas.Add(cita);
            SaveCitas(citas);
        }

        public void UpdateCita(Cita cita)
        {
            var citas = GetCitas();
            var index = citas.FindIndex(c => c.Id == cita.Id);
            if (index >= 0)
            {
                citas[index] = cita;
                SaveCitas(citas);
            }
        }

        public void DeleteCita(int id)
        {
            var citas = GetCitas();
            citas.RemoveAll(c => c.Id == id);
            SaveCitas(citas);
        }

        public List<Medico> GetMedicos()
        {
            var path = Path.Combine(_dataPath, "medicos.json");
            if (!File.Exists(path)) return new List<Medico>();
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<Medico>>(json, _options) ?? new List<Medico>();
        }

        public Medico? GetMedicoById(int id)
        {
            return GetMedicos().FirstOrDefault(m => m.Id == id);
        }

        public void SaveMedicos(List<Medico> medicos)
        {
            var path = Path.Combine(_dataPath, "medicos.json");
            var json = JsonSerializer.Serialize(medicos, _options);
            File.WriteAllText(path, json);
        }

        public void AddMedico(Medico medico)
        {
            var medicos = GetMedicos();
            medico.Id = medicos.Any() ? medicos.Max(m => m.Id) + 1 : 1;
            medicos.Add(medico);
            SaveMedicos(medicos);
        }

        public void UpdateMedico(Medico medico)
        {
            var medicos = GetMedicos();
            var index = medicos.FindIndex(m => m.Id == medico.Id);
            if (index >= 0)
            {
                medicos[index] = medico;
                SaveMedicos(medicos);
            }
        }

        public void DeleteMedico(int id)
        {
            var medicos = GetMedicos();
            medicos.RemoveAll(m => m.Id == id);
            SaveMedicos(medicos);
        }

        public List<Paciente> GetPacientes()
        {
            var path = Path.Combine(_dataPath, "pacientes.json");
            if (!File.Exists(path)) return new List<Paciente>();
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<Paciente>>(json, _options) ?? new List<Paciente>();
        }

        public Paciente? GetPacienteById(int id)
        {
            return GetPacientes().FirstOrDefault(p => p.Id == id);
        }

        public void SavePacientes(List<Paciente> pacientes)
        {
            var path = Path.Combine(_dataPath, "pacientes.json");
            var json = JsonSerializer.Serialize(pacientes, _options);
            File.WriteAllText(path, json);
        }

        public void AddPaciente(Paciente paciente)
        {
            var pacientes = GetPacientes();
            paciente.Id = pacientes.Any() ? pacientes.Max(p => p.Id) + 1 : 1;
            pacientes.Add(paciente);
            SavePacientes(pacientes);
        }

        public void UpdatePaciente(Paciente paciente)
        {
            var pacientes = GetPacientes();
            var index = pacientes.FindIndex(p => p.Id == paciente.Id);
            if (index >= 0)
            {
                pacientes[index] = paciente;
                SavePacientes(pacientes);
            }
        }

        public void DeletePaciente(int id)
        {
            var pacientes = GetPacientes();
            pacientes.RemoveAll(p => p.Id == id);
            SavePacientes(pacientes);
        }

        public List<AdminUser> GetAdmins()
        {
            var path = Path.Combine(_dataPath, "Admins.json");
            if (!File.Exists(path)) return new List<AdminUser>();
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<AdminUser>>(json, _options) ?? new List<AdminUser>();
        }

        public AdminUser? GetAdminByEmail(string email)
        {
            return GetAdmins().FirstOrDefault(a => a.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public void SaveAdmins(List<AdminUser> admins)
        {
            var path = Path.Combine(_dataPath, "Admins.json");
            var json = JsonSerializer.Serialize(admins, _options);
            File.WriteAllText(path, json);
        }

        public void AddAdmin(AdminUser admin)
        {
            var admins = GetAdmins();
            admin.Id = admins.Any() ? admins.Max(a => a.Id) + 1 : 1;
            admins.Add(admin);
            SaveAdmins(admins);
        }

        public SystemSettings GetSettings()
        {
            var path = Path.Combine(_dataPath, "Settings.json");
            if (!File.Exists(path)) return new SystemSettings();
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<SystemSettings>(json, _options) ?? new SystemSettings();
        }

        public void SaveSettings(SystemSettings settings)
        {
            var path = Path.Combine(_dataPath, "Settings.json");
            var json = JsonSerializer.Serialize(settings, _options);
            File.WriteAllText(path, json);
        }
    }
}
