using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;
using AplicacionS.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AplicacionS
{
    public partial class Form1 : Form
    {
        string SerialBufferRx;
        static string SerialSt = "1";
        public Form1()
        {
            InitializeComponent();

        }

        public void SerialConfig(string selectionCOM)
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
        public void Buffer(string Serial)
        {
            List<string> ListaDatosDelChip = new List<string>();
            while (SerialSt != "0")
            {
                //ListaDatosDelChip.Add(new DataFromChips { datos = SerialBufferRx });
                ListaDatosDelChip.Add(Serial);

                dataGridView1.DataSource = ListaDatosDelChip;
            }
            //ListaDatosDelChip.Add(new DataFromChips { datos = "me comi una salchipapa" });
            //dataGridView1.DataSource = ListaDatosDelChip;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //SerialESP = new SerialPort();
            List<string> comListados = new List<string>();
            //comListados.Add("serialpreuba");
            foreach (string com in SerialPort.GetPortNames())
            {
                comListados.Add(com);
                ListaCOM.Items.Add(com);
            }
            ListaCOM.DataSource = comListados;
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectionCOM = ListaCOM.SelectedValue.ToString();
            SerialConfig(selectionCOM);

        }
        private void button1_Click(object sender, EventArgs e)
        {
            SerialESP.Open();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            SerialESP.Close();
        }
        private void txtParametros_TextChanged(object sender, EventArgs e)
        {
            SerialSt = txtParametros.Text;
            Buffer(SerialSt);
        }
        private void SerialESP_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialBufferRx = SerialESP.ReadLine();
            Buffer(SerialBufferRx);
        }
    }
}