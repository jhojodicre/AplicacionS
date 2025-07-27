using System;
using System.Drawing;
using AplicacionS.Models;

namespace AplicacionS.Utils
{
    /// <summary>
    /// Utilidades para el manejo de colores y gráficos del sistema
    /// </summary>
    public static class GraphicsHelper
    {
        /// <summary>
        /// Colores para diferentes estados de zona
        /// </summary>
        public static class ZoneColors
        {
            public static readonly Color OK = Color.Green;
            public static readonly Color Alarm = Color.Red;
            public static readonly Color Error = Color.Blue;
            public static readonly Color Fault = Color.Purple;
            public static readonly Color Acknowledged = Color.Yellow;
            public static readonly Color LowBattery = Color.Orange;
            public static readonly Color Unknown = Color.Gray;
            public static readonly Color Offline = Color.DarkGray;
        }

        /// <summary>
        /// Obtiene el color correspondiente al estado de una zona
        /// </summary>
        /// <param name="status">Estado de la zona</param>
        /// <returns>Color correspondiente</returns>
        public static Color GetZoneColor(ZoneStatus status)
        {
            return status switch
            {
                ZoneStatus.OK => ZoneColors.OK,
                ZoneStatus.Alarm => ZoneColors.Alarm,
                ZoneStatus.Error => ZoneColors.Error,
                ZoneStatus.Fault => ZoneColors.Fault,
                ZoneStatus.Acknowledged => ZoneColors.Acknowledged,
                ZoneStatus.LowBattery => ZoneColors.LowBattery,
                ZoneStatus.Unknown => ZoneColors.Unknown,
                _ => ZoneColors.Offline
            };
        }

        /// <summary>
        /// Crea un Pen para dibujar líneas de zona
        /// </summary>
        /// <param name="status">Estado de la zona</param>
        /// <param name="width">Ancho de la línea</param>
        /// <returns>Pen configurado</returns>
        public static Pen CreateZonePen(ZoneStatus status, float width = 15f)
        {
            var color = GetZoneColor(status);
            return new Pen(color, width);
        }

        /// <summary>
        /// Coordenadas para dibujar las zonas del perímetro
        /// </summary>
        public static class ZoneCoordinates
        {
            // Nodo 1
            public static readonly Point Node1_ZoneA_Start = new Point(119, 335);
            public static readonly Point Node1_ZoneA_End = new Point(189, 426);
            public static readonly Point Node1_ZoneB_Start = new Point(206, 451);
            public static readonly Point Node1_ZoneB_End = new Point(359, 661);

            // Nodo 2
            public static readonly Point Node2_ZoneA_Start = new Point(385, 679);
            public static readonly Point Node2_ZoneA_End = new Point(616, 582);
            public static readonly Point Node2_ZoneB_Start = new Point(650, 563);
            public static readonly Point Node2_ZoneB_End = new Point(855, 473);

            // Nodo 3
            public static readonly Point Node3_ZoneA_Start = new Point(881, 460);
            public static readonly Point Node3_ZoneA_End = new Point(1012, 374);
            public static readonly Point Node3_ZoneB_Start = new Point(1017, 364);
            public static readonly Point Node3_ZoneB_End = new Point(932, 128);

            // Nodo 4
            public static readonly Point Node4_ZoneA_Start = new Point(924, 116);
            public static readonly Point Node4_ZoneA_End = new Point(827, 4);
            public static readonly Point Node4_ZoneB_Start = new Point(817, 14);
            public static readonly Point Node4_ZoneB_End = new Point(677, 62);

            // Nodo 5
            public static readonly Point Node5_ZoneA_Start = new Point(654, 72);
            public static readonly Point Node5_ZoneA_End = new Point(458, 157);
            public static readonly Point Node5_ZoneB_Start = new Point(444, 167);
            public static readonly Point Node5_ZoneB_End = new Point(121, 318);
        }

        /// <summary>
        /// Obtiene las coordenadas de inicio para una zona específica
        /// </summary>
        /// <param name="nodeNumber">Número del nodo (1-5)</param>
        /// <param name="zoneType">Tipo de zona (A o B)</param>
        /// <returns>Punto de inicio</returns>
        public static Point GetZoneStartPoint(int nodeNumber, char zoneType)
        {
            return (nodeNumber, zoneType) switch
            {
                (1, 'A') => ZoneCoordinates.Node1_ZoneA_Start,
                (1, 'B') => ZoneCoordinates.Node1_ZoneB_Start,
                (2, 'A') => ZoneCoordinates.Node2_ZoneA_Start,
                (2, 'B') => ZoneCoordinates.Node2_ZoneB_Start,
                (3, 'A') => ZoneCoordinates.Node3_ZoneA_Start,
                (3, 'B') => ZoneCoordinates.Node3_ZoneB_Start,
                (4, 'A') => ZoneCoordinates.Node4_ZoneA_Start,
                (4, 'B') => ZoneCoordinates.Node4_ZoneB_Start,
                (5, 'A') => ZoneCoordinates.Node5_ZoneA_Start,
                (5, 'B') => ZoneCoordinates.Node5_ZoneB_Start,
                _ => Point.Empty
            };
        }

