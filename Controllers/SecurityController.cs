using System;
using System.Collections.Generic;
using System.Linq;
using AplicacionS.Interfaces;
using AplicacionS.Models;

namespace AplicacionS.Controllers
{
    /// <summary>
    /// Controlador principal para el sistema de seguridad perimetral
    /// </summary>
    public class SecurityController
    {
        private readonly Dictionary<int, SecurityNode> _nodes;
        private readonly ISerialCommunicationService _serialService;
        private readonly ILoggingService _logger;

        /// <summary>
        /// Evento que se dispara cuando cambia el estado de una zona
        /// </summary>
        public event EventHandler<SecurityZone> ZoneStatusChanged;

        /// <summary>
        /// Evento que se dispara cuando se detecta una nueva alarma
        /// </summary>
        public event EventHandler<SecurityZone> AlarmTriggered;

        /// <summary>
        /// Evento que se dispara cuando se reconoce una alarma
        /// </summary>
        public event EventHandler<SecurityNode> AlarmAcknowledged;

        /// <summary>
        /// Constructor del controlador
        /// </summary>
        /// <param name="serialService">Servicio de comunicación serial</param>
        /// <param name="logger">Servicio de logging</param>
        public SecurityController(ISerialCommunicationService serialService, ILoggingService logger)
        {
            _serialService = serialService ?? throw new ArgumentNullException(nameof(serialService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            
            _nodes = new Dictionary<int, SecurityNode>();
            InitializeNodes();
            
            _serialService.DataReceived += OnDataReceived;
            _logger.LogInfo("Controlador de seguridad inicializado");
        }

        /// <summary>
        /// Inicializa los 5 nodos del sistema
        /// </summary>
        private void InitializeNodes()
        {
            for (int i = 1; i <= 5; i++)
            {
                _nodes[i] = new SecurityNode(i);
            }
            _logger.LogInfo("Nodos de seguridad inicializados");
        }

        /// <summary>
        /// Obtiene un nodo específico
        /// </summary>
        /// <param name="nodeNumber">Número del nodo (1-5)</param>
        /// <returns>El nodo solicitado o null si no existe</returns>
        public SecurityNode GetNode(int nodeNumber)
        {
            return _nodes.ContainsKey(nodeNumber) ? _nodes[nodeNumber] : null;
        }

        /// <summary>
        /// Obtiene todos los nodos
        /// </summary>
        /// <returns>Lista de todos los nodos</returns>
        public List<SecurityNode> GetAllNodes()
        {
            return _nodes.Values.ToList();
        }

        /// <summary>
        /// Obtiene todas las zonas del sistema
        /// </summary>
        /// <returns>Lista de todas las zonas</returns>
        public List<SecurityZone> GetAllZones()
        {
            var zones = new List<SecurityZone>();
            foreach (var node in _nodes.Values)
            {
                zones.AddRange(node.GetAllZones());
            }
            return zones;
        }

        /// <summary>
        /// Reconoce las alarmas de un nodo específico
        /// </summary>
        /// <param name="nodeNumber">Número del nodo</param>
        public void AcknowledgeNodeAlarms(int nodeNumber)
        {
            var node = GetNode(nodeNumber);
            if (node != null && node.HasActiveAlarm())
            {
                var previouslyActiveZones = node.GetAllZones()
                    .Where(z => z.Status == ZoneStatus.Alarm)
                    .ToList();

                node.AcknowledgeAlarms();
                
                foreach (var zone in previouslyActiveZones)
                {
                    _logger.LogInfo($"Alarma reconocida - Nodo {nodeNumber}, Zona {zone.ZoneType}");
                    ZoneStatusChanged?.Invoke(this, zone);
                }

                AlarmAcknowledged?.Invoke(this, node);
            }
        }

        /// <summary>
        /// Reinicia un nodo específico
        /// </summary>
        /// <param name="nodeNumber">Número del nodo a reiniciar</param>
        public void ResetNode(int nodeNumber)
        {
            var node = GetNode(nodeNumber);
            if (node != null)
            {
                var resetCommand = node.GetResetCommand();
                _serialService.SendCommand(resetCommand);
                
                node.Reset();
                _logger.LogInfo($"Nodo {nodeNumber} reiniciado");

                // Notificar cambios en las zonas
                foreach (var zone in node.GetAllZones())
                {
                    ZoneStatusChanged?.Invoke(this, zone);
                }
            }
        }

        /// <summary>
        /// Reinicia todos los nodos
        /// </summary>
        public void ResetAllNodes()
        {
            _serialService.SendCommand("B70A9"); // Comando para resetear todos los nodos
            
            foreach (var node in _nodes.Values)
            {
                node.Reset();
                foreach (var zone in node.GetAllZones())
                {
                    ZoneStatusChanged?.Invoke(this, zone);
                }
            }
            
            _logger.LogInfo("Todos los nodos han sido reiniciados");
        }

        /// <summary>
        /// Verifica si hay alarmas activas en el sistema
        /// </summary>
        /// <returns>True si hay al menos una alarma activa</returns>
        public bool HasActiveAlarms()
        {
            return _nodes.Values.Any(node => node.HasActiveAlarm());
        }

        /// <summary>
        /// Obtiene las zonas con alarmas activas
        /// </summary>
        /// <returns>Lista de zonas con alarmas</returns>
        public List<SecurityZone> GetActiveAlarms()
        {
            return GetAllZones().Where(zone => zone.Status == ZoneStatus.Alarm).ToList();
        }

        /// <summary>
        /// Obtiene nodos con batería baja
        /// </summary>
        /// <returns>Lista de nodos con batería baja</returns>
        public List<SecurityNode> GetNodesWithLowBattery()
        {
            return _nodes.Values.Where(node => node.HasLowBattery()).ToList();
        }

        /// <summary>
        /// Maneja los datos recibidos del puerto serial
        /// </summary>
        /// <param name="sender">Remitente del evento</param>
        /// <param name="data">Datos recibidos</param>
        private void OnDataReceived(object sender, string data)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(data) || data.Length != 12)
                {
                    _logger.LogWarning($"Datos recibidos en formato incorrecto: {data}");
                    return;
                }

                var parts = data.Split(',');
                if (parts.Length != 4)
                {
                    _logger.LogWarning($"Mensaje con formato incorrecto: {data}");
                    return;
                }

                var messageType = parts[0].Trim();
                var statusString = parts[1].Trim();
                var nodeNumberString = parts[2].Trim();
                var zoneString = parts[3].Trim();

                // Validar formato
                if (!int.TryParse(nodeNumberString, out int nodeNumber) || 
                    nodeNumber < 1 || nodeNumber > 5)
                {
                    _logger.LogWarning($"Número de nodo inválido: {nodeNumberString}");
                    return;
                }

                if (zoneString.Length != 1 || (zoneString != "A" && zoneString != "B"))
                {
                    _logger.LogWarning($"Zona inválida: {zoneString}");
                    return;
                }

                var zoneType = zoneString[0];
                var node = GetNode(nodeNumber);
                if (node == null)
                {
                    _logger.LogError($"Nodo {nodeNumber} no encontrado");
                    return;
                }

                var zone = node.GetZone(zoneType);
                if (zone == null)
                {
                    _logger.LogError($"Zona {zoneType} no encontrada en nodo {nodeNumber}");
                    return;
                }

                // Procesar el mensaje
                ProcessMessage(messageType, statusString, node, zone);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al procesar datos recibidos: {data}", ex);
            }
        }

