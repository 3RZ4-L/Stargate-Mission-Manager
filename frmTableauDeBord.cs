using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace projetStarGate
{
    public partial class frmTableauDeBord : Form
    {
        // ============================================================
        // ATTRIBUTS DE LA FORME
        // ============================================================
        private string m_planet;
        private string m_numMission;
        private string m_matChef;
        private string m_numGlobal;

        private Label lblLoading;
        private bool loading;

        // ============================================================
        // CONSTRUCTEUR ET CHARGEMENT DE LA FORME
        // ============================================================

        // Initialise la fenêtre des détails d'une mission.
        // Les informations de mission sont reçues depuis l'accueil.
        public frmTableauDeBord(string planet, string numMission, string matChef, string numGlobal)
        {
            InitializeComponent();

            m_planet = planet;
            m_numMission = numMission;
            m_matChef = matChef;
            m_numGlobal = numGlobal;

            WindowState = FormWindowState.Maximized;

            CreateLoadingUI();
            this.FormClosing += frmTableauDeBord_FormClosing;
        }

        // Crée les boutons principaux et affiche les données de la mission.
        private void frmTableauDeBord_Load(object sender, EventArgs e)
        {
            creerBoutonExit();
            creerButtonGenPdf();
            afficherMission();
        }

        // ============================================================
        // INTERFACE DE CHARGEMENT
        // ============================================================

        // Crée le label utilisé pour afficher le chargement.
        private void CreateLoadingUI()
        {
            lblLoading = new Label();
            lblLoading.Text = "Chargement";
            lblLoading.Font = new System.Drawing.Font("Rubik", 32, FontStyle.Bold);
            lblLoading.ForeColor = Color.White;
            lblLoading.AutoSize = true;
            lblLoading.Visible = false;
            lblLoading.BackColor = Color.Transparent;

            lblLoading.Location = new Point(ClientSize.Width / 2 - 150, ClientSize.Height / 2 - 40);

            Controls.Add(lblLoading);
            lblLoading.BringToFront();
        }

        // Anime le texte de chargement avec des points.
        private async Task AnimateLoading(string text)
        {
            int dots = 0;

            while (loading)
            {
                dots = (dots + 1) % 4;
                lblLoading.Text = text + new string('.', dots);

                lblLoading.Location = new Point(ClientSize.Width / 2 - lblLoading.Width / 2, ClientSize.Height / 2 - lblLoading.Height / 2);

                await Task.Delay(300);
            }
        }

        // Affiche une animation pendant une action donnée.
        // L'action est exécutée après un délai minimal.
        private async Task ShowLoadingDuringAction(string text, Action action, int minDelay)
        {
            lblLoading.Visible = true;
            lblLoading.BringToFront();
            loading = true;

            Task animationTask = AnimateLoading(text);

            try
            {
                await Task.Delay(minDelay);

                action();
            }
            finally
            {
                loading = false;
                await animationTask;
                lblLoading.Visible = false;
            }
        }

        // ============================================================
        // CRÉATION DES ÉLÉMENTS GRAPHIQUES
        // ============================================================

        // Crée un label avec le style général de l'application.
        private Label creerLabel(string name, string text, int x, int y, int fontSize)
        {
            Label lbl = new Label();
            lbl.Name = name;
            lbl.Text = text;
            lbl.Location = new Point(x, y);
            lbl.AutoSize = true;
            lbl.Font = new System.Drawing.Font(lbl.Font.FontFamily, fontSize);
            lbl.ForeColor = SystemColors.Info;
            lbl.BackColor = Color.Transparent;

            return lbl;
        }

        // Crée une GroupBox personnalisée.
        private GroupBoxCustom creerGroupBox(string name, string text, int x, int y, int sizeX, int sizeY, int fontSize)
        {
            GroupBoxCustom grp = new GroupBoxCustom();
            grp.Name = name;
            grp.Text = text;
            grp.Location = new Point(x, y);
            grp.Size = new Size(sizeX, sizeY);
            grp.Font = new System.Drawing.Font(grp.Font.FontFamily, fontSize);
            grp.TabStop = false;

            return grp;
        }

        // Crée un bouton avec un style commun.
        private Button creerButton(string name, string text, int x, int y, int sizeX, int sizeY, EventHandler click, int fontSize)
        {
            Button btn = new Button();
            btn.Name = name;
            btn.Text = text;
            btn.Location = new Point(x, y);
            btn.Size = new Size(sizeX, sizeY);
            btn.AutoSize = true;
            btn.Font = new System.Drawing.Font(btn.Font.FontFamily, fontSize);
            btn.Click += click;

            styleButton(btn);

            return btn;
        }

        // Crée le bouton permettant de retourner à l'accueil.
        private void creerBoutonExit()
        {
            Button btnExit = creerButton("btnExit", "Retour à la page d'accueil", ClientSize.Width - 250, ClientSize.Height - 54, 50, 50, buttonExit_Click, 14);
            styleButton(btnExit);

            Controls.Add(btnExit);
        }

        // Crée le bouton permettant de générer le rapport PDF.
        private void creerButtonGenPdf()
        {
            Button btnGenPdf = creerButton("btnGenPdf", "Générer PDF", ClientSize.Width / 2 + 10, ClientSize.Height - 54, 50, 50, buttonGenPdf_Click, 14);
            styleButton(btnGenPdf);

            Controls.Add(btnGenPdf);
        }

        // Crée le groupe principal qui contient les détails de la mission.
        private GroupBoxCustom createGroupDetails(string planet, int numMission)
        {
            Control oldDetails = Controls["grpDetails"];
            if (oldDetails != null)
            {
                Controls.Remove(oldDetails);
                oldDetails.Dispose();
            }

            int xGrpSize = ClientSize.Width / 2;

            GroupBoxCustom grpDetails = creerGroupBox("grpDetails", "Details pour mission " + numMission + " sur la planete " + planet, 12, 17, xGrpSize - 22, ClientSize.Height - 24, 20);

            return grpDetails;
        }

        // ============================================================
        // STYLES GRAPHIQUES
        // ============================================================

        // Applique le style principal aux boutons.
        private void styleButton(Button btn)
        {
            btn.BackColor = SystemColors.Info;
            btn.ForeColor = Color.Gray;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderColor = Color.Gray;
            btn.FlatAppearance.BorderSize = 1;
        }

        // Applique le style sombre aux RichTextBox.
        private void styleRichTextBox(RichTextBox rtxt)
        {
            rtxt.BackColor = Color.FromArgb(40, 40, 40);
            rtxt.ForeColor = SystemColors.Info;
            rtxt.BorderStyle = BorderStyle.FixedSingle;
        }

        // Applique le style sombre aux ComboBox.
        private void styleComboBox(ComboBox cmb)
        {
            cmb.BackColor = Color.FromArgb(40, 40, 40);
            cmb.ForeColor = SystemColors.Info;
            cmb.FlatStyle = FlatStyle.Flat;
            cmb.DropDownStyle = ComboBoxStyle.DropDownList;

            cmb.DrawMode = DrawMode.OwnerDrawFixed;
            cmb.ItemHeight = 28;

            cmb.DrawItem += comboBox_DrawItem;
        }

        // Applique le style sombre aux TextBox.
        private void styleTextBox(TextBox txt)
        {
            txt.BackColor = Color.FromArgb(40, 40, 40);
            txt.ForeColor = SystemColors.Info;
            txt.BorderStyle = BorderStyle.FixedSingle;
        }

        // Configure le format français du DateTimePicker.
        private void styleDateTimePicker(DateTimePicker dtm)
        {
            dtm.Format = DateTimePickerFormat.Custom;
            dtm.CustomFormat = "dd/MM/yyyy";
        }

        // Dessine manuellement les éléments d'une ComboBox.
        // Cela permet de garder le style sombre dans la liste.
        private void comboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
            {
                return;
            }

            ComboBox cmb = sender as ComboBox;
            if (cmb == null)
            {
                return;
            }

            Color backColor;
            Color textColor;

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                backColor = Color.Gray;
                textColor = SystemColors.Info;
            }
            else
            {
                backColor = Color.FromArgb(40, 40, 40);
                textColor = SystemColors.Info;
            }

            using (SolidBrush backBrush = new SolidBrush(backColor))
            {
                e.Graphics.FillRectangle(backBrush, e.Bounds);
            }

            string text = cmb.GetItemText(cmb.Items[e.Index]);

            using (SolidBrush textBrush = new SolidBrush(textColor))
            {
                e.Graphics.DrawString(text, e.Font, textBrush, e.Bounds.X + 5, e.Bounds.Y + 5);
            }
            e.DrawFocusRectangle();
        }

        // ============================================================
        // CHARGEMENT DU DATASET
        // ============================================================

        // Vide puis recharge toutes les tables SQLite dans le DataSet.

        private void clearFillDB()
        {
            MesDatas.DsGlobal.Tables.Clear();

            try
            {
                string sql = "select name from sqlite_master where type = 'table' order by name";

                SQLiteCommand cmd = new SQLiteCommand(sql, Connexion.Connec);
                SQLiteDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        string nomTab = dr.GetString(0);
                        addTab(nomTab);
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("clearFillDB : " + err.Message);
            }
        }

        // Ajoute une table SQLite dans le DataSet global.
        private void addTab(string nomTab)
        {
            try
            {
                string sql = "select * from " + nomTab;

                SQLiteDataAdapter da = new SQLiteDataAdapter(sql, Connexion.Connec);
                da.Fill(MesDatas.DsGlobal, nomTab);
            }
            catch (Exception err)
            {
                MessageBox.Show("addTab : " + err.Message);
            }
        }

        // ============================================================
        // AFFICHAGE DES DÉTAILS DE LA MISSION
        // ============================================================

        // Supprime un contrôle précis depuis un parent.
        private void supprimerControl(Control parent, string nom)
        {
            Control old = parent.Controls[nom];

            if (old != null)
            {
                parent.Controls.Remove(old);
                old.Dispose();
            }
        }

        // Supprime les anciens groupes avant d'afficher une autre mission.
        private void suprimmerAncienGroups()
        {
            supprimerControl(this, "grpDetails");
            supprimerControl(this, "grpModification");
            supprimerControl(this, "lblNotChangable");
        }

        // Affiche toutes les informations principales de la mission.
        private void afficherMission()
        {
            int numMission = int.Parse(m_numMission);

            GroupBoxCustom grpDetails = createGroupDetails(m_planet, numMission);
            Controls.Add(grpDetails);

            chargerInfoMission(grpDetails, m_planet, numMission, m_matChef, m_numGlobal);
            chargerMembers(grpDetails, m_planet, numMission);
            chargerObjectif(grpDetails, m_planet, numMission);
        }

        // Recharge la fenêtre avec une nouvelle mission.
        public void loadNewMission(string planet, string numMission, string matChef, string numGlobal)
        {
            m_planet = planet;
            m_numMission = numMission;
            m_matChef = matChef;
            m_numGlobal = numGlobal;

            suprimmerAncienGroups();
            afficherMission();
        }

        // Vérifie si la mission demandée est déjà affichée.
        public bool same(string planet, string numMission, string matChef, string numGlobal)
        {
            return m_planet == planet && m_numMission == numMission && m_matChef == matChef && m_numGlobal == numGlobal;
        }

        // Charge les informations principales de la mission.
        // Crée aussi le bloc de modification si la mission n'est pas terminée.
        private void chargerInfoMission(GroupBoxCustom grpDetails, string planet, int numMission, string matChef, string idPl)
        {
            string sql = @"select nbMembreRequis,dateDepart,dateRetour,feuilleDeRoute,objectifDatabaz,budget
                   from mission
                   where nomPlanete = @planet and numero = @numMission and matriculeChef = @matChef;";
            try
            {
                SQLiteCommand cmd = new SQLiteCommand(sql, Connexion.Connec);

                cmd.Parameters.AddWithValue("@planet", planet);
                cmd.Parameters.AddWithValue("@numMission", numMission);
                cmd.Parameters.AddWithValue("@matChef", matChef);

                SQLiteDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        int nbMembers = dr.GetInt32(0);
                        string dtStartDb = dr.GetString(1);
                        string dtEndDb = dr.GetString(2);
                        string feuille = dr.GetString(3);
                        int objectifDatabaz = dr.GetInt32(4);
                        int budget = dr.GetInt32(5);

                        int xGrpSize = grpDetails.Width;
                        int yGrpSize = grpDetails.Height;
                        string dtStart = dateNormalized(dtStartDb);
                        string dtEnd = dateNormalized(dtEndDb);

                        PictureBox pctrPl = new PictureBox();
                        pctrPl.Location = new Point(xGrpSize / 2 - 40, 30);
                        pctrPl.Name = "pctrPlanet";
                        pctrPl.Size = new System.Drawing.Size(150, 160);
                        pctrPl.SizeMode = PictureBoxSizeMode.Zoom;
                        pctrPl.Image = chargerImage("images/planetes/" + planet + ".png");
                        pctrPl.TabStop = false;
                        grpDetails.Controls.Add(pctrPl);

                        Label lblPlanetDetails = creerLabel("lblPlanetDetails", planet, xGrpSize / 2 - 40, 105, 18);
                        lblPlanetDetails.ForeColor = SystemColors.Info;
                        grpDetails.Controls.Add(lblPlanetDetails);

                        Label lblDtStart = creerLabel("lblDtStart", "Date de départ : " + dtStart, 10, 128, 16);
                        lblDtStart.Tag = dtStartDb;
                        grpDetails.Controls.Add(lblDtStart);

                        Label lblDtEnd = creerLabel("lblDtEnd", "Date de retour : " + dtEnd, 10, 151, 16);
                        lblDtEnd.Tag = dtEndDb;
                        grpDetails.Controls.Add(lblDtEnd);

                        Label lblBudget = creerLabel("lblBudget", "Budget : " + budget + "$", xGrpSize - 235, 128, 16);
                        grpDetails.Controls.Add(lblBudget);

                        int totalDepenses = soldes(planet, numMission);
                        int budgetReste = budget - totalDepenses;
                        Label lblBudgetReste = creerLabel("lblBudgetReste", "Budget restant : " + budgetReste + "$", xGrpSize - 235, 151, 16);
                        lblBudgetReste.ForeColor = budgetReste < 500 ? Color.Red : SystemColors.Info; lblBudgetReste.Tag = budgetReste;
                        grpDetails.Controls.Add(lblBudgetReste);

                        Label lblNbMembers = creerLabel("lblNbMembers", "Membres requis : " + nbMembers, 10, ClientSize.Height - 40 - 390, 16);
                        grpDetails.Controls.Add(lblNbMembers);

                        RichTextBox rtxtFeuille = new RichTextBox();
                        rtxtFeuille.Location = new Point(10, 220);
                        rtxtFeuille.Name = "rtxtFeuille";
                        rtxtFeuille.Size = new Size(xGrpSize - 30, yGrpSize - 650);
                        rtxtFeuille.Text = "Feuille de route :\n" + feuille;
                        rtxtFeuille.ReadOnly = true;
                        rtxtFeuille.TabStop = false;
                        rtxtFeuille.WordWrap = true;
                        styleRichTextBox(rtxtFeuille);
                        rtxtFeuille.ScrollBars = RichTextBoxScrollBars.Vertical;
                        rtxtFeuille.Font = new System.Drawing.Font(rtxtFeuille.Font.FontFamily, 14);
                        grpDetails.Controls.Add(rtxtFeuille);

                        DateTime dtRet = convertirDate(dtEndDb);
                        if (dtRet > DateTime.Today)
                        {
                            createGroupModification();
                        }
                        else
                        {
                            Label lblNotChangable = creerLabel("lblNotChangable", "Pas possible à changer l'information\ncar mission est déja fini.", ClientSize.Width - 650, ClientSize.Height / 2, 20);
                            Controls.Add(lblNotChangable);
                        }
                    }
                }
            }
            catch (SQLiteException err)
            {
                MessageBox.Show("chargerInfoMission\n" + err.Message);
            }
        }

        // Charge et affiche les membres affectés à la mission.
        private void chargerMembers(GroupBox grpDetails, string planet, int numMission)
        {
            FlowLayoutPanel flplMembers = new FlowLayoutPanel();

            flplMembers.AutoScroll = true;
            flplMembers.BackColor = Color.FromArgb(40, 40, 40);
            flplMembers.FlowDirection = FlowDirection.LeftToRight;
            flplMembers.Location = new Point(10, ClientSize.Height - 40 - 360);
            flplMembers.Name = "flpMembers";
            flplMembers.Size = new Size(grpDetails.Width - 30, 245);
            flplMembers.WrapContents = false;
            flplMembers.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;

            string sql = @"select mem.nom,mem.prenom,civ.specialite,mil.grade
                   from Composer com
                    join membre mem on mem.matricule = com.matriculeMembre
                    left join civil civ on civ.matriculeMembre = mem.matricule
                    left join militaire mil on mil.matriculeMembre = mem.matricule
                    where com.nomPlanete = @planet and com.numeroMission = @numMission
                    order by mem.nom, mem.prenom;";

            try
            {
                SQLiteCommand cmd = new SQLiteCommand(sql, Connexion.Connec);

                cmd.Parameters.AddWithValue("@planet", planet);
                cmd.Parameters.AddWithValue("@numMission", numMission);

                SQLiteDataReader dr = cmd.ExecuteReader();
                {
                    if (dr.HasRows)
                    {
                        int i = 0;
                        while (dr.Read())
                        {
                            i++;

                            string membreName = dr.GetString(0) + " " + dr.GetString(1);
                            string specialite = dr.IsDBNull(2) ? "" : dr.GetString(2);
                            string grade = dr.IsDBNull(3) ? "" : dr.GetString(3);

                            GroupBoxCustom grpMember = new GroupBoxCustom();
                            grpMember.Name = "grpMember_" + i;
                            grpMember.Size = new Size(257, 220);
                            grpMember.TabStop = false;
                            grpMember.Font = new System.Drawing.Font(grpMember.Font.FontFamily, 18);

                            PictureBox pctrMembre = new PictureBox();
                            pctrMembre.Location = new Point(180 / 2 - 40, 0);
                            pctrMembre.Name = "pctrMembre_" + i;
                            pctrMembre.Size = new Size(160, 160);
                            pctrMembre.SizeMode = PictureBoxSizeMode.Zoom;
                            pctrMembre.BackColor = Color.Transparent;
                            pctrMembre.TabStop = false;

                            string imageName;

                            if (!string.IsNullOrWhiteSpace(grade))
                            {
                                imageName = grade;
                                pctrMembre.Image = chargerImage("images/grades/" + imageName + ".png");
                            }
                            else if (!string.IsNullOrWhiteSpace(specialite))
                            {
                                imageName = specialite;
                                pctrMembre.Image = chargerImage("images/specs/" + imageName + ".png");
                            }
                            else
                            {
                                imageName = "default";
                            }
                            grpMember.Controls.Add(pctrMembre);

                            Label lblMembreName = creerLabel("lblMembre_" + i, membreName, 10, 170, 14);
                            lblMembreName.ForeColor = SystemColors.Info;
                            grpMember.Controls.Add(lblMembreName);

                            Label lblMembreSpecification = creerLabel("lblMembreSpecification_" + i, "", 10, 190, 14);
                            lblMembreSpecification.ForeColor = SystemColors.Info;

                            if (!string.IsNullOrEmpty(specialite))
                            {
                                lblMembreSpecification.Text = "Spécialité : " + specialite;
                            }
                            else if (!string.IsNullOrEmpty(grade))
                            {
                                lblMembreSpecification.Text = "Grade : " + grade;
                            }
                            else
                            {
                                lblMembreSpecification.Text = "Type inconnu";
                            }

                            grpMember.Controls.Add(lblMembreSpecification);

                            flplMembers.Controls.Add(grpMember);
                        }
                    }
                }
            }
            catch (SQLiteException err)
            {
                MessageBox.Show("chargerMembers" + err.Message);
            }
            grpDetails.Controls.Add(flplMembers);
        }

        // Charge et affiche les objectifs de capture de la mission.
        private void chargerObjectif(GroupBox grpDetails, string planet, int numMission)
        {
            RichTextBox rtxtObjectif = new RichTextBox();
            rtxtObjectif.Location = new Point(10, ClientSize.Height - 110 - 30);
            rtxtObjectif.Name = "rtxtObjectif";
            rtxtObjectif.Size = new Size(grpDetails.Width - 130, 110);
            rtxtObjectif.Font = new System.Drawing.Font(rtxtObjectif.Font.FontFamily, 14);
            rtxtObjectif.ReadOnly = true;
            styleRichTextBox(rtxtObjectif);
            rtxtObjectif.TabStop = false;

            Button btnOpenJournal = creerButton("btnopenJournal", "", grpDetails.Width - 105, ClientSize.Height - 130, 80, 80, buttonOpenJournal_Click, 14);
            btnOpenJournal.BackgroundImage = chargerImage("images/journal.png");
            btnOpenJournal.BackgroundImageLayout = ImageLayout.Zoom;
            btnOpenJournal.AutoSize = false;


            rtxtObjectif.Text = "Objectifs de capture :\n";

            string sql = @"select obCap.objectif, ifnull(cap.nombre, 0), esp.nom,esp.couleur
                           from ObjectifCapture obCap
                           left join Capturer cap on cap.nomPlanete = obCap.nomPlanete and cap.numeroMission = obCap.numeroMission and cap.idEspeceEnnemi = obCap.idEspeceEnnemi
                           left join espece esp on esp.id = obCap.idEspeceEnnemi
                           where obCap.nomPlanete = @planet and obCap.numeroMission = @numMission
                           order by esp.nom;";
            try
            {
                SQLiteCommand cmd = new SQLiteCommand(sql, Connexion.Connec);
                cmd.Parameters.AddWithValue("@planet", planet);
                cmd.Parameters.AddWithValue("@numMission", numMission);

                SQLiteDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        int objectif = dr.GetInt32(0);
                        int capture = dr.GetInt32(1);
                        string espece = dr.IsDBNull(2) ? "Espèce inconnue" : dr.GetString(2);
                        string couleur = dr.IsDBNull(3) ? "" : dr.GetString(3);

                        rtxtObjectif.Text +=
                            "- " + espece;

                        if (!string.IsNullOrEmpty(couleur))
                        {
                            rtxtObjectif.Text += " (" + couleur + ")";
                        }

                        rtxtObjectif.Text += " : " + capture + " / " + objectif + "\n";
                    }
                }
            }
            catch (SQLiteException err)
            {
                MessageBox.Show("chargerObjectif" + err.Message);
            }
            grpDetails.Controls.Add(rtxtObjectif);
            grpDetails.Controls.Add(btnOpenJournal);
        }

        // Calcule le total des dépenses de la mission.
        private int soldes(string planet, int numMission)
        {
            string sqlDep = @"select ifnull(sum(montant),0)
                           from Depense
                           where nomPlanete = @planet and numeroMission = @numMission";
            try
            {
                int sD = 0;

                SQLiteCommand cmdDep = new SQLiteCommand(sqlDep, Connexion.Connec);
                cmdDep.Parameters.AddWithValue("@planet", planet);
                cmdDep.Parameters.AddWithValue("@numMission", numMission);
                sD = Convert.ToInt32(cmdDep.ExecuteScalar());

                return sD;
            }
            catch (SQLiteException err)
            {
                MessageBox.Show("calculSoldes\n" + err.Message);
                return 0;
            }
        }

        // ============================================================
        // FORMULAIRE DE MODIFICATION
        // ============================================================

        // Crée le bloc contenant les boutons de modification.
        private void createGroupModification()
        {
            Control oldMod = Controls["grpModification"];
            if (oldMod != null)
            {
                Controls.Remove(oldMod);
                oldMod.Dispose();
            }

            GroupBoxCustom grpModification = creerGroupBox("grpModification", "Modification de la mission", ClientSize.Width / 2 + 10, 17, ClientSize.Width / 2 - 20, ClientSize.Height - 84, 20);
            Controls.Add(grpModification);

            GroupBoxCustom grpEdition = creerGroupBox("grpEdition", "Mode d'edition", 10, 35, grpModification.Width - 20, 90, 18);
            grpModification.Controls.Add(grpEdition);

            Button btnContact = creerButton("btnContact", "Contact", 57, 30, 50, 50, buttonContact_Click, 14);
            grpEdition.Controls.Add(btnContact);

            Button btnDepends = creerButton("btnDepends", "Dépense", 57 * 2 + 85, 30, 50, 50, buttonDepends_Click, 14);
            grpEdition.Controls.Add(btnDepends);

            Button btnCaptures = creerButton("bntCaptures", "Captures", 57 * 3 + 99 + 85, 30, 50, 50, buttonCaptures_Click, 14);
            grpEdition.Controls.Add(btnCaptures);

            Button btnEvent = creerButton("btnEvent", "Événements", 57 * 4 + 97 + 99 + 85, 30, 50, 50, buttonEvent_Click, 14);
            grpEdition.Controls.Add(btnEvent);
        }

        // Vérifie si un formulaire de modification contient des données.
        private bool modificationEnCours()
        {
            Control grpModification = Controls["grpModification"];
            if (grpModification == null)
            {
                return false;
            }

            Control grpForm = grpModification.Controls["grpFormModif"];
            if (grpForm == null)
            {
                return false;
            }

            foreach (Control ctrl in grpForm.Controls)
            {
                TextBox txt = ctrl as TextBox;
                if (txt != null && !string.IsNullOrWhiteSpace(txt.Text))
                {
                    return true;
                }

                RichTextBox rtxt = ctrl as RichTextBox;
                if (rtxt != null && !string.IsNullOrWhiteSpace(rtxt.Text))
                {
                    return true;
                }
            }
            return false;
        }

        // Ferme le bloc de modification sans toucher au reste de la page.
        private void fermerBlocModification()
        {
            Control grpModification = Controls["grpModification"];

            if (grpModification == null)
            {
                return;
            }

            Control grpFormModif = grpModification.Controls["grpFormModif"];

            if (grpFormModif != null)
            {
                grpModification.Controls.Remove(grpFormModif);
                grpFormModif.Dispose();
            }
        }

        // Annule le formulaire de modification actuellement affiché.
        private void buttonAnnuler_Click(object sender, EventArgs e)
        {
            Control grpModification = Controls["grpModification"];
            if (grpModification == null)
            {
                return;
            }

            Control old = grpModification.Controls["grpFormModif"];
            if (old != null)
            {
                grpModification.Controls.Remove(old);
                old.Dispose();
            }
        }

        // ============================================================
        // FORMULAIRE D'ÉVÉNEMENT
        // ============================================================

        // Affiche le formulaire d'ajout d'un événement.
        private void buttonEvent_Click(object sender, EventArgs e)
        {
            Control grpModification = Controls["grpModification"];
            if (grpModification == null)
            {
                MessageBox.Show("grpModification introuvable.");
                return;
            }

            Control grpEdition = grpModification.Controls["grpEdition"];
            if (grpEdition == null)
            {
                MessageBox.Show("grpEdition introuvable.");
                return;
            }

            Control old = grpModification.Controls["grpFormModif"];
            if (old != null)
            {
                grpModification.Controls.Remove(old);
                old.Dispose();
            }

            GroupBoxCustom grpAddEvent = creerGroupBox("grpFormModif", "Nouvel événement", 10, grpEdition.Height + 40, grpEdition.Width, grpModification.Height - grpEdition.Height - 50, 16);
            grpModification.Controls.Add(grpAddEvent);

            Label lblDate = creerLabel("lblDateEvent", "Date :", 10, 40, 14);
            lblDate.ForeColor = SystemColors.Info;
            grpAddEvent.Controls.Add(lblDate);

            DateTimePicker dtmEvent = new DateTimePicker();
            dtmEvent.Location = new Point(150, 40);
            dtmEvent.Name = "dtmEvent";
            dtmEvent.Size = new Size(200, 20);
            dtmEvent.Font = new System.Drawing.Font(dtmEvent.Font.FontFamily, 14);
            styleDateTimePicker(dtmEvent);
            grpAddEvent.Controls.Add(dtmEvent);

            Label lblCommentaire = creerLabel("lblCommentaireEvent", "Commentaire :", 10, 100, 14);
            lblCommentaire.ForeColor = SystemColors.Info;
            grpAddEvent.Controls.Add(lblCommentaire);

            RichTextBox rtxtCommentaire = new RichTextBox();
            rtxtCommentaire.Location = new Point(150, 100);
            rtxtCommentaire.Size = new Size(grpAddEvent.Width - 170, 250);
            rtxtCommentaire.Name = "rtxtCommentaireEvent";
            rtxtCommentaire.Font = new System.Drawing.Font(rtxtCommentaire.Font.FontFamily, 14);
            styleRichTextBox(rtxtCommentaire);
            grpAddEvent.Controls.Add(rtxtCommentaire);

            Button btnValider = creerButton("btnValiderEvent", "Valider", grpAddEvent.Width - 90, grpAddEvent.Height - 60, 50, 50, btnVEvent_Click, 14);
            grpAddEvent.Controls.Add(btnValider);

            Button btnAnnuler = creerButton("btnAnnulerEvent", "Annuler", grpAddEvent.Width - 185, grpAddEvent.Height - 60, 50, 50, buttonAnnuler_Click, 14);
            grpAddEvent.Controls.Add(btnAnnuler);
        }

        // Vérifie les données du formulaire événement avant insertion.
        private void btnVEvent_Click(object sender, EventArgs e)
        {
            Control grpModification = Controls["grpModification"];
            Control grpDetails = Controls["grpDetails"];

            if (grpModification == null || grpDetails == null)
            {
                MessageBox.Show("Erreur : bloc de modification ou détails introuvable.");
                return;
            }

            Control grpEvent = grpModification.Controls["grpFormModif"];
            if (grpEvent == null)
            {
                MessageBox.Show("Erreur : formulaire d'événement introuvable.");
                return;
            }

            DateTimePicker dtmEvent = grpEvent.Controls["dtmEvent"] as DateTimePicker;
            if (dtmEvent == null)
            {
                MessageBox.Show("Erreur : date introuvable.");
                return;
            }

            DateTime dateEvent = dtmEvent.Value.Date;
            if (!dateDansMission(grpDetails, dateEvent))
            {
                return;
            }

            RichTextBox rtxtCommentaire = grpEvent.Controls["rtxtCommentaireEvent"] as RichTextBox;
            if (rtxtCommentaire == null || string.IsNullOrWhiteSpace(rtxtCommentaire.Text))
            {
                MessageBox.Show("Veuillez saisir un commentaire.");
                return;
            }

            string commentaire = rtxtCommentaire.Text.Trim();

            changeDBEvent(dateEvent, commentaire);
        }

        // Ajoute un événement dans le journal de bord.
        private void changeDBEvent(DateTime dateEvent, string commentaire)
        {
            SQLiteTransaction trans = null;
            try
            {
                trans = Connexion.Connec.BeginTransaction();
                int numMission = int.Parse(m_numMission);

                ajouterJournal(trans, dateEvent, commentaire);

                trans.Commit();

                MessageBox.Show("Événement ajouté avec succès.");
                loadNewMission(m_planet, m_numMission, m_matChef, m_numGlobal);
            }
            catch (Exception err)
            {
                if (trans != null)
                {
                    trans.Rollback();
                }

                MessageBox.Show("Erreur lors de l'ajout de l'événement : " + err.Message);
            }
        }

        // Insère une ligne dans la table JournalDeBord.
        private void ajouterJournal(SQLiteTransaction trans, DateTime dateJ, string commentaire)
        {
            string sqlJournal = @"insert into JournalDeBord
                         (nomPlanete, numero, dateJ, commentaires)
                         values
                         (@planet, @numero, @dateJ, @commentaires)";

            SQLiteCommand cmdJournal = new SQLiteCommand(sqlJournal, Connexion.Connec, trans);

            cmdJournal.Parameters.AddWithValue("@planet", m_planet);
            cmdJournal.Parameters.AddWithValue("@numero", int.Parse(m_numMission));
            cmdJournal.Parameters.AddWithValue("@dateJ", dateJ.ToString("yyyy-MM-dd"));
            cmdJournal.Parameters.AddWithValue("@commentaires", commentaire);

            cmdJournal.ExecuteNonQuery();
        }

        // ============================================================
        // FORMULAIRE DE DÉPENSE
        // ============================================================

        // Affiche le formulaire d'ajout d'une dépense.
        private void buttonDepends_Click(object sender, EventArgs e)
        {
            Control grpModification = Controls["grpModification"];
            if (grpModification == null)
            {
                MessageBox.Show("grpModification introuvable.");
                return;
            }

            Control grpEdition = grpModification.Controls["grpEdition"];
            if (grpEdition == null)
            {
                MessageBox.Show("grpEdition introuvable.");
                return;
            }

            Control old = grpModification.Controls["grpFormModif"];
            if (old != null)
            {
                grpModification.Controls.Remove(old);
                old.Dispose();
            }

            GroupBoxCustom grpAddDepense = creerGroupBox("grpFormModif", "Nouvelle dépense", 10, grpEdition.Height + 40, grpEdition.Width, grpModification.Height - grpEdition.Height - 50, 16);
            grpModification.Controls.Add(grpAddDepense);

            Label lblDate = creerLabel("lblDateDepense", "Date :", 10, 40, 14);
            lblDate.ForeColor = SystemColors.Info;
            grpAddDepense.Controls.Add(lblDate);

            DateTimePicker dtmDepense = new DateTimePicker();
            dtmDepense.Location = new Point(120, 40);
            dtmDepense.Name = "dtmDepense";
            dtmDepense.Size = new Size(200, 20);
            dtmDepense.Font = new System.Drawing.Font(dtmDepense.Font.FontFamily, 14);
            styleDateTimePicker(dtmDepense);
            grpAddDepense.Controls.Add(dtmDepense);

            Label lblMontant = creerLabel("lblMontantDepense", "Montant :", 10, 90, 14);
            lblMontant.ForeColor = SystemColors.Info;
            grpAddDepense.Controls.Add(lblMontant);

            TextBox txtMontant = new TextBox();
            txtMontant.Location = new Point(120, 90);
            txtMontant.Name = "txtMontantDepense";
            txtMontant.Size = new Size(200, 20);
            txtMontant.Font = new System.Drawing.Font(txtMontant.Font.FontFamily, 14);
            txtMontant.KeyPress += txtSomme_KeyPress;
            styleTextBox(txtMontant);
            grpAddDepense.Controls.Add(txtMontant);

            Label lblMotif = creerLabel("lblMotifDepense", "Motif :", 10, 140, 14);
            lblMotif.ForeColor = SystemColors.Info;
            grpAddDepense.Controls.Add(lblMotif);

            RichTextBox rtxtMotif = new RichTextBox();
            rtxtMotif.Location = new Point(120, 140);
            rtxtMotif.Size = new Size(grpAddDepense.Width - 140, 180);
            rtxtMotif.Name = "rtxtMotifDepense";
            rtxtMotif.Font = new System.Drawing.Font(rtxtMotif.Font.FontFamily, 14);
            styleRichTextBox(rtxtMotif);
            grpAddDepense.Controls.Add(rtxtMotif);

            Label lblType = creerLabel("lblTypeDepense", "Type :", 10, 350, 14);
            lblType.ForeColor = SystemColors.Info;
            grpAddDepense.Controls.Add(lblType);

            ComboBox cmbTypeDepense = new ComboBox();
            cmbTypeDepense.Location = new Point(120, 350);
            cmbTypeDepense.Name = "cmbTypeDepense";
            cmbTypeDepense.Size = new Size(250, 20);
            cmbTypeDepense.Font = new System.Drawing.Font(cmbTypeDepense.Font.FontFamily, 14);
            cmbTypeDepense.DropDownStyle = ComboBoxStyle.DropDownList;

            chargerTypesDepense(cmbTypeDepense);

            grpAddDepense.Controls.Add(cmbTypeDepense);

            Button btnValider = creerButton("btnValiderDepense", "Valider", grpAddDepense.Width - 90, grpAddDepense.Height - 60, 50, 50, btnVDepense_Click, 14);
            grpAddDepense.Controls.Add(btnValider);

            Button btnAnnuler = creerButton("btnAnnulerDepense", "Annuler", grpAddDepense.Width - 185, grpAddDepense.Height - 60, 50, 50, buttonAnnuler_Click, 14);
            grpAddDepense.Controls.Add(btnAnnuler);
        }

        // Charge les types de dépense dans une ComboBox.
        private void chargerTypesDepense(ComboBox cmb)
        {
            DataTable table = MesDatas.DsGlobal.Tables["TypeDepense"];

            if (table == null)
            {
                MessageBox.Show("La table TypeDepense est introuvable dans le DataSet.");
                return;
            }

            cmb.DataSource = table;
            cmb.DisplayMember = "libelle";
            cmb.ValueMember = "id";

            styleComboBox(cmb);
        }

        // Vérifie les données du formulaire dépense avant insertion.
        private void btnVDepense_Click(object sender, EventArgs e)
        {
            Control grpModification = Controls["grpModification"];
            Control grpDetails = Controls["grpDetails"];

            if (grpModification == null || grpDetails == null)
            {
                MessageBox.Show("Erreur : bloc de modification ou détails introuvable.");
                return;
            }

            Control grpDepense = grpModification.Controls["grpFormModif"];
            if (grpDepense == null)
            {
                MessageBox.Show("Erreur : formulaire de dépense introuvable.");
                return;
            }

            int budgetRestant;
            if (!getBudgetRestant(grpDetails, out budgetRestant))
            {
                return;
            }

            TextBox txtMontant = grpDepense.Controls["txtMontantDepense"] as TextBox;
            if (txtMontant == null || string.IsNullOrWhiteSpace(txtMontant.Text))
            {
                MessageBox.Show("Veuillez saisir un montant.");
                return;
            }

            int montant;
            if (!int.TryParse(txtMontant.Text, out montant))
            {
                MessageBox.Show("Le montant doit être un nombre.");
                return;
            }

            if (montant <= 0)
            {
                MessageBox.Show("La montant doit être supérieure à 0.");
                return;
            }

            if (montant > budgetRestant)
            {
                MessageBox.Show("La montant dépasse le budget restant.");
                return;
            }

            RichTextBox rtxtMotif = grpDepense.Controls["rtxtMotifDepense"] as RichTextBox;
            if (rtxtMotif == null || string.IsNullOrWhiteSpace(rtxtMotif.Text))
            {
                MessageBox.Show("Veuillez saisir un motif.");
                return;
            }

            string motif = rtxtMotif.Text.Trim();

            DateTimePicker dtmDepense = grpDepense.Controls["dtmDepense"] as DateTimePicker;
            if (dtmDepense == null)
            {
                MessageBox.Show("Erreur : date introuvable.");
                return;
            }

            DateTime dateDepense = dtmDepense.Value.Date;

            if (!dateDansMission(grpDetails, dateDepense))
            {
                return;
            }

            ComboBox cmbTypeDepense = grpDepense.Controls["cmbTypeDepense"] as ComboBox;
            if (cmbTypeDepense == null || cmbTypeDepense.SelectedValue == null)
            {
                MessageBox.Show("Veuillez choisir un type de dépense.");
                return;
            }

            int idTypeDepense;
            if (!int.TryParse(cmbTypeDepense.SelectedValue.ToString(), out idTypeDepense))
            {
                MessageBox.Show("Type de dépense invalide.");
                return;
            }

            changeDBDepense(dateDepense, montant, motif, idTypeDepense);
        }

        // Ajoute une dépense et crée aussi un événement dans le journal.
        private void changeDBDepense(DateTime dateDepense, int montant, string motif, int idTypeDepense)
        {
            SQLiteTransaction trans = null;

            try
            {
                trans = Connexion.Connec.BeginTransaction();

                ajouterDepense(trans, dateDepense, montant, motif, idTypeDepense);

                string commentaireJournal = "Nouvelle dépense : " + motif + ". Montant : " + montant + "$.";

                ajouterJournal(trans, dateDepense, commentaireJournal);

                trans.Commit();

                MessageBox.Show("Dépense et événement ajoutés avec succès.");
                loadNewMission(m_planet, m_numMission, m_matChef, m_numGlobal);
            }
            catch (Exception err)
            {
                if (trans != null)
                {
                    trans.Rollback();
                }

                MessageBox.Show("Erreur lors de l'ajout de la dépense : " + err.Message);
            }
        }

        // Insère une dépense dans la table Depense.
        private void ajouterDepense(SQLiteTransaction trans, DateTime dateD, int montant, string motif, int idTypeDepense)
        {
            int numMission = int.Parse(m_numMission);
            int idDepense = getIdDep(m_planet, numMission, trans);

            string sqlDepense = @"insert into Depense
                                 (nomPlanete, numeroMission, id, dateD, montant, motif, idTypeDepense)
                                 values
                                 (@planet, @mission, @id, @dateD, @montant, @motif, @idTypeDepense)";

            SQLiteCommand cmdDepense = new SQLiteCommand(sqlDepense, Connexion.Connec, trans);

            cmdDepense.Parameters.AddWithValue("@planet", m_planet);
            cmdDepense.Parameters.AddWithValue("@mission", numMission);
            cmdDepense.Parameters.AddWithValue("@id", idDepense);
            cmdDepense.Parameters.AddWithValue("@dateD", dateD.ToString("yyyy-MM-dd"));
            cmdDepense.Parameters.AddWithValue("@montant", montant);
            cmdDepense.Parameters.AddWithValue("@motif", motif);
            cmdDepense.Parameters.AddWithValue("@idTypeDepense", idTypeDepense);

            cmdDepense.ExecuteNonQuery();
        }

        // Calcule le prochain identifiant de dépense pour une mission.
        private int getIdDep(string planet, int numMission, SQLiteTransaction trans)
        {
            string sql = @"select ifnull(max(id), 0) + 1
                           from Depense
                           where nomPlanete = @planet
                             and numeroMission = @numMission";

            SQLiteCommand cmd = new SQLiteCommand(sql, Connexion.Connec, trans);
            cmd.Parameters.AddWithValue("@planet", planet);
            cmd.Parameters.AddWithValue("@numMission", numMission);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        // ============================================================
        // FORMULAIRE DE CONTACT
        // ============================================================

        // Affiche le formulaire d'ajout d'un contact informateur.
        private void buttonContact_Click(object sender, EventArgs e)
        {
            Control grpModification = Controls["grpModification"];
            if (grpModification == null)
            {
                MessageBox.Show("grpModification introuvable.");
                return;
            }

            Control grpEdition = grpModification.Controls["grpEdition"];
            if (grpEdition == null)
            {
                MessageBox.Show("grpEdition introuvable.");
                return;
            }

            Control old = grpModification.Controls["grpFormModif"];
            if (old != null)
            {
                grpModification.Controls.Remove(old);
                old.Dispose();
            }

            if (grpModification != null && grpEdition != null)
            {
                GroupBoxCustom grpFormModif = creerGroupBox("grpFormModif", "Nouveau contact", 10, grpEdition.Height + 50, grpEdition.Width, grpModification.Height - grpEdition.Height - 60, 16);
                grpModification.Controls.Add(grpFormModif);

                Label lblDate = creerLabel("lblDate", "Date :", 10, 40, 14);
                lblDate.ForeColor = SystemColors.Info;
                grpFormModif.Controls.Add(lblDate);

                DateTimePicker dtmContact = new DateTimePicker();
                dtmContact.Location = new Point(100, 40);
                dtmContact.Name = "dtmContact";
                dtmContact.Size = new Size(200, 20);
                dtmContact.Font = new System.Drawing.Font(dtmContact.Font.FontFamily, 14);
                styleDateTimePicker(dtmContact);

                grpFormModif.Controls.Add(dtmContact);

                Label lblSomme = creerLabel("lblSomme", "Somme :", 10, 90, 14);
                lblSomme.ForeColor = SystemColors.Info;
                grpFormModif.Controls.Add(lblSomme);

                TextBox txtSomme = new TextBox();
                txtSomme.Location = new Point(100, 90);
                txtSomme.Name = "txtSomme";
                txtSomme.Size = new Size(200, 20);
                txtSomme.Font = new System.Drawing.Font(txtSomme.Font.FontFamily, 14);
                styleTextBox(txtSomme);
                txtSomme.KeyPress += txtSomme_KeyPress;
                grpFormModif.Controls.Add(txtSomme);

                Label lblAppr = creerLabel("lblAppr", "Appréciation :", 10, 140, 14);
                lblAppr.ForeColor = SystemColors.Info;
                grpFormModif.Controls.Add(lblAppr);

                RichTextBox rtxtAppr = new RichTextBox();
                rtxtAppr.Location = new Point(145, 140);
                rtxtAppr.Size = new Size(grpFormModif.Width - 10 - 145, 200);
                rtxtAppr.Name = "rtxtAppr";
                rtxtAppr.Font = new System.Drawing.Font(rtxtAppr.Font.FontFamily, 14);
                styleRichTextBox(rtxtAppr);
                grpFormModif.Controls.Add(rtxtAppr);

                Label lblInformateur = creerLabel("lblInformateur", "Informateur :", 10, 360, 14);
                lblInformateur.ForeColor = SystemColors.Info;
                grpFormModif.Controls.Add(lblInformateur);

                ComboBox cmbInformateurs = new ComboBox();
                cmbInformateurs.Location = new Point(145, 360);
                cmbInformateurs.Name = "cmbInformateurs";
                cmbInformateurs.Size = new Size(240, 20);
                cmbInformateurs.Font = new System.Drawing.Font(cmbInformateurs.Font.FontFamily, 14);
                cmbInformateurs.DropDownStyle = ComboBoxStyle.DropDownList;

                chargerInformateurs(cmbInformateurs);

                grpFormModif.Controls.Add(cmbInformateurs);

                Button btnValider = creerButton("btnValider", "Valider", grpFormModif.Width - 90, grpFormModif.Height - 60, 50, 50, btnVContact_Click, 14);
                grpFormModif.Controls.Add(btnValider);

                Button btnAnnuler = creerButton("btnAnnuler", "Annuler", grpFormModif.Width - 185, grpFormModif.Height - 60, 50, 50, buttonAnnuler_Click, 14);
                grpFormModif.Controls.Add(btnAnnuler);
            }
        }

        // Charge les informateurs dans une ComboBox.
        private void chargerInformateurs(ComboBox cmb)
        {
            DataTable table = MesDatas.DsGlobal.Tables["Informateur"];
            if (table == null)
            {
                MessageBox.Show("La table Informateur est introuvable dans le DataSet.");
                return;
            }

            cmb.DataSource = MesDatas.DsGlobal.Tables["Informateur"];
            cmb.DisplayMember = "nom";
            cmb.ValueMember = "nomCode";

            styleComboBox(cmb);
        }

        // Vérifie les données du formulaire contact avant insertion.
        private void btnVContact_Click(object sender, EventArgs e)
        {
            Control grpModification = Controls["grpModification"];
            Control grpDetails = Controls["grpDetails"];

            if (grpModification == null || grpDetails == null)
            {
                MessageBox.Show("Erreur : bloc de modification ou détails introuvable.");
                return;
            }

            Control grpContact = grpModification.Controls["grpFormModif"];
            if (grpContact == null)
            {
                MessageBox.Show("Erreur : formulaire de contact introuvable.");
                return;
            }

            Label lblBudgetReste = grpDetails.Controls["lblBudgetReste"] as Label;
            if (lblBudgetReste == null || lblBudgetReste.Tag == null)
            {
                MessageBox.Show("Erreur : budget restant introuvable.");
                return;
            }

            int budgetRestant;
            if (!getBudgetRestant(grpDetails, out budgetRestant))
            {
                return;
            }

            TextBox txtSomme = grpContact.Controls["txtSomme"] as TextBox;
            if (txtSomme == null || string.IsNullOrWhiteSpace(txtSomme.Text))
            {
                MessageBox.Show("Veuillez saisir une somme.");
                return;
            }

            int montant;
            if (!int.TryParse(txtSomme.Text, out montant))
            {
                MessageBox.Show("La somme doit être un nombre.");
                return;
            }

            if (montant <= 0)
            {
                MessageBox.Show("La somme doit être supérieure à 0.");
                return;
            }

            if (montant > budgetRestant)
            {
                MessageBox.Show("La somme dépasse le budget restant.");
                return;
            }

            RichTextBox rtxtAppr = grpContact.Controls["rtxtAppr"] as RichTextBox;
            if (rtxtAppr == null || string.IsNullOrWhiteSpace(rtxtAppr.Text))
            {
                MessageBox.Show("Veuillez saisir une appréciation.");
                return;
            }

            string appreciation = rtxtAppr.Text.Trim();
            DateTimePicker dtmContact = grpContact.Controls["dtmContact"] as DateTimePicker;
            if (dtmContact == null)
            {
                MessageBox.Show("Erreur : date introuvable.");
                return;
            }

            DateTime dateContact = dtmContact.Value.Date;
            Label lblDtStart = grpDetails.Controls["lblDtStart"] as Label;
            if (lblDtStart == null || lblDtStart.Tag == null)
            {
                MessageBox.Show("Date de départ introuvable.");
                return;
            }

            Label lblDtEnd = grpDetails.Controls["lblDtEnd"] as Label;

            if (lblDtEnd == null || lblDtEnd.Tag == null)
            {
                MessageBox.Show("Date de retour introuvable.");
                return;
            }

            if (!dateDansMission(grpDetails, dateContact))
            {
                return;
            }

            ComboBox cmbInformateurs = grpContact.Controls["cmbInformateurs"] as ComboBox;
            if (cmbInformateurs == null || cmbInformateurs.SelectedValue == null)
            {
                MessageBox.Show("Veuillez choisir un informateur.");
                return;
            }

            string nomCodeInformateur = cmbInformateurs.SelectedValue.ToString();

            if (string.IsNullOrWhiteSpace(nomCodeInformateur))
            {
                MessageBox.Show("Veuillez choisir un informateur.");
                return;
            }
            if (contactExiste(dateContact))
            {
                MessageBox.Show("Un contact existe déjà pour cette mission à cette date.");
                return;
            }
            changeDBContact(dateContact, montant, appreciation, nomCodeInformateur);
        }

        // Ajoute un contact, une dépense et un événement de journal.
        private void changeDBContact(DateTime dateContact, int montant, string comm, string nomCodeInformateur)
        {
            SQLiteTransaction trans = null;
            try
            {
                trans = Connexion.Connec.BeginTransaction();
                int numMission = int.Parse(m_numMission);

                // 1. INSERT Contact
                string sqlContact = @"insert into Contact
                                     (nomPlanete, numeroMission, dateC, sommeVersee, appreciation, nomCodeInformateur)
                                     values
                                     (@planet, @mission, @dateContact, @montant, @comm, @nomCodeInformateur)";

                SQLiteCommand cmd = new SQLiteCommand(sqlContact, Connexion.Connec, trans);

                cmd.Parameters.AddWithValue("@planet", m_planet);
                cmd.Parameters.AddWithValue("@mission", numMission);
                cmd.Parameters.AddWithValue("@dateContact", dateContact.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@montant", montant);
                cmd.Parameters.AddWithValue("@comm", comm);
                cmd.Parameters.AddWithValue("@nomCodeInformateur", nomCodeInformateur);
                cmd.ExecuteNonQuery();

                // 2. INSERT Depense
                string motif = "Contact avec informateur " + nomCodeInformateur;
                ajouterDepense(trans, dateContact, montant, motif, 2);

                // 3. INSERT JournalDeBord
                string commentaireJournal = "Contact avec l'informateur " + nomCodeInformateur +
                                            ". Somme versée : " + montant + "$. " +
                                            "Appréciation : " + comm;

                ajouterJournal(trans, dateContact, commentaireJournal);

                trans.Commit();
                MessageBox.Show("Contact, dépense et journal ajoutés avec succès.");
                loadNewMission(m_planet, m_numMission, m_matChef, m_numGlobal);
            }
            catch (Exception err)
            {
                if (trans != null)
                {
                    trans.Rollback();
                }
                MessageBox.Show("Erreur lors du changement de la base de données : " + err.Message);
            }
        }

        // Vérifie si un contact existe déjà à cette date pour la mission.
        private bool contactExiste(DateTime dateContact)
        {
            string sql = @"select count(*)
                           from Contact
                           where nomPlanete = @planet
                           and numeroMission = @mission
                           and dateC = @dateC";

            SQLiteCommand cmd = new SQLiteCommand(sql, Connexion.Connec);

            cmd.Parameters.AddWithValue("@planet", m_planet);
            cmd.Parameters.AddWithValue("@mission", int.Parse(m_numMission));
            cmd.Parameters.AddWithValue("@dateC", dateContact.ToString("yyyy-MM-dd"));

            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        // ============================================================
        // FORMULAIRE DE CAPTURE
        // ============================================================

        // Affiche le formulaire d'ajout d'une capture.
        private void buttonCaptures_Click(object sender, EventArgs e)
        {
            Control grpModification = Controls["grpModification"];
            if (grpModification == null)
            {
                MessageBox.Show("grpModification introuvable.");
                return;
            }

            Control grpEdition = grpModification.Controls["grpEdition"];
            if (grpEdition == null)
            {
                MessageBox.Show("grpEdition introuvable.");
                return;
            }

            Control old = grpModification.Controls["grpFormModif"];
            if (old != null)
            {
                grpModification.Controls.Remove(old);
                old.Dispose();
            }

            if (grpModification != null && grpEdition != null)
            {
                GroupBoxCustom grpFormModif = creerGroupBox("grpFormModif", "Nouvelle capture", 10, grpEdition.Height + 50, grpEdition.Width, grpModification.Height - grpEdition.Height - 60, 16);
                grpModification.Controls.Add(grpFormModif);

                Label lblDate = creerLabel("lblDate", "Date :", 10, 40, 14);
                lblDate.ForeColor = SystemColors.Info;
                grpFormModif.Controls.Add(lblDate);

                DateTimePicker dtmCaptures = new DateTimePicker();
                dtmCaptures.Location = new Point(210, 40);
                dtmCaptures.Name = "dtmCaptures";
                dtmCaptures.Size = new Size(200, 20);
                dtmCaptures.Font = new System.Drawing.Font(dtmCaptures.Font.FontFamily, 14);
                styleDateTimePicker(dtmCaptures);

                grpFormModif.Controls.Add(dtmCaptures);

                Label lblNbCaptures = creerLabel("lblNbCaptures", "Nombre de captures :", 10, 90, 14);
                grpFormModif.Controls.Add(lblNbCaptures);

                TextBox txtCaptures = new TextBox();
                txtCaptures.Location = new Point(210, 90);
                txtCaptures.Name = "txtNbCaptures";
                txtCaptures.Size = new Size(200, 20);
                txtCaptures.Font = new System.Drawing.Font(txtCaptures.Font.FontFamily, 14);
                styleTextBox(txtCaptures);
                txtCaptures.KeyPress += txtSomme_KeyPress;
                grpFormModif.Controls.Add(txtCaptures);

                Label lblComm = creerLabel("lblComm", "Commentaire :", 10, 140, 14);
                lblComm.ForeColor = SystemColors.Info;
                grpFormModif.Controls.Add(lblComm);

                RichTextBox rtxtComm = new RichTextBox();
                rtxtComm.Location = new Point(145, 140);
                rtxtComm.Size = new Size(grpFormModif.Width - 10 - 145, 200);
                rtxtComm.Name = "rtxtComm";
                rtxtComm.Font = new System.Drawing.Font(rtxtComm.Font.FontFamily, 14);
                styleRichTextBox(rtxtComm);
                grpFormModif.Controls.Add(rtxtComm);

                Label lblEspece = creerLabel("lblEspece", "Espèce :", 10, 360, 14);
                lblEspece.ForeColor = SystemColors.Info;
                grpFormModif.Controls.Add(lblEspece);

                ComboBox cmbEspece = new ComboBox();
                cmbEspece.Location = new Point(145, 360);
                cmbEspece.Name = "cmbEspece";
                cmbEspece.Size = new Size(240, 20);
                cmbEspece.Font = new System.Drawing.Font(cmbEspece.Font.FontFamily, 14);
                cmbEspece.DropDownStyle = ComboBoxStyle.DropDownList;

                chargerEspeces(cmbEspece);

                grpFormModif.Controls.Add(cmbEspece);

                Button btnValider = creerButton("btnValider", "Valider", grpFormModif.Width - 90, grpFormModif.Height - 60, 50, 50, btnVCaptures_Click, 14);
                grpFormModif.Controls.Add(btnValider);

                Button btnAnnuler = creerButton("btnAnnuler", "Annuler", grpFormModif.Width - 185, grpFormModif.Height - 60, 50, 50, buttonAnnuler_Click, 14);
                grpFormModif.Controls.Add(btnAnnuler);
            }
        }

        // Charge les espèces ennemies disponibles sur la planète.
        private void chargerEspeces(ComboBox cmb)
        {
            string sql = @"select distinct esp.id, esp.nom, esp.couleur
                           from Espece esp
                           join Ennemi en on en.idEspece = esp.id
                           join Habiter hab on hab.idEspece = esp.id
                           where hab.nomPlanete = @planet
                           order by esp.nom";

            try
            {
                SQLiteCommand cmd = new SQLiteCommand(sql, Connexion.Connec);
                cmd.Parameters.AddWithValue("@planet", m_planet);

                SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                DataTable table = new DataTable();
                da.Fill(table);

                if (table.Rows.Count == 0)
                {
                    MessageBox.Show("Aucune espèce trouvée pour cette planète.");
                    return;
                }

                cmb.DataSource = table;
                cmb.DisplayMember = "nom";
                cmb.ValueMember = "id";

                styleComboBox(cmb);
            }
            catch (Exception err)
            {
                MessageBox.Show("chargerEspeces : " + err.Message);
            }
        }

        // Vérifie les données du formulaire capture avant insertion.
        private void btnVCaptures_Click(object sender, EventArgs e)
        {
            Control grpModification = Controls["grpModification"];
            Control grpDetails = Controls["grpDetails"];

            if (grpModification == null || grpDetails == null)
            {
                MessageBox.Show("Erreur : bloc de modification ou détails introuvable.");
                return;
            }

            Control grpCaptures = grpModification.Controls["grpFormModif"];

            if (grpCaptures == null)
            {
                MessageBox.Show("Erreur : formulaire de capture introuvable.");
                return;
            }

            DateTimePicker dtmCaptures = grpCaptures.Controls["dtmCaptures"] as DateTimePicker;

            if (dtmCaptures == null)
            {
                MessageBox.Show("Erreur : date introuvable.");
                return;
            }

            DateTime dateCapture = dtmCaptures.Value.Date;

            if (!dateDansMission(grpDetails, dateCapture))
            {
                return;
            }

            TextBox txtNbCaptures = grpCaptures.Controls["txtNbCaptures"] as TextBox;

            if (txtNbCaptures == null || string.IsNullOrWhiteSpace(txtNbCaptures.Text))
            {
                MessageBox.Show("Veuillez saisir le nombre de captures.");
                return;
            }

            int nbCaptures;

            if (!int.TryParse(txtNbCaptures.Text, out nbCaptures))
            {
                MessageBox.Show("Le nombre de captures doit être un nombre.");
                return;
            }

            if (nbCaptures <= 0)
            {
                MessageBox.Show("Le nombre de captures doit être supérieur à 0.");
                return;
            }

            ComboBox cmbEspece = grpCaptures.Controls["cmbEspece"] as ComboBox;

            if (cmbEspece == null || cmbEspece.SelectedValue == null)
            {
                MessageBox.Show("Veuillez choisir une espèce.");
                return;
            }

            int idEspece;

            if (!int.TryParse(cmbEspece.SelectedValue.ToString(), out idEspece))
            {
                MessageBox.Show("Espèce invalide.");
                return;
            }

            string nomEspece = cmbEspece.Text;

            RichTextBox rtxtComm = grpCaptures.Controls["rtxtComm"] as RichTextBox;

            string commentaireUser = "";

            if (rtxtComm != null && !string.IsNullOrWhiteSpace(rtxtComm.Text))
            {
                commentaireUser = rtxtComm.Text.Trim();
            }

            changeDBCaptures(dateCapture, idEspece, nomEspece, nbCaptures, commentaireUser);
        }

        // Ajoute des captures et crée un événement dans le journal.
        private void changeDBCaptures(DateTime dateCapture, int idEspece, string nomEspece, int nbCaptures, string commentaireUser)
        {
            SQLiteTransaction trans = null;
            try
            {
                trans = Connexion.Connec.BeginTransaction();
                ajouterOuModifierCapture(trans, idEspece, nbCaptures);

                string commentaireJournal = "Capture de " + nbCaptures + " " + nomEspece + ".";
                if (!string.IsNullOrWhiteSpace(commentaireUser))
                {
                    commentaireJournal += " " + commentaireUser;
                }

                ajouterJournal(trans, dateCapture, commentaireJournal);

                trans.Commit();

                MessageBox.Show("Capture et événement ajoutés avec succès.");
                loadNewMission(m_planet, m_numMission, m_matChef, m_numGlobal);
            }
            catch (Exception err)
            {
                if (trans != null)
                {
                    trans.Rollback();
                }
                MessageBox.Show("Erreur lors de l'ajout de la capture : " + err.Message);
            }
        }

        // Ajoute une nouvelle capture ou met à jour une capture existante.
        private void ajouterOuModifierCapture(SQLiteTransaction trans, int idEspece, int nbCaptures)
        {
            int numMission = int.Parse(m_numMission);

            string sqlExiste = @"select count(*)
                                 from Capturer
                                 where nomPlanete = @planet
                                   and numeroMission = @mission
                                   and idEspeceEnnemi = @idEspece";

            SQLiteCommand cmdExiste = new SQLiteCommand(sqlExiste, Connexion.Connec, trans);
            cmdExiste.Parameters.AddWithValue("@planet", m_planet);
            cmdExiste.Parameters.AddWithValue("@mission", numMission);
            cmdExiste.Parameters.AddWithValue("@idEspece", idEspece);

            int existe = Convert.ToInt32(cmdExiste.ExecuteScalar());

            if (existe > 0)
            {
                string sqlUpdate = @"update Capturer
                                     set nombre = nombre + @nbCaptures
                                     where nomPlanete = @planet
                                       and numeroMission = @mission
                                       and idEspeceEnnemi = @idEspece";

                SQLiteCommand cmdUpdate = new SQLiteCommand(sqlUpdate, Connexion.Connec, trans);
                cmdUpdate.Parameters.AddWithValue("@nbCaptures", nbCaptures);
                cmdUpdate.Parameters.AddWithValue("@planet", m_planet);
                cmdUpdate.Parameters.AddWithValue("@mission", numMission);
                cmdUpdate.Parameters.AddWithValue("@idEspece", idEspece);

                cmdUpdate.ExecuteNonQuery();
            }
            else
            {
                string sqlInsert = @"insert into Capturer
                                     (nomPlanete, numeroMission, idEspeceEnnemi, nombre)
                                     values
                                     (@planet, @mission, @idEspece, @nbCaptures)";

                SQLiteCommand cmdInsert = new SQLiteCommand(sqlInsert, Connexion.Connec, trans);
                cmdInsert.Parameters.AddWithValue("@planet", m_planet);
                cmdInsert.Parameters.AddWithValue("@mission", numMission);
                cmdInsert.Parameters.AddWithValue("@idEspece", idEspece);
                cmdInsert.Parameters.AddWithValue("@nbCaptures", nbCaptures);

                cmdInsert.ExecuteNonQuery();
            }
        }

        // ============================================================
        // BILAN DES CAPTURES
        // ============================================================

        // Crée le nom de la table locale du bilan des captures.
        private string getBilCapPlMission()
        {
            return "BilanCapture" + m_planet + "-" + m_numMission;
        }

        // Crée une table locale contenant le bilan des captures.
        private void creerTableBilanCapture()
        {
            string nomTable = getBilCapPlMission();

            if (MesDatas.DsGlobal.Tables.Contains(nomTable))
            {
                MesDatas.DsGlobal.Tables.Remove(nomTable);
            }

            DataTable table = new DataTable(nomTable);

            table.Columns.Add("Nom de l'espèce", typeof(string));
            table.Columns.Add("Objectif initial", typeof(int));
            table.Columns.Add("Nombre de captures réalisées", typeof(int));
            table.Columns.Add("Taux de réussite (%)", typeof(double));

            string sql = @"select esp.nom, obCap.objectif, ifnull(cap.nombre, 0) as captures
                           from ObjectifCapture obCap
                           join Espece esp on esp.id = obCap.idEspeceEnnemi
                           join Ennemi en on en.idEspece = esp.id
                           join Habiter hab on hab.idEspece = esp.id 
                                           and hab.nomPlanete = obCap.nomPlanete
                           left join Capturer cap on cap.nomPlanete = obCap.nomPlanete and cap.numeroMission = obCap.numeroMission and cap.idEspeceEnnemi = obCap.idEspeceEnnemi
                           where obCap.nomPlanete = @planet and obCap.numeroMission = @numMission
                           order by esp.nom";

            try
            {
                SQLiteCommand cmd = new SQLiteCommand(sql, Connexion.Connec);
                cmd.Parameters.AddWithValue("@planet", m_planet);
                cmd.Parameters.AddWithValue("@numMission", int.Parse(m_numMission));

                SQLiteDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string nomEspece = dr.GetString(0);
                    int objectif = dr.GetInt32(1);
                    int captures = dr.GetInt32(2);

                    double taux = 0;

                    if (objectif > 0)
                    {
                        taux = captures * 100.0 / objectif;
                    }

                    table.Rows.Add(nomEspece, objectif, captures, Math.Round(taux, 2));
                }
                MesDatas.DsGlobal.Tables.Add(table);
            }
            catch (Exception err)
            {
                MessageBox.Show("creerTableBilanCapture : " + err.Message);
            }
        }

        // ============================================================
        // GÉNÉRATION DU PDF
        // ============================================================

        // Lance la génération du PDF après choix du fichier.
        private void buttonGenPdf_Click(object sender, EventArgs e)
        {
            clearFillDB();

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Fichier PDF (*.pdf)|*.pdf";
            sfd.FileName = "Rapport_" + m_planet + "_" + m_numMission + ".pdf";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                genererPdfMission(sfd.FileName);
            }

            return;
        }

        // Génère le rapport PDF complet de la mission.
        private void genererPdfMission(string fileName)
        {
            try
            {
                int numMission = int.Parse(m_numMission);
                Document doc = new Document(PageSize.A4, 40, 40, 40, 40);
                PdfWriter.GetInstance(doc, new FileStream(fileName, FileMode.Create));
                doc.Open();

                iTextSharp.text.Font titreFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18);
                iTextSharp.text.Font sousTitreFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 13);
                iTextSharp.text.Font normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 11);
                iTextSharp.text.Font boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11);

                Paragraph titre = new Paragraph("Rapport de mission", titreFont);
                titre.Alignment = Element.ALIGN_CENTER;
                doc.Add(titre);

                doc.Add(new Paragraph(" "));

                ajouterInfosMissionPdf(doc, normalFont, boldFont, m_planet, numMission);
                ajouterMembresPdf(doc, normalFont, sousTitreFont, m_planet, numMission);
                ajouterJournalPdf(doc, normalFont, sousTitreFont, m_planet, numMission);
                ajouterDepensesPdf(doc, normalFont, sousTitreFont, m_planet, numMission);
                ajouterContactsPdf(doc, normalFont, sousTitreFont, m_planet, numMission);
                ajouterBilanCapturePdf(doc, normalFont, sousTitreFont);
                doc.Close();

                MessageBox.Show("PDF généré avec succès.");
            }
            catch (Exception err)
            {
                MessageBox.Show("Erreur génération PDF : " + err.Message);
            }
        }

        // Ajoute les informations principales de la mission dans le PDF.
        private void ajouterInfosMissionPdf(Document doc, iTextSharp.text.Font normalFont, iTextSharp.text.Font boldFont, string planet, int numMission)
        {
            DataTable tableMission = MesDatas.DsGlobal.Tables["Mission"];
            if (tableMission == null)
            {
                doc.Add(new Paragraph("Table Mission introuvable.", normalFont));
                return;
            }

            string filtre = "nomPlanete = '" + planet.Replace("'", "''") + "' AND numero = " + numMission;
            DataRow[] lignes = tableMission.Select(filtre);
            if (lignes.Length == 0)
            {
                doc.Add(new Paragraph("Mission introuvable.", normalFont));
                return;
            }

            DataRow mission = lignes[0];
            doc.Add(new Paragraph("Mission : " + planet + " - " + numMission, boldFont));
            doc.Add(new Paragraph("Date de départ : " + dateNormalized(mission["dateDepart"].ToString()), normalFont));
            doc.Add(new Paragraph("Date de retour : " + dateNormalized(mission["dateRetour"].ToString()), normalFont));
            doc.Add(new Paragraph("Budget initial : " + mission["budget"].ToString() + " $", normalFont));

            int budget = Convert.ToInt32(mission["budget"]);
            int budgetRestant = budget - soldes(planet, numMission);

            doc.Add(new Paragraph("Budget restant : " + budgetRestant + " $", normalFont));
            doc.Add(new Paragraph("Nombre de membres requis : " + mission["nbMembreRequis"].ToString(), normalFont));
            doc.Add(new Paragraph(" "));
            doc.Add(new Paragraph("Feuille de route :", boldFont));
            doc.Add(new Paragraph(mission["feuilleDeRoute"].ToString(), normalFont));
            doc.Add(new Paragraph(" "));
        }

        // Ajoute la liste des membres dans le PDF.
        private void ajouterMembresPdf(Document doc, iTextSharp.text.Font normalFont, iTextSharp.text.Font boldFont, string planet, int numMission)
        {
            doc.Add(new Paragraph("Liste des membres", boldFont));

            DataTable composer = MesDatas.DsGlobal.Tables["Composer"];
            DataTable membre = MesDatas.DsGlobal.Tables["Membre"];
            DataTable civil = MesDatas.DsGlobal.Tables["Civil"];
            DataTable militaire = MesDatas.DsGlobal.Tables["Militaire"];

            if (composer == null || membre == null)
            {
                doc.Add(new Paragraph("Tables Composer ou Membre introuvables.", normalFont));
                return;
            }

            string filtreComposer = "nomPlanete = '" + planet.Replace("'", "''") + "' AND numeroMission = " + numMission;
            DataRow[] lignesComposer = composer.Select(filtreComposer);
            foreach (DataRow ligneComp in lignesComposer)
            {
                string matricule = ligneComp["matriculeMembre"].ToString();
                DataRow[] lignesMembre = membre.Select("matricule = '" + matricule.Replace("'", "''") + "'");

                if (lignesMembre.Length > 0)
                {
                    DataRow m = lignesMembre[0];
                    string nomComplet = m["nom"].ToString() + " " + m["prenom"].ToString();
                    string type = "Type inconnu";

                    if (civil != null && civil.Select("matriculeMembre = '" + matricule.Replace("'", "''") + "'").Length > 0)
                    {
                        type = "Civil";
                    }
                    else if (militaire != null && militaire.Select("matriculeMembre = '" + matricule.Replace("'", "''") + "'").Length > 0)
                    {
                        type = "Militaire";
                    }
                    doc.Add(new Paragraph("- " + nomComplet + " (" + type + ")", normalFont));
                }
            }
            doc.Add(new Paragraph(" "));
        }

        // Ajoute le journal de bord dans le PDF.
        private void ajouterJournalPdf(Document doc, iTextSharp.text.Font normalFont, iTextSharp.text.Font boldFont, string planet, int numMission)
        {
            doc.Add(new Paragraph("Journal de bord", boldFont));
            DataTable journal = MesDatas.DsGlobal.Tables["JournalDeBord"];

            if (journal == null)
            {
                doc.Add(new Paragraph("Table JournalDeBord introuvable.", normalFont));
                return;
            }

            DataView dv = new DataView(journal);
            dv.RowFilter = "nomPlanete = '" + planet.Replace("'", "''") + "' AND numero = " + numMission;
            dv.Sort = "dateJ ASC";
            foreach (DataRowView row in dv)
            {
                string date = dateNormalized(row["dateJ"].ToString());
                string commentaire = row["commentaires"].ToString();

                doc.Add(new Paragraph("Le " + date + " : " + commentaire, normalFont));
            }
            doc.Add(new Paragraph(" "));
        }

        // Ajoute les dépenses dans le PDF.
        private void ajouterDepensesPdf(Document doc, iTextSharp.text.Font normalFont, iTextSharp.text.Font boldFont, string planet, int numMission)
        {
            doc.Add(new Paragraph("Dépenses effectuées", boldFont));
            DataTable depense = MesDatas.DsGlobal.Tables["Depense"];

            if (depense == null)
            {
                doc.Add(new Paragraph("Table Depense introuvable.", normalFont));
                return;
            }

            DataView dv = new DataView(depense);
            dv.RowFilter = "nomPlanete = '" + planet.Replace("'", "''") + "' AND numeroMission = " + numMission;
            dv.Sort = "dateD ASC";
            int total = 0;

            foreach (DataRowView row in dv)
            {
                string date = dateNormalized(row["dateD"].ToString());
                string motif = row["motif"].ToString();
                int montant = Convert.ToInt32(row["montant"]);

                total += montant;

                doc.Add(new Paragraph("- " + date + " : " + motif + " -> " + montant + " $", normalFont));
            }

            doc.Add(new Paragraph("Total des dépenses : " + total + " $", normalFont));
            doc.Add(new Paragraph(" "));
        }

        // Ajoute les contacts informateurs dans le PDF.
        private void ajouterContactsPdf(Document doc, iTextSharp.text.Font normalFont, iTextSharp.text.Font boldFont, string planet, int numMission)
        {
            doc.Add(new Paragraph("Contacts avec les informateurs", boldFont));
            DataTable contact = MesDatas.DsGlobal.Tables["Contact"];

            if (contact == null)
            {
                doc.Add(new Paragraph("Table Contact introuvable.", normalFont));
                return;
            }

            DataView dv = new DataView(contact);
            dv.RowFilter = "nomPlanete = '" + planet.Replace("'", "''") + "' AND numeroMission = " + numMission;
            dv.Sort = "dateC ASC";
            int total = 0;

            foreach (DataRowView row in dv)
            {
                string date = dateNormalized(row["dateC"].ToString());
                string informateur = row["nomCodeInformateur"].ToString();
                string appreciation = row["appreciation"].ToString();
                int somme = Convert.ToInt32(row["sommeVersee"]);

                total += somme;

                doc.Add(new Paragraph("- " + date + " : " + informateur + " -> " + somme + " $ | " + appreciation, normalFont));
            }
            doc.Add(new Paragraph("Total des sommes versées : " + total + " $", normalFont));
            doc.Add(new Paragraph(" "));
        }

        // Ajoute le bilan des captures dans le PDF.
        private void ajouterBilanCapturePdf(Document doc, iTextSharp.text.Font normalFont, iTextSharp.text.Font titreFont)
        {
            creerTableBilanCapture();

            string nomTable = getBilCapPlMission();

            if (!MesDatas.DsGlobal.Tables.Contains(nomTable))
            {
                doc.Add(new Paragraph("Table bilan des captures introuvable.", normalFont));
                return;
            }

            DataTable table = MesDatas.DsGlobal.Tables[nomTable];

            doc.Add(new Paragraph("Bilan des captures", titreFont));
            doc.Add(new Paragraph(" "));

            PdfPTable pdfTable = new PdfPTable(4);
            pdfTable.WidthPercentage = 100;

            pdfTable.AddCell("Nom de l'espèce");
            pdfTable.AddCell("Objectif initial");
            pdfTable.AddCell("Captures réalisées");
            pdfTable.AddCell("Taux de réussite (%)");

            foreach (DataRow row in table.Rows)
            {
                pdfTable.AddCell(row["Nom de l'espèce"].ToString());
                pdfTable.AddCell(row["Objectif initial"].ToString());
                pdfTable.AddCell(row["Nombre de captures réalisées"].ToString());
                pdfTable.AddCell(row["Taux de réussite (%)"].ToString() + " %");
            }

            doc.Add(pdfTable);
            doc.Add(new Paragraph(" "));
        }

        // ============================================================
        // OUVERTURE DU JOURNAL
        // ============================================================

        // Ouvre la fenêtre du journal après avoir préparé les données.
        private async void buttonOpenJournal_Click(object sender, EventArgs e)
        {
            if (loading)
            {
                return;
            }

            Button btn = sender as Button;
            if (btn != null)
            {
                btn.Enabled = false;
            }

            try
            {
                await ShowLoadingDuringAction("Ouverture du journal", () =>
                {
                    clearFillDB();
                    creerTableBilanCapture();
                }, 1000);

                frmJournal frmJ = new frmJournal(m_planet, m_numMission, m_matChef, m_numGlobal, getBilCapPlMission());

                frmJ.ShowDialog();
            }
            catch (Exception err)
            {
                MessageBox.Show("Erreur ouverture journal : " + err.Message);
            }
            finally
            {
                if (btn != null)
                {
                    btn.Enabled = true;
                }
            }
        }

        // ============================================================
        // NAVIGATION ENTRE FORMES
        // ============================================================

        // Retourne à la page d'accueil sans fermer définitivement la fenêtre.
        private void buttonExit_Click(object sender, EventArgs e)
        {
            if (modificationEnCours())
            {
                DialogResult result = MessageBox.Show(
                    "Vous avez des modifications non enregistrées. Quitter sans enregistrer ?",
                    "Modifications non enregistrées",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.No)
                {
                    return;
                }
                fermerBlocModification();
            }
            if (this.Owner != null)
            {
                this.Owner.Show();
                this.Owner.WindowState = FormWindowState.Maximized;
                this.Owner.BringToFront();
                this.Owner.Activate();
            }

            this.Hide();
        }

        // Gère la fermeture par la croix et réaffiche l'accueil.
        private void frmTableauDeBord_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (modificationEnCours())
                {
                    DialogResult result = MessageBox.Show(
                        "Vous avez des modifications non enregistrées. Quitter sans enregistrer ?",
                        "Modifications non enregistrées",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    );

                    if (result == DialogResult.No)
                    {
                        e.Cancel = true;
                        return;
                    }

                    fermerBlocModification();
                }

                if (this.Owner != null)
                {
                    this.Owner.Show();
                    this.Owner.WindowState = FormWindowState.Maximized;
                    this.Owner.BringToFront();
                    this.Owner.Activate();
                }

                e.Cancel = true;
                this.Hide();
            }
        }

        // ============================================================
        // VALIDATIONS ET OUTILS
        // ============================================================

        // Vérifie que la date choisie est comprise dans la durée de la mission.
        private bool dateDansMission(Control grpDetails, DateTime dateChoisie)
        {
            Label lblDtStart = grpDetails.Controls["lblDtStart"] as Label;
            Label lblDtEnd = grpDetails.Controls["lblDtEnd"] as Label;

            if (lblDtStart == null || lblDtStart.Tag == null || lblDtEnd == null || lblDtEnd.Tag == null)
            {
                MessageBox.Show("Dates de mission introuvables.");
                return false;
            }

            DateTime dtDepart = convertirDate(lblDtStart.Tag.ToString());
            DateTime dtRetour = convertirDate(lblDtEnd.Tag.ToString());

            if (dateChoisie < dtDepart || dateChoisie > dtRetour)
            {
                MessageBox.Show("La date doit être comprise entre la date de départ et la date de retour de la mission.");
                return false;
            }

            return true;
        }

        // Récupère le budget restant depuis le label affiché.
        private bool getBudgetRestant(Control grpDetails, out int budgetRestant)
        {
            budgetRestant = 0;

            Label lblBudgetReste = grpDetails.Controls["lblBudgetReste"] as Label;

            if (lblBudgetReste == null || lblBudgetReste.Tag == null)
            {
                MessageBox.Show("Erreur : budget restant introuvable.");
                return false;
            }

            if (!int.TryParse(lblBudgetReste.Tag.ToString(), out budgetRestant))
            {
                MessageBox.Show("Erreur : budget restant invalide.");
                return false;
            }

            return true;
        }

        // Autorise uniquement la saisie de chiffres dans certains TextBox.
        private void txtSomme_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            if (e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = false;
            }
        }

        // Charge une image depuis le dossier de l'application.
        // Si elle n'existe pas, l'image par défaut est utilisée.
        private System.Drawing.Image chargerImage(string cheminRelatif)
        {
            string chemin = Path.Combine(Application.StartupPath, cheminRelatif);

            if (!File.Exists(chemin))
            {
                chemin = Path.Combine(Application.StartupPath, "images/default.png");
            }

            if (!File.Exists(chemin))
            {
                return null;
            }

            using (FileStream fs = new FileStream(chemin, FileMode.Open, FileAccess.Read))
            {
                return System.Drawing.Image.FromStream(fs);
            }
        }

        // Convertit une date en format français pour l'affichage.
        private string dateNormalized(string date)
        {
            try
            {
                return convertirDate(date).ToString("dd/MM/yyyy");
            }
            catch
            {
                return date;
            }
        }

        // Convertit une date texte en DateTime.
        // Plusieurs formats sont acceptés.
        private DateTime convertirDate(string date)
        {
            DateTime dt;
            string[] formats = { "yyyy-MM-dd", "dd/MM/yyyy", "dd.MM.yyyy" };

            if (DateTime.TryParseExact(
                date,
                formats,
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None,
                out dt))
            {
                return dt;
            }
            throw new Exception("Format de date invalide : " + date);
        }
    }   
}