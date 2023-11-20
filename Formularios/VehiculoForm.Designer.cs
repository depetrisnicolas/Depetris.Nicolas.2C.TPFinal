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
            lblErrorMarca = new Label();
            lblErrorModelo = new Label();
            lblErrorAnio = new Label();
            lblErrorPatente = new Label();
            lblErrorTipo = new Label();
            SuspendLayout();
            // 
            // txtMarca
            // 
            txtMarca.Location = new Point(75, 53);
            txtMarca.Name = "txtMarca";
            txtMarca.Size = new Size(136, 23);
            txtMarca.TabIndex = 1;
            // 
            // lblMarca
            // 
            lblMarca.AutoSize = true;
            lblMarca.Location = new Point(75, 35);
            lblMarca.Name = "lblMarca";
            lblMarca.Size = new Size(43, 15);
            lblMarca.TabIndex = 4;
            lblMarca.Text = "Marca:";
            // 
            // txtModelo
            // 
            txtModelo.Location = new Point(75, 126);
            txtModelo.Name = "txtModelo";
            txtModelo.Size = new Size(136, 23);
            txtModelo.TabIndex = 5;
            // 
            // lblModelo
            // 
            lblModelo.AutoSize = true;
            lblModelo.Location = new Point(75, 108);
            lblModelo.Name = "lblModelo";
            lblModelo.Size = new Size(51, 15);
            lblModelo.TabIndex = 6;
            lblModelo.Text = "Modelo:";
            // 
            // txtAnio
            // 
            txtAnio.Location = new Point(75, 196);
            txtAnio.Name = "txtAnio";
            txtAnio.Size = new Size(62, 23);
            txtAnio.TabIndex = 7;
            // 
            // lblAño
            // 
            lblAño.AutoSize = true;
            lblAño.Location = new Point(75, 178);
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
            cmbTipo.Location = new Point(75, 279);
            cmbTipo.Name = "cmbTipo";
            cmbTipo.Size = new Size(136, 23);
            cmbTipo.TabIndex = 9;
            // 
            // lblTipo
            // 
            lblTipo.AutoSize = true;
            lblTipo.Location = new Point(75, 261);
            lblTipo.Name = "lblTipo";
            lblTipo.Size = new Size(33, 15);
            lblTipo.TabIndex = 10;
            lblTipo.Text = "Tipo:";
            // 
            // btnGuardar
            // 
            btnGuardar.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnGuardar.Location = new Point(91, 409);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(105, 33);
            btnGuardar.TabIndex = 11;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // txtPatente
            // 
            txtPatente.Location = new Point(75, 349);
            txtPatente.Name = "txtPatente";
            txtPatente.Size = new Size(136, 23);
            txtPatente.TabIndex = 12;
            // 
            // lblPatente
            // 
            lblPatente.AutoSize = true;
            lblPatente.Location = new Point(75, 331);
            lblPatente.Name = "lblPatente";
            lblPatente.Size = new Size(47, 15);
            lblPatente.TabIndex = 13;
            lblPatente.Text = "Patente";
            // 
            // lblErrorMarca
            // 
            lblErrorMarca.AutoSize = true;
            lblErrorMarca.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            lblErrorMarca.ForeColor = Color.Red;
            lblErrorMarca.Location = new Point(75, 79);
            lblErrorMarca.Name = "lblErrorMarca";
            lblErrorMarca.Size = new Size(0, 13);
            lblErrorMarca.TabIndex = 14;
            // 
            // lblErrorModelo
            // 
            lblErrorModelo.AutoSize = true;
            lblErrorModelo.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            lblErrorModelo.ForeColor = Color.Red;
            lblErrorModelo.Location = new Point(75, 152);
            lblErrorModelo.Name = "lblErrorModelo";
            lblErrorModelo.Size = new Size(0, 13);
            lblErrorModelo.TabIndex = 15;
            // 
            // lblErrorAnio
            // 
            lblErrorAnio.AutoSize = true;
            lblErrorAnio.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            lblErrorAnio.ForeColor = Color.Red;
            lblErrorAnio.Location = new Point(74, 222);
            lblErrorAnio.Name = "lblErrorAnio";
            lblErrorAnio.Size = new Size(0, 13);
            lblErrorAnio.TabIndex = 16;
            // 
            // lblErrorPatente
            // 
            lblErrorPatente.AutoSize = true;
            lblErrorPatente.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            lblErrorPatente.ForeColor = Color.Red;
            lblErrorPatente.Location = new Point(75, 375);
            lblErrorPatente.Name = "lblErrorPatente";
            lblErrorPatente.Size = new Size(0, 13);
            lblErrorPatente.TabIndex = 17;
            // 
            // lblErrorTipo
            // 
            lblErrorTipo.AutoSize = true;
            lblErrorTipo.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            lblErrorTipo.ForeColor = Color.Red;
            lblErrorTipo.Location = new Point(75, 305);
            lblErrorTipo.Name = "lblErrorTipo";
            lblErrorTipo.Size = new Size(0, 13);
            lblErrorTipo.TabIndex = 18;
            // 
            // VehiculoForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(292, 454);
            Controls.Add(lblErrorTipo);
            Controls.Add(lblErrorPatente);
            Controls.Add(lblErrorAnio);
            Controls.Add(lblErrorModelo);
            Controls.Add(lblErrorMarca);
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
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "VehiculoForm";
            Text = "Alta Vehiculo";
            Load += VehiculoForm_Load;
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
        private Label lblErrorMarca;
        private Label lblErrorModelo;
        private Label lblErrorAnio;
        private Label lblErrorPatente;
        private Label lblErrorTipo;
    }
}