        /// <summary>
        /// Procesa un mensaje específico del protocolo
        /// </summary>
        /// <param name="messageType">Tipo de mensaje</param>
        /// <param name="statusString">Estado en formato string</param>
        /// <param name="node">Nodo afectado</param>
        /// <param name="zone">Zona afectada</param>
        private void ProcessMessage(string messageType, string statusString, SecurityNode node, SecurityZone zone)
        {
            var previousStatus = zone.Status;
            
            // Manejar mensaje de batería
            if (messageType == "BAT")
            {
                zone.HasLowBattery = true;
                _logger.LogWarning($"Batería baja detectada - Nodo {node.NodeNumber}, Zona {zone.ZoneType}");
                return;
            }

            // Convertir estado del protocolo
            var newStatus = SecurityZone.FromProtocolString(statusString);
            
            // Actualizar estado de la zona
            zone.UpdateStatus(newStatus);
            
            // Log del cambio
            _logger.LogInfo($"Estado actualizado - Nodo {node.NodeNumber}, Zona {zone.ZoneType}: {previousStatus} -> {newStatus}");
            
            // Notificar cambio de estado
            ZoneStatusChanged?.Invoke(this, zone);
            
            // Si es una nueva alarma, disparar evento específico
            if (newStatus == ZoneStatus.Alarm && previousStatus != ZoneStatus.Alarm)
            {
                _logger.LogWarning($"¡NUEVA ALARMA! - Nodo {node.NodeNumber}, Zona {zone.ZoneType}");
                AlarmTriggered?.Invoke(this, zone);
            }
        }

        /// <summary>
        /// Obtiene estadísticas del sistema
        /// </summary>
        /// <returns>Resumen de estadísticas</returns>
        public SystemStatistics GetSystemStatistics()
        {
            var zones = GetAllZones();
            var nodes = GetAllNodes();

            return new SystemStatistics
            {
                TotalNodes = nodes.Count,
                NodesOnline = nodes.Count(n => n.IsOnline()),
                TotalZones = zones.Count,
                ZonesOK = zones.Count(z => z.Status == ZoneStatus.OK),
                ActiveAlarms = zones.Count(z => z.Status == ZoneStatus.Alarm),
                AcknowledgedAlarms = zones.Count(z => z.Status == ZoneStatus.Acknowledged),
                ZonesWithErrors = zones.Count(z => z.Status == ZoneStatus.Error),
                NodesWithLowBattery = nodes.Count(n => n.HasLowBattery())
            };
        }
    }

    /// <summary>
    /// Clase para estadísticas del sistema
    /// </summary>
    public class SystemStatistics
    {
        public int TotalNodes { get; set; }
        public int NodesOnline { get; set; }
        public int TotalZones { get; set; }
        public int ZonesOK { get; set; }
        public int ActiveAlarms { get; set; }
        public int AcknowledgedAlarms { get; set; }
        public int ZonesWithErrors { get; set; }
        public int NodesWithLowBattery { get; set; }
    }
}
