namespace projetStarGate
{
    partial class frmRaces
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelFiltres = new System.Windows.Forms.Panel();
            this.res = new System.Windows.Forms.Button();
            this.Espece_Trouver = new System.Windows.Forms.Label();
            this.btnRechercher = new System.Windows.Forms.Button();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbCouleur = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNom = new System.Windows.Forms.TextBox();
            this.lbl1 = new System.Windows.Forms.Label();
            this.flpRaces = new System.Windows.Forms.FlowLayoutPanel();
            this.btnRetour = new System.Windows.Forms.Button();
            this.panelBas = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelFiltres.SuspendLayout();
            this.panelBas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelFiltres
            // 
            this.panelFiltres.Controls.Add(this.pictureBox1);
            this.panelFiltres.Controls.Add(this.res);
            this.panelFiltres.Controls.Add(this.Espece_Trouver);
            this.panelFiltres.Controls.Add(this.btnRechercher);
            this.panelFiltres.Controls.Add(this.cmbType);
            this.panelFiltres.Controls.Add(this.label2);
            this.panelFiltres.Controls.Add(this.cmbCouleur);
            this.panelFiltres.Controls.Add(this.label1);
            this.panelFiltres.Controls.Add(this.txtNom);
            this.panelFiltres.Controls.Add(this.lbl1);
            this.panelFiltres.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFiltres.Location = new System.Drawing.Point(10, 10);
            this.panelFiltres.Name = "panelFiltres";
            this.panelFiltres.Size = new System.Drawing.Size(1858, 180);
            this.panelFiltres.TabIndex = 0;
            // 
            // res
            // 
            this.res.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.res.Font = new System.Drawing.Font("Segoe UI", 20F);
            this.res.ForeColor = System.Drawing.SystemColors.Info;
            this.res.Location = new System.Drawing.Point(1585, 40);
            this.res.Name = "res";
            this.res.Size = new System.Drawing.Size(250, 76);
            this.res.TabIndex = 8;
            this.res.Text = "🔄 Réinitialiser";
            this.res.UseVisualStyleBackColor = false;
            this.res.Click += new System.EventHandler(this.res_Click);
            // 
            // Espece_Trouver
            // 
            this.Espece_Trouver.AutoSize = true;
            this.Espece_Trouver.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Italic);
            this.Espece_Trouver.ForeColor = System.Drawing.SystemColors.Info;
            this.Espece_Trouver.Location = new System.Drawing.Point(32, 140);
            this.Espece_Trouver.Name = "Espece_Trouver";
            this.Espece_Trouver.Size = new System.Drawing.Size(166, 28);
            this.Espece_Trouver.TabIndex = 7;
            this.Espece_Trouver.Text = "Espèces trouvées :";
            // 
            // btnRechercher
            // 
            this.btnRechercher.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.btnRechercher.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRechercher.Font = new System.Drawing.Font("Segoe UI", 25F);
            this.btnRechercher.ForeColor = System.Drawing.SystemColors.Info;
            this.btnRechercher.Location = new System.Drawing.Point(1443, 49);
            this.btnRechercher.Name = "btnRechercher";
            this.btnRechercher.Size = new System.Drawing.Size(97, 55);
            this.btnRechercher.TabIndex = 6;
            this.btnRechercher.Text = "OK";
            this.btnRechercher.UseVisualStyleBackColor = false;
            this.btnRechercher.Click += new System.EventHandler(this.btnRechercher_Click);
            // 
            // cmbType
            // 
            this.cmbType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.cmbType.ForeColor = System.Drawing.SystemColors.Info;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(1001, 65);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(200, 31);
            this.cmbType.TabIndex = 5;
            this.cmbType.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbType_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.label2.ForeColor = System.Drawing.SystemColors.Info;
            this.label2.Location = new System.Drawing.Point(907, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 32);
            this.label2.TabIndex = 4;
            this.label2.Text = "Type :";
            // 
            // cmbCouleur
            // 
            this.cmbCouleur.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
            this.cmbCouleur.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCouleur.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.cmbCouleur.ForeColor = System.Drawing.SystemColors.Info;
            this.cmbCouleur.FormattingEnabled = true;
            this.cmbCouleur.Location = new System.Drawing.Point(682, 66);
            this.cmbCouleur.Name = "cmbCouleur";
            this.cmbCouleur.Size = new System.Drawing.Size(200, 31);
            this.cmbCouleur.TabIndex = 3;
            this.cmbCouleur.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbCouleur_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.label1.ForeColor = System.Drawing.SystemColors.Info;
            this.label1.Location = new System.Drawing.Point(545, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 32);
            this.label1.TabIndex = 2;
            this.label1.Text = "Couleur :";
            // 
            // txtNom
            // 
            this.txtNom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
            this.txtNom.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.txtNom.ForeColor = System.Drawing.SystemColors.Info;
            this.txtNom.Location = new System.Drawing.Point(331, 64);
            this.txtNom.Name = "txtNom";
            this.txtNom.Size = new System.Drawing.Size(190, 31);
            this.txtNom.TabIndex = 1;
            this.txtNom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNom_KeyDown);
            this.txtNom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNom_KeyPress);
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.lbl1.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.lbl1.ForeColor = System.Drawing.SystemColors.Info;
            this.lbl1.Location = new System.Drawing.Point(155, 64);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(137, 32);
            this.lbl1.TabIndex = 0;
            this.lbl1.Text = "Nom (like) :";
            // 
            // flpRaces
            // 
            this.flpRaces.AutoScroll = true;
            this.flpRaces.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpRaces.Location = new System.Drawing.Point(10, 190);
            this.flpRaces.Name = "flpRaces";
            this.flpRaces.Padding = new System.Windows.Forms.Padding(10, 10, 10, 50);
            this.flpRaces.Size = new System.Drawing.Size(1858, 719);
            this.flpRaces.TabIndex = 0;
            // 
            // btnRetour
            // 
            this.btnRetour.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.btnRetour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRetour.Font = new System.Drawing.Font("Segoe UI", 25F);
            this.btnRetour.ForeColor = System.Drawing.SystemColors.Info;
            this.btnRetour.Location = new System.Drawing.Point(238, 6);
            this.btnRetour.Name = "btnRetour";
            this.btnRetour.Size = new System.Drawing.Size(417, 50);
            this.btnRetour.TabIndex = 0;
            this.btnRetour.Text = "← Retour";
            this.btnRetour.UseVisualStyleBackColor = false;
            this.btnRetour.Click += new System.EventHandler(this.btnRetour_Click_1);
            // 
            // panelBas
            // 
            this.panelBas.Controls.Add(this.btnRetour);
            this.panelBas.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBas.Location = new System.Drawing.Point(10, 909);
            this.panelBas.Name = "panelBas";
            this.panelBas.Size = new System.Drawing.Size(1858, 62);
            this.panelBas.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::projetStarGate.Properties.Resources.Logo_Stargate1;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(146, 134);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // frmRaces
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.ClientSize = new System.Drawing.Size(1878, 971);
            this.Controls.Add(this.flpRaces);
            this.Controls.Add(this.panelBas);
            this.Controls.Add(this.panelFiltres);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRaces";
            this.Padding = new System.Windows.Forms.Padding(10, 10, 10, 0);
            this.Text = "frmRaces";
            this.Load += new System.EventHandler(this.frmRaces_Load);
            this.panelFiltres.ResumeLayout(false);
            this.panelFiltres.PerformLayout();
            this.panelBas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelFiltres;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbCouleur;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNom;
        private System.Windows.Forms.Button btnRechercher;
        private System.Windows.Forms.FlowLayoutPanel flpRaces;
        private System.Windows.Forms.Label Espece_Trouver;
        private System.Windows.Forms.Button res;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnRetour;
        private System.Windows.Forms.Panel panelBas;
    }
}