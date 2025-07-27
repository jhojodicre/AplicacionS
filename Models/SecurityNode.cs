using System;
using System.Collections.Generic;
using System.Linq;

namespace AplicacionS.Models
{
    /// <summary>
    /// Modelo mejorado para representar un nodo de seguridad
    /// </summary>
    public class SecurityNode
    {
        /// <summary>
        /// Número del nodo (1-5)
        /// </summary>
        public int NodeNumber { get; set; }

        /// <summary>
        /// Zona A del nodo
        /// </summary>
        public SecurityZone ZoneA { get; set; }

        /// <summary>
        /// Zona B del nodo
        /// </summary>
        public SecurityZone ZoneB { get; set; }

        /// <summary>
        /// Indica si el nodo está activo
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Fecha y hora de la última comunicación
        /// </summary>
        public DateTime LastCommunication { get; set; }

        /// <summary>
        /// Descripción del nodo
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Constructor del nodo
        /// </summary>
        /// <param name="nodeNumber">Número del nodo</param>
        public SecurityNode(int nodeNumber)
        {
            NodeNumber = nodeNumber;
            ZoneA = new SecurityZone(nodeNumber, 'A');
            ZoneB = new SecurityZone(nodeNumber, 'B');
            IsActive = false;
            LastCommunication = DateTime.MinValue;
            Description = $"Nodo de Seguridad #{nodeNumber}";
        }

        /// <summary>
        /// Actualiza el estado de una zona específica
        /// </summary>
        /// <param name="zoneType">Tipo de zona (A o B)</param>
        /// <param name="status">Nuevo estado</param>
        public void UpdateZoneStatus(char zoneType, ZoneStatus status)
        {
            var zone = GetZone(zoneType);
            if (zone != null)
            {
                zone.UpdateStatus(status);
                LastCommunication = DateTime.Now;
                IsActive = true;
            }
        }

        /// <summary>
        /// Obtiene una zona específica
        /// </summary>
        /// <param name="zoneType">Tipo de zona (A o B)</param>
        /// <returns>La zona solicitada o null si no existe</returns>
        public SecurityZone GetZone(char zoneType)
        {
            return zoneType.ToString().ToUpper() switch
            {
                "A" => ZoneA,
                "B" => ZoneB,
                _ => null
            };
        }

        /// <summary>
        /// Obtiene todas las zonas del nodo
        /// </summary>
        /// <returns>Lista de zonas</returns>
        public List<SecurityZone> GetAllZones()
        {
            return new List<SecurityZone> { ZoneA, ZoneB };
        }

        /// <summary>
        /// Verifica si alguna zona tiene alarma activa
        /// </summary>
        /// <returns>True si hay alarma activa</returns>
        public bool HasActiveAlarm()
        {
            return ZoneA.Status == ZoneStatus.Alarm || ZoneB.Status == ZoneStatus.Alarm;
        }

        /// <summary>
        /// Verifica si alguna zona tiene alarma no reconocida
        /// </summary>
        /// <returns>True si hay alarma sin reconocer</returns>
        public bool HasUnacknowledgedAlarm()
        {
            return HasActiveAlarm() && 
                   ZoneA.Status != ZoneStatus.Acknowledged && 
                   ZoneB.Status != ZoneStatus.Acknowledged;
        }

        /// <summary>
        /// Verifica si alguna zona tiene batería baja
        /// </summary>
        /// <returns>True si hay batería baja</returns>
        public bool HasLowBattery()
        {
            return ZoneA.HasLowBattery || ZoneB.HasLowBattery;
        }

        /// <summary>
        /// Reconoce las alarmas activas del nodo
        /// </summary>
        public void AcknowledgeAlarms()
        {
            if (ZoneA.Status == ZoneStatus.Alarm)
                ZoneA.UpdateStatus(ZoneStatus.Acknowledged);
            if (ZoneB.Status == ZoneStatus.Alarm)
                ZoneB.UpdateStatus(ZoneStatus.Acknowledged);
        }

        /// <summary>
        /// Reinicia el estado del nodo
        /// </summary>
        public void Reset()
        {
            ZoneA.UpdateStatus(ZoneStatus.OK);
            ZoneB.UpdateStatus(ZoneStatus.OK);
            ZoneA.HasLowBattery = false;
            ZoneB.HasLowBattery = false;
        }

        /// <summary>
        /// Obtiene el comando de reset para este nodo
        /// </summary>
        /// <returns>Comando de reset</returns>
        public string GetResetCommand()
        {
            return NodeNumber switch
            {
                1 => "B71A9",
                2 => "B72A9", 
                3 => "B32A9",
                _ => $"B{NodeNumber}0A9"
            };
        }

        /// <summary>
        /// Verifica si el nodo está en línea
        /// </summary>
        /// <param name="timeoutMinutes">Minutos de timeout</param>
        /// <returns>True si está en línea</returns>
        public bool IsOnline(int timeoutMinutes = 5)
        {
            return IsActive && 
                   DateTime.Now.Subtract(LastCommunication).TotalMinutes < timeoutMinutes;
        }

        public override string ToString()
        {
            var alarmStatus = HasActiveAlarm() ? " [ALARMA]" : "";
            var batteryStatus = HasLowBattery() ? " [BAT BAJA]" : "";
            var onlineStatus = IsOnline() ? " [EN LÍNEA]" : " [DESCONECTADO]";
            
            return $"{Description}{alarmStatus}{batteryStatus}{onlineStatus}";
        }
    }
}
