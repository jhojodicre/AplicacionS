using System;
using System.IO;
using Newtonsoft.Json;
using AplicacionS.Interfaces;

namespace AplicacionS.Services
{
    /// <summary>
    /// Configuración del sistema de seguridad
    /// </summary>
    public class SecurityConfiguration
    {
        /// <summary>
        /// Puerto COM por defecto
        /// </summary>
        public string DefaultComPort { get; set; } = "";

        /// <summary>
        /// Velocidad de baudios
        /// </summary>
        public int BaudRate { get; set; } = 115200;

        /// <summary>
        /// Timeout de conexión en minutos
        /// </summary>
        public int ConnectionTimeoutMinutes { get; set; } = 5;

        /// <summary>
        /// Habilitar sonidos de alarma
        /// </summary>
        public bool EnableAlarmSounds { get; set; } = true;

        /// <summary>
        /// Volumen de alarmas (0-100)
        /// </summary>
        public int AlarmVolume { get; set; } = 80;

        /// <summary>
        /// Intervalo de actualización de gráficos en ms
        /// </summary>
        public int GraphicsUpdateInterval { get; set; } = 100;

        /// <summary>
        /// Mantener logs por días
        /// </summary>
        public int LogRetentionDays { get; set; } = 30;

        /// <summary>
        /// Habilitar modo debug
        /// </summary>
        public bool DebugMode { get; set; } = false;

        /// <summary>
        /// Reconocimiento automático de alarmas después de X minutos
        /// </summary>
        public int AutoAcknowledgeMinutes { get; set; } = 0; // 0 = deshabilitado

        /// <summary>
        /// Rutas de archivos de sonido
        /// </summary>
        public SoundPaths Sounds { get; set; } = new SoundPaths();
    }

    /// <summary>
    /// Configuración de rutas de sonidos
    /// </summary>
    public class SoundPaths
    {
        public string AlarmSound { get; set; } = @"Resources\sound_alarm_Zones.wav";
        public string WarningSound { get; set; } = @"Resources\sound_alarma_1.wav";
    }

    /// <summary>
    /// Servicio para manejar la configuración del sistema
    /// </summary>
    public class ConfigurationService
    {
        private readonly string _configFilePath;
        private readonly ILoggingService _logger;
        private SecurityConfiguration _configuration;

        /// <summary>
        /// Configuración actual del sistema
        /// </summary>
        public SecurityConfiguration Current => _configuration;

        /// <summary>
        /// Constructor del servicio de configuración
        /// </summary>
        /// <param name="logger">Servicio de logging</param>
        /// <param name="configDirectory">Directorio de configuración personalizado</param>
        public ConfigurationService(ILoggingService logger, string configDirectory = null)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            
            var configDir = configDirectory ?? Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "SeguridadPerimetral");
            
            EnsureConfigDirectoryExists(configDir);
            _configFilePath = Path.Combine(configDir, "config.json");
            
