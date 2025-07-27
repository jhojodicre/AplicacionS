using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AplicacionS.Controllers;
using AplicacionS.Interfaces;
using AplicacionS.Models;
using AplicacionS.Services;
using AplicacionS.Utils;

namespace AplicacionS.Views
{
    /// <summary>
    /// Formulario principal mejorado para el sistema de seguridad perimetral
    /// </summary>
    public partial class MainForm : Form
    {
        #region Private Fields
        private readonly SecurityController _securityController;
        private readonly ISerialCommunicationService _serialService;
        private readonly ILoggingService _logger;
        private readonly ConfigurationService _config;
        private readonly AudioAlertService _audioService;
        
        private Graphics _perimeterGraphics;
        private Timer _updateTimer;
        private Timer _statusTimer;
        
        // UI Controls - se definen en el designer
        private ComboBox _comPortComboBox;
        private Button _connectButton;
        private Button _consoleButton;
        private TextBox _consoleTextBox;
        private PictureBox _perimeterPictureBox;
        private Label _statusLabel;
        private Label _statisticsLabel;
        
        // Botones de ACK por nodo
        private Dictionary<int, Button> _ackButtons;
        private Dictionary<int, Button> _resetButtons;
        private Dictionary<int, Label> _batteryLabels;
        private Dictionary<int, Label> _zoneLabels;
        
        private bool _consoleVisible = false;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor del formulario principal
        /// </summary>
        public MainForm()
        {
            try
            {
                // Inicializar servicios
                _logger = new FileLoggingService();
                _config = new ConfigurationService(_logger);
                _serialService = new SerialCommunicationService(_logger);
                _audioService = new AudioAlertService(_logger, _config);
                _securityController = new SecurityController(_serialService, _logger);
                
                InitializeComponent();
                InitializeCustomComponents();
                SetupEventHandlers();
                
                _logger.LogInfo("Formulario principal inicializado correctamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al inicializar la aplicación: {ex.Message}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Inicializa componentes personalizados
        /// </summary>
        private void InitializeCustomComponents()
        {
            try
            {
                // Configurar ventana
                this.WindowState = FormWindowState.Maximized;
                this.Text = "Sistema de Seguridad Perimetral v2.0";
                this.BackColor = Color.White;
                
                // Inicializar diccionarios de controles
                _ackButtons = new Dictionary<int, Button>();
                _resetButtons = new Dictionary<int, Button>();
                _batteryLabels = new Dictionary<int, Label>();
                _zoneLabels = new Dictionary<int, Label>();
                
                // Configurar gráficos del perímetro
                if (_perimeterPictureBox != null)
                {
                    _perimeterGraphics = _perimeterPictureBox.CreateGraphics();
                }
                
                // Configurar timers
                _updateTimer = new Timer
                {
                    Interval = _config.Current.GraphicsUpdateInterval,
                    Enabled = false
                };
                _updateTimer.Tick += UpdateTimer_Tick;
                
                _statusTimer = new Timer
                {
                    Interval = 1000, // Actualizar cada segundo
                    Enabled = true
                };
                _statusTimer.Tick += StatusTimer_Tick;
                
                // Cargar puertos COM disponibles
                LoadAvailableComPorts();
                
                // Ocultar controles inicialmente
                HideControlsInitially();
                
                _logger.LogInfo("Componentes personalizados inicializados");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al inicializar componentes personalizados", ex);
            }
        }

        /// <summary>
        /// Configura los manejadores de eventos
        /// </summary>
        private void SetupEventHandlers()
        {
            try
            {
                // Eventos del controlador de seguridad
                _securityController.ZoneStatusChanged += OnZoneStatusChanged;
                _securityController.AlarmTriggered += OnAlarmTriggered;
                _securityController.AlarmAcknowledged += OnAlarmAcknowledged;
                
                // Eventos del formulario
                this.FormClosed += MainForm_FormClosed;
                this.Load += MainForm_Load;
                
                _logger.LogInfo("Manejadores de eventos configurados");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al configurar manejadores de eventos", ex);
            }
        }

        /// <summary>
        /// Oculta controles que no deben estar visibles inicialmente
        /// </summary>
        private void HideControlsInitially()
        {
            try
            {
                // Ocultar consola
                if (_consoleTextBox != null)
                    _consoleTextBox.Visible = false;
                
                // Ocultar botones ACK y labels de zonas
                foreach (var button in _ackButtons.Values)
                    button.Visible = false;
                
                foreach (var label in _batteryLabels.Values)
                    label.Visible = false;
                
                foreach (var label in _zoneLabels.Values)
                    label.Visible = false;
                
                _logger.LogDebug("Controles ocultados inicialmente");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al ocultar controles inicialmente", ex);
            }
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Maneja el evento de carga del formulario
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                UpdateStatusDisplay();
                _logger.LogInfo("Formulario principal cargado");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al cargar formulario principal", ex);
            }
        }

        /// <summary>
        /// Maneja el evento de cierre del formulario
        /// </summary>
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                _audioService?.StopAllSounds();
                _serialService?.Disconnect();
                _updateTimer?.Stop();
                _statusTimer?.Stop();
                
                _audioService?.Dispose();
                _serialService?.Dispose();
                _updateTimer?.Dispose();
                _statusTimer?.Dispose();
                _perimeterGraphics?.Dispose();
                
                _logger.LogInfo("Aplicación cerrada correctamente");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al cerrar aplicación", ex);
            }
        }

