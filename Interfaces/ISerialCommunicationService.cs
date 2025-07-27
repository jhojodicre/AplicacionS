using System;

namespace AplicacionS.Interfaces
{
    /// <summary>
    /// Interfaz para el servicio de comunicación serial
    /// </summary>
    public interface ISerialCommunicationService
    {
        /// <summary>
        /// Evento que se dispara cuando se reciben datos
        /// </summary>
        event EventHandler<string> DataReceived;
        
        /// <summary>
        /// Conecta al puerto serial especificado
        /// </summary>
        /// <param name="portName">Nombre del puerto COM</param>
        /// <returns>True si la conexión fue exitosa</returns>
        bool Connect(string portName);
        
        /// <summary>
        /// Desconecta del puerto serial
        /// </summary>
        void Disconnect();
        
        /// <summary>
        /// Envía un comando al dispositivo conectado
        /// </summary>
        /// <param name="command">Comando a enviar</param>
        void SendCommand(string command);
        
        /// <summary>
        /// Verifica si la conexión está activa
        /// </summary>
        bool IsConnected { get; }
        
        /// <summary>
        /// Obtiene los puertos COM disponibles
        /// </summary>
        /// <returns>Array de nombres de puertos disponibles</returns>
        string[] GetAvailablePorts();
    }
}
