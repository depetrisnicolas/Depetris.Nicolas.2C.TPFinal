namespace Formularios
{
    partial class VehiculoForm
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
            txtMarca = new TextBox();
            lblMarca = new Label();
            txtModelo = new TextBox();
            lblModelo = new Label();
            txtAnio = new TextBox();
            lblAño = new Label();
            cmbTipo = new ComboBox();
            lblTipo = new Label();
            btnGuardar = new Button();
            txtPatente = new TextBox();
            lblPatente = new Label();
            SuspendLayout();
            // 
            // txtMarca
            // 
            txtMarca.Location = new Point(62, 64);
            txtMarca.Name = "txtMarca";
            txtMarca.Size = new Size(136, 23);
            txtMarca.TabIndex = 1;
            // 
            // lblMarca
            // 
            lblMarca.AutoSize = true;
            lblMarca.Location = new Point(62, 46);
            lblMarca.Name = "lblMarca";
            lblMarca.Size = new Size(43, 15);
            lblMarca.TabIndex = 4;
            lblMarca.Text = "Marca:";
            // 
            // txtModelo
            // 
            txtModelo.Location = new Point(62, 123);
            txtModelo.Name = "txtModelo";
            txtModelo.Size = new Size(136, 23);
            txtModelo.TabIndex = 5;
            // 
            // lblModelo
            // 
            lblModelo.AutoSize = true;
            lblModelo.Location = new Point(62, 105);
            lblModelo.Name = "lblModelo";
            lblModelo.Size = new Size(51, 15);
            lblModelo.TabIndex = 6;
            lblModelo.Text = "Modelo:";
            // 
            // txtAnio
            // 
            txtAnio.Location = new Point(62, 184);
            txtAnio.Name = "txtAnio";
            txtAnio.Size = new Size(62, 23);
            txtAnio.TabIndex = 7;
            // 
            // lblAño
            // 
            lblAño.AutoSize = true;
            lblAño.Location = new Point(62, 166);
            lblAño.Name = "lblAño";
            lblAño.Size = new Size(32, 15);
            lblAño.TabIndex = 8;
            lblAño.Text = "Año:";
            // 
            // cmbTipo
            // 
            cmbTipo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipo.FormattingEnabled = true;
            cmbTipo.Items.AddRange(new object[] { "Auto", "Camioneta" });
            cmbTipo.Location = new Point(62, 242);
            cmbTipo.Name = "cmbTipo";
            cmbTipo.Size = new Size(136, 23);
            cmbTipo.TabIndex = 9;
            // 
            // lblTipo
            // 
            lblTipo.AutoSize = true;
            lblTipo.Location = new Point(62, 224);
            lblTipo.Name = "lblTipo";
            lblTipo.Size = new Size(33, 15);
            lblTipo.TabIndex = 10;
            lblTipo.Text = "Tipo:";
            // 
            // btnGuardar
            // 
            btnGuardar.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnGuardar.Location = new Point(75, 369);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(105, 33);
            btnGuardar.TabIndex = 11;
            btnGuardar.Text = "GUARDAR";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // txtPatente
            // 
            txtPatente.Location = new Point(62, 302);
            txtPatente.Name = "txtPatente";
            txtPatente.Size = new Size(136, 23);
            txtPatente.TabIndex = 12;
            txtPatente.TextChanged += txtPatente_TextChanged;
            // 
            // lblPatente
            // 
            lblPatente.AutoSize = true;
            lblPatente.Location = new Point(62, 284);
            lblPatente.Name = "lblPatente";
            lblPatente.Size = new Size(47, 15);
            lblPatente.TabIndex = 13;
            lblPatente.Text = "Patente";
            // 
            // VehiculoForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(260, 442);
            Controls.Add(lblPatente);
            Controls.Add(txtPatente);
            Controls.Add(btnGuardar);
            Controls.Add(lblTipo);
            Controls.Add(cmbTipo);
            Controls.Add(lblAño);
            Controls.Add(txtAnio);
            Controls.Add(lblModelo);
            Controls.Add(txtModelo);
            Controls.Add(lblMarca);
            Controls.Add(txtMarca);
            Name = "VehiculoForm";
            Text = "Alta Vehiculo";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtMarca;
        private Label lblMarca;
        private TextBox txtModelo;
        private Label lblModelo;
        private TextBox txtAnio;
        private Label lblAño;
        private ComboBox cmbTipo;
        private Label lblTipo;
        private Button btnGuardar;
        private TextBox txtPatente;
        private Label lblPatente;
    }
}