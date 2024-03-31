namespace AplicacionS
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.SerialESP = new System.IO.Ports.SerialPort(this.components);
            this.cmbSerial_COM = new System.Windows.Forms.ComboBox();
            this.btnSerial_Conectar = new System.Windows.Forms.Button();
            this.txtSerial_BufferTX = new System.Windows.Forms.TextBox();
            this.btnNodo1_RST = new System.Windows.Forms.Button();
            this.btnSerial_Enviar = new System.Windows.Forms.Button();
            this.panelSuperior = new System.Windows.Forms.Panel();
            this.lblHora = new System.Windows.Forms.Label();
            this.lblFecha = new System.Windows.Forms.Label();
            this.lblForm1 = new System.Windows.Forms.Label();
            this.btnSerial_Consola = new System.Windows.Forms.Button();
            this.panelIzquierdo = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnNodo5_RST = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnNodo4_RST = new System.Windows.Forms.Button();
            this.txBSerial = new System.Windows.Forms.TextBox();
            this.lblSerial_Status = new System.Windows.Forms.Label();
            this.lblNodo2_ZA = new System.Windows.Forms.Label();
            this.lblNodo2_ZB = new System.Windows.Forms.Label();
            this.btnNodo2_RST = new System.Windows.Forms.Button();
            this.lblNodo1_ZA = new System.Windows.Forms.Label();
            this.lblNodo1_ZB = new System.Windows.Forms.Label();
            this.lblNodo3_ZA = new System.Windows.Forms.Label();
            this.lblNodo3_ZB = new System.Windows.Forms.Label();
            this.btnNodo3_RST = new System.Windows.Forms.Button();
            this.labelprueba = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnNodos_Reset = new System.Windows.Forms.Button();
            this.btnConfig_Zones = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panelCentral = new System.Windows.Forms.Panel();
            this.lblNodo2_BAT = new System.Windows.Forms.Label();
            this.lblNodo1_BAT = new System.Windows.Forms.Label();
            this.lblz10 = new System.Windows.Forms.Label();
            this.lblZ9 = new System.Windows.Forms.Label();
            this.lblZ8 = new System.Windows.Forms.Label();
            this.lblZ7 = new System.Windows.Forms.Label();
            this.lblZ6 = new System.Windows.Forms.Label();
            this.lblZ5 = new System.Windows.Forms.Label();
            this.lblZ4 = new System.Windows.Forms.Label();
            this.lblZ3 = new System.Windows.Forms.Label();
            this.lblZ2 = new System.Windows.Forms.Label();
            this.lblZ1 = new System.Windows.Forms.Label();
            this.btnNodo4_ACK = new System.Windows.Forms.Button();
            this.btnNodo5_ACK = new System.Windows.Forms.Button();
            this.btnNodo3_ACK = new System.Windows.Forms.Button();
            this.btnNodo2_ACK = new System.Windows.Forms.Button();
            this.btnNodo1_ACK = new System.Windows.Forms.Button();
            this.pbxPerimetro = new System.Windows.Forms.PictureBox();
            this.tmrHora = new System.Windows.Forms.Timer(this.components);
            this.panelSuperior.SuspendLayout();
            this.panelIzquierdo.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panelCentral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxPerimetro)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 19);
            this.label1.TabIndex = 6;
            // 
            // SerialESP
            // 
            this.SerialESP.PortName = "COM4";
            this.SerialESP.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.bufSerial_DataReceived);
            // 
            // cmbSerial_COM
            // 
            this.cmbSerial_COM.FormattingEnabled = true;
            this.cmbSerial_COM.Location = new System.Drawing.Point(955, 19);
            this.cmbSerial_COM.Margin = new System.Windows.Forms.Padding(2);
            this.cmbSerial_COM.Name = "cmbSerial_COM";
            this.cmbSerial_COM.Size = new System.Drawing.Size(82, 21);
            this.cmbSerial_COM.TabIndex = 3;
            this.cmbSerial_COM.Text = "Seleccionar Puerto";
            this.cmbSerial_COM.DropDown += new System.EventHandler(this.cmbSerial_DropDown);
            this.cmbSerial_COM.SelectedIndexChanged += new System.EventHandler(this.cmbSerial_SelectedIndexChanged);
            // 
            // btnSerial_Conectar
            // 
            this.btnSerial_Conectar.Font = new System.Drawing.Font("Yu Gothic UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSerial_Conectar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnSerial_Conectar.Location = new System.Drawing.Point(1041, 16);
            this.btnSerial_Conectar.Margin = new System.Windows.Forms.Padding(2);
            this.btnSerial_Conectar.Name = "btnSerial_Conectar";
            this.btnSerial_Conectar.Size = new System.Drawing.Size(83, 34);
            this.btnSerial_Conectar.TabIndex = 4;
            this.btnSerial_Conectar.Text = "Conectar";
            this.btnSerial_Conectar.UseVisualStyleBackColor = true;
            this.btnSerial_Conectar.Click += new System.EventHandler(this.btnSerial_Conectar_Click);
            // 
            // txtSerial_BufferTX
            // 
            this.txtSerial_BufferTX.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtSerial_BufferTX.Location = new System.Drawing.Point(0, 419);
            this.txtSerial_BufferTX.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.txtSerial_BufferTX.Name = "txtSerial_BufferTX";
            this.txtSerial_BufferTX.Size = new System.Drawing.Size(173, 22);
            this.txtSerial_BufferTX.TabIndex = 7;
            this.txtSerial_BufferTX.TextChanged += new System.EventHandler(this.txtSerial_BufferTX_TextChanged);
            // 
            // btnNodo1_RST
            // 
            this.btnNodo1_RST.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnNodo1_RST.Location = new System.Drawing.Point(32, 26);
            this.btnNodo1_RST.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnNodo1_RST.Name = "btnNodo1_RST";
            this.btnNodo1_RST.Size = new System.Drawing.Size(55, 31);
            this.btnNodo1_RST.TabIndex = 8;
            this.btnNodo1_RST.Text = "Nodo 1";
            this.btnNodo1_RST.UseVisualStyleBackColor = true;
            this.btnNodo1_RST.Click += new System.EventHandler(this.btnNodo1_RST_Click);
            // 
            // btnSerial_Enviar
            // 
            this.btnSerial_Enviar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSerial_Enviar.Location = new System.Drawing.Point(0, 465);
            this.btnSerial_Enviar.Margin = new System.Windows.Forms.Padding(2);
            this.btnSerial_Enviar.Name = "btnSerial_Enviar";
            this.btnSerial_Enviar.Size = new System.Drawing.Size(173, 19);
            this.btnSerial_Enviar.TabIndex = 9;
            this.btnSerial_Enviar.Text = "Enviar";
            this.btnSerial_Enviar.UseVisualStyleBackColor = true;
            this.btnSerial_Enviar.Click += new System.EventHandler(this.btnSerial_Enviar_Click);
            // 
            // panelSuperior
            // 
            this.panelSuperior.BackColor = System.Drawing.Color.DimGray;
            this.panelSuperior.Controls.Add(this.lblHora);
            this.panelSuperior.Controls.Add(this.lblFecha);
            this.panelSuperior.Controls.Add(this.lblForm1);
            this.panelSuperior.Controls.Add(this.btnSerial_Conectar);
            this.panelSuperior.Controls.Add(this.cmbSerial_COM);
            this.panelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSuperior.Location = new System.Drawing.Point(0, 0);
            this.panelSuperior.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.panelSuperior.Name = "panelSuperior";
            this.panelSuperior.Size = new System.Drawing.Size(1366, 64);
            this.panelSuperior.TabIndex = 10;
            // 
            // lblHora
            // 
            this.lblHora.AutoSize = true;
            this.lblHora.Font = new System.Drawing.Font("XOUMEG S57", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHora.ForeColor = System.Drawing.Color.LightSkyBlue;
            this.lblHora.Location = new System.Drawing.Point(1185, 18);
            this.lblHora.Name = "lblHora";
            this.lblHora.Size = new System.Drawing.Size(66, 33);
            this.lblHora.TabIndex = 27;
            this.lblHora.Text = "Hora";
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.ForeColor = System.Drawing.Color.LightCyan;
            this.lblFecha.Location = new System.Drawing.Point(1216, 49);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(35, 13);
            this.lblFecha.TabIndex = 26;
            this.lblFecha.Text = "Fecha";
            // 
            // lblForm1
            // 
            this.lblForm1.AutoSize = true;
            this.lblForm1.BackColor = System.Drawing.Color.Transparent;
            this.lblForm1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblForm1.ForeColor = System.Drawing.Color.LightSteelBlue;
            this.lblForm1.Location = new System.Drawing.Point(512, 16);
            this.lblForm1.Name = "lblForm1";
            this.lblForm1.Size = new System.Drawing.Size(133, 37);
            this.lblForm1.TabIndex = 5;
            this.lblForm1.Text = "Securec";
            this.lblForm1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSerial_Consola
            // 
            this.btnSerial_Consola.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSerial_Consola.Location = new System.Drawing.Point(0, 441);
            this.btnSerial_Consola.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnSerial_Consola.Name = "btnSerial_Consola";
            this.btnSerial_Consola.Size = new System.Drawing.Size(173, 24);
            this.btnSerial_Consola.TabIndex = 10;
            this.btnSerial_Consola.TabStop = false;
            this.btnSerial_Consola.Text = "Consola";
            this.btnSerial_Consola.UseVisualStyleBackColor = true;
            this.btnSerial_Consola.Click += new System.EventHandler(this.btnSerial_Consola_Click);
            // 
            // panelIzquierdo
            // 
            this.panelIzquierdo.BackColor = System.Drawing.Color.DimGray;
            this.panelIzquierdo.Controls.Add(this.label4);
            this.panelIzquierdo.Controls.Add(this.label5);
            this.panelIzquierdo.Controls.Add(this.btnNodo5_RST);
            this.panelIzquierdo.Controls.Add(this.label2);
            this.panelIzquierdo.Controls.Add(this.label3);
            this.panelIzquierdo.Controls.Add(this.btnNodo4_RST);
            this.panelIzquierdo.Controls.Add(this.txtSerial_BufferTX);
            this.panelIzquierdo.Controls.Add(this.btnSerial_Consola);
            this.panelIzquierdo.Controls.Add(this.btnSerial_Enviar);
            this.panelIzquierdo.Controls.Add(this.txBSerial);
            this.panelIzquierdo.Controls.Add(this.lblSerial_Status);
            this.panelIzquierdo.Controls.Add(this.lblNodo2_ZA);
            this.panelIzquierdo.Controls.Add(this.lblNodo2_ZB);
            this.panelIzquierdo.Controls.Add(this.btnNodo2_RST);
            this.panelIzquierdo.Controls.Add(this.lblNodo1_ZA);
            this.panelIzquierdo.Controls.Add(this.lblNodo1_ZB);
            this.panelIzquierdo.Controls.Add(this.lblNodo3_ZA);
            this.panelIzquierdo.Controls.Add(this.lblNodo3_ZB);
            this.panelIzquierdo.Controls.Add(this.btnNodo3_RST);
            this.panelIzquierdo.Controls.Add(this.btnNodo1_RST);
            this.panelIzquierdo.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelIzquierdo.Location = new System.Drawing.Point(0, 64);
            this.panelIzquierdo.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.panelIzquierdo.Name = "panelIzquierdo";
            this.panelIzquierdo.Size = new System.Drawing.Size(173, 681);
            this.panelIzquierdo.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 178);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "ZA";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(95, 178);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(19, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "ZB";
            // 
            // btnNodo5_RST
            // 
            this.btnNodo5_RST.Location = new System.Drawing.Point(34, 169);
            this.btnNodo5_RST.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnNodo5_RST.Name = "btnNodo5_RST";
            this.btnNodo5_RST.Size = new System.Drawing.Size(55, 31);
            this.btnNodo5_RST.TabIndex = 22;
            this.btnNodo5_RST.Text = "Nodo 5";
            this.btnNodo5_RST.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 143);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "ZA";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(95, 143);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "ZB";
            // 
            // btnNodo4_RST
            // 
            this.btnNodo4_RST.Location = new System.Drawing.Point(34, 134);
            this.btnNodo4_RST.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnNodo4_RST.Name = "btnNodo4_RST";
            this.btnNodo4_RST.Size = new System.Drawing.Size(55, 31);
            this.btnNodo4_RST.TabIndex = 19;
            this.btnNodo4_RST.Text = "Nodo 4";
            this.btnNodo4_RST.UseVisualStyleBackColor = true;
            // 
            // txBSerial
            // 
            this.txBSerial.BackColor = System.Drawing.SystemColors.MenuBar;
            this.txBSerial.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txBSerial.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txBSerial.ForeColor = System.Drawing.Color.Maroon;
            this.txBSerial.Location = new System.Drawing.Point(0, 484);
            this.txBSerial.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.txBSerial.Multiline = true;
            this.txBSerial.Name = "txBSerial";
            this.txBSerial.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txBSerial.Size = new System.Drawing.Size(173, 197);
            this.txBSerial.TabIndex = 18;
            // 
            // lblSerial_Status
            // 
            this.lblSerial_Status.ForeColor = System.Drawing.Color.SkyBlue;
            this.lblSerial_Status.Location = new System.Drawing.Point(7, 2);
            this.lblSerial_Status.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSerial_Status.Name = "lblSerial_Status";
            this.lblSerial_Status.Size = new System.Drawing.Size(162, 24);
            this.lblSerial_Status.TabIndex = 0;
            this.lblSerial_Status.Text = "NODOS";
            this.lblSerial_Status.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNodo2_ZA
            // 
            this.lblNodo2_ZA.AutoSize = true;
            this.lblNodo2_ZA.Location = new System.Drawing.Point(7, 71);
            this.lblNodo2_ZA.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNodo2_ZA.Name = "lblNodo2_ZA";
            this.lblNodo2_ZA.Size = new System.Drawing.Size(20, 13);
            this.lblNodo2_ZA.TabIndex = 17;
            this.lblNodo2_ZA.Text = "ZA";
            // 
            // lblNodo2_ZB
            // 
            this.lblNodo2_ZB.AutoSize = true;
            this.lblNodo2_ZB.Location = new System.Drawing.Point(95, 71);
            this.lblNodo2_ZB.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNodo2_ZB.Name = "lblNodo2_ZB";
            this.lblNodo2_ZB.Size = new System.Drawing.Size(19, 13);
            this.lblNodo2_ZB.TabIndex = 16;
            this.lblNodo2_ZB.Text = "ZB";
            // 
            // btnNodo2_RST
            // 
            this.btnNodo2_RST.Location = new System.Drawing.Point(34, 63);
            this.btnNodo2_RST.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnNodo2_RST.Name = "btnNodo2_RST";
            this.btnNodo2_RST.Size = new System.Drawing.Size(55, 31);
            this.btnNodo2_RST.TabIndex = 15;
            this.btnNodo2_RST.Text = "Nodo 2";
            this.btnNodo2_RST.UseVisualStyleBackColor = true;
            this.btnNodo2_RST.Click += new System.EventHandler(this.btnNodo2_RST_Click);
            // 
            // lblNodo1_ZA
            // 
            this.lblNodo1_ZA.AutoSize = true;
            this.lblNodo1_ZA.Location = new System.Drawing.Point(7, 35);
            this.lblNodo1_ZA.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNodo1_ZA.Name = "lblNodo1_ZA";
            this.lblNodo1_ZA.Size = new System.Drawing.Size(20, 13);
            this.lblNodo1_ZA.TabIndex = 14;
            this.lblNodo1_ZA.Text = "ZA";
            // 
            // lblNodo1_ZB
            // 
            this.lblNodo1_ZB.AutoSize = true;
            this.lblNodo1_ZB.Location = new System.Drawing.Point(95, 35);
            this.lblNodo1_ZB.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNodo1_ZB.Name = "lblNodo1_ZB";
            this.lblNodo1_ZB.Size = new System.Drawing.Size(19, 13);
            this.lblNodo1_ZB.TabIndex = 13;
            this.lblNodo1_ZB.Text = "ZB";
            // 
            // lblNodo3_ZA
            // 
            this.lblNodo3_ZA.AutoSize = true;
            this.lblNodo3_ZA.Location = new System.Drawing.Point(7, 108);
            this.lblNodo3_ZA.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNodo3_ZA.Name = "lblNodo3_ZA";
            this.lblNodo3_ZA.Size = new System.Drawing.Size(20, 13);
            this.lblNodo3_ZA.TabIndex = 12;
            this.lblNodo3_ZA.Text = "ZA";
            // 
            // lblNodo3_ZB
            // 
            this.lblNodo3_ZB.AutoSize = true;
            this.lblNodo3_ZB.Location = new System.Drawing.Point(95, 108);
            this.lblNodo3_ZB.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNodo3_ZB.Name = "lblNodo3_ZB";
            this.lblNodo3_ZB.Size = new System.Drawing.Size(19, 13);
            this.lblNodo3_ZB.TabIndex = 11;
            this.lblNodo3_ZB.Text = "ZB";
            // 
            // btnNodo3_RST
            // 
            this.btnNodo3_RST.Location = new System.Drawing.Point(34, 99);
            this.btnNodo3_RST.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnNodo3_RST.Name = "btnNodo3_RST";
            this.btnNodo3_RST.Size = new System.Drawing.Size(55, 31);
            this.btnNodo3_RST.TabIndex = 10;
            this.btnNodo3_RST.Text = "Nodo 3";
            this.btnNodo3_RST.UseVisualStyleBackColor = true;
            this.btnNodo3_RST.Click += new System.EventHandler(this.btnNodo3_RST_Click);
            // 
            // labelprueba
            // 
            this.labelprueba.AutoSize = true;
            this.labelprueba.Location = new System.Drawing.Point(161, 17);
            this.labelprueba.Name = "labelprueba";
            this.labelprueba.Size = new System.Drawing.Size(41, 13);
            this.labelprueba.TabIndex = 25;
            this.labelprueba.Text = "prueba";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.DarkSlateGray;
            this.panel3.Controls.Add(this.labelprueba);
            this.panel3.Controls.Add(this.btnNodos_Reset);
            this.panel3.Controls.Add(this.btnConfig_Zones);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(173, 699);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1193, 46);
            this.panel3.TabIndex = 12;
            // 
            // btnNodos_Reset
            // 
            this.btnNodos_Reset.Location = new System.Drawing.Point(16, 8);
            this.btnNodos_Reset.Name = "btnNodos_Reset";
            this.btnNodos_Reset.Size = new System.Drawing.Size(58, 30);
            this.btnNodos_Reset.TabIndex = 1;
            this.btnNodos_Reset.Text = "Reset";
            this.btnNodos_Reset.UseVisualStyleBackColor = true;
            this.btnNodos_Reset.Click += new System.EventHandler(this.btnNodos_RST_Click);
            // 
            // btnConfig_Zones
            // 
            this.btnConfig_Zones.Location = new System.Drawing.Point(80, 8);
            this.btnConfig_Zones.Name = "btnConfig_Zones";
            this.btnConfig_Zones.Size = new System.Drawing.Size(75, 30);
            this.btnConfig_Zones.TabIndex = 0;
            this.btnConfig_Zones.Text = "CONFIG";
            this.btnConfig_Zones.UseVisualStyleBackColor = true;
            this.btnConfig_Zones.Click += new System.EventHandler(this.btnConfig_Zones_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.DimGray;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(1334, 64);
            this.panel4.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(32, 635);
            this.panel4.TabIndex = 13;
            // 
            // panelCentral
            // 
            this.panelCentral.AutoScroll = true;
            this.panelCentral.BackColor = System.Drawing.Color.DarkSlateGray;
            this.panelCentral.Controls.Add(this.lblNodo2_BAT);
            this.panelCentral.Controls.Add(this.lblNodo1_BAT);
            this.panelCentral.Controls.Add(this.lblz10);
            this.panelCentral.Controls.Add(this.lblZ9);
            this.panelCentral.Controls.Add(this.lblZ8);
            this.panelCentral.Controls.Add(this.lblZ7);
            this.panelCentral.Controls.Add(this.lblZ6);
            this.panelCentral.Controls.Add(this.lblZ5);
            this.panelCentral.Controls.Add(this.lblZ4);
            this.panelCentral.Controls.Add(this.lblZ3);
            this.panelCentral.Controls.Add(this.lblZ2);
            this.panelCentral.Controls.Add(this.lblZ1);
            this.panelCentral.Controls.Add(this.btnNodo4_ACK);
            this.panelCentral.Controls.Add(this.btnNodo5_ACK);
            this.panelCentral.Controls.Add(this.btnNodo3_ACK);
            this.panelCentral.Controls.Add(this.btnNodo2_ACK);
            this.panelCentral.Controls.Add(this.btnNodo1_ACK);
            this.panelCentral.Controls.Add(this.pbxPerimetro);
            this.panelCentral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCentral.Font = new System.Drawing.Font("Yu Gothic UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelCentral.Location = new System.Drawing.Point(173, 64);
            this.panelCentral.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.panelCentral.Name = "panelCentral";
            this.panelCentral.Size = new System.Drawing.Size(1161, 635);
            this.panelCentral.TabIndex = 14;
            // 
            // lblNodo2_BAT
            // 
            this.lblNodo2_BAT.AutoSize = true;
            this.lblNodo2_BAT.BackColor = System.Drawing.Color.Yellow;
            this.lblNodo2_BAT.Location = new System.Drawing.Point(644, 542);
            this.lblNodo2_BAT.Name = "lblNodo2_BAT";
            this.lblNodo2_BAT.Size = new System.Drawing.Size(49, 13);
            this.lblNodo2_BAT.TabIndex = 18;
            this.lblNodo2_BAT.Text = "BATERIA";
            // 
            // lblNodo1_BAT
            // 
            this.lblNodo1_BAT.AutoSize = true;
            this.lblNodo1_BAT.BackColor = System.Drawing.Color.Yellow;
            this.lblNodo1_BAT.Location = new System.Drawing.Point(247, 428);
            this.lblNodo1_BAT.Name = "lblNodo1_BAT";
            this.lblNodo1_BAT.Size = new System.Drawing.Size(49, 13);
            this.lblNodo1_BAT.TabIndex = 17;
            this.lblNodo1_BAT.Text = "BATERIA";
            // 
            // lblz10
            // 
            this.lblz10.AutoSize = true;
            this.lblz10.BackColor = System.Drawing.Color.Silver;
            this.lblz10.Location = new System.Drawing.Point(247, 237);
            this.lblz10.Name = "lblz10";
            this.lblz10.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblz10.Size = new System.Drawing.Size(25, 13);
            this.lblz10.TabIndex = 16;
            this.lblz10.Text = "Z10";
            // 
            // lblZ9
            // 
            this.lblZ9.AutoSize = true;
            this.lblZ9.BackColor = System.Drawing.Color.Silver;
            this.lblZ9.Location = new System.Drawing.Point(578, 111);
            this.lblZ9.Name = "lblZ9";
            this.lblZ9.Size = new System.Drawing.Size(19, 13);
            this.lblZ9.TabIndex = 15;
            this.lblZ9.Text = "Z9";
            // 
            // lblZ8
            // 
            this.lblZ8.AutoSize = true;
            this.lblZ8.BackColor = System.Drawing.Color.Silver;
            this.lblZ8.Location = new System.Drawing.Point(717, 48);
            this.lblZ8.Name = "lblZ8";
            this.lblZ8.Size = new System.Drawing.Size(19, 13);
            this.lblZ8.TabIndex = 14;
            this.lblZ8.Text = "Z8";
            // 
            // lblZ7
            // 
            this.lblZ7.AutoSize = true;
            this.lblZ7.BackColor = System.Drawing.Color.Silver;
            this.lblZ7.Location = new System.Drawing.Point(905, 75);
            this.lblZ7.Name = "lblZ7";
            this.lblZ7.Size = new System.Drawing.Size(19, 13);
            this.lblZ7.TabIndex = 13;
            this.lblZ7.Text = "Z7";
            // 
            // lblZ6
            // 
            this.lblZ6.AutoSize = true;
            this.lblZ6.BackColor = System.Drawing.Color.Silver;
            this.lblZ6.Location = new System.Drawing.Point(985, 219);
            this.lblZ6.Name = "lblZ6";
            this.lblZ6.Size = new System.Drawing.Size(19, 13);
            this.lblZ6.TabIndex = 12;
            this.lblZ6.Text = "Z6";
            // 
            // lblZ5
            // 
            this.lblZ5.AutoSize = true;
            this.lblZ5.BackColor = System.Drawing.Color.Silver;
            this.lblZ5.Location = new System.Drawing.Point(932, 432);
            this.lblZ5.Name = "lblZ5";
            this.lblZ5.Size = new System.Drawing.Size(19, 13);
            this.lblZ5.TabIndex = 11;
            this.lblZ5.Text = "Z5";
            // 
            // lblZ4
            // 
            this.lblZ4.AutoSize = true;
            this.lblZ4.BackColor = System.Drawing.Color.Silver;
            this.lblZ4.Location = new System.Drawing.Point(739, 525);
            this.lblZ4.Name = "lblZ4";
            this.lblZ4.Size = new System.Drawing.Size(19, 13);
            this.lblZ4.TabIndex = 10;
            this.lblZ4.Text = "Z4";
            // 
            // lblZ3
            // 
            this.lblZ3.AutoSize = true;
            this.lblZ3.BackColor = System.Drawing.Color.Silver;
            this.lblZ3.Location = new System.Drawing.Point(505, 629);
            this.lblZ3.Name = "lblZ3";
            this.lblZ3.Size = new System.Drawing.Size(19, 13);
            this.lblZ3.TabIndex = 9;
            this.lblZ3.Text = "Z3";
            // 
            // lblZ2
            // 
            this.lblZ2.AutoSize = true;
            this.lblZ2.BackColor = System.Drawing.Color.Silver;
            this.lblZ2.Location = new System.Drawing.Point(283, 588);
            this.lblZ2.Name = "lblZ2";
            this.lblZ2.Size = new System.Drawing.Size(19, 13);
            this.lblZ2.TabIndex = 8;
            this.lblZ2.Text = "Z2";
            // 
            // lblZ1
            // 
            this.lblZ1.AutoSize = true;
            this.lblZ1.BackColor = System.Drawing.Color.Silver;
            this.lblZ1.Location = new System.Drawing.Point(123, 407);
            this.lblZ1.Name = "lblZ1";
            this.lblZ1.Size = new System.Drawing.Size(19, 13);
            this.lblZ1.TabIndex = 6;
            this.lblZ1.Text = "Z1";
            // 
            // btnNodo4_ACK
            // 
            this.btnNodo4_ACK.Location = new System.Drawing.Point(792, 5);
            this.btnNodo4_ACK.Name = "btnNodo4_ACK";
            this.btnNodo4_ACK.Size = new System.Drawing.Size(53, 35);
            this.btnNodo4_ACK.TabIndex = 5;
            this.btnNodo4_ACK.Text = "Nodo 4";
            this.btnNodo4_ACK.UseVisualStyleBackColor = true;
            // 
            // btnNodo5_ACK
            // 
            this.btnNodo5_ACK.Location = new System.Drawing.Point(430, 156);
            this.btnNodo5_ACK.Name = "btnNodo5_ACK";
            this.btnNodo5_ACK.Size = new System.Drawing.Size(64, 41);
            this.btnNodo5_ACK.TabIndex = 4;
            this.btnNodo5_ACK.Text = "Nodo 5";
            this.btnNodo5_ACK.UseVisualStyleBackColor = true;
            this.btnNodo5_ACK.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnNodoS_ACK_Click);
            // 
            // btnNodo3_ACK
            // 
            this.btnNodo3_ACK.Location = new System.Drawing.Point(989, 352);
            this.btnNodo3_ACK.Name = "btnNodo3_ACK";
            this.btnNodo3_ACK.Size = new System.Drawing.Size(65, 32);
            this.btnNodo3_ACK.TabIndex = 3;
            this.btnNodo3_ACK.Text = "Nodo 3";
            this.btnNodo3_ACK.UseVisualStyleBackColor = true;
            // 
            // btnNodo2_ACK
            // 
            this.btnNodo2_ACK.Location = new System.Drawing.Point(607, 568);
            this.btnNodo2_ACK.Name = "btnNodo2_ACK";
            this.btnNodo2_ACK.Size = new System.Drawing.Size(65, 39);
            this.btnNodo2_ACK.TabIndex = 2;
            this.btnNodo2_ACK.Text = "Nodo 2";
            this.btnNodo2_ACK.UseVisualStyleBackColor = true;
            this.btnNodo2_ACK.Click += new System.EventHandler(this.btnNodoS_ACK_Click);
            // 
            // btnNodo1_ACK
            // 
            this.btnNodo1_ACK.Location = new System.Drawing.Point(174, 442);
            this.btnNodo1_ACK.Name = "btnNodo1_ACK";
            this.btnNodo1_ACK.Size = new System.Drawing.Size(67, 37);
            this.btnNodo1_ACK.TabIndex = 1;
            this.btnNodo1_ACK.Text = "Nodo 1";
            this.btnNodo1_ACK.UseVisualStyleBackColor = true;
            this.btnNodo1_ACK.Click += new System.EventHandler(this.btnNodoS_ACK_Click);
            // 
            // pbxPerimetro
            // 
            this.pbxPerimetro.Image = global::AplicacionS.Properties.Resources.BARRIO_ZARATE;
            this.pbxPerimetro.Location = new System.Drawing.Point(7, 4);
            this.pbxPerimetro.Name = "pbxPerimetro";
            this.pbxPerimetro.Size = new System.Drawing.Size(1347, 690);
            this.pbxPerimetro.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxPerimetro.TabIndex = 0;
            this.pbxPerimetro.TabStop = false;
            this.pbxPerimetro.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbxPerimetro_MouseClick);
            this.pbxPerimetro.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbxPerimetro_MouseDown);
            // 
            // tmrHora
            // 
            this.tmrHora.Enabled = true;
            this.tmrHora.Tick += new System.EventHandler(this.tmrHora_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(1366, 745);
            this.Controls.Add(this.panelCentral);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panelIzquierdo);
            this.Controls.Add(this.panelSuperior);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Seguridad Perimetral";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelSuperior.ResumeLayout(false);
            this.panelSuperior.PerformLayout();
            this.panelIzquierdo.ResumeLayout(false);
            this.panelIzquierdo.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panelCentral.ResumeLayout(false);
            this.panelCentral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxPerimetro)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.IO.Ports.SerialPort SerialESP;
        private System.Windows.Forms.ComboBox cmbSerial_COM;
        private System.Windows.Forms.Button btnSerial_Conectar;
        private System.Windows.Forms.TextBox txtSerial_BufferTX;
        private System.Windows.Forms.Button btnNodo1_RST;
        private System.Windows.Forms.Button btnSerial_Enviar;
        private System.Windows.Forms.Panel panelSuperior;
        private System.Windows.Forms.Panel panelIzquierdo;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panelCentral;
        private System.Windows.Forms.Label lblSerial_Status;
        private System.Windows.Forms.Button btnNodo3_RST;
        private System.Windows.Forms.Label lblNodo3_ZB;
        private System.Windows.Forms.Label lblNodo3_ZA;
        private System.Windows.Forms.Label lblNodo2_ZA;
        private System.Windows.Forms.Label lblNodo2_ZB;
        private System.Windows.Forms.Button btnNodo2_RST;
        private System.Windows.Forms.Label lblNodo1_ZA;
        private System.Windows.Forms.Label lblNodo1_ZB;
        private System.Windows.Forms.TextBox txBSerial;
        private System.Windows.Forms.Button btnSerial_Consola;
        private System.Windows.Forms.PictureBox pbxPerimetro;
        private System.Windows.Forms.Label lblForm1;
        private System.Windows.Forms.Button btnConfig_Zones;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnNodo5_RST;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnNodo4_RST;
        private System.Windows.Forms.Button btnNodos_Reset;
        private System.Windows.Forms.Button btnNodo1_ACK;
        private System.Windows.Forms.Button btnNodo2_ACK;
        private System.Windows.Forms.Button btnNodo3_ACK;
        private System.Windows.Forms.Button btnNodo5_ACK;
        private System.Windows.Forms.Button btnNodo4_ACK;
        private System.Windows.Forms.Label lblZ1;
        private System.Windows.Forms.Label lblz10;
        private System.Windows.Forms.Label lblZ9;
        private System.Windows.Forms.Label lblZ8;
        private System.Windows.Forms.Label lblZ7;
        private System.Windows.Forms.Label lblZ6;
        private System.Windows.Forms.Label lblZ5;
        private System.Windows.Forms.Label lblZ4;
        private System.Windows.Forms.Label lblZ3;
        private System.Windows.Forms.Label lblZ2;
        private System.Windows.Forms.Label labelprueba;
        private System.Windows.Forms.Label lblHora;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Timer tmrHora;
        private System.Windows.Forms.Label lblNodo1_BAT;
        private System.Windows.Forms.Label lblNodo2_BAT;
    }
}

