# Documentación Técnica - Sistema de Seguridad Perimetral v2.0

## Arquitectura del Sistema

### Patrón de Diseño Implementado
El sistema utiliza el patrón **MVC (Model-View-Controller)** junto con **Dependency Injection** para mejorar la mantenibilidad y testabilidad:

```
├── Models/              # Modelos de datos
│   ├── SecurityZone.cs     # Representa una zona de seguridad
│   ├── SecurityNode.cs     # Representa un nodo con 2 zonas
│   └── DataFromChips.cs    # Datos legacy del chip
├── Views/               # Interfaz de usuario
│   └── MainForm.cs         # Formulario principal mejorado
├── Controllers/         # Lógica de negocio
│   └── SecurityController.cs # Controlador principal
├── Services/            # Servicios
│   ├── SerialCommunicationService.cs
│   ├── FileLoggingService.cs
│   ├── ConfigurationService.cs
│   └── AudioAlertService.cs
├── Interfaces/          # Contratos
│   ├── ISerialCommunicationService.cs
│   └── ILoggingService.cs
└── Utils/              # Utilidades
    └── GraphicsHelper.cs
```

## Modelos de Datos

### SecurityZone
Representa una zona individual de seguridad con los siguientes estados:
- **OK**: Zona funcionando correctamente
- **Alarm**: Alarma activa (intrusión detectada)  
- **Error**: Error en la zona
- **Fault**: Falla del sistema
- **Acknowledged**: Alarma reconocida
- **LowBattery**: Batería baja
- **Unknown**: Estado desconocido

### SecurityNode
Representa un nodo físico que contiene 2 zonas (A y B). Cada nodo:
- Maneja el estado de sus 2 zonas
- Rastrea la comunicación
- Proporciona comandos de reset específicos
- Maneja reconocimiento de alarmas

## Servicios

### SerialCommunicationService
- Maneja la comunicación con dispositivos ESP
- Implementa reconexión automática
- Logging detallado de comunicaciones
- Manejo robusto de errores

### FileLoggingService  
- Registro de eventos en archivos
- Rotación automática de logs
- Niveles de logging (Info, Warning, Error, Debug)
- Limpieza automática de logs antiguos

### ConfigurationService
- Configuración externalizada en JSON
- Validación de configuración
- Importación/exportación de configuraciones
- Valores por defecto seguros

### AudioAlertService
- Manejo de alertas sonoras
- Soporte para múltiples tipos de sonido
- Control de volumen
- Reproducción asíncrona

## Protocolo de Comunicación

### Formato de Mensaje
Los mensajes recibidos tienen el formato: `SEC,ESTADO,NODO,ZONA`

**Ejemplos:**
- `SEC,BOK,1,A` - Nodo 1, Zona A está OK
- `SEC,NOK,2,B` - Nodo 2, Zona B tiene alarma
- `BAT,BAT,3,A` - Nodo 3, Zona A tiene batería baja

### Estados Válidos
- **BOK**: Zona OK (funcionando correctamente)
- **NOK**: Alarma activa (intrusión detectada)
- **ERR**: Error en la zona
- **FAL**: Falla del sistema
- **ACK**: Alarma reconocida
- **BAT**: Batería baja

### Comandos de Control
- `B71A9`: Reset Nodo 1
- `B72A9`: Reset Nodo 2  
- `B32A9`: Reset Nodo 3
- `B70A9`: Reset todos los nodos

## Mejoras Implementadas

### 1. Separación de Responsabilidades
- Lógica de negocio separada de la UI
- Servicios independientes y reutilizables
- Interfaces para mejor testabilidad

### 2. Manejo de Errores Robusto
- Try-catch en todas las operaciones críticas
- Logging detallado de errores
- Recuperación automática cuando es posible

### 3. Configuración Externalizada
- Configuración en archivo JSON
- Valores por defecto seguros
- Validación de configuración

### 4. Logging Centralizado
- Logs estructurados con timestamp
- Diferentes niveles de logging
- Rotación automática de archivos

### 5. Interfaz Mejorada
- Actualización visual en tiempo real
- Indicadores de estado claros
- Notificaciones de alarma prominentes

### 6. Gestión de Recursos
- Liberación adecuada de recursos
- Patrón Dispose implementado
- Manejo de memoria optimizado

## Configuración

### Archivo de Configuración (config.json)
```json
{
  "DefaultComPort": "COM3",
  "BaudRate": 115200,
  "ConnectionTimeoutMinutes": 5,
  "EnableAlarmSounds": true,
  "AlarmVolume": 80,
  "GraphicsUpdateInterval": 100,
  "LogRetentionDays": 30,
  "DebugMode": false,
  "AutoAcknowledgeMinutes": 0,
  "Sounds": {
    "AlarmSound": "Resources\\sound_alarm_Zones.wav",
    "WarningSound": "Resources\\sound_alarma_1.wav"
  }
}
```

## Instalación y Deployment

### Requisitos del Sistema
- Windows 7/8/10/11
- .NET Framework 4.7.2 o superior
- Puerto serial disponible
- Permisos de escritura en directorio de aplicación

### Archivos de Configuración
La aplicación crea automáticamente los siguientes directorios:
- `%AppData%\SeguridadPerimetral\` - Configuración del usuario
- `%AppData%\SeguridadPerimetral\Logs\` - Archivos de log

### Actualización desde Versión Anterior
1. Hacer backup de configuración actual
2. Instalar nueva versión
3. La aplicación migrará automáticamente la configuración

## Troubleshooting

### Problemas Comunes

#### No se puede conectar al puerto COM
- Verificar que el puerto no esté en uso
- Comprobar permisos de usuario
- Revisar logs para errores específicos

#### No se reproducen sonidos de alarma
- Verificar que los archivos de sonido existen
- Comprobar configuración `EnableAlarmSounds`
- Revisar permisos de lectura de archivos

#### Gráficos no se actualizan
- Verificar que la conexión esté activa
- Comprobar logs para errores de comunicación
- Ajustar `GraphicsUpdateInterval` en configuración

### Logs de Depuración
Habilitar `DebugMode: true` en configuración para logs detallados.
Los logs se encuentran en: `%AppData%\SeguridadPerimetral\Logs\`

## Futuras Mejoras Sugeridas

1. **Base de Datos**: Almacenar historial de eventos
2. **API REST**: Integración con sistemas externos
3. **Reportes**: Generación de reportes automáticos
4. **Notificaciones**: Email/SMS en caso de alarmas
5. **Dashboard Web**: Acceso remoto vía navegador
6. **Backup Automático**: Respaldo de configuración
7. **Multi-idioma**: Soporte para varios idiomas
8. **Autenticación**: Control de acceso con usuarios
