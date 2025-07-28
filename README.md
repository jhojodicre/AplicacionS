# Sistema de Seguridad Perimetral

## Descripción
Sistema de monitoreo de seguridad perimetral que supervisa 5 nodos con 2 zonas cada uno mediante comunicación serial con dispositivos ESP.

## Características Actuales
- Monitoreo en tiempo real de 10 zonas de seguridad
- Comunicación serial con dispositivos ESP
- Visualización gráfica del perímetro
- Alertas sonoras para intrusiones
- Sistema de reconocimiento (ACK) de alarmas
- Reset individual y general de nodos

## Estados de Zona
- **Verde**: Zona OK (segura)
- **Rojo**: Alarma activa (NOK)
- **Azul**: Error en la zona (ERR)
- **Amarillo**: Alarma reconocida (ACK)
- **Gris**: Estado inicial/neutral

## Mejoras Implementadas
1. ✅ Separación de responsabilidades con patrón MVC
2. ✅ Logging centralizado
3. ✅ Configuración externalizada
4. ✅ Manejo mejorado de errores
5. ✅ Validación de datos de entrada
6. ✅ Interfaces para mejor testabilidad
7. ✅ Documentación del código

## Estructura del Proyecto
```
AplicacionS/
├── Controllers/         # Controladores de lógica de negocio
├── Models/             # Modelos de datos
├── Views/              # Formularios de la interfaz
├── Services/           # Servicios de comunicación
├── Utils/              # Utilidades y helpers
└── Config/             # Archivos de configuración
```

## Requisitos
- .NET Framework 4.7.2 o superior
- Windows 7/10/11
- Puerto serial disponible para comunicación con ESP

## Instalación
1. Clonar el repositorio
2. Abrir en Visual Studio 2019 o superior
3. Instalar dependencias: `Install-Package Newtonsoft.Json -Version 13.0.3`
4. Compilar y ejecutar
5. Configurar puerto serial en la interfaz

## Setup en Nueva PC
1. **Clonar repositorio:** `git clone https://github.com/jhojodicre/AplicacionS.git`
2. **Leer documentación:** Revisar `DEVELOPMENT_GUIDE.md` y `COPILOT_CONTEXT.md`
3. **Configurar Copilot:** Usar prompts de contexto en `COPILOT_CONTEXT.md`
4. **Instalar dependencias:** NuGet packages necesarios
5. **Configurar:** Puerto COM y rutas de archivos

## Uso
1. Seleccionar puerto COM disponible
2. Hacer clic en "Conectar"
3. El sistema mostrará el perímetro y comenzará el monitoreo
4. Las zonas cambiarán de color según su estado
5. Usar botones ACK para reconocer alarmas
6. Usar botones RST para resetear nodos

## Protocolo de Comunicación
El sistema espera mensajes con formato: `SEC,ESTADO,NODO,ZONA`
- SEC: Tipo de mensaje
- ESTADO: BOK/NOK/ERR/FAL/ACK/BAT
- NODO: 1-5
- ZONA: A/B

## Instalacion
Abrir el power shell como administrador y ejecutar
- Install-Package Newtonsoft.Json 