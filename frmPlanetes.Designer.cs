namespace projetStarGate
{
    partial class frmPlanetes
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
            this.panelHaut = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cmbFiltreDataBaz = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.flpPlanetes = new System.Windows.Forms.FlowLayoutPanel();
            this.grpMissions = new System.Windows.Forms.GroupBox();
            this.dgvMissions = new System.Windows.Forms.DataGridView();
            this.grpEspeces = new System.Windows.Forms.GroupBox();
            this.dgvEspeces = new System.Windows.Forms.DataGridView();
            this.lblDataBazDetail = new System.Windows.Forms.Label();
            this.lblGraviteDetail = new System.Windows.Forms.Label();
            this.lblTempDetail = new System.Windows.Forms.Label();
            this.pbDetailPlanete = new System.Windows.Forms.PictureBox();
            this.lblNomPlanete = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRetour = new System.Windows.Forms.Button();
            this.panelHaut.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.grpMissions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMissions)).BeginInit();
            this.grpEspeces.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEspeces)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDetailPlanete)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHaut
            // 
            this.panelHaut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.panelHaut.Controls.Add(this.pictureBox1);
            this.panelHaut.Controls.Add(this.cmbFiltreDataBaz);
            this.panelHaut.Controls.Add(this.label1);
            this.panelHaut.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHaut.Location = new System.Drawing.Point(0, 0);
            this.panelHaut.Name = "panelHaut";
            this.panelHaut.Size = new System.Drawing.Size(1878, 98);
            this.panelHaut.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::projetStarGate.Properties.Resources.Logo_Stargate1;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(549, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(101, 85);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // cmbFiltreDataBaz
            // 
            this.cmbFiltreDataBaz.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
            this.cmbFiltreDataBaz.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiltreDataBaz.Font = new System.Drawing.Font("Segoe UI Light", 13F);
            this.cmbFiltreDataBaz.ForeColor = System.Drawing.SystemColors.Info;
            this.cmbFiltreDataBaz.FormattingEnabled = true;
            this.cmbFiltreDataBaz.Location = new System.Drawing.Point(20, 29);
            this.cmbFiltreDataBaz.Name = "cmbFiltreDataBaz";
            this.cmbFiltreDataBaz.Size = new System.Drawing.Size(280, 31);
            this.cmbFiltreDataBaz.TabIndex = 1;
            this.cmbFiltreDataBaz.SelectedIndexChanged += new System.EventHandler(this.cmbFiltreDataBaz_SelectedIndexChanged);
            this.cmbFiltreDataBaz.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbFiltreDataBaz_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Info;
            this.label1.Location = new System.Drawing.Point(656, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(533, 55);
            this.label1.TabIndex = 0;
            this.label1.Text = "Planètes de la galaxie";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 98);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.flpPlanetes);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.splitContainer1.Panel2.BackgroundImage = global::projetStarGate.Properties.Resources.Logo_Stargate2;
            this.splitContainer1.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.splitContainer1.Panel2.Controls.Add(this.grpMissions);
            this.splitContainer1.Panel2.Controls.Add(this.grpEspeces);
            this.splitContainer1.Panel2.Controls.Add(this.lblDataBazDetail);
            this.splitContainer1.Panel2.Controls.Add(this.lblGraviteDetail);
            this.splitContainer1.Panel2.Controls.Add(this.lblTempDetail);
            this.splitContainer1.Panel2.Controls.Add(this.pbDetailPlanete);
            this.splitContainer1.Panel2.Controls.Add(this.lblNomPlanete);
            this.splitContainer1.Size = new System.Drawing.Size(1878, 802);
            this.splitContainer1.SplitterDistance = 1201;
            this.splitContainer1.TabIndex = 1;
            // 
            // flpPlanetes
            // 
            this.flpPlanetes.AutoScroll = true;
            this.flpPlanetes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.flpPlanetes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpPlanetes.Location = new System.Drawing.Point(0, 0);
            this.flpPlanetes.Name = "flpPlanetes";
            this.flpPlanetes.Size = new System.Drawing.Size(1201, 802);
            this.flpPlanetes.TabIndex = 0;
            this.flpPlanetes.Paint += new System.Windows.Forms.PaintEventHandler(this.flpPlanetes_Paint);
            // 
            // grpMissions
            // 
            this.grpMissions.Controls.Add(this.dgvMissions);
            this.grpMissions.ForeColor = System.Drawing.SystemColors.Info;
            this.grpMissions.Location = new System.Drawing.Point(16, 633);
            this.grpMissions.Name = "grpMissions";
            this.grpMissions.Size = new System.Drawing.Size(574, 142);
            this.grpMissions.TabIndex = 6;
            this.grpMissions.TabStop = false;
            this.grpMissions.Text = "Missions effectuées";
            // 
            // dgvMissions
            // 
            this.dgvMissions.AllowUserToAddRows = false;
            this.dgvMissions.AllowUserToDeleteRows = false;
            this.dgvMissions.AllowUserToResizeColumns = false;
            this.dgvMissions.AllowUserToResizeRows = false;
            this.dgvMissions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMissions.Location = new System.Drawing.Point(6, 19);
            this.dgvMissions.Name = "dgvMissions";
            this.dgvMissions.ReadOnly = true;
            this.dgvMissions.Size = new System.Drawing.Size(562, 117);
            this.dgvMissions.TabIndex = 0;
            this.dgvMissions.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgvEspeces_KeyPress);
            // 
            // grpEspeces
            // 
            this.grpEspeces.Controls.Add(this.dgvEspeces);
            this.grpEspeces.ForeColor = System.Drawing.SystemColors.Info;
            this.grpEspeces.Location = new System.Drawing.Point(16, 460);
            this.grpEspeces.Name = "grpEspeces";
            this.grpEspeces.Size = new System.Drawing.Size(420, 167);
            this.grpEspeces.TabIndex = 5;
            this.grpEspeces.TabStop = false;
            this.grpEspeces.Text = "Espèces présentes";
            // 
            // dgvEspeces
            // 
            this.dgvEspeces.AllowUserToAddRows = false;
            this.dgvEspeces.AllowUserToDeleteRows = false;
            this.dgvEspeces.AllowUserToResizeColumns = false;
            this.dgvEspeces.AllowUserToResizeRows = false;
            this.dgvEspeces.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEspeces.Location = new System.Drawing.Point(6, 19);
            this.dgvEspeces.Name = "dgvEspeces";
            this.dgvEspeces.ReadOnly = true;
            this.dgvEspeces.Size = new System.Drawing.Size(408, 142);
            this.dgvEspeces.TabIndex = 0;
            this.dgvEspeces.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgvEspeces_KeyPress);
            // 
            // lblDataBazDetail
            // 
            this.lblDataBazDetail.AutoSize = true;
            this.lblDataBazDetail.Location = new System.Drawing.Point(19, 385);
            this.lblDataBazDetail.Name = "lblDataBazDetail";
            this.lblDataBazDetail.Size = new System.Drawing.Size(35, 13);
            this.lblDataBazDetail.TabIndex = 4;
            this.lblDataBazDetail.Text = "label2";
            // 
            // lblGraviteDetail
            // 
            this.lblGraviteDetail.AutoSize = true;
            this.lblGraviteDetail.ForeColor = System.Drawing.SystemColors.Info;
            this.lblGraviteDetail.Location = new System.Drawing.Point(13, 126);
            this.lblGraviteDetail.Name = "lblGraviteDetail";
            this.lblGraviteDetail.Size = new System.Drawing.Size(35, 13);
            this.lblGraviteDetail.TabIndex = 3;
            this.lblGraviteDetail.Text = "label2";
            // 
            // lblTempDetail
            // 
            this.lblTempDetail.AutoSize = true;
            this.lblTempDetail.ForeColor = System.Drawing.SystemColors.Info;
            this.lblTempDetail.Location = new System.Drawing.Point(13, 76);
            this.lblTempDetail.Name = "lblTempDetail";
            this.lblTempDetail.Size = new System.Drawing.Size(35, 13);
            this.lblTempDetail.TabIndex = 2;
            this.lblTempDetail.Text = "label2";
            // 
            // pbDetailPlanete
            // 
            this.pbDetailPlanete.BackColor = System.Drawing.Color.Transparent;
            this.pbDetailPlanete.Location = new System.Drawing.Point(138, 53);
            this.pbDetailPlanete.Name = "pbDetailPlanete";
            this.pbDetailPlanete.Size = new System.Drawing.Size(523, 401);
            this.pbDetailPlanete.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbDetailPlanete.TabIndex = 1;
            this.pbDetailPlanete.TabStop = false;
            // 
            // lblNomPlanete
            // 
            this.lblNomPlanete.AutoSize = true;
            this.lblNomPlanete.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNomPlanete.ForeColor = System.Drawing.SystemColors.Info;
            this.lblNomPlanete.Location = new System.Drawing.Point(152, 19);
            this.lblNomPlanete.Name = "lblNomPlanete";
            this.lblNomPlanete.Size = new System.Drawing.Size(86, 31);
            this.lblNomPlanete.TabIndex = 0;
            this.lblNomPlanete.Text = "label2";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.panel1.Controls.Add(this.btnRetour);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 900);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1878, 71);
            this.panel1.TabIndex = 2;
            // 
            // btnRetour
            // 
            this.btnRetour.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.btnRetour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRetour.Font = new System.Drawing.Font("Microsoft Sans Serif", 35F);
            this.btnRetour.ForeColor = System.Drawing.SystemColors.Info;
            this.btnRetour.Location = new System.Drawing.Point(354, 6);
            this.btnRetour.Name = "btnRetour";
            this.btnRetour.Size = new System.Drawing.Size(493, 57);
            this.btnRetour.TabIndex = 0;
            this.btnRetour.Text = "← Retour";
            this.btnRetour.UseVisualStyleBackColor = false;
            this.btnRetour.Click += new System.EventHandler(this.btnRetour_Click_1);
            // 
            // frmPlanetes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.ClientSize = new System.Drawing.Size(1878, 971);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelHaut);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPlanetes";
            this.Text = "frmPlanetes";
            this.Load += new System.EventHandler(this.frmPlanetes_Load);
            this.panelHaut.ResumeLayout(false);
            this.panelHaut.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.grpMissions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMissions)).EndInit();
            this.grpEspeces.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEspeces)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDetailPlanete)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHaut;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.FlowLayoutPanel flpPlanetes;
        private System.Windows.Forms.Label lblNomPlanete;
        private System.Windows.Forms.Label lblDataBazDetail;
        private System.Windows.Forms.Label lblGraviteDetail;
        private System.Windows.Forms.Label lblTempDetail;
        private System.Windows.Forms.PictureBox pbDetailPlanete;
        private System.Windows.Forms.GroupBox grpMissions;
        private System.Windows.Forms.DataGridView dgvMissions;
        private System.Windows.Forms.GroupBox grpEspeces;
        private System.Windows.Forms.DataGridView dgvEspeces;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnRetour;
        private System.Windows.Forms.ComboBox cmbFiltreDataBaz;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}