        /// <summary>
        /// Maneja los cambios de estado de zona
        /// </summary>
        private void OnZoneStatusChanged(object sender, SecurityZone zone)
        {
            try
            {
                if (InvokeRequired)
                {
                    Invoke(new Action<object, SecurityZone>(OnZoneStatusChanged), sender, zone);
                    return;
                }
                
                UpdateZoneDisplay(zone);
                UpdateStatusDisplay();
                
                _logger.LogDebug($"Estado de zona actualizado: Nodo {zone.NodeNumber}-{zone.ZoneType}: {zone.Status}");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al manejar cambio de estado de zona", ex);
            }
        }

        /// <summary>
        /// Maneja la activación de nuevas alarmas
        /// </summary>
        private void OnAlarmTriggered(object sender, SecurityZone zone)
        {
            try
            {
                if (InvokeRequired)
                {
                    Invoke(new Action<object, SecurityZone>(OnAlarmTriggered), sender, zone);
                    return;
                }
                
                // Reproducir sonido de alarma
                _audioService.PlayAlarm();
                
                // Mostrar notificación visual
                ShowAlarmNotification(zone);
                
                _logger.LogWarning($"¡ALARMA ACTIVADA! Nodo {zone.NodeNumber}-{zone.ZoneType}");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al manejar alarma activada", ex);
            }
        }

        /// <summary>
        /// Maneja el reconocimiento de alarmas
        /// </summary>
        private void OnAlarmAcknowledged(object sender, SecurityNode node)
        {
            try
            {
                if (InvokeRequired)
                {
                    Invoke(new Action<object, SecurityNode>(OnAlarmAcknowledged), sender, node);
                    return;
                }
                
                // Si no hay más alarmas activas, detener sonido
                if (!_securityController.HasActiveAlarms())
                {
                    _audioService.StopAlarm();
                }
                
                _audioService.PlayWarning(); // Sonido de confirmación
                
                _logger.LogInfo($"Alarmas reconocidas en Nodo {node.NodeNumber}");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al manejar reconocimiento de alarma", ex);
            }
        }

