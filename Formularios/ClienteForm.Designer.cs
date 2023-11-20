namespace Formularios
{
    partial class ClienteForm
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
            txtNombre = new TextBox();
            txtDni = new TextBox();
            txtApellido = new TextBox();
            lblNombre = new Label();
            lblApellido = new Label();
            lblDni = new Label();
            txtTelefono = new TextBox();
            lblTelefono = new Label();
            btnGuardar = new Button();
            lblErrorNombre = new Label();
            lblErrorApellido = new Label();
            lblErrorDni = new Label();
            lblErrorCel = new Label();
            SuspendLayout();
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(75, 62);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(137, 23);
            txtNombre.TabIndex = 0;
            // 
            // txtDni
            // 
            txtDni.Location = new Point(75, 204);
            txtDni.Name = "txtDni";
            txtDni.Size = new Size(137, 23);
            txtDni.TabIndex = 2;
            // 
            // txtApellido
            // 
            txtApellido.Location = new Point(75, 134);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(137, 23);
            txtApellido.TabIndex = 1;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(75, 44);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(54, 15);
            lblNombre.TabIndex = 3;
            lblNombre.Text = "Nombre:";
            // 
            // lblApellido
            // 
            lblApellido.AutoSize = true;
            lblApellido.Location = new Point(75, 116);
            lblApellido.Name = "lblApellido";
            lblApellido.Size = new Size(54, 15);
            lblApellido.TabIndex = 4;
            lblApellido.Text = "Apellido:";
            // 
            // lblDni
            // 
            lblDni.AutoSize = true;
            lblDni.Location = new Point(75, 186);
            lblDni.Name = "lblDni";
            lblDni.Size = new Size(30, 15);
            lblDni.TabIndex = 5;
            lblDni.Text = "DNI:";
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(75, 277);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(137, 23);
            txtTelefono.TabIndex = 3;
            // 
            // lblTelefono
            // 
            lblTelefono.AutoSize = true;
            lblTelefono.Location = new Point(75, 259);
            lblTelefono.Name = "lblTelefono";
            lblTelefono.Size = new Size(102, 15);
            lblTelefono.TabIndex = 7;
            lblTelefono.Text = "Celular sin prefijo:";
            // 
            // btnGuardar
            // 
            btnGuardar.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnGuardar.Location = new Point(90, 357);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(105, 33);
            btnGuardar.TabIndex = 4;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // lblErrorNombre
            // 
            lblErrorNombre.AutoSize = true;
            lblErrorNombre.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            lblErrorNombre.ForeColor = Color.Red;
            lblErrorNombre.Location = new Point(75, 88);
            lblErrorNombre.Name = "lblErrorNombre";
            lblErrorNombre.Size = new Size(0, 13);
            lblErrorNombre.TabIndex = 8;
            // 
            // lblErrorApellido
            // 
            lblErrorApellido.AutoSize = true;
            lblErrorApellido.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            lblErrorApellido.ForeColor = Color.Red;
            lblErrorApellido.Location = new Point(75, 160);
            lblErrorApellido.Name = "lblErrorApellido";
            lblErrorApellido.Size = new Size(0, 13);
            lblErrorApellido.TabIndex = 9;
            // 
            // lblErrorDni
            // 
            lblErrorDni.AutoSize = true;
            lblErrorDni.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            lblErrorDni.ForeColor = Color.Red;
            lblErrorDni.Location = new Point(75, 230);
            lblErrorDni.Name = "lblErrorDni";
            lblErrorDni.Size = new Size(0, 13);
            lblErrorDni.TabIndex = 10;
            // 
            // lblErrorCel
            // 
            lblErrorCel.AutoSize = true;
            lblErrorCel.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            lblErrorCel.ForeColor = Color.Red;
            lblErrorCel.Location = new Point(75, 303);
            lblErrorCel.Name = "lblErrorCel";
            lblErrorCel.Size = new Size(0, 13);
            lblErrorCel.TabIndex = 11;
            // 
            // ClienteForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(292, 426);
            Controls.Add(lblErrorCel);
            Controls.Add(lblErrorDni);
            Controls.Add(lblErrorApellido);
            Controls.Add(lblErrorNombre);
            Controls.Add(btnGuardar);
            Controls.Add(lblTelefono);
            Controls.Add(txtTelefono);
            Controls.Add(lblDni);
            Controls.Add(lblApellido);
            Controls.Add(lblNombre);
            Controls.Add(txtApellido);
            Controls.Add(txtDni);
            Controls.Add(txtNombre);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "ClienteForm";
            Text = "Alta Cliente";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtNombre;
        private TextBox txtDni;
        private TextBox txtApellido;
        private Label lblNombre;
        private Label lblApellido;
        private Label lblDni;
        private TextBox txtTelefono;
        private Label lblTelefono;
        private Button btnGuardar;
        private Label lblErrorNombre;
        private Label lblErrorApellido;
        private Label lblErrorDni;
        private Label lblErrorCel;
    }
}