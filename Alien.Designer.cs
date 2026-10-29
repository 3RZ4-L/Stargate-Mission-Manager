namespace projetStarGate
{
    partial class Alien
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblArme = new System.Windows.Forms.Label();
            this.lblNom = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblNB = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblArme
            // 
            this.lblArme.AutoSize = true;
            this.lblArme.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblArme.ForeColor = System.Drawing.SystemColors.Info;
            this.lblArme.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblArme.Location = new System.Drawing.Point(4, 203);
            this.lblArme.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblArme.Name = "lblArme";
            this.lblArme.Size = new System.Drawing.Size(62, 20);
            this.lblArme.TabIndex = 19;
            this.lblArme.Text = "lblArme";
            this.lblArme.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblArme.Click += new System.EventHandler(this.Alien_Click);
            // 
            // lblNom
            // 
            this.lblNom.AutoSize = true;
            this.lblNom.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNom.ForeColor = System.Drawing.SystemColors.Info;
            this.lblNom.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblNom.Location = new System.Drawing.Point(4, 179);
            this.lblNom.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNom.Name = "lblNom";
            this.lblNom.Size = new System.Drawing.Size(57, 20);
            this.lblNom.TabIndex = 18;
            this.lblNom.Text = "lblNom";
            this.lblNom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblNom.Click += new System.EventHandler(this.Alien_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(4, 4);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(153, 171);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 21;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.Alien_Click);
            // 
            // lblNB
            // 
            this.lblNB.AutoSize = true;
            this.lblNB.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.lblNB.ForeColor = System.Drawing.SystemColors.Info;
            this.lblNB.Location = new System.Drawing.Point(-6, 0);
            this.lblNB.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNB.Name = "lblNB";
            this.lblNB.Size = new System.Drawing.Size(41, 31);
            this.lblNB.TabIndex = 22;
            this.lblNB.Text = "lbl";
            this.lblNB.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblNB.Click += new System.EventHandler(this.Alien_Click);
            // 
            // Alien
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.lblNB);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblArme);
            this.Controls.Add(this.lblNom);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "Alien";
            this.Size = new System.Drawing.Size(163, 228);
            this.Tag = "0";
            this.Click += new System.EventHandler(this.Alien_Click);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblArme;
        private System.Windows.Forms.Label lblNom;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblNB;
    }
}
