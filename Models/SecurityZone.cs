using System;

namespace AplicacionS.Models
{
    /// <summary>
    /// Representa el estado de una zona de seguridad
    /// </summary>
    public enum ZoneStatus
    {
        /// <summary>Zona funcionando correctamente</summary>
        OK,
        /// <summary>Alarma activa - intrusión detectada</summary>
        Alarm,
        /// <summary>Error en la zona</summary>
        Error,
        /// <summary>Falla en el sistema</summary>
        Fault,
        /// <summary>Alarma reconocida</summary>
        Acknowledged,
        /// <summary>Batería baja</summary>
        LowBattery,
        /// <summary>Estado desconocido</summary>
        Unknown
    }

    /// <summary>
    /// Modelo mejorado para representar una zona de seguridad
    /// </summary>
    public class SecurityZone
    {
        /// <summary>
        /// Identificador único de la zona
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Número del nodo al que pertenece (1-5)
        /// </summary>
        public int NodeNumber { get; set; }

        /// <summary>
        /// Zona dentro del nodo (A o B)
        /// </summary>
        public char ZoneType { get; set; }

        /// <summary>
        /// Estado actual de la zona
        /// </summary>
        public ZoneStatus Status { get; set; }

        /// <summary>
        /// Fecha y hora de la última actualización
        /// </summary>
        public DateTime LastUpdate { get; set; }

        /// <summary>
        /// Indica si la zona tiene batería baja
        /// </summary>
        public bool HasLowBattery { get; set; }

        /// <summary>
        /// Descripción opcional de la zona
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public SecurityZone()
        {
            Status = ZoneStatus.Unknown;
            LastUpdate = DateTime.Now;
            HasLowBattery = false;
        }

        /// <summary>
        /// Constructor con parámetros
        /// </summary>
        /// <param name="nodeNumber">Número del nodo</param>
        /// <param name="zoneType">Tipo de zona (A o B)</param>
        public SecurityZone(int nodeNumber, char zoneType) : this()
        {
            NodeNumber = nodeNumber;
            ZoneType = zoneType;
            Id = (nodeNumber - 1) * 2 + (zoneType == 'A' ? 1 : 2);
        }

        /// <summary>
        /// Actualiza el estado de la zona
        /// </summary>
        /// <param name="newStatus">Nuevo estado</param>
        public void UpdateStatus(ZoneStatus newStatus)
        {
            Status = newStatus;
            LastUpdate = DateTime.Now;
        }

        /// <summary>
        /// Obtiene una descripción legible del estado
        /// </summary>
        /// <returns>Descripción del estado actual</returns>
        public string GetStatusDescription()
        {
            return Status switch
            {
                ZoneStatus.OK => "Zona Segura",
                ZoneStatus.Alarm => "¡ALARMA ACTIVA!",
                ZoneStatus.Error => "Error en Zona",
                ZoneStatus.Fault => "Falla del Sistema",
                ZoneStatus.Acknowledged => "Alarma Reconocida",
                ZoneStatus.LowBattery => "Batería Baja",
                ZoneStatus.Unknown => "Estado Desconocido",
                _ => "Estado No Definido"
            };
        }

        /// <summary>
        /// Convierte el estado a string compatible con el protocolo legacy
        /// </summary>
        /// <returns>Código de estado para protocolo</returns>
        public string ToProtocolString()
        {
            return Status switch
            {
                ZoneStatus.OK => "BOK",
                ZoneStatus.Alarm => "NOK",
                ZoneStatus.Error => "ERR",
                ZoneStatus.Fault => "FAL",
                ZoneStatus.Acknowledged => "ACK",
                ZoneStatus.LowBattery => "BAT",
                _ => "UNK"
            };
        }

        /// <summary>
        /// Crea una zona desde un string de protocolo
        /// </summary>
        /// <param name="protocolStatus">Estado en formato protocolo</param>
        /// <returns>Estado correspondiente</returns>
        public static ZoneStatus FromProtocolString(string protocolStatus)
        {
            return protocolStatus?.ToUpper() switch
            {
                "BOK" => ZoneStatus.OK,
                "NOK" => ZoneStatus.Alarm,
                "ERR" => ZoneStatus.Error,
                "FAL" => ZoneStatus.Fault,
                "ACK" => ZoneStatus.Acknowledged,
                "BAT" => ZoneStatus.LowBattery,
                _ => ZoneStatus.Unknown
            };
        }

        public override string ToString()
        {
            return $"Nodo {NodeNumber}-{ZoneType}: {GetStatusDescription()}";
        }
    }
}
