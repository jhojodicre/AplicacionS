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

namespace AplicacionS
{
    public partial class Form1 : Form
    {
        string SerialBufferRx;
        string SerialBufferTx;
        static string SerialSt = "1";
        List<DataFromChips> ListaDatosDelChip = new List<DataFromChips>();
        private bool SerialESPconect;

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
            SerialESPconect = true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            SerialESP.Close();
        }
        private void txtParametros_TextChanged(object sender, EventArgs e)
        {
            SerialBufferTx = txtParametros.Text;
            SerialESP.WriteLine(txtParametros.Text);
        }
        private void SerialESP_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            if (SerialESPconect && SerialESP.IsOpen)
            {
                try
                {
                    SerialBufferRx = SerialESP.ReadLine();

                }
                catch (Exception ex)
                {

                    throw ex;
                }
                if(SerialBufferRx != "0")
                {
                    if (SerialBufferRx == "SEC,NOK,1,B\r")
                        LineaPerimetral1.BackColor = Color.Green;
                    ListaDatosDelChip.Add(new DataFromChips { datos = SerialBufferRx });


                    dataGridView1.Invoke(new MethodInvoker(delegate
                    {
                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = ListaDatosDelChip;
                    }));
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SerialESP.WriteLine("A145");
        }

    }
}