        /// <summary>
        /// Timer para actualizar gráficos
        /// </summary>
        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                UpdatePerimeterGraphics();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error en timer de actualización de gráficos", ex);
            }
        }

        /// <summary>
        /// Timer para actualizar estado general
        /// </summary>
        private void StatusTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                UpdateStatusDisplay();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error en timer de estado", ex);
            }
        }
        #endregion

        #region UI Updates
        /// <summary>
        /// Actualiza la visualización de una zona específica
        /// </summary>
        private void UpdateZoneDisplay(SecurityZone zone)
        {
            try
            {
                if (_perimeterGraphics != null)
                {
                    GraphicsHelper.DrawZone(_perimeterGraphics, zone.NodeNumber, zone.ZoneType, zone.Status);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar visualización de zona {zone.NodeNumber}-{zone.ZoneType}", ex);
            }
        }

        /// <summary>
        /// Actualiza todos los gráficos del perímetro
        /// </summary>
        private void UpdatePerimeterGraphics()
        {
            try
            {
                if (_perimeterGraphics == null) return;
                
                // Limpiar área de dibujo
                var hasAlarms = _securityController.HasActiveAlarms();
                var hasErrors = _securityController.GetAllZones().Any(z => z.Status == ZoneStatus.Error);
                var backgroundColor = GraphicsHelper.GetRecommendedBackgroundColor(hasAlarms, hasErrors);
                
                GraphicsHelper.ClearDrawingArea(_perimeterGraphics, backgroundColor, _perimeterPictureBox.Bounds);
                
                // Dibujar todas las zonas
                var zones = _securityController.GetAllZones();
                foreach (var zone in zones)
                {
                    GraphicsHelper.DrawZone(_perimeterGraphics, zone.NodeNumber, zone.ZoneType, zone.Status);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al actualizar gráficos del perímetro", ex);
            }
        }

        /// <summary>
        /// Actualiza la visualización del estado general
        /// </summary>
        private void UpdateStatusDisplay()
        {
            try
            {
                var stats = _securityController.GetSystemStatistics();
                
                if (_statusLabel != null)
                {
                    var connectionStatus = _serialService.IsConnected ? "CONECTADO" : "DESCONECTADO";
                    var connectionColor = _serialService.IsConnected ? Color.Green : Color.Red;
                    
                    _statusLabel.Text = $"Estado: {connectionStatus}";
                    _statusLabel.ForeColor = connectionColor;
                }
                
                if (_statisticsLabel != null)
                {
                    _statisticsLabel.Text = $"Nodos: {stats.NodesOnline}/{stats.TotalNodes} | " +
                                          $"Zonas OK: {stats.ZonesOK} | " +
                                          $"Alarmas: {stats.ActiveAlarms} | " +
                                          $"Errores: {stats.ZonesWithErrors}";
                }
                
                // Actualizar indicadores de batería
                UpdateBatteryIndicators();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al actualizar visualización de estado", ex);
            }
        }

        /// <summary>
        /// Actualiza los indicadores de batería baja
        /// </summary>
        private void UpdateBatteryIndicators()
        {
            try
            {
                var nodesWithLowBattery = _securityController.GetNodesWithLowBattery();
                
                foreach (var kvp in _batteryLabels)
                {
                    var nodeNumber = kvp.Key;
                    var label = kvp.Value;
                    var hasLowBattery = nodesWithLowBattery.Any(n => n.NodeNumber == nodeNumber);
                    
                    label.Visible = hasLowBattery;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al actualizar indicadores de batería", ex);
            }
        }

        /// <summary>
        /// Muestra una notificación visual de alarma
        /// </summary>
        private void ShowAlarmNotification(SecurityZone zone)
        {
            try
            {
                var message = $"¡ALARMA ACTIVA!\n\nNodo: {zone.NodeNumber}\nZona: {zone.ZoneType}\nHora: {DateTime.Now:HH:mm:ss}";
                var result = MessageBox.Show(message, "ALARMA DE SEGURIDAD", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al mostrar notificación de alarma", ex);
            }
        }
        #endregion

        #region COM Port Management
        /// <summary>
        /// Carga los puertos COM disponibles
        /// </summary>
        private void LoadAvailableComPorts()
        {
            try
            {
                if (_comPortComboBox != null)
                {
                    _comPortComboBox.Items.Clear();
                    var ports = _serialService.GetAvailablePorts();
                    
                    foreach (var port in ports)
                    {
                        _comPortComboBox.Items.Add(port);
                    }
                    
                    // Seleccionar puerto por defecto si está disponible
                    var defaultPort = _config.Current.DefaultComPort;
                    if (!string.IsNullOrEmpty(defaultPort) && _comPortComboBox.Items.Contains(defaultPort))
                    {
                        _comPortComboBox.SelectedItem = defaultPort;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al cargar puertos COM", ex);
            }
        }
        #endregion

        #region Button Event Handlers (to be connected in designer)
        /// <summary>
        /// Maneja el clic del botón de conexión
        /// </summary>
        public void OnConnectButtonClick(object sender, EventArgs e)
        {
            try
            {
                if (_serialService.IsConnected)
                {
                    // Desconectar
                    var result = MessageBox.Show("¿Está seguro que desea desconectar el sistema?", 
                        "Confirmar Desconexión", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    
                    if (result == DialogResult.Yes)
                    {
                        _serialService.Disconnect();
                        _connectButton.Text = "Conectar";
                        _updateTimer.Enabled = false;
                        HideOperationalControls();
                    }
                }
                else
                {
                    // Conectar
                    if (_comPortComboBox.SelectedItem == null)
                    {
                        MessageBox.Show("Seleccione un puerto COM", "Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    
                    var portName = _comPortComboBox.SelectedItem.ToString();
                    if (_serialService.Connect(portName))
                    {
                        _connectButton.Text = "Desconectar";
                        _updateTimer.Enabled = true;
                        ShowOperationalControls();
                        GraphicsHelper.DrawInitialPerimeter(_perimeterGraphics);
                    }
                    else
                    {
                        MessageBox.Show($"No se pudo conectar al puerto {portName}", "Error de Conexión", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error en conexión/desconexión", ex);
                MessageBox.Show("Error en la operación de conexión", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Maneja el clic del botón de consola
        /// </summary>
        public void OnConsoleButtonClick(object sender, EventArgs e)
        {
            try
            {
                _consoleVisible = !_consoleVisible;
                
                if (_consoleTextBox != null)
                {
                    _consoleTextBox.Visible = _consoleVisible;
                }
                
                if (_consoleButton != null)
                {
                    _consoleButton.Text = _consoleVisible ? "Ocultar Consola" : "Mostrar Consola";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al alternar consola", ex);
            }
        }

        /// <summary>
        /// Maneja los clics de botones ACK
        /// </summary>
        public void OnAckButtonClick(object sender, EventArgs e)
        {
            try
            {
                if (sender is Button button && button.Tag is int nodeNumber)
                {
                    var result = MessageBox.Show($"¿Reconocer alarmas del Nodo {nodeNumber}?", 
                        "Confirmar Reconocimiento", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    
                    if (result == DialogResult.Yes)
                    {
                        _securityController.AcknowledgeNodeAlarms(nodeNumber);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al reconocer alarma", ex);
            }
        }

        /// <summary>
        /// Maneja los clics de botones de reset
        /// </summary>
        public void OnResetButtonClick(object sender, EventArgs e)
        {
            try
            {
                if (sender is Button button && button.Tag is int nodeNumber)
                {
                    var result = MessageBox.Show($"¿Reiniciar el Nodo {nodeNumber}?", 
                        "Confirmar Reinicio", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    
                    if (result == DialogResult.Yes)
                    {
                        _securityController.ResetNode(nodeNumber);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al reiniciar nodo", ex);
            }
        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// Muestra los controles operacionales
        /// </summary>
        private void ShowOperationalControls()
        {
            try
            {
                foreach (var button in _ackButtons.Values)
                    button.Visible = true;
                
                foreach (var label in _zoneLabels.Values)
                    label.Visible = true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al mostrar controles operacionales", ex);
            }
        }

        /// <summary>
        /// Oculta los controles operacionales
        /// </summary>
        private void HideOperationalControls()
        {
            try
            {
                foreach (var button in _ackButtons.Values)
                    button.Visible = false;
                
                foreach (var label in _zoneLabels.Values)
                    label.Visible = false;
                
                foreach (var label in _batteryLabels.Values)
                    label.Visible = false;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al ocultar controles operacionales", ex);
            }
        }
        #endregion
    }
}
