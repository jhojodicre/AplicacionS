# 🤖 Contexto para GitHub Copilot

## Prompt de Contexto Principal
```
Este es un Sistema de Seguridad Perimetral en C# Windows Forms que:

ARQUITECTURA:
- Patrón MVC: Controllers/, Models/, Services/, Views/, Interfaces/, Utils/
- SecurityController maneja lógica de negocio principal
- SerialCommunicationService para comunicación con ESP
- FileLoggingService para logging centralizado
- ConfigurationService para configuración JSON

DOMINIO:
- 5 nodos de seguridad, cada uno con 2 zonas (A y B)
- Estados de zona: OK, Alarm, Error, Fault, Acknowledged, LowBattery, Unknown
- Comunicación serial con dispositivos ESP (115200 baudios)

PROTOCOLO:
- Formato mensaje: "SEC,ESTADO,NODO,ZONA" (12 caracteres)
- Ejemplo: "SEC,BOK,1,A" = Nodo 1, Zona A está OK
- Estados: BOK=OK, NOK=Alarm, ERR=Error, FAL=Fault, ACK=Acknowledged, BAT=LowBattery
- Comandos reset: B71A9 (nodo 1), B72A9 (nodo 2), B32A9 (nodo 3), B70A9 (todos)

VISUALIZACIÓN:
- Perímetro dibujado con líneas de colores
- Verde=OK, Rojo=Alarma, Azul=Error, Amarillo=Acknowledged, Gris=Unknown
- GraphicsHelper maneja coordenadas y colores
- Actualización en tiempo real via eventos

CARACTERÍSTICAS:
- Logging a archivos con rotación automática
- Configuración JSON externalizada
- Alertas sonoras para alarmas
- Eventos: ZoneStatusChanged, AlarmTriggered, AlarmAcknowledged
- Manejo robusto de errores y reconexión
```

## Prompts Específicos por Área

### Para Trabajar en Comunicación Serial:
```
Trabajando en SerialCommunicationService.cs - maneja comunicación con dispositivos ESP.
Protocolo: mensajes "SEC,ESTADO,NODO,ZONA" de exactamente 12 caracteres.
Configuración: 115200 baudios, 8 bits datos, sin paridad, 1 stop bit.
Manejo de eventos DataReceived con parsing y validación de mensajes.
```

### Para Trabajar en Modelos:
```
SecurityZone y SecurityNode son los modelos principales.
SecurityZone: estados OK/Alarm/Error/Fault/Acknowledged/LowBattery/Unknown.
SecurityNode: contiene 2 SecurityZones (A y B), maneja comandos reset específicos.
Eventos para cambios de estado y validación de transiciones.
```

### Para Trabajar en UI/Gráficos:
```
GraphicsHelper dibuja perímetro de seguridad con coordenadas fijas.
10 zonas (5 nodos × 2 zonas) como líneas con colores por estado.
MainForm actualiza visualización via eventos del SecurityController.
Timers para actualización gráfica y de estado.
```

### Para Trabajar en Servicios:
```
FileLoggingService: logs estructurados con timestamp, rotación automática.
ConfigurationService: configuración JSON con validación y valores por defecto.
AudioAlertService: alertas sonoras con SoundPlayer, control de volumen.
Todos implementan IDisposable para liberación de recursos.
```

## Comandos Útiles

### Compilación:
```
# En Visual Studio Package Manager Console:
Install-Package Newtonsoft.Json -Version 13.0.3
```

### Estructura de Archivos Clave:
```
Controllers/SecurityController.cs - Lógica principal
Models/SecurityZone.cs - Modelo de zona
Models/SecurityNode.cs - Modelo de nodo  
Services/SerialCommunicationService.cs - Comunicación
Services/FileLoggingService.cs - Logging
Utils/GraphicsHelper.cs - Gráficos
Config/default-config.json - Configuración
```

---
Usar estos prompts al iniciar sesiones de Copilot para mantener contexto.
