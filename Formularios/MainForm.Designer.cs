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
            lblRentCar = new Label();
            pictureCar = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureCar).BeginInit();
            SuspendLayout();
            // 
            // btnAgregarCliente
            // 
            btnAgregarCliente.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnAgregarCliente.Location = new Point(37, 448);
            btnAgregarCliente.Name = "btnAgregarCliente";
            btnAgregarCliente.Size = new Size(139, 36);
            btnAgregarCliente.TabIndex = 0;
            btnAgregarCliente.Text = "Alta Cliente";
            btnAgregarCliente.UseVisualStyleBackColor = true;
            btnAgregarCliente.Click += btnAgregarCliente_Click;
            // 
            // btnAgregarVehiculo
            // 
            btnAgregarVehiculo.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnAgregarVehiculo.Location = new Point(477, 448);
            btnAgregarVehiculo.Name = "btnAgregarVehiculo";
            btnAgregarVehiculo.Size = new Size(132, 36);
            btnAgregarVehiculo.TabIndex = 1;
            btnAgregarVehiculo.Text = "Alta Vehiculo";
            btnAgregarVehiculo.UseVisualStyleBackColor = true;
            btnAgregarVehiculo.Click += btnAgregarVehiculo_Click;
            // 
            // btnReserva
            // 
            btnReserva.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnReserva.Location = new Point(251, 509);
            btnReserva.Name = "btnReserva";
            btnReserva.Size = new Size(145, 65);
            btnReserva.TabIndex = 2;
            btnReserva.Text = "Reservas";
            btnReserva.UseVisualStyleBackColor = true;
            btnReserva.Click += btnReserva_Click;
            // 
            // lblRentCar
            // 
            lblRentCar.AutoSize = true;
            lblRentCar.Font = new Font("Calibri", 27.75F, FontStyle.Bold, GraphicsUnit.Point);
            lblRentCar.ForeColor = Color.DarkSlateGray;
            lblRentCar.Location = new Point(210, 38);
            lblRentCar.Name = "lblRentCar";
            lblRentCar.Size = new Size(225, 45);
            lblRentCar.TabIndex = 3;
            lblRentCar.Text = "UTN Rent Car";
            // 
            // pictureCar
            // 
            pictureCar.Location = new Point(86, 86);
            pictureCar.Name = "pictureCar";
            pictureCar.Size = new Size(469, 311);
            pictureCar.TabIndex = 4;
            pictureCar.TabStop = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(645, 607);
            Controls.Add(pictureCar);
            Controls.Add(lblRentCar);
            Controls.Add(btnReserva);
            Controls.Add(btnAgregarVehiculo);
            Controls.Add(btnAgregarCliente);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "MainForm";
            Text = "UTN Rent Car";
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)pictureCar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnAgregarCliente;
        private Button btnAgregarVehiculo;
        private Button btnReserva;
        private Label lblRentCar;
        private PictureBox pictureCar;
    }
}