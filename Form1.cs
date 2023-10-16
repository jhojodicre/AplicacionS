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
            // Desactivar Controles.
            btnNodo1.Enabled = false;
            btnSerial_Enviar.Enabled = false;
            txBSerial.Visible = false;

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
                    btnNodo1.Enabled = true;
                    btnSerial_Enviar.Enabled = true;

                    btnSerial_Conectar.Text = "Desconectar";
                    lblSerial_Status.BackColor = Color.Green;
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
                }
                catch { }
            }
        }


    }

}



////1. Librerias.
////- 1.1 Librerias
////****************************

////2. Definicion de Pinout.
////****************************
//// Las Etiquetas para los pinout son los que comienzan con GPIO
//// Es decir, si queremos activar la salida 1, tenemos que buscar la referencia GPIO 1, Pero solomante Escribir 1 sin "GPIO"
//// NO tomar como referencia las etiquetas D1.
////- 2.1 Definicion de etiquetas para las Entradas.
//#define PB_ENTER 0

////- 2.2 Definicion de etiquetas para las Salidas.
//#define LED_azul 2               // Led Por defecto embebido con la Placa GPIO 2 "D4"


////3. Variables Globales.
//int nodo;
//int state;
//String zonaS;
//String stateS;

////-3.1 Variables para las Interrupciones
//String inputString;                     // Buffer recepcion Serial.
//volatile byte Hola_1 = 0;
//volatile bool stringComplete = false;   // Flag: mensaje Serial Recibido completo.
//                                        //-3.2 Variables Globales para Las Funciones.
//bool inicio = true;             // Habilitar mensaje de inicio por unica vez
//String funtion_Mode;          // Tipo de funcion para ejecutar.
//String funtion_Number;        // Numero de funcion a EJECUTAR.
//String funtion_Parmeter1;     // Parametro 1 de la Funcion;
//String funtion_Parmeter2;     // Parametro 2 de la Funcion;
//bool codified_funtion = false;  // Notifica que la funcion ha sido codificada

////-3.3 RFM95 Variables
//int16_t packetnum = 0;  // packet counter, we increment per xmission
//unsigned int placa; // placa en el perimetro.
//unsigned int zona;  // Zona del perimetro.
//char radiopacket[32] = "012345 23456789 1   ";

//int packetSize = 0;
//String outgoing;              // outgoing message
//byte msgCount = 0;            // count of outgoing messages
//byte localAddress = 0xFF;     // address of this device
//byte destination = 0x01;      // destination to send to
//long lastSendTime = 0;        // last send time
//int interval = 2000;          // interval between sends.

////4. Instancias de Clases de Librerias Incluidas.
////-4.1
////5. Funciones ISR.
////-5.1 ISR Serial
//void serialEvent()
//{
//    while (Serial.available())
//    {
//        // get the new byte:
//        char inChar = (char)Serial.read();
//        // add it to the inputString:
//        inputString += inChar;
//        // if the incoming character is a newline, set a flag so the main loop can
//        // do something about it:
//        if (inChar == '\n')
//        {
//            stringComplete = true;
//            codified_funtion = false;
//        }
//    }
//    Serial.println(inputString);

//}
//void setup()
//{
//    //1. Configuracion de Puertos
//    //-1.1 Configuracion de Salidas:
//    pinMode(LED_azul, OUTPUT);
//    //-1.2 Configuracion de Entradas

//    //2. Condiciones Iniciales.
//    //-2.1 Estado de Salidas.
//    digitalWrite(LED_azul, HIGH);
//    //-2.2 Valores y Espacios de Variables
//    inputString.reserve(200);         // reserve 200 bytes for the inputString:

//    //3. Configuracion de Perifericos:
//    //-3.1 Initialize serial communication at 9600 bits per second:
//    Serial.begin(9600);
//    delay(10);
//    //-3.2 Interrupciones Habilitadas.
//    //interrupts ();

