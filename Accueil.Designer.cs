namespace projetStarGate
{
    partial class frmAccueil
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnPlanetes = new System.Windows.Forms.Button();
            this.btnAliens = new System.Windows.Forms.Button();
            this.btnStats = new System.Windows.Forms.Button();
            this.btnCreationMission = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.groupBox1.Controls.Add(this.btnPlanetes);
            this.groupBox1.Controls.Add(this.btnAliens);
            this.groupBox1.Controls.Add(this.btnStats);
            this.groupBox1.Controls.Add(this.btnCreationMission);
            this.groupBox1.Location = new System.Drawing.Point(-2, -13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(608, 1116);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // btnPlanetes
            // 
            this.btnPlanetes.BackColor = System.Drawing.SystemColors.Info;
            this.btnPlanetes.Font = new System.Drawing.Font("Rubik", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlanetes.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.btnPlanetes.Location = new System.Drawing.Point(109, 732);
            this.btnPlanetes.Name = "btnPlanetes";
            this.btnPlanetes.Size = new System.Drawing.Size(393, 112);
            this.btnPlanetes.TabIndex = 7;
            this.btnPlanetes.Text = "Infos Planetes";
            this.btnPlanetes.UseVisualStyleBackColor = false;
            this.btnPlanetes.Click += new System.EventHandler(this.btnPlanetes_Click);
            // 
            // btnAliens
            // 
            this.btnAliens.BackColor = System.Drawing.SystemColors.Info;
            this.btnAliens.Font = new System.Drawing.Font("Rubik", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAliens.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.btnAliens.Location = new System.Drawing.Point(109, 557);
            this.btnAliens.Name = "btnAliens";
            this.btnAliens.Size = new System.Drawing.Size(393, 112);
            this.btnAliens.TabIndex = 6;
            this.btnAliens.Text = "Infos Aliens";
            this.btnAliens.UseVisualStyleBackColor = false;
            this.btnAliens.Click += new System.EventHandler(this.btnAliens_Click);
            // 
            // btnStats
            // 
            this.btnStats.BackColor = System.Drawing.SystemColors.Info;
            this.btnStats.Font = new System.Drawing.Font("Rubik", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStats.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.btnStats.Location = new System.Drawing.Point(109, 381);
            this.btnStats.Name = "btnStats";
            this.btnStats.Size = new System.Drawing.Size(393, 112);
            this.btnStats.TabIndex = 5;
            this.btnStats.Text = "les Statistiques";
            this.btnStats.UseVisualStyleBackColor = false;
            this.btnStats.Click += new System.EventHandler(this.btnStats_Click);
            // 
            // btnCreationMission
            // 
            this.btnCreationMission.BackColor = System.Drawing.SystemColors.Info;
            this.btnCreationMission.Font = new System.Drawing.Font("Rubik", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreationMission.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.btnCreationMission.Location = new System.Drawing.Point(109, 204);
            this.btnCreationMission.Name = "btnCreationMission";
            this.btnCreationMission.Size = new System.Drawing.Size(393, 112);
            this.btnCreationMission.TabIndex = 4;
            this.btnCreationMission.Text = "Nouvelle Mission";
            this.btnCreationMission.UseVisualStyleBackColor = false;
            this.btnCreationMission.Click += new System.EventHandler(this.btnCreationMission_Click);
            // 
            // frmAccueil
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(58)))));
            this.BackgroundImage = global::projetStarGate.Properties.Resources.Logo_Stargate2;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(1467, 1091);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "frmAccueil";
            this.Text = "Tableau de bord";
            this.Load += new System.EventHandler(this.frmAccueil_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnPlanetes;
        private System.Windows.Forms.Button btnAliens;
        private System.Windows.Forms.Button btnStats;
        private System.Windows.Forms.Button btnCreationMission;
    }
}

