using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;


namespace AplicacionS
{
    public partial class Form1 : Form
    {
        static SerialPort SerialESP;
        static Thread SerialHilo;
        static string SerialBufferRx;
        static string SerialSt = "1";
        public Form1()
        {
            InitializeComponent();
            SerialESP = new SerialPort();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectionCOM = ListaCOM.SelectedValue.ToString();
            SerialConfig(selectionCOM);

        }
        public static void SerialConfig(string selectionCOM)
        {
            //SerialPort SerialESP = new SerialPort();
            SerialESP.PortName = selectionCOM;
            SerialESP.BaudRate = 9600;
            SerialESP.Parity = Parity.None;
            SerialESP.DataBits = 8;
            SerialESP.StopBits = StopBits.One;
            SerialESP.WriteTimeout = 300;
            SerialESP.ReadTimeout = 300;
            SerialESP.Handshake = Handshake.None;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //SerialESP = new SerialPort();
            List<string> comListados = new List<string>();
            foreach (string com in SerialPort.GetPortNames())
            {
                comListados.Add(com);
                ListaCOM.Items.Add(com);
            }
            ListaCOM.DataSource = comListados;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SerialESP.Open();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SerialESP.Close();
        }
    }
}
