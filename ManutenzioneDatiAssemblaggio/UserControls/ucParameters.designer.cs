namespace Metra.ManutenzioneDatiAssemblaggio
{
    partial class ucParametri
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
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Tabelle");
            this.lblTitolo = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvScelta = new System.Windows.Forms.TreeView();
            this.dgvElenco = new System.Windows.Forms.DataGridView();
            this.btnSalva = new System.Windows.Forms.Button();
            this.btnAnnulla = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvElenco)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitolo
            // 
            this.lblTitolo.AutoSize = true;
            this.lblTitolo.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitolo.ForeColor = System.Drawing.Color.White;
            this.lblTitolo.Location = new System.Drawing.Point(6, 3);
            this.lblTitolo.Name = "lblTitolo";
            this.lblTitolo.Size = new System.Drawing.Size(110, 23);
            this.lblTitolo.TabIndex = 1;
            this.lblTitolo.Text = "Anagrafiche";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(6, 29);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvScelta);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnAnnulla);
            this.splitContainer1.Panel2.Controls.Add(this.dgvElenco);
            this.splitContainer1.Panel2.Controls.Add(this.btnSalva);
            this.splitContainer1.Size = new System.Drawing.Size(612, 528);
            this.splitContainer1.SplitterDistance = 161;
            this.splitContainer1.TabIndex = 7;
            // 
            // tvScelta
            // 
            this.tvScelta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvScelta.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvScelta.Location = new System.Drawing.Point(0, 0);
            this.tvScelta.Name = "tvScelta";
            treeNode2.Name = "Node0";
            treeNode2.Text = "Tabelle";
            this.tvScelta.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2});
            this.tvScelta.Size = new System.Drawing.Size(161, 528);
            this.tvScelta.TabIndex = 1;
            this.tvScelta.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvScelta_AfterSelect);
            // 
            // dgvElenco
            // 
            this.dgvElenco.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvElenco.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvElenco.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvElenco.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvElenco.Location = new System.Drawing.Point(0, 0);
            this.dgvElenco.Margin = new System.Windows.Forms.Padding(0);
            this.dgvElenco.Name = "dgvElenco";
            this.dgvElenco.RowTemplate.Height = 23;
            this.dgvElenco.Size = new System.Drawing.Size(447, 492);
            this.dgvElenco.TabIndex = 2;
            this.dgvElenco.DoubleClick += new System.EventHandler(this.dgTabella_DoubleClick);
            // 
            // btnSalva
            // 
            this.btnSalva.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalva.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnSalva.Enabled = false;
            this.btnSalva.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalva.Location = new System.Drawing.Point(0, 499);
            this.btnSalva.Name = "btnSalva";
            this.btnSalva.Size = new System.Drawing.Size(447, 30);
            this.btnSalva.TabIndex = 4;
            this.btnSalva.Text = "Salva";
            this.btnSalva.UseVisualStyleBackColor = true;
            this.btnSalva.Click += new System.EventHandler(this.btnSalva_Click);
            // 
            // btnAnnulla
            // 
            this.btnAnnulla.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAnnulla.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnAnnulla.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnnulla.Location = new System.Drawing.Point(356, 448);
            this.btnAnnulla.Name = "btnAnnulla";
            this.btnAnnulla.Size = new System.Drawing.Size(78, 30);
            this.btnAnnulla.TabIndex = 5;
            this.btnAnnulla.Text = "Chiudi";
            this.btnAnnulla.UseVisualStyleBackColor = true;
            this.btnAnnulla.Visible = false;
            this.btnAnnulla.Click += new System.EventHandler(this.btnAnnulla_Click);
            // 
            // ucParametri
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.lblTitolo);
            this.Name = "ucParametri";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Size = new System.Drawing.Size(624, 563);
            this.Load += new System.EventHandler(this.ctlParametri_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvElenco)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvElenco;
        private System.Windows.Forms.Button btnAnnulla;
        private System.Windows.Forms.Button btnSalva;
        private System.Windows.Forms.TreeView tvScelta;
        private System.Windows.Forms.Label lblTitolo;
        private System.Windows.Forms.SplitContainer splitContainer1;

    }
}