//    //4. Sitema Minimo Configurado.
//    Serial.println("Sistema Minimo Configurado");
//    //5. Configuracion de DEVICE externos
//}
//void loop()
//{
//    //1. Mensaje de Bienvenida Para Comprobar el Sistema minimo de Funcionamiento.
//    while (inicio)
//    {
//        welcome();
//        led_Monitor(2);
//    }
//    //2. Decodificar Funcion
//    if (stringComplete)
//    {
//        decodificar_solicitud();
//    }
//    //3. Ejecutar Funcion
//    if (codified_funtion)
//    {
//        ejecutar_solicitud();
//        // 3.1 Desactivar Banderas.
//        codified_funtion = false;
//    }

//    aleatorio();
//    Serial.println("SEC," + stateS + "," + nodo + "," + zonaS);
//    delay(5000);

//}
////1. Funciones de Logic interna del Micro.
//void welcome()
//{
//    // Deshabilitamos Banderas
//    inicio = false;
//    Serial.println("Comenzamos el Programa");
//    Serial.println("Esperamos recibir un Dato");
//    Serial.println("ESP8266 MASTER CONFIGURADO");
//}
//void led_Monitor(int repeticiones)
//{
//    // Deshabilitamos Banderas
//    int repetir = repeticiones;
//    for (int encender = 0; encender <= repetir; ++encender)
//    {
//        digitalWrite(LED_azul, LOW);   // Led ON.
//        delay(500);                    // pausa 1 seg.
//        digitalWrite(LED_azul, HIGH);    // Led OFF.
//        delay(500);                    // pausa 1 seg.
//    }
//}
//void decodificar_solicitud()
//{
//    //Deshabilitamos Banderas
//    stringComplete = false;
//    codified_funtion = true;
//    Serial.println(inputString);         // Pureba de Comunicacion Serial.
//    funtion_Mode = inputString.substring(0, 1);
//    funtion_Number = inputString.substring(1, 2);
//    funtion_Parmeter1 = inputString.substring(2, 3);
//    funtion_Parmeter2 = inputString.substring(3, 4);
//    inputString = "";
//    Serial.println("funcion: " + funtion_Mode);
//    Serial.println("Numero: " + funtion_Number);
//    Serial.println("Parametro1: " + funtion_Parmeter1);
//    Serial.println("Parametro2: " + funtion_Parmeter2 + "\n");
//    //Serial.println("Numero de funcion: ")
//}

////2. Funciones Seleccionadas para Ejecutar.
//void f1_Destellos(int repeticiones, int tiempo)
//{
//    int veces = repeticiones;
//    int retardo = tiempo * 100;
//    Serial.println("Ejecutando F1.. \n");
//    for (int repetir = 0; repetir < veces; ++repetir)
//    {
//        delay(retardo);                  // pausa 1 seg.
//        digitalWrite(LED_azul, LOW);     // Led ON.
//        delay(retardo);                  // pausa 1 seg.
//        digitalWrite(LED_azul, HIGH);    // Led OFF.
//    }
//}
//void f2_serial_Enviar(int par1)
//{
//    Serial.println("a");
//}
//void f3_XXXXX_reserva(int par1)
//{
//    int uno = 0;
//}
////3. Gestiona las funciones a Ejecutar.
//void ejecutar_solicitud()
//{
//    // Deshabilitamos Banderas
//    int x1 = funtion_Parmeter1.toInt();
//    int x2 = funtion_Parmeter2.toInt();
//    if (funtion_Number == "1")
//    {
//        Serial.println("funion Nº1");
//        f1_Destellos(x1, x2);
//    }
//    if (funtion_Number == "2")
//    {
//        Serial.println("funion Nº2");
//        f2_serial_Enviar(x2);
//    }
//    if (funtion_Number == "3")
//    {
//        Serial.println("funion Nº3");
//        //f3_rf95();
//    }
//    if (funtion_Number == "4")
//    {
//        Serial.println("funion Nº4");
//    }
//    if (funtion_Number == "5")
//    {
//        Serial.println("funion Nº5");
//    }
//    else
//    {
//        Serial.println("Ninguna Funcion");
//    }

//}
//void aleatorio()
//{
//    nodo = random(1, 6);
//    zona = random(1, 3);
//    if (zona == 1)
//        zonaS = "A";
//    else
//        zonaS = "B";

//    state = random(1, 3);
//    if (state == 1)
//        stateS = "BOK";
//    else
//        stateS = "NOK";

//}
////4. Funciones de Dispositivos Externos.
////-4.1 