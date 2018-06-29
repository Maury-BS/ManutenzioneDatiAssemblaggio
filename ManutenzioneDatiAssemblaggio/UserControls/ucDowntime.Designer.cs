namespace Metra.ManutenzioneDatiAssemblaggio
{
    partial class ucDowntime
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbDiagnostica = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbPostazione = new System.Windows.Forms.ComboBox();
            this.btnAnnulla = new System.Windows.Forms.Button();
            this.btnDiagnostica = new System.Windows.Forms.Button();
            this.btnAggiorna = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbTipo = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.dgvElenco = new System.Windows.Forms.DataGridView();
            this.lblTitolo = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFine = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpInizio = new System.Windows.Forms.DateTimePicker();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvElenco)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbDiagnostica
            // 
            this.cmbDiagnostica.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDiagnostica.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDiagnostica.FormattingEnabled = true;
            this.cmbDiagnostica.Items.AddRange(new object[] {
            "TUTTO",
            "OK",
            "ERR"});
            this.cmbDiagnostica.Location = new System.Drawing.Point(603, 6);
            this.cmbDiagnostica.Margin = new System.Windows.Forms.Padding(4);
            this.cmbDiagnostica.Name = "cmbDiagnostica";
            this.cmbDiagnostica.Size = new System.Drawing.Size(154, 24);
            this.cmbDiagnostica.TabIndex = 91;
            this.cmbDiagnostica.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(514, 9);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 16);
            this.label5.TabIndex = 92;
            this.label5.Text = "Diagnostica:";
            this.label5.Visible = false;
            // 
            // cmbPostazione
            // 
            this.cmbPostazione.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPostazione.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPostazione.FormattingEnabled = true;
            this.cmbPostazione.Location = new System.Drawing.Point(97, 38);
            this.cmbPostazione.Margin = new System.Windows.Forms.Padding(4);
            this.cmbPostazione.Name = "cmbPostazione";
            this.cmbPostazione.Size = new System.Drawing.Size(92, 24);
            this.cmbPostazione.TabIndex = 90;
            // 
            // btnAnnulla
            // 
            this.btnAnnulla.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAnnulla.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnAnnulla.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnnulla.Location = new System.Drawing.Point(690, 416);
            this.btnAnnulla.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAnnulla.Name = "btnAnnulla";
            this.btnAnnulla.Size = new System.Drawing.Size(91, 30);
            this.btnAnnulla.TabIndex = 89;
            this.btnAnnulla.Text = "Chiudi";
            this.btnAnnulla.UseVisualStyleBackColor = true;
            this.btnAnnulla.Visible = false;
            this.btnAnnulla.Click += new System.EventHandler(this.btnAnnulla_Click);
            // 
            // btnDiagnostica
            // 
            this.btnDiagnostica.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDiagnostica.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnDiagnostica.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDiagnostica.Location = new System.Drawing.Point(400, 415);
            this.btnDiagnostica.Margin = new System.Windows.Forms.Padding(4);
            this.btnDiagnostica.Name = "btnDiagnostica";
            this.btnDiagnostica.Size = new System.Drawing.Size(155, 30);
            this.btnDiagnostica.TabIndex = 88;
            this.btnDiagnostica.Text = "Diagnostica";
            this.btnDiagnostica.UseVisualStyleBackColor = false;
            this.btnDiagnostica.Visible = false;
            this.btnDiagnostica.Click += new System.EventHandler(this.btnDiagnostica_Click);
            // 
            // btnAggiorna
            // 
            this.btnAggiorna.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAggiorna.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnAggiorna.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAggiorna.Location = new System.Drawing.Point(8, 415);
            this.btnAggiorna.Margin = new System.Windows.Forms.Padding(4);
            this.btnAggiorna.Name = "btnAggiorna";
            this.btnAggiorna.Size = new System.Drawing.Size(205, 30);
            this.btnAggiorna.TabIndex = 81;
            this.btnAggiorna.Text = "Aggiorna visualizzazione";
            this.btnAggiorna.UseVisualStyleBackColor = false;
            this.btnAggiorna.Click += new System.EventHandler(this.RefreshData);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(9, 41);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 16);
            this.label3.TabIndex = 86;
            this.label3.Text = "Postazione:";
            // 
            // cmbTipo
            // 
            this.cmbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTipo.FormattingEnabled = true;
            this.cmbTipo.Location = new System.Drawing.Point(264, 38);
            this.cmbTipo.Margin = new System.Windows.Forms.Padding(4);
            this.cmbTipo.Name = "cmbTipo";
            this.cmbTipo.Size = new System.Drawing.Size(112, 24);
            this.cmbTipo.TabIndex = 77;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(217, 41);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(39, 16);
            this.label11.TabIndex = 85;
            this.label11.Text = "Tipo:";
            // 
            // dgvElenco
            // 
            this.dgvElenco.AllowUserToAddRows = false;
            this.dgvElenco.AllowUserToDeleteRows = false;
            this.dgvElenco.AllowUserToResizeRows = false;
            this.dgvElenco.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvElenco.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvElenco.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvElenco.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvElenco.Location = new System.Drawing.Point(8, 70);
            this.dgvElenco.Margin = new System.Windows.Forms.Padding(4);
            this.dgvElenco.Name = "dgvElenco";
            this.dgvElenco.RowHeadersVisible = false;
            this.dgvElenco.RowHeadersWidth = 20;
            this.dgvElenco.RowTemplate.Height = 23;
            this.dgvElenco.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvElenco.Size = new System.Drawing.Size(773, 340);
            this.dgvElenco.TabIndex = 76;
            // 
            // lblTitolo
            // 
            this.lblTitolo.AutoSize = true;
            this.lblTitolo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitolo.ForeColor = System.Drawing.Color.White;
            this.lblTitolo.Location = new System.Drawing.Point(8, 6);
            this.lblTitolo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitolo.Name = "lblTitolo";
            this.lblTitolo.Size = new System.Drawing.Size(146, 24);
            this.lblTitolo.TabIndex = 82;
            this.lblTitolo.Text = "Fermi macchina";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(600, 41);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 16);
            this.label2.TabIndex = 84;
            this.label2.Text = "al";
            // 
            // dtpFine
            // 
            this.dtpFine.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFine.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFine.Location = new System.Drawing.Point(627, 38);
            this.dtpFine.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpFine.Name = "dtpFine";
            this.dtpFine.Size = new System.Drawing.Size(94, 22);
            this.dtpFine.TabIndex = 80;
            this.dtpFine.CloseUp += new System.EventHandler(this.RefreshData);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(411, 41);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 16);
            this.label1.TabIndex = 83;
            this.label1.Text = "Periodo: dal";
            // 
            // dtpInizio
            // 
            this.dtpInizio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpInizio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpInizio.Location = new System.Drawing.Point(500, 38);
            this.dtpInizio.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpInizio.Name = "dtpInizio";
            this.dtpInizio.Size = new System.Drawing.Size(92, 22);
            this.dtpInizio.TabIndex = 79;
            this.dtpInizio.CloseUp += new System.EventHandler(this.RefreshData);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(220, 415);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(172, 30);
            this.btnSave.TabIndex = 93;
            this.btnSave.Text = "Salva modifiche";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // ucDowntime
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Gray;
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cmbDiagnostica);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbPostazione);
            this.Controls.Add(this.btnAnnulla);
            this.Controls.Add(this.btnDiagnostica);
            this.Controls.Add(this.btnAggiorna);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbTipo);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.dgvElenco);
            this.Controls.Add(this.lblTitolo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpFine);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpInizio);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ucDowntime";
            this.Size = new System.Drawing.Size(789, 450);
            ((System.ComponentModel.ISupportInitialize)(this.dgvElenco)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbDiagnostica;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbPostazione;
        private System.Windows.Forms.Button btnAnnulla;
        private System.Windows.Forms.Button btnDiagnostica;
        private System.Windows.Forms.Button btnAggiorna;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbTipo;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridView dgvElenco;
        private System.Windows.Forms.Label lblTitolo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFine;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpInizio;
        private System.Windows.Forms.Button btnSave;
    }
}
