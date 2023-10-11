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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.SerialESP = new System.IO.Ports.SerialPort(this.components);
            this.ListaCOM = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.txtParametros = new System.Windows.Forms.TextBox();
            this.LineaPerimetral1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 19);
            this.label1.TabIndex = 6;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(753, 142);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(264, 259);
            this.dataGridView1.TabIndex = 1;
            // 
            // SerialESP
            // 
            this.SerialESP.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.SerialESP_DataReceived);
            // 
            // ListaCOM
            // 
            this.ListaCOM.FormattingEnabled = true;
            this.ListaCOM.Location = new System.Drawing.Point(893, 76);
            this.ListaCOM.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ListaCOM.Name = "ListaCOM";
            this.ListaCOM.Size = new System.Drawing.Size(124, 21);
            this.ListaCOM.TabIndex = 3;
            this.ListaCOM.Text = "Seleccionar Puerto";
            this.ListaCOM.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(753, 102);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(124, 35);
            this.button1.TabIndex = 4;
            this.button1.Text = "Conectar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(893, 101);
            this.button2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(124, 37);
            this.button2.TabIndex = 5;
            this.button2.Text = "Desconectar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtParametros
            // 
            this.txtParametros.Location = new System.Drawing.Point(753, 76);
            this.txtParametros.Name = "txtParametros";
            this.txtParametros.Size = new System.Drawing.Size(124, 20);
            this.txtParametros.TabIndex = 7;
            this.txtParametros.TextChanged += new System.EventHandler(this.txtParametros_TextChanged);
            // 
            // LineaPerimetral1
            // 
            this.LineaPerimetral1.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.LineaPerimetral1.Location = new System.Drawing.Point(47, 76);
            this.LineaPerimetral1.Name = "LineaPerimetral1";
            this.LineaPerimetral1.Size = new System.Drawing.Size(630, 23);
            this.LineaPerimetral1.TabIndex = 8;
            this.LineaPerimetral1.Text = "Perimetro Micro 1";
            this.LineaPerimetral1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(1028, 412);
            this.Controls.Add(this.LineaPerimetral1);
            this.Controls.Add(this.txtParametros);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ListaCOM);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.IO.Ports.SerialPort SerialESP;
        private System.Windows.Forms.ComboBox ListaCOM;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtParametros;
        private System.Windows.Forms.Button LineaPerimetral1;
    }
}

