using System;
using System.IO.Ports;
using System.Threading.Tasks;
using AplicacionS.Interfaces;

namespace AplicacionS.Services
{
    /// <summary>
    /// Servicio mejorado para comunicación serial con dispositivos ESP
    /// </summary>
    public class SerialCommunicationService : ISerialCommunicationService, IDisposable
    {
        private SerialPort _serialPort;
        private readonly ILoggingService _logger;
        private bool _disposed = false;

        /// <summary>
        /// Evento que se dispara cuando se reciben datos
        /// </summary>
        public event EventHandler<string> DataReceived;

        /// <summary>
        /// Verifica si la conexión está activa
        /// </summary>
        public bool IsConnected => _serialPort?.IsOpen ?? false;

        /// <summary>
        /// Constructor del servicio
        /// </summary>
        /// <param name="logger">Servicio de logging</param>
        public SerialCommunicationService(ILoggingService logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            InitializeSerialPort();
        }

        /// <summary>
        /// Inicializa el puerto serial con configuración por defecto
        /// </summary>
        private void InitializeSerialPort()
        {
            _serialPort = new SerialPort
            {
                BaudRate = 115200,
                Parity = Parity.None,
                DataBits = 8,
                StopBits = StopBits.One,
                WriteTimeout = 300,
                ReadTimeout = 1000,
                Handshake = Handshake.None
            };

            _serialPort.DataReceived += OnDataReceived;
        }

        /// <summary>
        /// Conecta al puerto serial especificado
        /// </summary>
        /// <param name="portName">Nombre del puerto COM</param>
        /// <returns>True si la conexión fue exitosa</returns>
        public bool Connect(string portName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(portName))
                {
                    _logger.LogError("Nombre de puerto inválido");
                    return false;
                }

                if (IsConnected)
                {
                    _logger.LogWarning($"Ya existe una conexión activa en {_serialPort.PortName}");
                    return true;
                }

                _serialPort.PortName = portName;
                _serialPort.Open();
                
                _logger.LogInfo($"Conexión establecida exitosamente en puerto {portName}");
                return true;
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError($"Acceso denegado al puerto {portName}. El puerto puede estar en uso.", ex);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError($"Puerto {portName} no válido.", ex);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"El puerto {portName} ya está abierto.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inesperado al conectar al puerto {portName}", ex);
            }

            return false;
        }

        /// <summary>
        /// Desconecta del puerto serial
        /// </summary>
        public void Disconnect()
        {
            try
            {
                if (IsConnected)
                {
                    var portName = _serialPort.PortName;
                    _serialPort.Close();
                    _logger.LogInfo($"Desconectado del puerto {portName}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al desconectar del puerto serial", ex);
            }
        }

        /// <summary>
        /// Envía un comando al dispositivo conectado
        /// </summary>
        /// <param name="command">Comando a enviar</param>
        public void SendCommand(string command)
        {
            try
            {
                if (!IsConnected)
                {
                    _logger.LogWarning("Intento de enviar comando sin conexión activa");
                    return;
                }

                if (string.IsNullOrWhiteSpace(command))
                {
                    _logger.LogWarning("Intento de enviar comando vacío");
                    return;
                }

                _serialPort.WriteLine(command);
                _logger.LogDebug($"Comando enviado: {command}");
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError("El puerto serial no está abierto", ex);
            }
            catch (TimeoutException ex)
            {
                _logger.LogError($"Timeout al enviar comando: {command}", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al enviar comando: {command}", ex);
            }
        }

        /// <summary>
        /// Envía un comando de forma asíncrona
        /// </summary>
        /// <param name="command">Comando a enviar</param>
        /// <returns>Task para operación asíncrona</returns>
        public async Task SendCommandAsync(string command)
        {
            await Task.Run(() => SendCommand(command));
        }

        /// <summary>
        /// Obtiene los puertos COM disponibles
        /// </summary>
        /// <returns>Array de nombres de puertos disponibles</returns>
        public string[] GetAvailablePorts()
        {
            try
            {
                var ports = SerialPort.GetPortNames();
                _logger.LogInfo($"Puertos disponibles encontrados: {string.Join(", ", ports)}");
                return ports;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al obtener puertos disponibles", ex);
                return new string[0];
            }
        }

        /// <summary>
        /// Maneja los datos recibidos del puerto serial
        /// </summary>
        private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (_serialPort?.IsOpen == true)
                {
                    string data = _serialPort.ReadLine();
                    if (!string.IsNullOrWhiteSpace(data))
                    {
                        _logger.LogDebug($"Datos recibidos: {data.Trim()}");
                        DataReceived?.Invoke(this, data.Trim());
                    }
                }
            }
            catch (TimeoutException)
            {
                _logger.LogWarning("Timeout al leer datos del puerto serial");
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError("Puerto serial no disponible para lectura", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al procesar datos recibidos", ex);
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
                        Disconnect();
                        _serialPort?.Dispose();
                    }
                    catch (Exception ex)
                    {
                        _logger?.LogError("Error al liberar recursos del puerto serial", ex);
                    }
                }
                _disposed = true;
            }
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~SerialCommunicationService()
        {
            Dispose(false);
        }
    }
}