        /// <summary>
        /// Obtiene las coordenadas de fin para una zona específica
        /// </summary>
        /// <param name="nodeNumber">Número del nodo (1-5)</param>
        /// <param name="zoneType">Tipo de zona (A o B)</param>
        /// <returns>Punto de fin</returns>
        public static Point GetZoneEndPoint(int nodeNumber, char zoneType)
        {
            return (nodeNumber, zoneType) switch
            {
                (1, 'A') => ZoneCoordinates.Node1_ZoneA_End,
                (1, 'B') => ZoneCoordinates.Node1_ZoneB_End,
                (2, 'A') => ZoneCoordinates.Node2_ZoneA_End,
                (2, 'B') => ZoneCoordinates.Node2_ZoneB_End,
                (3, 'A') => ZoneCoordinates.Node3_ZoneA_End,
                (3, 'B') => ZoneCoordinates.Node3_ZoneB_End,
                (4, 'A') => ZoneCoordinates.Node4_ZoneA_End,
                (4, 'B') => ZoneCoordinates.Node4_ZoneB_End,
                (5, 'A') => ZoneCoordinates.Node5_ZoneA_End,
                (5, 'B') => ZoneCoordinates.Node5_ZoneB_End,
                _ => Point.Empty
            };
        }

        /// <summary>
        /// Dibuja una zona específica en el gráfico
        /// </summary>
        /// <param name="graphics">Objeto Graphics para dibujar</param>
        /// <param name="nodeNumber">Número del nodo</param>
        /// <param name="zoneType">Tipo de zona</param>
        /// <param name="status">Estado de la zona</param>
        public static void DrawZone(Graphics graphics, int nodeNumber, char zoneType, ZoneStatus status)
        {
            if (graphics == null) return;

            try
            {
                var startPoint = GetZoneStartPoint(nodeNumber, zoneType);
                var endPoint = GetZoneEndPoint(nodeNumber, zoneType);

                if (startPoint != Point.Empty && endPoint != Point.Empty)
                {
                    using (var pen = CreateZonePen(status))
                    {
                        graphics.DrawLine(pen, startPoint, endPoint);
                    }
                }
            }
            catch (Exception)
            {
                // En caso de error en el dibujo, no hacer nada para evitar crashes
            }
        }

        /// <summary>
        /// Limpia el área de dibujo
        /// </summary>
        /// <param name="graphics">Objeto Graphics</param>
        /// <param name="backgroundColor">Color de fondo</param>
        /// <param name="bounds">Área a limpiar</param>
        public static void ClearDrawingArea(Graphics graphics, Color backgroundColor, Rectangle bounds)
        {
            if (graphics == null) return;

            try
            {
                using (var brush = new SolidBrush(backgroundColor))
                {
                    graphics.FillRectangle(brush, bounds);
                }
            }
            catch (Exception)
            {
                // En caso de error en el dibujo, no hacer nada
            }
        }

        /// <summary>
        /// Dibuja todas las zonas del perímetro en su estado inicial (gris)
        /// </summary>
        /// <param name="graphics">Objeto Graphics para dibujar</param>
        public static void DrawInitialPerimeter(Graphics graphics)
        {
            if (graphics == null) return;

            try
            {
                using (var grayPen = new Pen(ZoneColors.Unknown, 15))
                {
                    // Nodo 1
                    graphics.DrawLine(grayPen, ZoneCoordinates.Node1_ZoneA_Start, ZoneCoordinates.Node1_ZoneA_End);
                    graphics.DrawLine(grayPen, ZoneCoordinates.Node1_ZoneB_Start, ZoneCoordinates.Node1_ZoneB_End);

                    // Nodo 2
                    graphics.DrawLine(grayPen, ZoneCoordinates.Node2_ZoneA_Start, ZoneCoordinates.Node2_ZoneA_End);
                    graphics.DrawLine(grayPen, ZoneCoordinates.Node2_ZoneB_Start, ZoneCoordinates.Node2_ZoneB_End);

                    // Nodo 3
                    graphics.DrawLine(grayPen, ZoneCoordinates.Node3_ZoneA_Start, ZoneCoordinates.Node3_ZoneA_End);
                    graphics.DrawLine(grayPen, ZoneCoordinates.Node3_ZoneB_Start, ZoneCoordinates.Node3_ZoneB_End);

                    // Nodo 4
                    graphics.DrawLine(grayPen, ZoneCoordinates.Node4_ZoneA_Start, ZoneCoordinates.Node4_ZoneA_End);
                    graphics.DrawLine(grayPen, ZoneCoordinates.Node4_ZoneB_Start, ZoneCoordinates.Node4_ZoneB_End);

                    // Nodo 5
                    graphics.DrawLine(grayPen, ZoneCoordinates.Node5_ZoneA_Start, ZoneCoordinates.Node5_ZoneA_End);
                    graphics.DrawLine(grayPen, ZoneCoordinates.Node5_ZoneB_Start, ZoneCoordinates.Node5_ZoneB_End);
                }
            }
            catch (Exception)
            {
                // En caso de error en el dibujo, no hacer nada
            }
        }

        /// <summary>
        /// Obtiene el color de fondo recomendado para un estado específico
        /// </summary>
        /// <param name="hasActiveAlarms">Si hay alarmas activas</param>
        /// <param name="hasErrors">Si hay errores</param>
        /// <returns>Color de fondo recomendado</returns>
        public static Color GetRecommendedBackgroundColor(bool hasActiveAlarms, bool hasErrors)
        {
            if (hasActiveAlarms)
                return Color.FromArgb(255, 240, 240); // Rojo muy claro
            if (hasErrors)
                return Color.FromArgb(240, 240, 255); // Azul muy claro
            
            return Color.White; // Blanco por defecto
        }
    }
}
