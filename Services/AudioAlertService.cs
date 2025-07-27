using System;
using System.IO;
using System.Media;
using System.Threading.Tasks;
using AplicacionS.Interfaces;

namespace AplicacionS.Services
{
    /// <summary>
    /// Servicio para manejar alertas sonoras del sistema
    /// </summary>
    public class AudioAlertService : IDisposable
    {
        private readonly ILoggingService _logger;
        private readonly ConfigurationService _config;
        private SoundPlayer _alarmPlayer;
        private SoundPlayer _warningPlayer;
        private bool _disposed = false;
        private bool _isPlayingAlarm = false;

        /// <summary>
        /// Constructor del servicio de audio
        /// </summary>
        /// <param name="logger">Servicio de logging</param>
        /// <param name="config">Servicio de configuración</param>
        public AudioAlertService(ILoggingService logger, ConfigurationService config)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _config = config ?? throw new ArgumentNullException(nameof(config));
            
            InitializeAudioPlayers();
        }

        /// <summary>
        /// Inicializa los reproductores de audio
        /// </summary>
        private void InitializeAudioPlayers()
        {
            try
            {
                _alarmPlayer = new SoundPlayer();
                _warningPlayer = new SoundPlayer();
                
                LoadSoundFiles();
                _logger.LogInfo("Servicio de audio inicializado correctamente");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al inicializar servicio de audio", ex);
            }
        }

        /// <summary>
        /// Carga los archivos de sonido desde la configuración
        /// </summary>
        private void LoadSoundFiles()
        {
            try
            {
                var alarmSoundPath = GetFullSoundPath(_config.Current.Sounds.AlarmSound);
                var warningSoundPath = GetFullSoundPath(_config.Current.Sounds.WarningSound);

                if (File.Exists(alarmSoundPath))
                {
                    _alarmPlayer.SoundLocation = alarmSoundPath;
                    _logger.LogInfo($"Sonido de alarma cargado: {alarmSoundPath}");
                }
                else
                {
                    _logger.LogWarning($"Archivo de sonido de alarma no encontrado: {alarmSoundPath}");
                }

                if (File.Exists(warningSoundPath))
                {
                    _warningPlayer.SoundLocation = warningSoundPath;
                    _logger.LogInfo($"Sonido de advertencia cargado: {warningSoundPath}");
                }
                else
                {
                    _logger.LogWarning($"Archivo de sonido de advertencia no encontrado: {warningSoundPath}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al cargar archivos de sonido", ex);
            }
        }

        /// <summary>
        /// Obtiene la ruta completa del archivo de sonido
        /// </summary>
        /// <param name="relativePath">Ruta relativa del archivo</param>
        /// <returns>Ruta completa del archivo</returns>
        private string GetFullSoundPath(string relativePath)
        {
            if (Path.IsPathRooted(relativePath))
                return relativePath;

            // Intentar ruta relativa al ejecutable
            var executablePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var fullPath = Path.Combine(executablePath, relativePath);
            
            if (File.Exists(fullPath))
                return fullPath;

            // Intentar ruta relativa al directorio de trabajo
            fullPath = Path.Combine(Environment.CurrentDirectory, relativePath);
            if (File.Exists(fullPath))
                return fullPath;

            return relativePath; // Devolver ruta original si no se encuentra
        }

        /// <summary>
        /// Reproduce el sonido de alarma
        /// </summary>
        public void PlayAlarm()
        {
            if (!_config.Current.EnableAlarmSounds)
            {
                _logger.LogDebug("Sonidos de alarma deshabilitados en configuración");
                return;
            }

            try
            {
                if (_alarmPlayer?.SoundLocation != null && !_isPlayingAlarm)
                {
                    _isPlayingAlarm = true;
                    _alarmPlayer.PlayLooping();
                    _logger.LogInfo("Reproduciendo sonido de alarma");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al reproducir sonido de alarma", ex);
                _isPlayingAlarm = false;
            }
        }

        /// <summary>
        /// Reproduce el sonido de alarma de forma asíncrona
        /// </summary>
        public async Task PlayAlarmAsync()
        {
            await Task.Run(() => PlayAlarm());
        }

        /// <summary>
        /// Detiene el sonido de alarma
        /// </summary>
        public void StopAlarm()
        {
            try
            {
                if (_isPlayingAlarm)
                {
                    _alarmPlayer?.Stop();
                    _isPlayingAlarm = false;
                    _logger.LogInfo("Sonido de alarma detenido");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al detener sonido de alarma", ex);
            }
        }

        /// <summary>
        /// Reproduce un sonido de advertencia una sola vez
        /// </summary>
        public void PlayWarning()
        {
            if (!_config.Current.EnableAlarmSounds)
            {
                _logger.LogDebug("Sonidos de alarma deshabilitados en configuración");
                return;
            }

            try
            {
                if (_warningPlayer?.SoundLocation != null)
                {
                    _warningPlayer.Play();
                    _logger.LogInfo("Reproduciendo sonido de advertencia");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al reproducir sonido de advertencia", ex);
            }
        }

        /// <summary>
        /// Reproduce un sonido de advertencia de forma asíncrona
        /// </summary>
        public async Task PlayWarningAsync()
        {
            await Task.Run(() => PlayWarning());
        }

        /// <summary>
        /// Reproduce un beep del sistema
        /// </summary>
        /// <param name="frequency">Frecuencia del beep</param>
        /// <param name="duration">Duración en milisegundos</param>
        public void PlaySystemBeep(int frequency = 800, int duration = 200)
        {
            if (!_config.Current.EnableAlarmSounds)
                return;

            try
            {
                Console.Beep(frequency, duration);
                _logger.LogDebug($"Beep del sistema reproducido: {frequency}Hz, {duration}ms");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al reproducir beep del sistema", ex);
            }
        }

        /// <summary>
        /// Reproduce un beep del sistema de forma asíncrona
        /// </summary>
        /// <param name="frequency">Frecuencia del beep</param>
        /// <param name="duration">Duración en milisegundos</param>
        public async Task PlaySystemBeepAsync(int frequency = 800, int duration = 200)
        {
            await Task.Run(() => PlaySystemBeep(frequency, duration));
        }

        /// <summary>
        /// Verifica si hay algún sonido reproduciéndose
        /// </summary>
        public bool IsPlayingAudio => _isPlayingAlarm;

        /// <summary>
        /// Detiene todos los sonidos
        /// </summary>
        public void StopAllSounds()
        {
            try
            {
                StopAlarm();
                _warningPlayer?.Stop();
                _logger.LogInfo("Todos los sonidos detenidos");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al detener todos los sonidos", ex);
            }
        }

        /// <summary>
        /// Actualiza la configuración de sonido
        /// </summary>
        public void RefreshSoundConfiguration()
        {
            try
            {
                StopAllSounds();
                LoadSoundFiles();
                _logger.LogInfo("Configuración de sonido actualizada");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al actualizar configuración de sonido", ex);
            }
        }

        /// <summary>
        /// Libera los recursos utilizados
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Libera los recursos manejados y no manejados
        /// </summary>
        /// <param name="disposing">True si se está liberando explícitamente</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    try
                    {
                        StopAllSounds();
                        _alarmPlayer?.Dispose();
                        _warningPlayer?.Dispose();
                    }
                    catch (Exception ex)
                    {
                        _logger?.LogError("Error al liberar recursos de audio", ex);
                    }
                }
                _disposed = true;
            }
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~AudioAlertService()
        {
            Dispose(false);
        }
    }
}
