using System;

namespace AplicacionS.Interfaces
{
    /// <summary>
    /// Interfaz para el servicio de logging
    /// </summary>
    public interface ILoggingService
    {
        /// <summary>
        /// Registra un mensaje de información
        /// </summary>
        /// <param name="message">Mensaje a registrar</param>
        void LogInfo(string message);
        
        /// <summary>
        /// Registra un mensaje de advertencia
        /// </summary>
        /// <param name="message">Mensaje a registrar</param>
        void LogWarning(string message);
        
        /// <summary>
        /// Registra un error
        /// </summary>
        /// <param name="message">Mensaje del error</param>
        /// <param name="exception">Excepción relacionada (opcional)</param>
        void LogError(string message, Exception exception = null);
        
        /// <summary>
        /// Registra un mensaje de debug
        /// </summary>
        /// <param name="message">Mensaje a registrar</param>
        void LogDebug(string message);
    }
}
