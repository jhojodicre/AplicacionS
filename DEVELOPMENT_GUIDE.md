# Guía de Desarrollo - Sistema de Seguridad Perimetral

## 📋 **Resumen del Proyecto**

Este es un **Sistema de Seguridad Perimetral** modernizado que monitorea 5 nodos con 2 zonas cada uno mediante comunicación serial con dispositivos ESP.

## 🔄 **Historial de Mejoras Implementadas**

### Versión 2.0 - Modernización Completa
**Fecha:** Julio 2025

#### ✅ **Arquitectura Mejorada:**
- Implementación de patrón MVC
- Separación de responsabilidades
- Dependency Injection
- Interfaces para mejor testabilidad

#### ✅ **Nuevos Componentes Creados:**
```
AplicacionS/
├── Controllers/
│   └── SecurityController.cs       # Lógica de negocio principal
├── Models/
│   ├── SecurityZone.cs            # Modelo de zona mejorado
│   └── SecurityNode.cs            # Modelo de nodo mejorado
├── Services/
│   ├── SerialCommunicationService.cs  # Comunicación serial robusta
│   ├── FileLoggingService.cs          # Logging centralizado
│   ├── ConfigurationService.cs       # Configuración JSON
│   └── AudioAlertService.cs           # Alertas sonoras
├── Interfaces/
│   ├── ISerialCommunicationService.cs
│   └── ILoggingService.cs
├── Views/
│   └── MainForm.cs                    # Formulario mejorado
└── Utils/
    └── GraphicsHelper.cs              # Utilidades gráficas
```

#### ✅ **Características Principales:**
1. **Comunicación Serial Robusta**
   - Manejo de errores mejorado
   - Reconexión automática
   - Logging detallado

2. **Sistema de Estados Avanzado**
   - Estados claros: OK, Alarm, Error, Fault, Acknowledged, LowBattery
   - Transiciones de estado validadas
   - Eventos para cambios de estado

3. **Configuración Externalizada**
   - Archivo JSON para configuración
   - Validación de configuración
   - Importación/exportación

4. **Logging Centralizado**
   - Logs estructurados con timestamp
   - Rotación automática de archivos
   - Diferentes niveles (Info, Warning, Error, Debug)

5. **Audio Mejorado**
   - Alertas sonoras profesionales
   - Control de volumen
   - Reproducción asíncrona

## 🚀 **Cómo Continuar el Desarrollo**

### Para GitHub Copilot:
Cuando abras el proyecto en una nueva PC, puedes usar estos prompts para que Copilot entienda el contexto:

#### 📝 **Prompt de Contexto Inicial:**
```
Este es un Sistema de Seguridad Perimetral en C# que:
- Monitorea 5 nodos con 2 zonas cada uno (A y B)
- Se comunica por serial con dispositivos ESP
- Usa patrón MVC con Controllers, Models, Services, Views
- Tiene estados: OK, Alarm, Error, Fault, Acknowledged, LowBattery
- Protocolo: "SEC,ESTADO,NODO,ZONA" ejemplo: "SEC,BOK,1,A"
- Comandos reset: B71A9 (nodo 1), B72A9 (nodo 2), etc.
- Tiene logging, configuración JSON, y alertas sonoras
```

#### 📝 **Prompts Específicos por Área:**

**Para Comunicación Serial:**
```
Trabajando en SerialCommunicationService que maneja comunicación con ESP.
Protocolo: mensajes "SEC,ESTADO,NODO,ZONA" de 12 caracteres.
Estados: BOK, NOK, ERR, FAL, ACK, BAT. Baudrate: 115200.
```

**Para UI/Gráficos:**
```
Trabajando en visualización del perímetro con GraphicsHelper.
10 zonas dibujadas como líneas con colores por estado:
Verde=OK, Rojo=Alarma, Azul=Error, Amarillo=Acknowledged, Gris=Unknown
```

**Para Lógica de Negocio:**
```
SecurityController maneja 5 SecurityNodes, cada uno con 2 SecurityZones.
Eventos: ZoneStatusChanged, AlarmTriggered, AlarmAcknowledged.
Comandos: AcknowledgeNodeAlarms(), ResetNode(), ResetAllNodes()
```

## 🔧 **Setup Rápido en Nueva PC**

### 1. Clonar y Configurar:
```bash
git clone https://github.com/jhojodicre/AplicacionS.git
cd AplicacionS
```

### 2. Instalar Dependencias:
```bash
# En Package Manager Console de Visual Studio:
Install-Package Newtonsoft.Json -Version 13.0.3
```

### 3. Configuración Inicial:
- Copiar `Config/default-config.json` como base
- Configurar puerto COM en la aplicación
- Verificar rutas de archivos de sonido

### 4. Compilar y Ejecutar:
- Abrir `AplicacionS.sln` en Visual Studio
- Rebuild Solution
- Ejecutar en modo Debug

## 🐛 **Problemas Comunes y Soluciones**

### Error de Compilación - Newtonsoft.Json
```bash
Install-Package Newtonsoft.Json -Version 13.0.3
```

### Puerto COM no disponible
- Verificar que el puerto no esté en uso
- Comprobar permisos de usuario
- Revisar Device Manager

### Archivos de sonido no encontrados
- Verificar rutas en configuración JSON
- Copiar archivos WAV a directorio Resources/
- Comprobar permisos de lectura

## 📚 **Documentación de Referencia**

- `README.md` - Descripción general del proyecto
- `TECHNICAL_DOCUMENTATION.md` - Documentación técnica detallada
- Comentarios en código - Documentación inline de cada clase

## 🔄 **Flujo de Desarrollo Recomendado**

1. **Antes de desarrollar:** Leer esta guía y documentación técnica
2. **Al trabajar:** Usar prompts de contexto con Copilot
3. **Después de cambios:** Actualizar documentación si es necesario
4. **Commits:** Hacer commits descriptivos para mantener historial

## 📞 **Contacto y Soporte**

Para dudas sobre la implementación, referirse a:
- Documentación técnica en el repositorio
- Comentarios en el código fuente
- Issues en GitHub para problemas específicos

---
**Última actualización:** Julio 2025
**Versión:** 2.0