            LoadConfiguration();
        }

        /// <summary>
        /// Carga la configuración desde el archivo
        /// </summary>
        public void LoadConfiguration()
        {
            try
            {
                if (File.Exists(_configFilePath))
                {
                    var jsonString = File.ReadAllText(_configFilePath);
                    _configuration = JsonConvert.DeserializeObject<SecurityConfiguration>(jsonString);
                    _logger.LogInfo("Configuración cargada exitosamente");
                }
                else
                {
                    _configuration = new SecurityConfiguration();
                    SaveConfiguration();
                    _logger.LogInfo("Configuración por defecto creada");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al cargar configuración, usando valores por defecto", ex);
                _configuration = new SecurityConfiguration();
            }
        }

        /// <summary>
        /// Guarda la configuración actual al archivo
        /// </summary>
        public void SaveConfiguration()
        {
            try
            {
                var jsonString = JsonConvert.SerializeObject(_configuration, Formatting.Indented);
                File.WriteAllText(_configFilePath, jsonString);
                _logger.LogInfo("Configuración guardada exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al guardar configuración", ex);
            }
        }

        /// <summary>
        /// Actualiza una configuración específica
        /// </summary>
        /// <typeparam name="T">Tipo del valor</typeparam>
        /// <param name="key">Clave de configuración</param>
        /// <param name="value">Nuevo valor</param>
        public void UpdateSetting<T>(string key, T value)
        {
            try
            {
                var property = typeof(SecurityConfiguration).GetProperty(key);
                if (property != null && property.CanWrite)
                {
                    property.SetValue(_configuration, value);
                    SaveConfiguration();
                    _logger.LogInfo($"Configuración actualizada: {key} = {value}");
                }
                else
                {
                    _logger.LogWarning($"Configuración no válida: {key}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar configuración {key}", ex);
            }
        }

        /// <summary>
        /// Obtiene un valor de configuración
        /// </summary>
        /// <typeparam name="T">Tipo del valor</typeparam>
        /// <param name="key">Clave de configuración</param>
        /// <returns>Valor de configuración</returns>
        public T GetSetting<T>(string key)
        {
            try
            {
                var property = typeof(SecurityConfiguration).GetProperty(key);
                if (property != null && property.CanRead)
                {
                    return (T)property.GetValue(_configuration);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener configuración {key}", ex);
            }
            
            return default(T);
        }

        /// <summary>
        /// Restaura la configuración por defecto
        /// </summary>
        public void RestoreDefaults()
        {
            try
            {
                _configuration = new SecurityConfiguration();
                SaveConfiguration();
                _logger.LogInfo("Configuración restaurada a valores por defecto");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al restaurar configuración por defecto", ex);
            }
        }

        /// <summary>
        /// Valida la configuración actual
        /// </summary>
        /// <returns>True si la configuración es válida</returns>
        public bool ValidateConfiguration()
        {
            try
            {
                var isValid = true;
                
                // Validar valores numéricos
                if (_configuration.BaudRate <= 0)
                {
                    _logger.LogWarning("BaudRate debe ser mayor que 0");
                    isValid = false;
                }
                
                if (_configuration.ConnectionTimeoutMinutes <= 0)
                {
                    _logger.LogWarning("ConnectionTimeoutMinutes debe ser mayor que 0");
                    isValid = false;
                }
                
                if (_configuration.AlarmVolume < 0 || _configuration.AlarmVolume > 100)
                {
                    _logger.LogWarning("AlarmVolume debe estar entre 0 y 100");
                    isValid = false;
                }
                
                if (_configuration.GraphicsUpdateInterval < 50)
                {
                    _logger.LogWarning("GraphicsUpdateInterval debe ser al menos 50ms");
                    isValid = false;
                }
                
                return isValid;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al validar configuración", ex);
                return false;
            }
        }

        /// <summary>
        /// Asegura que el directorio de configuración existe
        /// </summary>
        /// <param name="configDirectory">Directorio de configuración</param>
        private void EnsureConfigDirectoryExists(string configDirectory)
        {
            try
            {
                if (!Directory.Exists(configDirectory))
                {
                    Directory.CreateDirectory(configDirectory);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError("Error al crear directorio de configuración", ex);
            }
        }

        /// <summary>
        /// Exporta la configuración a un archivo específico
        /// </summary>
        /// <param name="filePath">Ruta del archivo de exportación</param>
        public void ExportConfiguration(string filePath)
        {
            try
            {
                var jsonString = JsonConvert.SerializeObject(_configuration, Formatting.Indented);
                File.WriteAllText(filePath, jsonString);
                _logger.LogInfo($"Configuración exportada a: {filePath}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al exportar configuración a {filePath}", ex);
            }
        }

        /// <summary>
        /// Importa configuración desde un archivo
        /// </summary>
        /// <param name="filePath">Ruta del archivo de importación</param>
        public void ImportConfiguration(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    var jsonString = File.ReadAllText(filePath);
                    var importedConfig = JsonConvert.DeserializeObject<SecurityConfiguration>(jsonString);
                    
                    _configuration = importedConfig;
                    SaveConfiguration();
                    _logger.LogInfo($"Configuración importada desde: {filePath}");
                }
                else
                {
                    _logger.LogWarning($"Archivo de configuración no encontrado: {filePath}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al importar configuración desde {filePath}", ex);
            }
        }
    }
}
