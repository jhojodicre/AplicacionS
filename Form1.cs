using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;
using AplicacionS.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Security.Policy;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace AplicacionS
{
    public partial class Form1 : Form
    {
        private delegate void DelegadoAcceso(string iterruptToForm);
        string SerialBufferRx;
        string SerialBufferTx;
        List<string> BufferProcesar = new List<string>();
        List<string> comListados;
        string selectionCOM;
        static string SerialSt = "1";
        List<DataFromChips> ListaDatosDelChip = new List<DataFromChips>();
        private bool SerialESPconect;
        Point startPoint;
        Point endPoint;
        public Graphics Perimetro;
        private Pen zRoja;
        private Pen zVerde;
        private Pen zGris;
        private Pen zAzul;
        private Pen zAmarilla;
        private Pen zBlanca;
        private Pen z7;
        private Pen z8;
        bool PerimetroState = true;
        #region Form1
        public Form1()
        {
            InitializeComponent();
            comListados = new List<string>();

        }
        private void AccesoForm(string accion)
        {
            SerialBufferRx = accion;

            // A partir de aqui Se trantan Los datos.
            txBSerial.AppendText(SerialBufferRx.ToString());

            BufferProcesar = SerialBufferRx.Split(',').ToList();

            if (BufferProcesar[0] == "SEC")
            {
                ActualizarInterfaz(BufferProcesar[1], BufferProcesar[2], BufferProcesar[3]);

            }

        }
        private void AccesoInterrupcion(string accion)
        {
            // Creamos una variable delegado que te permita llegar al Form
            DelegadoAcceso Accediendo;
            Accediendo = new DelegadoAcceso(AccesoForm);
            // Objeto que permita pasar los datos.
            object[] arg = { accion };
            base.Invoke(Accediendo, arg);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (string com in SerialPort.GetPortNames())
            {
                cmbSerial_COM.Items.Add(com);
                comListados.Add(com);

            }
            zVerde = new Pen(Color.Green, 15);
            zRoja = new Pen(Color.Green, 15);
            zAmarilla = new Pen(Color.Green, 15);
            zAzul = new Pen(Color.Green, 15);
            zGris = new Pen(Color.DarkGray, 15);
            //zBlanco = new Pen(Color.Green, 15);
            z7 = new Pen(Color.Green, 15);
            z8 = new Pen(Color.Green, 15);

            Perimetro = pbxPerimetro.CreateGraphics();
            // Desactivar Controles.
            btnNodo1.Enabled = false;
            btnSerial_Enviar.Enabled = false;

            //Ocultar Controles
            txBSerial.Visible = false;
            lblZ1.Visible = false;
            lblZ2.Visible = false;
            lblZ3.Visible = false;
            lblZ4.Visible = false;
            lblZ5.Visible = false;
            lblZ6.Visible = false;
            lblZ7.Visible = false;
            lblZ8.Visible = false;
            lblZ9.Visible = false;
            lblz10.Visible = false;

            btnNodo_1.Visible = false;
            btnNodo_2.Visible = false;
            btnNodo_3.Visible = false;
            btnNodo_4.Visible = false;
            btnNodo_5.Visible = false;

            // Condicion Inicial
            WindowState = FormWindowState.Maximized;


        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            SerialESP.Close();
            SerialESPconect = false;
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {

                MessageBox.Show("Hola");
            }
        }
        #endregion
        #region Serial Puerto
        private void cmbSerial_DropDown(object sender, EventArgs e)
        {
            cmbSerial_COM.BackColor = Color.LightBlue;
            cmbSerial_COM.Items.Clear();
            try
            {
                foreach (string com in SerialPort.GetPortNames())
                {
                    cmbSerial_COM.Items.Add(com);
                    comListados.Add(com);

                }
            }
            catch (Exception ex)
            {

            }

            //ListaCOM.DataSource = comListados;
        }
        private void cmbSerial_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectionCOM = comListados[cmbSerial_COM.SelectedIndex];
            cfgSerial_Config(selectionCOM);
            cmbSerial_COM.BackColor = Color.White;
            btnSerial_Conectar.BackColor = Color.LightBlue;

        }
        public void cfgSerial_Config(string selectionCOM)
        {
            SerialESP.PortName = selectionCOM;
            SerialESP.BaudRate = 9600;
            SerialESP.Parity = Parity.None;
            SerialESP.DataBits = 8;
            SerialESP.StopBits = StopBits.One;
            SerialESP.WriteTimeout = 300;
            SerialESP.ReadTimeout = 300;
            SerialESP.Handshake = Handshake.None;
        }
        private void btnSerial_Conectar_Click(object sender, EventArgs e)
        {
            if (selectionCOM == null)
            {
                MessageBox.Show("Seleccione Un Puerto");
                cmbSerial_COM.BackColor = Color.MediumVioletRed;
                return;
            }
            if (btnSerial_Conectar.Text == "Conectar")
            {
                try
                {
                    SerialESP.Open();
                    SerialESPconect = true;
                    btnSerial_Conectar.BackColor = Color.White;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
                if (SerialESP.IsOpen)
                {
                    btnNodo1.Enabled = true;
                    btnSerial_Enviar.Enabled = true;

                    btnSerial_Conectar.Text = "Desconectar";
                    lblSerial_Status.BackColor = Color.Green;
                    EstablecerZonas();
                }
            }
            else if (btnSerial_Conectar.Text == "Desconectar" && SerialESP.IsOpen)
            {
                MessageBox.Show("Cuidado Dejara el Sistema sin Comunicaion");
                MessageBoxButtons msgSerial_Desconectar = MessageBoxButtons.YesNoCancel;
                string message = "Está seguro que Desea Desconectar el Sistema";
                string caption = "Desconexion del Sistema";
                DialogResult result;

                // Displays the MessageBox.
                result = MessageBox.Show(message, caption, msgSerial_Desconectar);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    try
                    {
                        SerialESP.Close();
                        SerialESPconect = false;
                        btnSerial_Conectar.Text = "Conectar";
                        lblSerial_Status.BackColor = Color.White;
                    }
                    catch { }
                }

            }

        }
        private void txtSerial_BufferTX_TextChanged(object sender, EventArgs e)
        {
            SerialBufferTx = txtSerial_BufferTX.Text;
            SerialESP.WriteLine(txtSerial_BufferTX.Text);
        }
        private void btnSerial_Enviar_Click(object sender, EventArgs e)
        {
            try
            {
                SerialESP.DiscardOutBuffer();
                SerialESP.WriteLine("A145");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void btnSerial_Consola_Click(object sender, EventArgs e)
        {
            if (btnSerial_Consola.Text == "Consola")
            {
                txBSerial.Visible = true;
                btnSerial_Consola.Text = "NO Consola";
            }
            else if (btnSerial_Consola.Text == "NO Consola")
            {
                txBSerial.Visible = false;
                btnSerial_Consola.Text = "Consola";
            }
        }
        private void SerialESP_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            AccesoInterrupcion(SerialESP.ReadExisting());
        }
        #endregion
        #region Botones Nodos
        private void btnNodo1_Click(object sender, EventArgs e)
        {
            MessageBoxButtons msgNodo1 = MessageBoxButtons.YesNoCancel;
            string message = "Desea Resetear el Nodo1";
            string caption = "Reset Nodo1";
            DialogResult result;

            // Displays the MessageBox.
            result = MessageBox.Show(message, caption, msgNodo1);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    SerialESP.WriteLine("B71A9");
                    //SerialESP.WriteLine("A134");
                }
                catch { }
            }
        }
        #endregion
        #region Perimetro
        private void btnConfig_Zones_Click(object sender, EventArgs e)
        {


        }
        private void EstablecerZonas()
        {
            PerimetroState = true;
            Perimetro.DrawLine(zGris, 119, 335, 189, 426);
            Perimetro.DrawLine(zGris, 206, 451, 359, 661);
            Perimetro.DrawLine(zGris, 385, 679, 616, 582);
            Perimetro.DrawLine(zGris, 650, 563, 855, 473);
            Perimetro.DrawLine(zGris, 881, 460, 1012, 374);
            Perimetro.DrawLine(zGris, 1017, 364, 932, 128);
            Perimetro.DrawLine(zGris, 924, 116, 827, 4);
            Perimetro.DrawLine(zGris, 817, 14, 677, 62);
            Perimetro.DrawLine(zGris, 654, 72, 458, 157);
            Perimetro.DrawLine(zGris, 444, 167, 121, 318);

            lblZ1.Visible = true;
            lblZ2.Visible = true;
            lblZ3.Visible = true;
            lblZ4.Visible = true;
            lblZ5.Visible = true;
            lblZ6.Visible = true;
            lblZ7.Visible = true;
            lblZ8.Visible = true;
            lblZ9.Visible = true;
            lblz10.Visible = true;

            btnNodo_1.Visible = true;
            btnNodo_2.Visible = true;
            btnNodo_3.Visible = true;
            btnNodo_4.Visible = true;
            btnNodo_5.Visible = true;
        }

        #region Dibujar Zona
        private void pbxPerimetro_MouseDown(object sender, MouseEventArgs e)
        {
            // Guarda la posición inicial del mouse
            startPoint = new Point(e.X, e.Y);
            // Dibuja un punto en la posición inicial del mouse
            Pen pen = new Pen(Color.Red, 5);
            Perimetro.DrawEllipse(pen, startPoint.X, startPoint.Y, 1, 1);

        }

        private void pbxPerimetro_MouseClick(object sender, MouseEventArgs e)
        {

            // Dibuja una línea entre la posición inicial y final del mouse
            endPoint = new Point(e.X, e.Y);
            Pen pen = new Pen(Color.Blue, 5);
            Perimetro.DrawLine(pen, startPoint, endPoint);
            txBSerial.AppendText("P1: (" + startPoint.X + "," + startPoint.Y + ") " + "P2: (" + endPoint.X + "," + endPoint.Y + ")");
        }
        #endregion
        #endregion
        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void ActualizarInterfaz(string estado, string nodo, string zona)
        {
            switch (estado)
            {
                case "BOK":
                    switch (nodo)
                    {
                        case "1":
                            switch (zona)
                            {
                                case "A":
                                    Perimetro.DrawLine(zVerde, 119, 335, 189, 426);
                                    break;
                                case "B":
                                    Perimetro.DrawLine(zVerde, 206, 451, 359, 661);
                                    break;

                            }
                            break;
                        case "2":
                            switch (zona)
                            {
                                case "A":
                                    Perimetro.DrawLine(zVerde, 385, 679, 616, 582);
                                    break;
                                case "B":
                                    Perimetro.DrawLine(zVerde, 650, 563, 855, 473);
                                    break;
                            }
                            break;
                        case "3":
                            switch (zona)
                            {
                                case "A":
                                    Perimetro.DrawLine(zVerde, 881, 460, 1012, 374);
                                    break;
                                case "B":
                                    Perimetro.DrawLine(zVerde, 1017, 364, 932, 128);
                                    break;
                            }
                            break;
                        case "4":
                            switch (zona)
                            {
                                case "A":
                                    Perimetro.DrawLine(zVerde, 924, 116, 827, 4);
                                    break;
                                case "B":
                                    Perimetro.DrawLine(zVerde, 817, 14, 677, 62);
                                    break;
                            }
                            break;
                        case "5":
                            switch (zona)
                            {
                                case "A":
                                    Perimetro.DrawLine(zVerde, 654, 72, 458, 157);
                                    break;
                                case "B":
                                    Perimetro.DrawLine(zVerde, 444, 167, 121, 318);
                                    break;
                            }
                            break;
                    }
                    break;
                case "NOK":
                    switch (nodo)
                    {
                        case "1":
                            switch (zona)
                            {
                                case "A":
                                    Perimetro.DrawLine(zRoja, 119, 335, 189, 426);
                                    break;
                                case "B":
                                    Perimetro.DrawLine(zRoja, 206, 451, 359, 661);
                                    break;

                            }
                            break;
                        case "2":
                            switch (zona)
                            {
                                case "A":
                                    Perimetro.DrawLine(zRoja, 385, 679, 616, 582);
                                    break;
                                case "B":
                                    Perimetro.DrawLine(zRoja, 650, 563, 855, 473);
                                    break;
                            }
                            break;
                        case "3":
                            switch (zona)
                            {
                                case "A":
                                    Perimetro.DrawLine(zRoja, 881, 460, 1012, 374);
                                    break;
                                case "B":
                                    Perimetro.DrawLine(zRoja, 1017, 364, 932, 128);
                                    break;
                            }
                            break;
                        case "4":
                            switch (zona)
                            {
                                case "A":
                                    Perimetro.DrawLine(zRoja, 924, 116, 827, 4);
                                    break;
                                case "B":
                                    Perimetro.DrawLine(zRoja, 817, 14, 677, 62);
                                    break;
                            }
                            break;
                        case "5":
                            switch (zona)
                            {
                                case "A":
                                    Perimetro.DrawLine(zRoja, 654, 72, 458, 157);
                                    break;
                                case "B":
                                    Perimetro.DrawLine(zRoja, 444, 167, 121, 318);
                                    break;
                            }
                            break;
                    }
                    break;
                case "ALL":
                    Perimetro.DrawLine(zVerde, 119, 335, 189, 426);
                    Perimetro.DrawLine(zVerde, 206, 451, 359, 661);
                    Perimetro.DrawLine(zVerde, 385, 679, 616, 582);
                    Perimetro.DrawLine(zVerde, 650, 563, 855, 473);
                    Perimetro.DrawLine(zVerde, 881, 460, 1012, 374);
                    Perimetro.DrawLine(zVerde, 1017, 364, 932, 128);
                    Perimetro.DrawLine(zVerde, 924, 116, 827, 4);
                    Perimetro.DrawLine(zVerde, 817, 14, 677, 62);
                    Perimetro.DrawLine(zVerde, 654, 72, 458, 157);
                    Perimetro.DrawLine(zVerde, 444, 167, 121, 318);
                    break;
            }
        }
    }

}