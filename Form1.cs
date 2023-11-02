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
using System.Runtime.InteropServices;
using System.Media;

namespace AplicacionS
{
    public partial class Form1 : Form
    {
        SoundPlayer sound_Alarma;
        bool sound_Alarma_Status = false;
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
        int Cont = 0;
        bool PerimetroState = true;
        string mensajeProcesado;
        bool mensajeCompleto = false;

        string sec;
        string zona;
        string nodo;
        string estado;


        Nodo Nodo_1;
        Nodo Nodo_2;
        Nodo Nodo_3;
        Nodo Nodo_4;
        Nodo Nodo_5;

        #region Form1
        public Form1()
        {
            InitializeComponent();
            comListados = new List<string>();

        }
        private void Form1_AccesoForm(string accion)
        {
            SerialBufferRx = accion;
            labelprueba.Text = SerialBufferRx.Count().ToString();


            if (SerialBufferRx.Count() == 12)
            {
                txBSerial.AppendText(SerialBufferRx);

                mensajeProcesado = SerialBufferRx.Trim();

                BufferProcesar = mensajeProcesado.Split(',').ToList();

                sec = BufferProcesar[0];
                estado = BufferProcesar[1];
                nodo = BufferProcesar[2];
                zona = BufferProcesar[3];

                mensajeCompleto = true;
            }

            if (mensajeCompleto)
            {
                mensajeCompleto = false;

                Perimetro_Actualizar_2();
                Zonas_Actualizar();
            }

        }
        private void Form1_AccesoInterrupcion(string accion)
        {
            // Creamos una variable delegado que te permita llegar al Form
            DelegadoAcceso Accediendo;
            Accediendo = new DelegadoAcceso(Form1_AccesoForm);
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
            //sound_Alarma = new SoundPlayer(Application.StartupPath+@"\Resource\sound_alarma_1.mp3");
            sound_Alarma = new SoundPlayer();
            sound_Alarma.SoundLocation = "C:/Users/ILATINA/source/repos/jhojodicre/AplicacionS/Resources/sound_alarma_1.wav";
            zVerde = new Pen(Color.Green, 15);
            zRoja = new Pen(Color.Red, 15);
            zAmarilla = new Pen(Color.Yellow, 15);
            zAzul = new Pen(Color.Blue, 15);
            zGris = new Pen(Color.DarkGray, 15);
            //zBlanco = new Pen(Color.Green, 15);
            z7 = new Pen(Color.Green, 15);
            z8 = new Pen(Color.Green, 15);

            Perimetro = pbxPerimetro.CreateGraphics();
            // Desactivar Controles.
            btnNodo1_RST.Enabled = false;
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

            btnNodo1_ACK.Visible = false;
            btnNodo2_ACK.Visible = false;
            btnNodo3_ACK.Visible = false;
            btnNodo4_ACK.Visible = false;
            btnNodo5_ACK.Visible = false;

            // Condicion Inicial
            WindowState = FormWindowState.Maximized;

            Nodo_1 = new Nodo(1, new Point(50, 50), new Point(100, 50), new Point(110, 50), new Point(200, 50));
            Nodo_2 = new Nodo(2);
            Nodo_3 = new Nodo(3);
            Nodo_4 = new Nodo(4);
            Nodo_5 = new Nodo(5);

        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (SerialESP.IsOpen)
                    SerialESP.Close();
                SerialESPconect = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                MessageBox.Show(ex.Message);
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
        private void cfgSerial_Config(string selectionCOM)
        {
            SerialESP.PortName = selectionCOM;
            SerialESP.BaudRate = 115200;
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
                    btnNodo1_RST.Enabled = true;
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
        private void bufSerial_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Form1_AccesoInterrupcion(SerialESP.ReadLine());
        }
        #endregion
        #region Botones Nodos
        private void btnNodoS_ACK_Click(object sender, EventArgs e)
        {
            // Initializes the variables to pass to the MessageBox.Show method.
            string message = "ZONA ACTIVADA";
            string caption = "Reconocer Zonas";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result;

            Button button = (Button)sender;
            string Nodo_ACK = button.Text;
            char Nodo_ACK_Indice = Nodo_ACK[Nodo_ACK.Length - 1];
            Nodo_ACK = Nodo_ACK_Indice.ToString();
            txBSerial.AppendText(Nodo_ACK);
            switch (Nodo_ACK)
            {
                case "1":
                    Nodo_1.NodeUpdate("ACK", Nodo_ACK, zona);
                    break;
                case "2":
                    Nodo_2.NodeUpdate("ACK", Nodo_ACK, zona);
                    break;
                case "3":
                    Nodo_3.NodeUpdate("ACK", Nodo_ACK, zona);
                    break;
                case "4":
                    Nodo_4.NodeUpdate("ACK", Nodo_ACK, zona);
                    break;
                case "5":
                    Nodo_5.NodeUpdate("ACK", Nodo_ACK, zona);
                    break;
                default:
                    break;
            }
            // Displays the MessageBox.
            result = MessageBox.Show(message, caption, buttons);
            if (result == System.Windows.Forms.DialogResult.OK)
            {
            }
        }
        private void btnNodo1_RST_Click(object sender, EventArgs e)
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
        private void btnNodo2_RST_Click(object sender, EventArgs e)
        {
            MessageBoxButtons msgNodo2 = MessageBoxButtons.YesNoCancel;
            string message = "Desea Resetear el Nodo2";
            string caption = "Reset Nodo2";
            DialogResult result;

            // Displays the MessageBox.
            result = MessageBox.Show(message, caption, msgNodo2);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    SerialESP.WriteLine("B72A9");
                }
                catch { }
            }
        }
        private void btnNodo3_RST_Click(object sender, EventArgs e)
        {
            MessageBoxButtons msgNodo3 = MessageBoxButtons.YesNoCancel;
            string message = "Desea Resetear el Nodo3";
            string caption = "Reset Nodo3";
            DialogResult result;

            // Displays the MessageBox.
            result = MessageBox.Show(message, caption, msgNodo3);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    SerialESP.WriteLine("B32A9");
                }
                catch { }
            }
        }
        private void btnNodos_RST_Click(object sender, EventArgs e)
        {
            try
            {
                SerialESP.WriteLine("B70A9");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region Perimetro

        private void Perimetro_Actualizar_2()
        {
            switch (nodo)
            {
                case "1":
                    Nodo_1.NodeUpdate(estado, nodo, zona);
                    break;
                case "2":
                    Nodo_2.NodeUpdate(estado, nodo, zona);
                    break;
                case "3":
                    Nodo_3.NodeUpdate(estado, nodo, zona);
                    break;
                case "4":
                    Nodo_4.NodeUpdate(estado, nodo, zona);
                    break;
                case "5":
                    Nodo_5.NodeUpdate(estado, nodo, zona);
                    break;
                default:

                    break;
            }

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

            btnNodo1_ACK.Visible = true;
            btnNodo2_ACK.Visible = true;
            btnNodo3_ACK.Visible = true;
            btnNodo4_ACK.Visible = true;
            btnNodo5_ACK.Visible = true;

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

        private void Zonas_Actualizar()
        {
            try
            {
                // Zonas Verdes

                if (Nodo_1.Zone_A_OK) Perimetro.DrawLine(zVerde, 119, 335, 189, 426);
                if (Nodo_1.Zone_B_OK) Perimetro.DrawLine(zVerde, 206, 451, 359, 661);

                if (Nodo_2.Zone_A_OK) Perimetro.DrawLine(zVerde, 385, 679, 616, 582);
                if (Nodo_2.Zone_B_OK) Perimetro.DrawLine(zVerde, 650, 563, 855, 473);

                if (Nodo_3.Zone_A_OK) Perimetro.DrawLine(zVerde, 881, 460, 1012, 374);
                if (Nodo_3.Zone_B_OK) Perimetro.DrawLine(zVerde, 1017, 364, 932, 128);

                if (Nodo_4.Zone_A_OK) Perimetro.DrawLine(zVerde, 924, 116, 827, 4);
                if (Nodo_4.Zone_B_OK) Perimetro.DrawLine(zVerde, 817, 14, 677, 62);

                if (Nodo_5.Zone_A_OK) Perimetro.DrawLine(zVerde, 654, 72, 458, 157);
                if (Nodo_5.Zone_B_OK) Perimetro.DrawLine(zVerde, 444, 167, 121, 318);


                //ZONA ROJA
                if (Nodo_1.Zone_A_ALR) Perimetro.DrawLine(zRoja, 119, 335, 189, 426);
                if (Nodo_1.Zone_B_ALR) Perimetro.DrawLine(zRoja, 206, 451, 359, 661);

                if (Nodo_2.Zone_A_ALR) Perimetro.DrawLine(zRoja, 385, 679, 616, 582);
                if (Nodo_2.Zone_B_ALR) Perimetro.DrawLine(zRoja, 650, 563, 855, 473);

                if (Nodo_3.Zone_A_ALR) Perimetro.DrawLine(zRoja, 881, 460, 1012, 374);
                if (Nodo_3.Zone_B_ALR) Perimetro.DrawLine(zRoja, 1017, 364, 932, 128);

                if (Nodo_4.Zone_A_ALR) Perimetro.DrawLine(zRoja, 924, 116, 827, 4);
                if (Nodo_4.Zone_B_ALR) Perimetro.DrawLine(zRoja, 817, 14, 677, 62);

                if (Nodo_5.Zone_A_ALR) Perimetro.DrawLine(zRoja, 654, 72, 458, 157);
                if (Nodo_5.Zone_B_ALR) Perimetro.DrawLine(zRoja, 444, 167, 121, 318);

                // ZONA AZUL
                if (Nodo_1.Zone_A_ERR) Perimetro.DrawLine(zAzul, 119, 335, 189, 426);
                if (Nodo_1.Zone_B_ERR) Perimetro.DrawLine(zAzul, 206, 451, 359, 661);

                if (Nodo_2.Zone_A_ERR) Perimetro.DrawLine(zAzul, 385, 679, 616, 582);
                if (Nodo_2.Zone_B_ERR) Perimetro.DrawLine(zAzul, 650, 563, 855, 473);

                if (Nodo_3.Zone_A_ERR) Perimetro.DrawLine(zAzul, 881, 460, 1012, 374);
                if (Nodo_3.Zone_B_ERR) Perimetro.DrawLine(zAzul, 1017, 364, 932, 128);

                if (Nodo_4.Zone_A_ERR) Perimetro.DrawLine(zAzul, 924, 116, 827, 4);
                if (Nodo_4.Zone_B_ERR) Perimetro.DrawLine(zAzul, 817, 14, 677, 62);

                if (Nodo_5.Zone_A_ERR) Perimetro.DrawLine(zAzul, 654, 72, 458, 157);
                if (Nodo_5.Zone_B_ERR) Perimetro.DrawLine(zAzul, 444, 167, 121, 318);

                //Nodo1
                if (Nodo_1.Zone_A_ACK) Perimetro.DrawLine(zAmarilla, 119, 335, 189, 426);
                if (Nodo_1.Zone_B_ACK) Perimetro.DrawLine(zAmarilla, 206, 451, 359, 661);

                //Nodo2
                if (Nodo_2.Zone_A_ACK) Perimetro.DrawLine(zAmarilla, 385, 679, 616, 582);
                if (Nodo_2.Zone_B_ACK) Perimetro.DrawLine(zAmarilla, 650, 563, 855, 473);

                //Nodo3
                if (Nodo_3.Zone_A_ACK) Perimetro.DrawLine(zAmarilla, 881, 460, 1012, 374);
                if (Nodo_3.Zone_B_ACK) Perimetro.DrawLine(zAmarilla, 1017, 364, 932, 128);

                //Nodo4
                if (Nodo_4.Zone_A_ACK) Perimetro.DrawLine(zAmarilla, 924, 116, 827, 4);
                if (Nodo_4.Zone_B_ACK) Perimetro.DrawLine(zAmarilla, 817, 14, 677, 62);

                //Nodo5
                if (Nodo_5.Zone_A_ACK) Perimetro.DrawLine(zAmarilla, 654, 72, 458, 157);
                if (Nodo_5.Zone_B_ACK) Perimetro.DrawLine(zAmarilla, 444, 167, 121, 318);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        private void btnConfig_Zones_Click(object sender, EventArgs e)
        {


        }
        private void tmrHora_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToLongTimeString();
            //lblHora.Text = DateTime.Now.ToString("hh:mm:ss");
            //lblHora.Text = DateTime.Now.ToString("h:mm:s");
            //lblFecha.Text = DateTime.Now.ToLongDateString();
            lblFecha.Text = DateTime.Now.ToShortDateString();
            //lblHora.Text = DateTime.Now.ToString("dddd:MMMM:yyyy");
            //lblHora.Text = DateTime.Now.ToString("dddd MMMM yyyy");
        }

    }
}
