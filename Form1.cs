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