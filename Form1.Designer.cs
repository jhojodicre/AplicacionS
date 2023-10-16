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
            this.btnNodo1 = new System.Windows.Forms.Button();
            this.btnSerial_Enviar = new System.Windows.Forms.Button();
            this.panelSuperior = new System.Windows.Forms.Panel();
            this.btnSerial_Consola = new System.Windows.Forms.Button();
            this.panelIzquierdo = new System.Windows.Forms.Panel();
            this.txBSerial = new System.Windows.Forms.TextBox();
            this.lblSerial_Status = new System.Windows.Forms.Label();
            this.lblNodo2_ZA = new System.Windows.Forms.Label();
            this.lblNodo2_ZB = new System.Windows.Forms.Label();
            this.btnNodo2 = new System.Windows.Forms.Button();
            this.lblNodo1_ZA = new System.Windows.Forms.Label();
            this.lblNodo1_ZB = new System.Windows.Forms.Label();
            this.lblNodo3_ZA = new System.Windows.Forms.Label();
            this.lblNodo3_ZB = new System.Windows.Forms.Label();
            this.btnNodo3 = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panelCentral = new System.Windows.Forms.Panel();
            this.lblForm1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panelSuperior.SuspendLayout();
            this.panelIzquierdo.SuspendLayout();
            this.panelCentral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
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
            this.SerialESP.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.SerialESP_DataReceived);
            // 
            // cmbSerial_COM
            // 
            this.cmbSerial_COM.FormattingEnabled = true;
            this.cmbSerial_COM.Location = new System.Drawing.Point(4, 13);
            this.cmbSerial_COM.Margin = new System.Windows.Forms.Padding(2);
            this.cmbSerial_COM.Name = "cmbSerial_COM";
            this.cmbSerial_COM.Size = new System.Drawing.Size(124, 21);
            this.cmbSerial_COM.TabIndex = 3;
            this.cmbSerial_COM.Text = "Seleccionar Puerto";
            this.cmbSerial_COM.DropDown += new System.EventHandler(this.cmbSerial_DropDown);
            this.cmbSerial_COM.SelectedIndexChanged += new System.EventHandler(this.cmbSerial_SelectedIndexChanged);
            // 
            // btnSerial_Conectar
            // 
            this.btnSerial_Conectar.Font = new System.Drawing.Font("Yu Gothic UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSerial_Conectar.ForeColor = System.Drawing.SystemColors.Info;
            this.btnSerial_Conectar.Image = global::AplicacionS.Properties.Resources._0abde68a_e3a8_4d20_b346_ce66116c8a05;
            this.btnSerial_Conectar.Location = new System.Drawing.Point(131, 11);
            this.btnSerial_Conectar.Margin = new System.Windows.Forms.Padding(2);
            this.btnSerial_Conectar.Name = "btnSerial_Conectar";
            this.btnSerial_Conectar.Size = new System.Drawing.Size(101, 24);
            this.btnSerial_Conectar.TabIndex = 4;
            this.btnSerial_Conectar.Text = "Conectar";
            this.btnSerial_Conectar.UseVisualStyleBackColor = true;
            this.btnSerial_Conectar.Click += new System.EventHandler(this.btnSerial_Conectar_Click);
            // 
            // txtSerial_BufferTX
            // 
            this.txtSerial_BufferTX.Location = new System.Drawing.Point(-5, 322);
            this.txtSerial_BufferTX.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.txtSerial_BufferTX.Name = "txtSerial_BufferTX";
            this.txtSerial_BufferTX.Size = new System.Drawing.Size(81, 22);
            this.txtSerial_BufferTX.TabIndex = 7;
            this.txtSerial_BufferTX.TextChanged += new System.EventHandler(this.txtSerial_BufferTX_TextChanged);
            // 
            // btnNodo1
            // 
            this.btnNodo1.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnNodo1.Location = new System.Drawing.Point(32, 26);
            this.btnNodo1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnNodo1.Name = "btnNodo1";
            this.btnNodo1.Size = new System.Drawing.Size(55, 31);
            this.btnNodo1.TabIndex = 8;
            this.btnNodo1.Text = "Nodo 1";
            this.btnNodo1.UseVisualStyleBackColor = true;
            this.btnNodo1.Click += new System.EventHandler(this.btnNodo1_Click);
            // 
            // btnSerial_Enviar
            // 
            this.btnSerial_Enviar.Location = new System.Drawing.Point(72, 322);
            this.btnSerial_Enviar.Margin = new System.Windows.Forms.Padding(2);
            this.btnSerial_Enviar.Name = "btnSerial_Enviar";
            this.btnSerial_Enviar.Size = new System.Drawing.Size(42, 19);
            this.btnSerial_Enviar.TabIndex = 9;
            this.btnSerial_Enviar.Text = "Enviar";
            this.btnSerial_Enviar.UseVisualStyleBackColor = true;
            this.btnSerial_Enviar.Click += new System.EventHandler(this.btnSerial_Enviar_Click);
            // 
            // panelSuperior
            // 
            this.panelSuperior.BackColor = System.Drawing.Color.DarkSlateGray;
            this.panelSuperior.Controls.Add(this.pictureBox2);
            this.panelSuperior.Controls.Add(this.lblForm1);
            this.panelSuperior.Controls.Add(this.btnSerial_Conectar);
            this.panelSuperior.Controls.Add(this.cmbSerial_COM);
            this.panelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSuperior.Location = new System.Drawing.Point(0, 0);
            this.panelSuperior.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.panelSuperior.Name = "panelSuperior";
            this.panelSuperior.Size = new System.Drawing.Size(1028, 50);
            this.panelSuperior.TabIndex = 10;
            // 
            // btnSerial_Consola
            // 
            this.btnSerial_Consola.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSerial_Consola.Location = new System.Drawing.Point(0, 338);
            this.btnSerial_Consola.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnSerial_Consola.Name = "btnSerial_Consola";
            this.btnSerial_Consola.Size = new System.Drawing.Size(119, 24);
            this.btnSerial_Consola.TabIndex = 10;
            this.btnSerial_Consola.TabStop = false;
            this.btnSerial_Consola.Text = "Consola";
            this.btnSerial_Consola.UseVisualStyleBackColor = true;
            this.btnSerial_Consola.Click += new System.EventHandler(this.btnSerial_Consola_Click);
            // 
            // panelIzquierdo
            // 
            this.panelIzquierdo.BackColor = System.Drawing.Color.DarkSlateGray;
            this.panelIzquierdo.Controls.Add(this.txtSerial_BufferTX);
            this.panelIzquierdo.Controls.Add(this.btnSerial_Consola);
            this.panelIzquierdo.Controls.Add(this.btnSerial_Enviar);
            this.panelIzquierdo.Controls.Add(this.txBSerial);
            this.panelIzquierdo.Controls.Add(this.lblSerial_Status);
            this.panelIzquierdo.Controls.Add(this.lblNodo2_ZA);
            this.panelIzquierdo.Controls.Add(this.lblNodo2_ZB);
            this.panelIzquierdo.Controls.Add(this.btnNodo2);
            this.panelIzquierdo.Controls.Add(this.lblNodo1_ZA);
            this.panelIzquierdo.Controls.Add(this.lblNodo1_ZB);
            this.panelIzquierdo.Controls.Add(this.lblNodo3_ZA);
            this.panelIzquierdo.Controls.Add(this.lblNodo3_ZB);
            this.panelIzquierdo.Controls.Add(this.btnNodo3);
            this.panelIzquierdo.Controls.Add(this.btnNodo1);
            this.panelIzquierdo.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelIzquierdo.Location = new System.Drawing.Point(0, 50);
            this.panelIzquierdo.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.panelIzquierdo.Name = "panelIzquierdo";
            this.panelIzquierdo.Size = new System.Drawing.Size(119, 362);
            this.panelIzquierdo.TabIndex = 11;
            // 
            // txBSerial
            // 
            this.txBSerial.BackColor = System.Drawing.SystemColors.MenuBar;
            this.txBSerial.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txBSerial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txBSerial.ForeColor = System.Drawing.Color.Maroon;
            this.txBSerial.Location = new System.Drawing.Point(0, 0);
            this.txBSerial.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.txBSerial.Multiline = true;
            this.txBSerial.Name = "txBSerial";
            this.txBSerial.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txBSerial.Size = new System.Drawing.Size(119, 362);
            this.txBSerial.TabIndex = 18;
            // 
            // lblSerial_Status
            // 
            this.lblSerial_Status.Location = new System.Drawing.Point(7, 2);
            this.lblSerial_Status.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSerial_Status.Name = "lblSerial_Status";
            this.lblSerial_Status.Size = new System.Drawing.Size(100, 24);
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
            // btnNodo2
            // 
            this.btnNodo2.Location = new System.Drawing.Point(34, 63);
            this.btnNodo2.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnNodo2.Name = "btnNodo2";
            this.btnNodo2.Size = new System.Drawing.Size(55, 31);
            this.btnNodo2.TabIndex = 15;
            this.btnNodo2.Text = "Nodo 2";
            this.btnNodo2.UseVisualStyleBackColor = true;
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
            // btnNodo3
            // 
            this.btnNodo3.Location = new System.Drawing.Point(34, 99);
            this.btnNodo3.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnNodo3.Name = "btnNodo3";
            this.btnNodo3.Size = new System.Drawing.Size(55, 31);
            this.btnNodo3.TabIndex = 10;
            this.btnNodo3.Text = "Nodo 3";
            this.btnNodo3.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Black;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(119, 375);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(909, 37);
            this.panel3.TabIndex = 12;
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(999, 50);
            this.panel4.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(29, 325);
            this.panel4.TabIndex = 13;
            // 
            // panelCentral
            // 
            this.panelCentral.BackColor = System.Drawing.Color.DarkSlateGray;
            this.panelCentral.Controls.Add(this.pictureBox1);
            this.panelCentral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCentral.Font = new System.Drawing.Font("Yu Gothic UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelCentral.Location = new System.Drawing.Point(119, 50);
            this.panelCentral.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.panelCentral.Name = "panelCentral";
            this.panelCentral.Size = new System.Drawing.Size(880, 325);
            this.panelCentral.TabIndex = 14;
            // 
            // lblForm1
            // 
            this.lblForm1.AutoSize = true;
            this.lblForm1.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.lblForm1.Font = new System.Drawing.Font("Algerian", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblForm1.ForeColor = System.Drawing.Color.DarkCyan;
            this.lblForm1.Location = new System.Drawing.Point(400, 11);
            this.lblForm1.Name = "lblForm1";
            this.lblForm1.Size = new System.Drawing.Size(387, 35);
            this.lblForm1.TabIndex = 5;
            this.lblForm1.Text = "Seguridad Perimetral";
            this.lblForm1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::AplicacionS.Properties.Resources.BARRIO_ZARATE;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(880, 325);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::AplicacionS.Properties.Resources.Imagen_de_Seguridad;
            this.pictureBox2.Location = new System.Drawing.Point(869, 5);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(88, 45);
            this.pictureBox2.TabIndex = 6;
            this.pictureBox2.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(1028, 412);
            this.Controls.Add(this.panelCentral);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panelIzquierdo);
            this.Controls.Add(this.panelSuperior);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Seguridad Perimetral";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.panelSuperior.ResumeLayout(false);
            this.panelSuperior.PerformLayout();
            this.panelIzquierdo.ResumeLayout(false);
            this.panelIzquierdo.PerformLayout();
            this.panelCentral.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.IO.Ports.SerialPort SerialESP;
        private System.Windows.Forms.ComboBox cmbSerial_COM;
        private System.Windows.Forms.Button btnSerial_Conectar;
        private System.Windows.Forms.TextBox txtSerial_BufferTX;
        private System.Windows.Forms.Button btnNodo1;
        private System.Windows.Forms.Button btnSerial_Enviar;
        private System.Windows.Forms.Panel panelSuperior;
        private System.Windows.Forms.Panel panelIzquierdo;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panelCentral;
        private System.Windows.Forms.Label lblSerial_Status;
        private System.Windows.Forms.Button btnNodo3;
        private System.Windows.Forms.Label lblNodo3_ZB;
        private System.Windows.Forms.Label lblNodo3_ZA;
        private System.Windows.Forms.Label lblNodo2_ZA;
        private System.Windows.Forms.Label lblNodo2_ZB;
        private System.Windows.Forms.Button btnNodo2;
        private System.Windows.Forms.Label lblNodo1_ZA;
        private System.Windows.Forms.Label lblNodo1_ZB;
        private System.Windows.Forms.TextBox txBSerial;
        private System.Windows.Forms.Button btnSerial_Consola;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblForm1;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}

