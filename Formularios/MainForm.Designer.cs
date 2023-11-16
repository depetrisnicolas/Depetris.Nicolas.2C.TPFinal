namespace Formularios
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnAgregarCliente = new Button();
            btnAgregarVehiculo = new Button();
            btnReserva = new Button();
            SuspendLayout();
            // 
            // btnAgregarCliente
            // 
            btnAgregarCliente.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnAgregarCliente.Location = new Point(64, 79);
            btnAgregarCliente.Name = "btnAgregarCliente";
            btnAgregarCliente.Size = new Size(184, 92);
            btnAgregarCliente.TabIndex = 0;
            btnAgregarCliente.Text = "Agregar Cliente";
            btnAgregarCliente.UseVisualStyleBackColor = true;
            btnAgregarCliente.Click += btnAgregarCliente_Click;
            // 
            // btnAgregarVehiculo
            // 
            btnAgregarVehiculo.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnAgregarVehiculo.Location = new Point(396, 79);
            btnAgregarVehiculo.Name = "btnAgregarVehiculo";
            btnAgregarVehiculo.Size = new Size(184, 92);
            btnAgregarVehiculo.TabIndex = 1;
            btnAgregarVehiculo.Text = "Agregar vehiculo";
            btnAgregarVehiculo.UseVisualStyleBackColor = true;
            btnAgregarVehiculo.Click += btnAgregarVehiculo_Click;
            // 
            // btnReserva
            // 
            btnReserva.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnReserva.Location = new Point(208, 257);
            btnReserva.Name = "btnReserva";
            btnReserva.Size = new Size(228, 112);
            btnReserva.TabIndex = 2;
            btnReserva.Text = "Reservas";
            btnReserva.UseVisualStyleBackColor = true;
            btnReserva.Click += btnReserva_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(647, 412);
            Controls.Add(btnReserva);
            Controls.Add(btnAgregarVehiculo);
            Controls.Add(btnAgregarCliente);
            Name = "MainForm";
            Text = "UTN Rent Car";
            Load += MainForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button btnAgregarCliente;
        private Button btnAgregarVehiculo;
        private Button btnReserva;
    }
}