using System;
using System.IO;
using AplicacionS.Interfaces;

namespace AplicacionS.Services
{
    /// <summary>
    /// Servicio de logging para registrar eventos del sistema
    /// </summary>
    public class FileLoggingService : ILoggingService
    {
        private readonly string _logDirectory;
        private readonly object _lockObject = new object();

        /// <summary>
        /// Constructor del servicio de logging
        /// </summary>
        /// <param name="logDirectory">Directorio donde guardar los logs</param>
        public FileLoggingService(string logDirectory = null)
        {
            _logDirectory = logDirectory ?? Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "SeguridadPerimetral", "Logs");
            
            EnsureLogDirectoryExists();
        }

        /// <summary>
        /// Registra un mensaje de información
        /// </summary>
        /// <param name="message">Mensaje a registrar</param>
        public void LogInfo(string message)
        {
            WriteLog("INFO", message);
        }

        /// <summary>
        /// Registra un mensaje de advertencia
        /// </summary>
        /// <param name="message">Mensaje a registrar</param>
        public void LogWarning(string message)
        {
            WriteLog("WARN", message);
        }

        /// <summary>
        /// Registra un error
        /// </summary>
        /// <param name="message">Mensaje del error</param>
        /// <param name="exception">Excepción relacionada (opcional)</param>
        public void LogError(string message, Exception exception = null)
        {
            var fullMessage = exception != null 
                ? $"{message} | Exception: {exception.Message} | StackTrace: {exception.StackTrace}"
                : message;
            
            WriteLog("ERROR", fullMessage);
        }

        /// <summary>
        /// Registra un mensaje de debug
        /// </summary>
        /// <param name="message">Mensaje a registrar</param>
        public void LogDebug(string message)
        {
#if DEBUG
            WriteLog("DEBUG", message);
#endif
        }

        /// <summary>
        /// Escribe el mensaje al archivo de log
        /// </summary>
        /// <param name="level">Nivel del log</param>
        /// <param name="message">Mensaje a escribir</param>
        private void WriteLog(string level, string message)
        {
            try
            {
                lock (_lockObject)
                {
                    var logFileName = $"SeguridadPerimetral_{DateTime.Now:yyyyMMdd}.log";
                    var logFilePath = Path.Combine(_logDirectory, logFileName);
                    
                    var logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} [{level}] {message}{Environment.NewLine}";
                    
                    File.AppendAllText(logFilePath, logEntry);
                }
            }
            catch
            {
                // En caso de error al escribir log, no hacer nada para evitar bucles
            }
        }

        /// <summary>
        /// Asegura que el directorio de logs existe
        /// </summary>
        private void EnsureLogDirectoryExists()
        {
            try
            {
                if (!Directory.Exists(_logDirectory))
                {
                    Directory.CreateDirectory(_logDirectory);
                }
            }
            catch
            {
                // En caso de error, usar directorio temporal
                _logDirectory = Path.GetTempPath();
            }
        }

        /// <summary>
        /// Limpia logs antiguos (más de 30 días)
        /// </summary>
        public void CleanOldLogs()
        {
            try
            {
                var files = Directory.GetFiles(_logDirectory, "SeguridadPerimetral_*.log");
                var cutoffDate = DateTime.Now.AddDays(-30);

                foreach (var file in files)
                {
                    var fileInfo = new FileInfo(file);
                    if (fileInfo.CreationTime < cutoffDate)
                    {
                        fileInfo.Delete();
                    }
                }
            }
            catch (Exception ex)
            {
                LogError("Error al limpiar logs antiguos", ex);
            }
        }
    }
}
