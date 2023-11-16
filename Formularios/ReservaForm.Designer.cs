namespace Formularios
{
    partial class ReservaForm
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
            lstReservas = new ListBox();
            lstVehiculosDisp = new ListBox();
            lblVehiculosDisp = new Label();
            txtDniCliente = new TextBox();
            label1 = new Label();
            label2 = new Label();
            dtpDesde = new DateTimePicker();
            lblDesde = new Label();
            lblHasta = new Label();
            dtpHasta = new DateTimePicker();
            btnBuscar = new Button();
            btnConfirmarReserva = new Button();
            btnCancelarReserva = new Button();
            label3 = new Label();
            txtNombreYApellido = new TextBox();
            SuspendLayout();
            // 
            // lstReservas
            // 
            lstReservas.FormattingEnabled = true;
            lstReservas.ItemHeight = 15;
            lstReservas.Location = new Point(25, 373);
            lstReservas.Name = "lstReservas";
            lstReservas.Size = new Size(621, 259);
            lstReservas.TabIndex = 0;
            // 
            // lstVehiculosDisp
            // 
            lstVehiculosDisp.FormattingEnabled = true;
            lstVehiculosDisp.ItemHeight = 15;
            lstVehiculosDisp.Location = new Point(440, 49);
            lstVehiculosDisp.Name = "lstVehiculosDisp";
            lstVehiculosDisp.Size = new Size(206, 214);
            lstVehiculosDisp.TabIndex = 1;
            // 
            // lblVehiculosDisp
            // 
            lblVehiculosDisp.AutoSize = true;
            lblVehiculosDisp.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lblVehiculosDisp.Location = new Point(485, 25);
            lblVehiculosDisp.Name = "lblVehiculosDisp";
            lblVehiculosDisp.Size = new Size(161, 21);
            lblVehiculosDisp.TabIndex = 2;
            lblVehiculosDisp.Text = "Vehiculos Disponibles";
            // 
            // txtDniCliente
            // 
            txtDniCliente.Location = new Point(25, 49);
            txtDniCliente.Name = "txtDniCliente";
            txtDniCliente.Size = new Size(147, 23);
            txtDniCliente.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(25, 25);
            label1.Name = "label1";
            label1.Size = new Size(89, 21);
            label1.TabIndex = 4;
            label1.Text = "DNI Cliente";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(25, 349);
            label2.Name = "label2";
            label2.Size = new Size(72, 21);
            label2.TabIndex = 5;
            label2.Text = "Reservas";
            // 
            // dtpDesde
            // 
            dtpDesde.Location = new Point(25, 171);
            dtpDesde.Name = "dtpDesde";
            dtpDesde.Size = new Size(211, 23);
            dtpDesde.TabIndex = 6;
            // 
            // lblDesde
            // 
            lblDesde.AutoSize = true;
            lblDesde.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            lblDesde.Location = new Point(25, 151);
            lblDesde.Name = "lblDesde";
            lblDesde.Size = new Size(45, 17);
            lblDesde.TabIndex = 7;
            lblDesde.Text = "Desde";
            // 
            // lblHasta
            // 
            lblHasta.AutoSize = true;
            lblHasta.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            lblHasta.Location = new Point(25, 220);
            lblHasta.Name = "lblHasta";
            lblHasta.Size = new Size(41, 17);
            lblHasta.TabIndex = 8;
            lblHasta.Text = "Hasta";
            // 
            // dtpHasta
            // 
            dtpHasta.Location = new Point(25, 240);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new Size(211, 23);
            dtpHasta.TabIndex = 9;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(178, 49);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(75, 23);
            btnBuscar.TabIndex = 10;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // btnConfirmarReserva
            // 
            btnConfirmarReserva.BackColor = SystemColors.ActiveCaption;
            btnConfirmarReserva.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnConfirmarReserva.Location = new Point(111, 286);
            btnConfirmarReserva.Name = "btnConfirmarReserva";
            btnConfirmarReserva.Size = new Size(154, 34);
            btnConfirmarReserva.TabIndex = 11;
            btnConfirmarReserva.Text = "Confirmar Reserva";
            btnConfirmarReserva.UseVisualStyleBackColor = false;
            btnConfirmarReserva.Click += btnConfirmarReserva_Click;
            // 
            // btnCancelarReserva
            // 
            btnCancelarReserva.BackColor = Color.IndianRed;
            btnCancelarReserva.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnCancelarReserva.Location = new Point(403, 286);
            btnCancelarReserva.Name = "btnCancelarReserva";
            btnCancelarReserva.Size = new Size(154, 34);
            btnCancelarReserva.TabIndex = 12;
            btnCancelarReserva.Text = "Cancelar Reserva";
            btnCancelarReserva.UseVisualStyleBackColor = false;
            btnCancelarReserva.Click += btnCancelarReserva_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(25, 85);
            label3.Name = "label3";
            label3.Size = new Size(141, 21);
            label3.TabIndex = 13;
            label3.Text = "Nombre y Apellido";
            // 
            // txtNombreYApellido
            // 
            txtNombreYApellido.Location = new Point(25, 109);
            txtNombreYApellido.Name = "txtNombreYApellido";
            txtNombreYApellido.Size = new Size(211, 23);
            txtNombreYApellido.TabIndex = 14;
            // 
            // ReservaForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(683, 644);
            Controls.Add(txtNombreYApellido);
            Controls.Add(label3);
            Controls.Add(btnCancelarReserva);
            Controls.Add(btnConfirmarReserva);
            Controls.Add(btnBuscar);
            Controls.Add(dtpHasta);
            Controls.Add(lblHasta);
            Controls.Add(lblDesde);
            Controls.Add(dtpDesde);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtDniCliente);
            Controls.Add(lblVehiculosDisp);
            Controls.Add(lstVehiculosDisp);
            Controls.Add(lstReservas);
            Name = "ReservaForm";
            Text = "ReservaForm";
            Load += ReservaForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox lstReservas;
        private ListBox lstVehiculosDisp;
        private Label lblVehiculosDisp;
        private TextBox txtDniCliente;
        private Label label1;
        private Label label2;
        private DateTimePicker dtpDesde;
        private Label lblDesde;
        private Label lblHasta;
        private DateTimePicker dtpHasta;
        private Button btnBuscar;
        private Button btnConfirmarReserva;
        private Button btnCancelarReserva;
        private Label label3;
        private TextBox txtNombreYApellido;
    }
}