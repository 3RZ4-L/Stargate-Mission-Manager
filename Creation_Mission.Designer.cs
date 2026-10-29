using System.Windows.Forms;

namespace projetStarGate
{
    partial class Creation_Mission
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
            this.components = new System.ComponentModel.Container();
            this.lblEtape2 = new System.Windows.Forms.Label();
            this.cboChef = new System.Windows.Forms.ComboBox();
            this.grpEtape2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFeuille = new System.Windows.Forms.RichTextBox();
            this.txtBudget = new System.Windows.Forms.TextBox();
            this.txtDataBaz = new System.Windows.Forms.TextBox();
            this.txtEquipage = new System.Windows.Forms.TextBox();
            this.lblBudget = new System.Windows.Forms.Label();
            this.lblDataBaz = new System.Windows.Forms.Label();
            this.lblEquipage = new System.Windows.Forms.Label();
            this.lblFeuille = new System.Windows.Forms.Label();
            this.lblEtape3 = new System.Windows.Forms.Label();
            this.btnValiderEtape1 = new System.Windows.Forms.Button();
            this.grpEtape3 = new System.Windows.Forms.GroupBox();
            this.btnValider = new System.Windows.Forms.Button();
            this.btnSuppriemrAlien = new System.Windows.Forms.Button();
            this.flpCaptures = new System.Windows.Forms.FlowLayoutPanel();
            this.lblNBAlien = new System.Windows.Forms.Label();
            this.txtObjectif = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboFiltreMembres = new System.Windows.Forms.ComboBox();
            this.flpMembres = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSuppriemrMembre = new System.Windows.Forms.Button();
            this.btnAjouterCapture = new System.Windows.Forms.Button();
            this.cboCapture = new System.Windows.Forms.ComboBox();
            this.lblObjectifs = new System.Windows.Forms.Label();
            this.btnAjouterMembre = new System.Windows.Forms.Button();
            this.cboMembres = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.lblSucces = new System.Windows.Forms.Label();
            this.errNBMembres = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnRetour = new System.Windows.Forms.Button();
            this.errDate = new System.Windows.Forms.ErrorProvider(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.grpEtape1 = new System.Windows.Forms.GroupBox();
            this.btnValiderPlanete = new System.Windows.Forms.Button();
            this.lblNomMission = new System.Windows.Forms.Label();
            this.cboPlanete = new System.Windows.Forms.ComboBox();
            this.lblEtape1 = new System.Windows.Forms.Label();
            this.dtpRetour = new System.Windows.Forms.DateTimePicker();
            this.dtpDepart = new System.Windows.Forms.DateTimePicker();
            this.lblDepart = new System.Windows.Forms.Label();
            this.lblRetour = new System.Windows.Forms.Label();
            this.errBudget = new System.Windows.Forms.ErrorProvider(this.components);
            this.errPage1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.grpEtape2.SuspendLayout();
            this.grpEtape3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errNBMembres)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.grpEtape1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errBudget)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errPage1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblEtape2
            // 
            this.lblEtape2.AutoSize = true;
            this.lblEtape2.Font = new System.Drawing.Font("Rubik", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtape2.ForeColor = System.Drawing.SystemColors.Info;
            this.lblEtape2.Location = new System.Drawing.Point(32, 53);
            this.lblEtape2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblEtape2.Name = "lblEtape2";
            this.lblEtape2.Size = new System.Drawing.Size(253, 28);
            this.lblEtape2.TabIndex = 1;
            this.lblEtape2.Text = "2 - Chef de la mission";
            // 
            // cboChef
            // 
            this.cboChef.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.cboChef.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboChef.ForeColor = System.Drawing.SystemColors.Info;
            this.cboChef.FormattingEnabled = true;
            this.cboChef.Location = new System.Drawing.Point(303, 53);
            this.cboChef.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.cboChef.Name = "cboChef";
            this.cboChef.Size = new System.Drawing.Size(274, 27);
            this.cboChef.TabIndex = 4;
            this.cboChef.SelectedIndexChanged += new System.EventHandler(this.cboChef_SelectedIndexChanged);
            this.cboChef.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbo_KeyPress);
            // 
            // grpEtape2
            // 
            this.grpEtape2.BackColor = System.Drawing.Color.Transparent;
            this.grpEtape2.Controls.Add(this.cboChef);
            this.grpEtape2.Controls.Add(this.label3);
            this.grpEtape2.Controls.Add(this.lblEtape2);
            this.grpEtape2.Controls.Add(this.label2);
            this.grpEtape2.Controls.Add(this.label1);
            this.grpEtape2.Controls.Add(this.txtFeuille);
            this.grpEtape2.Controls.Add(this.txtBudget);
            this.grpEtape2.Controls.Add(this.txtDataBaz);
            this.grpEtape2.Controls.Add(this.txtEquipage);
            this.grpEtape2.Controls.Add(this.lblBudget);
            this.grpEtape2.Controls.Add(this.lblDataBaz);
            this.grpEtape2.Controls.Add(this.lblEquipage);
            this.grpEtape2.Controls.Add(this.lblFeuille);
            this.grpEtape2.Controls.Add(this.lblEtape3);
            this.grpEtape2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grpEtape2.ForeColor = System.Drawing.SystemColors.Info;
            this.grpEtape2.Location = new System.Drawing.Point(-1, 405);
            this.grpEtape2.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.grpEtape2.Name = "grpEtape2";
            this.grpEtape2.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.grpEtape2.Size = new System.Drawing.Size(848, 668);
            this.grpEtape2.TabIndex = 23;
            this.grpEtape2.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Rubik", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.Info;
            this.label3.Location = new System.Drawing.Point(273, 596);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 23);
            this.label3.TabIndex = 21;
            this.label3.Text = "€";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Rubik", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Info;
            this.label2.Location = new System.Drawing.Point(273, 555);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 23);
            this.label2.TabIndex = 20;
            this.label2.Text = "tonnes";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Rubik", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Info;
            this.label1.Location = new System.Drawing.Point(273, 515);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 23);
            this.label1.TabIndex = 19;
            this.label1.Text = "personnes";
            // 
            // txtFeuille
            // 
            this.txtFeuille.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.txtFeuille.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFeuille.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFeuille.ForeColor = System.Drawing.SystemColors.Info;
            this.txtFeuille.Location = new System.Drawing.Point(163, 212);
            this.txtFeuille.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtFeuille.Name = "txtFeuille";
            this.txtFeuille.Size = new System.Drawing.Size(657, 255);
            this.txtFeuille.TabIndex = 17;
            this.txtFeuille.Text = "";
            // 
            // txtBudget
            // 
            this.txtBudget.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.txtBudget.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBudget.ForeColor = System.Drawing.SystemColors.Info;
            this.txtBudget.Location = new System.Drawing.Point(197, 596);
            this.txtBudget.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtBudget.Name = "txtBudget";
            this.txtBudget.ShortcutsEnabled = false;
            this.txtBudget.Size = new System.Drawing.Size(76, 29);
            this.txtBudget.TabIndex = 16;
            this.txtBudget.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Infos_KeyPress);
            // 
            // txtDataBaz
            // 
            this.txtDataBaz.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.txtDataBaz.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataBaz.ForeColor = System.Drawing.SystemColors.Info;
            this.txtDataBaz.Location = new System.Drawing.Point(197, 555);
            this.txtDataBaz.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtDataBaz.Name = "txtDataBaz";
            this.txtDataBaz.ShortcutsEnabled = false;
            this.txtDataBaz.Size = new System.Drawing.Size(76, 29);
            this.txtDataBaz.TabIndex = 15;
            this.txtDataBaz.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Infos_KeyPress);
            // 
            // txtEquipage
            // 
            this.txtEquipage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.txtEquipage.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEquipage.ForeColor = System.Drawing.SystemColors.Info;
            this.txtEquipage.Location = new System.Drawing.Point(197, 515);
            this.txtEquipage.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtEquipage.Name = "txtEquipage";
            this.txtEquipage.ShortcutsEnabled = false;
            this.txtEquipage.Size = new System.Drawing.Size(76, 29);
            this.txtEquipage.TabIndex = 14;
            this.txtEquipage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Infos_KeyPress);
            // 
            // lblBudget
            // 
            this.lblBudget.AutoSize = true;
            this.lblBudget.Font = new System.Drawing.Font("Rubik", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBudget.ForeColor = System.Drawing.SystemColors.Info;
            this.lblBudget.Location = new System.Drawing.Point(103, 596);
            this.lblBudget.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblBudget.Name = "lblBudget";
            this.lblBudget.Size = new System.Drawing.Size(75, 23);
            this.lblBudget.TabIndex = 12;
            this.lblBudget.Text = "Budget";
            // 
            // lblDataBaz
            // 
            this.lblDataBaz.AutoSize = true;
            this.lblDataBaz.Font = new System.Drawing.Font("Rubik", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataBaz.ForeColor = System.Drawing.SystemColors.Info;
            this.lblDataBaz.Location = new System.Drawing.Point(33, 554);
            this.lblDataBaz.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblDataBaz.Name = "lblDataBaz";
            this.lblDataBaz.Size = new System.Drawing.Size(160, 23);
            this.lblDataBaz.TabIndex = 11;
            this.lblDataBaz.Text = "Objectif DataBaz";
            // 
            // lblEquipage
            // 
            this.lblEquipage.AutoSize = true;
            this.lblEquipage.Font = new System.Drawing.Font("Rubik", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEquipage.ForeColor = System.Drawing.SystemColors.Info;
            this.lblEquipage.Location = new System.Drawing.Point(96, 514);
            this.lblEquipage.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblEquipage.Name = "lblEquipage";
            this.lblEquipage.Size = new System.Drawing.Size(91, 23);
            this.lblEquipage.TabIndex = 10;
            this.lblEquipage.Text = "Equipage";
            // 
            // lblFeuille
            // 
            this.lblFeuille.AutoSize = true;
            this.lblFeuille.Font = new System.Drawing.Font("Rubik", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFeuille.ForeColor = System.Drawing.SystemColors.Info;
            this.lblFeuille.Location = new System.Drawing.Point(38, 185);
            this.lblFeuille.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblFeuille.Name = "lblFeuille";
            this.lblFeuille.Size = new System.Drawing.Size(149, 23);
            this.lblFeuille.TabIndex = 9;
            this.lblFeuille.Text = "Feuille de route";
            // 
            // lblEtape3
            // 
            this.lblEtape3.AutoSize = true;
            this.lblEtape3.Font = new System.Drawing.Font("Rubik", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtape3.ForeColor = System.Drawing.SystemColors.Info;
            this.lblEtape3.Location = new System.Drawing.Point(24, 127);
            this.lblEtape3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblEtape3.Name = "lblEtape3";
            this.lblEtape3.Size = new System.Drawing.Size(328, 28);
            this.lblEtape3.TabIndex = 2;
            this.lblEtape3.Text = "3 - Paramêtres de la mission";
            // 
            // btnValiderEtape1
            // 
            this.btnValiderEtape1.BackColor = System.Drawing.SystemColors.Info;
            this.btnValiderEtape1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnValiderEtape1.Font = new System.Drawing.Font("Impact", 18F);
            this.btnValiderEtape1.ForeColor = System.Drawing.Color.DimGray;
            this.btnValiderEtape1.Location = new System.Drawing.Point(724, 55);
            this.btnValiderEtape1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnValiderEtape1.Name = "btnValiderEtape1";
            this.btnValiderEtape1.Size = new System.Drawing.Size(106, 41);
            this.btnValiderEtape1.TabIndex = 18;
            this.btnValiderEtape1.Text = "Suivant";
            this.btnValiderEtape1.UseVisualStyleBackColor = false;
            this.btnValiderEtape1.Click += new System.EventHandler(this.btnValiderEtape1_Click);
            // 
            // grpEtape3
            // 
            this.grpEtape3.BackColor = System.Drawing.Color.Transparent;
            this.grpEtape3.Controls.Add(this.btnValider);
            this.grpEtape3.Controls.Add(this.btnSuppriemrAlien);
            this.grpEtape3.Controls.Add(this.flpCaptures);
            this.grpEtape3.Controls.Add(this.lblNBAlien);
            this.grpEtape3.Controls.Add(this.txtObjectif);
            this.grpEtape3.Controls.Add(this.label5);
            this.grpEtape3.Controls.Add(this.cboFiltreMembres);
            this.grpEtape3.Controls.Add(this.flpMembres);
            this.grpEtape3.Controls.Add(this.btnSuppriemrMembre);
            this.grpEtape3.Controls.Add(this.btnAjouterCapture);
            this.grpEtape3.Controls.Add(this.cboCapture);
            this.grpEtape3.Controls.Add(this.lblObjectifs);
            this.grpEtape3.Controls.Add(this.btnAjouterMembre);
            this.grpEtape3.Controls.Add(this.cboMembres);
            this.grpEtape3.Controls.Add(this.label13);
            this.grpEtape3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grpEtape3.ForeColor = System.Drawing.SystemColors.Info;
            this.grpEtape3.Location = new System.Drawing.Point(-4, 109);
            this.grpEtape3.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.grpEtape3.Name = "grpEtape3";
            this.grpEtape3.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.grpEtape3.Size = new System.Drawing.Size(849, 964);
            this.grpEtape3.TabIndex = 25;
            this.grpEtape3.TabStop = false;
            // 
            // btnValider
            // 
            this.btnValider.BackColor = System.Drawing.SystemColors.Info;
            this.btnValider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnValider.Font = new System.Drawing.Font("Impact", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnValider.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.btnValider.Location = new System.Drawing.Point(317, 848);
            this.btnValider.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnValider.Name = "btnValider";
            this.btnValider.Size = new System.Drawing.Size(196, 65);
            this.btnValider.TabIndex = 26;
            this.btnValider.Text = "Valider";
            this.btnValider.UseVisualStyleBackColor = false;
            this.btnValider.Click += new System.EventHandler(this.btnValider_Click);
            // 
            // btnSuppriemrAlien
            // 
            this.btnSuppriemrAlien.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.btnSuppriemrAlien.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSuppriemrAlien.Font = new System.Drawing.Font("Rubik", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSuppriemrAlien.ForeColor = System.Drawing.Color.Cornsilk;
            this.btnSuppriemrAlien.Location = new System.Drawing.Point(678, 795);
            this.btnSuppriemrAlien.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnSuppriemrAlien.Name = "btnSuppriemrAlien";
            this.btnSuppriemrAlien.Size = new System.Drawing.Size(111, 38);
            this.btnSuppriemrAlien.TabIndex = 39;
            this.btnSuppriemrAlien.Text = "Supprimer";
            this.btnSuppriemrAlien.UseVisualStyleBackColor = false;
            this.btnSuppriemrAlien.Click += new System.EventHandler(this.btnSuppriemrAlien_Click);
            // 
            // flpCaptures
            // 
            this.flpCaptures.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.flpCaptures.AutoScroll = true;
            this.flpCaptures.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.flpCaptures.Location = new System.Drawing.Point(51, 525);
            this.flpCaptures.Name = "flpCaptures";
            this.flpCaptures.Size = new System.Drawing.Size(740, 263);
            this.flpCaptures.TabIndex = 37;
            this.flpCaptures.WrapContents = false;
            // 
            // lblNBAlien
            // 
            this.lblNBAlien.AutoSize = true;
            this.lblNBAlien.Font = new System.Drawing.Font("Rubik", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNBAlien.Location = new System.Drawing.Point(364, 490);
            this.lblNBAlien.Name = "lblNBAlien";
            this.lblNBAlien.Size = new System.Drawing.Size(89, 23);
            this.lblNBAlien.TabIndex = 36;
            this.lblNBAlien.Text = "objectif :";
            // 
            // txtObjectif
            // 
            this.txtObjectif.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.txtObjectif.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtObjectif.ForeColor = System.Drawing.SystemColors.Info;
            this.txtObjectif.Location = new System.Drawing.Point(460, 486);
            this.txtObjectif.Margin = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this.txtObjectif.Name = "txtObjectif";
            this.txtObjectif.ShortcutsEnabled = false;
            this.txtObjectif.Size = new System.Drawing.Size(87, 29);
            this.txtObjectif.TabIndex = 35;
            this.txtObjectif.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Infos_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Rubik", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(390, 76);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 23);
            this.label5.TabIndex = 33;
            this.label5.Text = "flitre :";
            // 
            // cboFiltreMembres
            // 
            this.cboFiltreMembres.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.cboFiltreMembres.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboFiltreMembres.ForeColor = System.Drawing.SystemColors.Info;
            this.cboFiltreMembres.FormattingEnabled = true;
            this.cboFiltreMembres.Location = new System.Drawing.Point(460, 74);
            this.cboFiltreMembres.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.cboFiltreMembres.Name = "cboFiltreMembres";
            this.cboFiltreMembres.Size = new System.Drawing.Size(162, 27);
            this.cboFiltreMembres.TabIndex = 32;
            this.cboFiltreMembres.SelectedIndexChanged += new System.EventHandler(this.cboFiltreMembres_SelectedIndexChanged);
            this.cboFiltreMembres.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbo_KeyPress);
            // 
            // flpMembres
            // 
            this.flpMembres.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.flpMembres.AutoScroll = true;
            this.flpMembres.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.flpMembres.Location = new System.Drawing.Point(49, 109);
            this.flpMembres.Name = "flpMembres";
            this.flpMembres.Size = new System.Drawing.Size(740, 262);
            this.flpMembres.TabIndex = 27;
            this.flpMembres.WrapContents = false;
            // 
            // btnSuppriemrMembre
            // 
            this.btnSuppriemrMembre.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.btnSuppriemrMembre.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSuppriemrMembre.Font = new System.Drawing.Font("Rubik", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSuppriemrMembre.ForeColor = System.Drawing.Color.Cornsilk;
            this.btnSuppriemrMembre.Location = new System.Drawing.Point(679, 379);
            this.btnSuppriemrMembre.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnSuppriemrMembre.Name = "btnSuppriemrMembre";
            this.btnSuppriemrMembre.Size = new System.Drawing.Size(111, 38);
            this.btnSuppriemrMembre.TabIndex = 7;
            this.btnSuppriemrMembre.Text = "Supprimer";
            this.btnSuppriemrMembre.UseVisualStyleBackColor = false;
            this.btnSuppriemrMembre.Click += new System.EventHandler(this.btnSupprimerMembre_Click);
            // 
            // btnAjouterCapture
            // 
            this.btnAjouterCapture.BackColor = System.Drawing.Color.Gray;
            this.btnAjouterCapture.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAjouterCapture.Font = new System.Drawing.Font("Rubik", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAjouterCapture.ForeColor = System.Drawing.SystemColors.Info;
            this.btnAjouterCapture.Location = new System.Drawing.Point(678, 482);
            this.btnAjouterCapture.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnAjouterCapture.Name = "btnAjouterCapture";
            this.btnAjouterCapture.Size = new System.Drawing.Size(111, 38);
            this.btnAjouterCapture.TabIndex = 25;
            this.btnAjouterCapture.Text = "Ajouter";
            this.btnAjouterCapture.UseVisualStyleBackColor = false;
            this.btnAjouterCapture.Click += new System.EventHandler(this.btnAjouterCapture_Click);
            // 
            // cboCapture
            // 
            this.cboCapture.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.cboCapture.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboCapture.ForeColor = System.Drawing.SystemColors.Info;
            this.cboCapture.FormattingEnabled = true;
            this.cboCapture.Location = new System.Drawing.Point(51, 491);
            this.cboCapture.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.cboCapture.Name = "cboCapture";
            this.cboCapture.Size = new System.Drawing.Size(274, 27);
            this.cboCapture.TabIndex = 8;
            this.cboCapture.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbo_KeyPress);
            // 
            // lblObjectifs
            // 
            this.lblObjectifs.AutoSize = true;
            this.lblObjectifs.Font = new System.Drawing.Font("Rubik", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblObjectifs.ForeColor = System.Drawing.SystemColors.Info;
            this.lblObjectifs.Location = new System.Drawing.Point(22, 447);
            this.lblObjectifs.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblObjectifs.Name = "lblObjectifs";
            this.lblObjectifs.Size = new System.Drawing.Size(274, 28);
            this.lblObjectifs.TabIndex = 23;
            this.lblObjectifs.Text = "5 - Objectifs de capture";
            // 
            // btnAjouterMembre
            // 
            this.btnAjouterMembre.BackColor = System.Drawing.Color.Gray;
            this.btnAjouterMembre.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAjouterMembre.Font = new System.Drawing.Font("Rubik", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAjouterMembre.ForeColor = System.Drawing.SystemColors.Info;
            this.btnAjouterMembre.Location = new System.Drawing.Point(679, 65);
            this.btnAjouterMembre.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnAjouterMembre.Name = "btnAjouterMembre";
            this.btnAjouterMembre.Size = new System.Drawing.Size(111, 38);
            this.btnAjouterMembre.TabIndex = 9;
            this.btnAjouterMembre.Text = "Ajouter";
            this.btnAjouterMembre.UseVisualStyleBackColor = false;
            this.btnAjouterMembre.Click += new System.EventHandler(this.btnAjouterMembre_Click);
            // 
            // cboMembres
            // 
            this.cboMembres.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.cboMembres.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboMembres.ForeColor = System.Drawing.SystemColors.Info;
            this.cboMembres.FormattingEnabled = true;
            this.cboMembres.Location = new System.Drawing.Point(50, 76);
            this.cboMembres.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.cboMembres.Name = "cboMembres";
            this.cboMembres.Size = new System.Drawing.Size(253, 27);
            this.cboMembres.TabIndex = 8;
            this.cboMembres.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbo_KeyPress);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Rubik", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.SystemColors.Info;
            this.label13.Location = new System.Drawing.Point(22, 32);
            this.label13.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(332, 28);
            this.label13.TabIndex = 2;
            this.label13.Text = "4 - Affectation des membres";
            // 
            // lblSucces
            // 
            this.lblSucces.AutoSize = true;
            this.lblSucces.BackColor = System.Drawing.Color.Transparent;
            this.lblSucces.Font = new System.Drawing.Font("Rubik", 71.99999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSucces.ForeColor = System.Drawing.SystemColors.Info;
            this.lblSucces.Location = new System.Drawing.Point(-10, 457);
            this.lblSucces.Name = "lblSucces";
            this.lblSucces.Size = new System.Drawing.Size(868, 114);
            this.lblSucces.TabIndex = 29;
            this.lblSucces.Text = "Création Réussie";
            this.lblSucces.Visible = false;
            // 
            // errNBMembres
            // 
            this.errNBMembres.ContainerControl = this;
            // 
            // btnRetour
            // 
            this.btnRetour.BackColor = System.Drawing.SystemColors.Info;
            this.btnRetour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRetour.Font = new System.Drawing.Font("Impact", 18F);
            this.btnRetour.ForeColor = System.Drawing.Color.DimGray;
            this.btnRetour.Location = new System.Drawing.Point(14, 55);
            this.btnRetour.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnRetour.Name = "btnRetour";
            this.btnRetour.Size = new System.Drawing.Size(106, 41);
            this.btnRetour.TabIndex = 27;
            this.btnRetour.Text = "Retour";
            this.btnRetour.UseVisualStyleBackColor = false;
            this.btnRetour.Click += new System.EventHandler(this.btnRetour_Click);
            // 
            // errDate
            // 
            this.errDate.ContainerControl = this;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::projetStarGate.Properties.Resources.Logo_Stargate1;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(353, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(132, 115);
            this.pictureBox1.TabIndex = 28;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // grpEtape1
            // 
            this.grpEtape1.BackColor = System.Drawing.Color.Transparent;
            this.grpEtape1.Controls.Add(this.btnValiderPlanete);
            this.grpEtape1.Controls.Add(this.lblNomMission);
            this.grpEtape1.Controls.Add(this.cboPlanete);
            this.grpEtape1.Controls.Add(this.lblEtape1);
            this.grpEtape1.Controls.Add(this.dtpRetour);
            this.grpEtape1.Controls.Add(this.dtpDepart);
            this.grpEtape1.Controls.Add(this.lblDepart);
            this.grpEtape1.Controls.Add(this.lblRetour);
            this.grpEtape1.Font = new System.Drawing.Font("Rubik", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpEtape1.ForeColor = System.Drawing.SystemColors.Info;
            this.grpEtape1.Location = new System.Drawing.Point(-3, 104);
            this.grpEtape1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.grpEtape1.Name = "grpEtape1";
            this.grpEtape1.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.grpEtape1.Size = new System.Drawing.Size(851, 319);
            this.grpEtape1.TabIndex = 0;
            this.grpEtape1.TabStop = false;
            this.grpEtape1.Text = "Nouvelle mission";
            // 
            // btnValiderPlanete
            // 
            this.btnValiderPlanete.BackColor = System.Drawing.SystemColors.Info;
            this.btnValiderPlanete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnValiderPlanete.Font = new System.Drawing.Font("Impact", 18F);
            this.btnValiderPlanete.ForeColor = System.Drawing.Color.DimGray;
            this.btnValiderPlanete.Location = new System.Drawing.Point(636, 224);
            this.btnValiderPlanete.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnValiderPlanete.Name = "btnValiderPlanete";
            this.btnValiderPlanete.Size = new System.Drawing.Size(112, 41);
            this.btnValiderPlanete.TabIndex = 5;
            this.btnValiderPlanete.Text = "Valider";
            this.btnValiderPlanete.UseVisualStyleBackColor = false;
            this.btnValiderPlanete.Click += new System.EventHandler(this.btnValiderPlanete_Click);
            // 
            // lblNomMission
            // 
            this.lblNomMission.AutoSize = true;
            this.lblNomMission.Font = new System.Drawing.Font("Rubik", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNomMission.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.lblNomMission.Location = new System.Drawing.Point(161, 112);
            this.lblNomMission.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblNomMission.Name = "lblNomMission";
            this.lblNomMission.Size = new System.Drawing.Size(209, 25);
            this.lblNomMission.TabIndex = 6;
            this.lblNomMission.Text = "Nom de la mission :";
            // 
            // cboPlanete
            // 
            this.cboPlanete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.cboPlanete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboPlanete.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboPlanete.ForeColor = System.Drawing.SystemColors.Info;
            this.cboPlanete.FormattingEnabled = true;
            this.cboPlanete.Location = new System.Drawing.Point(343, 57);
            this.cboPlanete.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.cboPlanete.Name = "cboPlanete";
            this.cboPlanete.Size = new System.Drawing.Size(172, 28);
            this.cboPlanete.TabIndex = 3;
            this.cboPlanete.SelectedIndexChanged += new System.EventHandler(this.cboPlanete_SelectedIndexChanged);
            this.cboPlanete.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbo_KeyPress);
            // 
            // lblEtape1
            // 
            this.lblEtape1.AutoSize = true;
            this.lblEtape1.Font = new System.Drawing.Font("Rubik", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtape1.ForeColor = System.Drawing.SystemColors.Info;
            this.lblEtape1.Location = new System.Drawing.Point(35, 57);
            this.lblEtape1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblEtape1.Name = "lblEtape1";
            this.lblEtape1.Size = new System.Drawing.Size(298, 28);
            this.lblEtape1.TabIndex = 0;
            this.lblEtape1.Text = "1 - Planête de destination";
            // 
            // dtpRetour
            // 
            this.dtpRetour.CalendarFont = new System.Drawing.Font("Rubik", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpRetour.CalendarForeColor = System.Drawing.SystemColors.Info;
            this.dtpRetour.CalendarMonthBackground = System.Drawing.Color.Gray;
            this.dtpRetour.CalendarTitleBackColor = System.Drawing.Color.DodgerBlue;
            this.dtpRetour.CalendarTitleForeColor = System.Drawing.SystemColors.Info;
            this.dtpRetour.CalendarTrailingForeColor = System.Drawing.SystemColors.Info;
            this.dtpRetour.CustomFormat = "yyyy-MM-dd";
            this.dtpRetour.Font = new System.Drawing.Font("Rubik", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpRetour.Location = new System.Drawing.Point(251, 233);
            this.dtpRetour.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.dtpRetour.Name = "dtpRetour";
            this.dtpRetour.Size = new System.Drawing.Size(264, 26);
            this.dtpRetour.TabIndex = 13;
            // 
            // dtpDepart
            // 
            this.dtpDepart.CalendarFont = new System.Drawing.Font("Rubik", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDepart.CalendarForeColor = System.Drawing.SystemColors.Info;
            this.dtpDepart.CalendarMonthBackground = System.Drawing.Color.Gray;
            this.dtpDepart.CalendarTitleBackColor = System.Drawing.Color.DodgerBlue;
            this.dtpDepart.CalendarTitleForeColor = System.Drawing.SystemColors.Info;
            this.dtpDepart.CalendarTrailingForeColor = System.Drawing.SystemColors.Info;
            this.dtpDepart.CustomFormat = "yyyy-MM-dd";
            this.dtpDepart.Font = new System.Drawing.Font("Rubik", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDepart.Location = new System.Drawing.Point(252, 178);
            this.dtpDepart.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.dtpDepart.Name = "dtpDepart";
            this.dtpDepart.Size = new System.Drawing.Size(263, 26);
            this.dtpDepart.TabIndex = 6;
            // 
            // lblDepart
            // 
            this.lblDepart.AutoSize = true;
            this.lblDepart.Font = new System.Drawing.Font("Rubik", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDepart.ForeColor = System.Drawing.SystemColors.Info;
            this.lblDepart.Location = new System.Drawing.Point(121, 184);
            this.lblDepart.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblDepart.Name = "lblDepart";
            this.lblDepart.Size = new System.Drawing.Size(121, 19);
            this.lblDepart.TabIndex = 7;
            this.lblDepart.Text = "Date de départ";
            // 
            // lblRetour
            // 
            this.lblRetour.AutoSize = true;
            this.lblRetour.Font = new System.Drawing.Font("Rubik", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRetour.ForeColor = System.Drawing.SystemColors.Info;
            this.lblRetour.Location = new System.Drawing.Point(120, 239);
            this.lblRetour.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblRetour.Name = "lblRetour";
            this.lblRetour.Size = new System.Drawing.Size(117, 19);
            this.lblRetour.TabIndex = 8;
            this.lblRetour.Text = "Date de retour";
            // 
            // errBudget
            // 
            this.errBudget.ContainerControl = this;
            // 
            // errPage1
            // 
            this.errPage1.ContainerControl = this;
            // 
            // Creation_Mission
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(58)))));
            this.BackgroundImage = global::projetStarGate.Properties.Resources.Logo_Stargate2;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(844, 1041);
            this.Controls.Add(this.lblSucces);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnRetour);
            this.Controls.Add(this.grpEtape3);
            this.Controls.Add(this.btnValiderEtape1);
            this.Controls.Add(this.grpEtape2);
            this.Controls.Add(this.grpEtape1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Rubik", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(860, 1080);
            this.MinimumSize = new System.Drawing.Size(860, 1068);
            this.Name = "Creation_Mission";
            this.Text = "Création d\'une nouvelle mission";
            this.Load += new System.EventHandler(this.Creation_Mission_Load);
            this.grpEtape2.ResumeLayout(false);
            this.grpEtape2.PerformLayout();
            this.grpEtape3.ResumeLayout(false);
            this.grpEtape3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errNBMembres)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.grpEtape1.ResumeLayout(false);
            this.grpEtape1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errBudget)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errPage1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblEtape1;
        private System.Windows.Forms.Button btnValiderPlanete;
        private System.Windows.Forms.GroupBox grpEtape1;
        private System.Windows.Forms.Label lblEtape2;
        private System.Windows.Forms.ComboBox cboChef;
        private System.Windows.Forms.GroupBox grpEtape2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnValiderEtape1;
        private System.Windows.Forms.RichTextBox txtFeuille;
        private System.Windows.Forms.TextBox txtBudget;
        private System.Windows.Forms.TextBox txtDataBaz;
        private System.Windows.Forms.TextBox txtEquipage;
        private System.Windows.Forms.DateTimePicker dtpRetour;
        private System.Windows.Forms.Label lblBudget;
        private System.Windows.Forms.Label lblDataBaz;
        private System.Windows.Forms.Label lblEquipage;
        private System.Windows.Forms.Label lblFeuille;
        private System.Windows.Forms.Label lblRetour;
        private System.Windows.Forms.Label lblDepart;
        private System.Windows.Forms.DateTimePicker dtpDepart;
        private System.Windows.Forms.Label lblEtape3;
        private System.Windows.Forms.ComboBox cboPlanete;
        private System.Windows.Forms.Label lblNomMission;
        private GroupBox grpEtape3;
        private Label label13;
        private ComboBox cboMembres;
        private Button btnAjouterMembre;
        private Button btnSuppriemrMembre;
        private FlowLayoutPanel flpMembres;
        private Button btnValider;
        private Button btnAjouterCapture;
        private ComboBox cboCapture;
        private Label lblObjectifs;
        private Label label5;
        private ComboBox cboFiltreMembres;
        private TextBox txtObjectif;
        private Label lblNBAlien;
        private FlowLayoutPanel flpCaptures;
        private Button btnSuppriemrAlien;
        private ErrorProvider errNBMembres;
        private Button btnRetour;
        private PictureBox pictureBox1;
        private ErrorProvider errDate;
        private ErrorProvider errBudget;
        private Label lblSucces;
        private ErrorProvider errPage1;
    }
}