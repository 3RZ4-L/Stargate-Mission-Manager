using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace projetStarGate
{
    public partial class frmPlanetes : Form
    {
        public frmPlanetes()
        {
            InitializeComponent();
        }

        private void frmPlanetes_Load(object sender, EventArgs e)
        {
            this.Text = "Informations sur les planètes";
            this.WindowState = FormWindowState.Maximized;

            // Splitter fixe : on règle sa position une fois la fenêtre affichée (65% à gauche)
            splitContainer1.IsSplitterFixed = true;
            this.Shown += (s, ev) => splitContainer1.SplitterDistance = (int)(this.ClientSize.Width * 0.65);
            pbDetailPlanete.SizeMode = PictureBoxSizeMode.Zoom;

            // 1) Charger les données en premier
            ChargerDonnees();

            // 2) Remplir la combobox
            cmbFiltreDataBaz.Items.Add("Toutes les planètes");
            cmbFiltreDataBaz.Items.Add("Avec DataBaz");
            cmbFiltreDataBaz.Items.Add("Sans DataBaz");
            cmbFiltreDataBaz.SelectedIndex = 0;

            // 3) Afficher les cartes
            AfficherCartes("Toutes les planètes");

            // 4) Vider le panneau droit
            ViderPanneauDetail();
        }

        private void ChargerDonnees()
        {
            // Liste des planètes
            if (!MesDatas.DsGlobal.Tables.Contains("Planetes"))
            {
                string sql = "SELECT nom AS NomPlanete, temperature AS Temperature, " +
                             "gravite AS Gravite, dataBazON AS DataBaz " +
                             "FROM Planete ORDER BY nom";
                MesDatas.ChargerTable("Planetes", sql);
            }

            // Espèces présentes sur chaque planète (avec leur type allié/ennemi)
            if (!MesDatas.DsGlobal.Tables.Contains("EspecesPlanete"))
            {
                string sql = "SELECT h.nomPlanete AS NomPlanete, e.nom AS NomEspece, " +
                             "h.pourcentage AS Pourcentage, " +
                             "CASE WHEN a.idEspece IS NOT NULL THEN 'Alliée' " +
                             "WHEN n.idEspece IS NOT NULL THEN 'Ennemie' " +
                             "ELSE 'Inconnue' END AS TypeEspece " +
                             "FROM Habiter h " +
                             "JOIN Espece e ON e.id = h.idEspece " +
                             "LEFT JOIN Allie a ON a.idEspece = e.id " +
                             "LEFT JOIN Ennemi n ON n.idEspece = e.id " +
                             "ORDER BY h.nomPlanete, h.pourcentage DESC";
                MesDatas.ChargerTable("EspecesPlanete", sql);
            }

            // Missions effectuées sur chaque planète
            if (!MesDatas.DsGlobal.Tables.Contains("MissionsPlanete"))
            {
                string sql = "SELECT m.nomPlanete AS NomPlanete, m.numero AS Numero, " +
                             "m.dateDepart AS DateDepart, m.dateRetour AS DateRetour, " +
                             "m.budget AS Budget " +
                             "FROM Mission m ORDER BY m.nomPlanete, m.numero";
                MesDatas.ChargerTable("MissionsPlanete", sql);
            }
        }

        private void AfficherCartes(string filtre)
        {
            flpPlanetes.Controls.Clear();

            foreach (DataRow row in MesDatas.DsGlobal.Tables["Planetes"].Rows)
            {
                // DataBaz vaut 1 si la planète en possède
                bool aDataBaz = row["DataBaz"] != DBNull.Value
                                && Convert.ToInt32(row["DataBaz"]) == 1;

                // On saute les planètes qui ne correspondent pas au filtre
                if (filtre == "Avec DataBaz" && !aDataBaz) continue;
                if (filtre == "Sans DataBaz" && aDataBaz) continue;

                flpPlanetes.Controls.Add(CreerCarte(row));
            }
        }

        private Panel CreerCarte(DataRow row)
        {
            string nom = row["NomPlanete"].ToString();
            string temp = row["Temperature"]?.ToString() ?? "?";
            string gravite = row["Gravite"]?.ToString() ?? "?";
            bool aDataBaz = row["DataBaz"] != DBNull.Value
                             && Convert.ToInt32(row["DataBaz"]) == 1;

            Color fond = Color.FromArgb(70, 70, 70);
            Color survol = Color.FromArgb(90, 90, 90);

            // Tag = nom : permet de retrouver la planète au clic
            Panel carte = new Panel
            {
                Width = 220,
                Height = 300,
                BackColor = fond,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(10),
                Cursor = Cursors.Hand,
                Tag = nom
            };
            carte.MouseEnter += (s, e) => carte.BackColor = survol;
            carte.MouseLeave += (s, e) => carte.BackColor = fond;
            carte.Click += Carte_Click;

            PictureBox pb = new PictureBox
            {
                Width = 150,
                Height = 150,
                Left = 35,
                Top = 10,
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = ChargerImage(nom),
                Tag = nom
            };
            pb.Click += Carte_Click;
            pb.MouseEnter += (s, e) => carte.BackColor = survol;
            pb.MouseLeave += (s, e) => carte.BackColor = fond;
            carte.Controls.Add(pb);

            Label lblNom = new Label
            {
                Text = nom,
                Font = new Font("Segoe UI", 12f, FontStyle.Bold),
                ForeColor = Color.White,
                Left = 5,
                Top = 168,
                Width = 210,
                Height = 30,
                TextAlign = ContentAlignment.MiddleCenter,
                Tag = nom
            };
            lblNom.Click += Carte_Click;
            lblNom.MouseEnter += (s, e) => carte.BackColor = survol;
            lblNom.MouseLeave += (s, e) => carte.BackColor = fond;
            carte.Controls.Add(lblNom);

            Label lblTemp = new Label
            {
                Text = $"🌡 {temp}°",
                Font = new Font("Segoe UI", 11f),
                ForeColor = Color.LightGray,
                Left = 5,
                Top = 203,
                Width = 210,
                Height = 28,
                TextAlign = ContentAlignment.MiddleLeft,
                Tag = nom
            };
            lblTemp.Click += Carte_Click;
            lblTemp.MouseEnter += (s, e) => carte.BackColor = survol;
            lblTemp.MouseLeave += (s, e) => carte.BackColor = fond;
            carte.Controls.Add(lblTemp);

            Label lblGrav = new Label
            {
                Text = $"Gravité : {gravite}",
                Font = new Font("Segoe UI", 11f),
                ForeColor = Color.LightGray,
                Left = 5,
                Top = 235,
                Width = 210,
                Height = 28,
                TextAlign = ContentAlignment.MiddleLeft,
                Tag = nom
            };
            lblGrav.Click += Carte_Click;
            lblGrav.MouseEnter += (s, e) => carte.BackColor = survol;
            lblGrav.MouseLeave += (s, e) => carte.BackColor = fond;
            carte.Controls.Add(lblGrav);

            // Vert si DataBaz présent, rouge sinon
            Label lblDataBaz = new Label
            {
                Text = aDataBaz ? "Présence de Databaz" : "Pas de Databaz",
                Font = new Font("Segoe UI", 11f, FontStyle.Bold),
                ForeColor = aDataBaz ? Color.LimeGreen : Color.OrangeRed,
                Left = 5,
                Top = 265,
                Width = 210,
                Height = 28,
                TextAlign = ContentAlignment.MiddleCenter,
                Tag = nom
            };
            lblDataBaz.Click += Carte_Click;
            lblDataBaz.MouseEnter += (s, e) => carte.BackColor = survol;
            lblDataBaz.MouseLeave += (s, e) => carte.BackColor = fond;
            carte.Controls.Add(lblDataBaz);

            return carte;
        }

        private void Carte_Click(object sender, EventArgs e)
        {
            // Le Tag contient le nom de la planète cliquée
            string nomPlanete = ((Control)sender).Tag?.ToString() ?? "";
            if (nomPlanete == "") return;
            AfficherDetail(nomPlanete);
        }

        private void AfficherDetail(string nomPlanete)
        {
            // Replace("'", "''") : protège si le nom contient une apostrophe
            DataRow[] resultats = MesDatas.DsGlobal.Tables["Planetes"]
                                  .Select($"NomPlanete = '{nomPlanete.Replace("'", "''")}'");

            if (resultats.Length == 0) return;
            DataRow planete = resultats[0];

            bool aDataBaz = planete["DataBaz"] != DBNull.Value
                            && Convert.ToInt32(planete["DataBaz"]) == 1;

            // --- Remplir les labels du panneau droit ---
            lblNomPlanete.Text = nomPlanete;
            lblNomPlanete.Font = new Font("Segoe UI", 20f, FontStyle.Bold);

            lblTempDetail.Text = $"🌡 Température : {planete["Temperature"]}°";
            lblTempDetail.Font = new Font("Segoe UI", 13f);

            lblGraviteDetail.Text = $"Gravité : {planete["Gravite"]}";
            lblGraviteDetail.Font = new Font("Segoe UI", 13f);

            lblDataBazDetail.Text = aDataBaz ? "✅ Présence de Databaz" : "❌ Pas de Databaz";
            lblDataBazDetail.Font = new Font("Segoe UI", 13f, FontStyle.Bold);
            lblDataBazDetail.ForeColor = aDataBaz ? Color.LimeGreen : Color.OrangeRed;

            pbDetailPlanete.Image = ChargerImage(nomPlanete);

            // --- ESPÈCES (tableau du bas) ---
            DataRow[] especes = MesDatas.DsGlobal.Tables["EspecesPlanete"]
                                .Select($"NomPlanete = '{nomPlanete.Replace("'", "''")}'");

            DataTable tableEspeces = new DataTable();
            tableEspeces.Columns.Add("Espèce");
            tableEspeces.Columns.Add("Type");
            tableEspeces.Columns.Add("Présence");

            foreach (DataRow r in especes)
                tableEspeces.Rows.Add(r["NomEspece"], r["TypeEspece"], r["Pourcentage"] + "%");
            dgvEspeces.RowTemplate.Height = 35; // hauteur des lignes (à mettre avant DataSource)
            dgvEspeces.DataSource = tableEspeces;

            // Couleurs sombres du tableau
            dgvEspeces.BackgroundColor = Color.FromArgb(70, 70, 70);
            dgvEspeces.DefaultCellStyle.BackColor = Color.FromArgb(70, 70, 70);
            dgvEspeces.DefaultCellStyle.ForeColor = Color.White;
            dgvEspeces.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(90, 90, 90);
            dgvEspeces.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvEspeces.EnableHeadersVisualStyles = false;

            // Sélection = mêmes couleurs (sinon le texte devient illisible au clic)
            dgvEspeces.DefaultCellStyle.SelectionBackColor = dgvEspeces.DefaultCellStyle.BackColor;
            dgvEspeces.DefaultCellStyle.SelectionForeColor = dgvEspeces.DefaultCellStyle.ForeColor;

            dgvEspeces.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEspeces.Font = new Font("Segoe UI", 13f);
            dgvEspeces.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 13f, FontStyle.Bold);

            // Vert pour les alliées, rouge pour le reste
            foreach (DataGridViewRow r in dgvEspeces.Rows)
            {
                string type = r.Cells["Type"]?.Value?.ToString() ?? "";
                r.DefaultCellStyle.ForeColor = (type == "Alliée") ? Color.LimeGreen : Color.OrangeRed;
            }

            // --- MISSIONS (tableau du bas) ---
            DataRow[] missions = MesDatas.DsGlobal.Tables["MissionsPlanete"]
                                 .Select($"NomPlanete = '{nomPlanete.Replace("'", "''")}'");

            DataTable tableMissions = new DataTable();
            tableMissions.Columns.Add("N°");
            tableMissions.Columns.Add("Départ");
            tableMissions.Columns.Add("Retour");
            tableMissions.Columns.Add("Budget");

            foreach (DataRow r in missions)
                tableMissions.Rows.Add(r["Numero"], r["DateDepart"], r["DateRetour"], r["Budget"] + " €");
            dgvMissions.RowTemplate.Height = 35;
            dgvMissions.DataSource = tableMissions;
            dgvMissions.BackgroundColor = Color.FromArgb(70, 70, 70);
            dgvMissions.DefaultCellStyle.BackColor = Color.FromArgb(70, 70, 70);
            dgvMissions.DefaultCellStyle.ForeColor = Color.White;
            dgvMissions.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(90, 90, 90);
            dgvMissions.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvMissions.EnableHeadersVisualStyles = false;
            dgvMissions.DefaultCellStyle.SelectionBackColor = dgvMissions.DefaultCellStyle.BackColor;
            dgvMissions.DefaultCellStyle.SelectionForeColor = dgvMissions.DefaultCellStyle.ForeColor;
            dgvMissions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMissions.Font = new Font("Segoe UI", 13f);
            dgvMissions.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 13f, FontStyle.Bold);

            // Titre du groupe avec le nombre de missions
            grpMissions.Text = missions.Length == 0
                ? "Missions (aucune)"
                : $"Missions ({missions.Length})";
            grpMissions.Font = new Font("Segoe UI", 12f, FontStyle.Bold);
            grpEspeces.Font = new Font("Segoe UI", 12f, FontStyle.Bold);
        }

        // Remet le panneau droit à zéro (avant qu'on clique sur une planète)
        private void ViderPanneauDetail()
        {
            lblNomPlanete.Text = "← Cliquez sur une planète";
            lblTempDetail.Text = "";
            lblGraviteDetail.Text = "";
            lblDataBazDetail.Text = "";
            pbDetailPlanete.Image = null;
            dgvEspeces.DataSource = null;
            dgvMissions.DataSource = null;
        }

        // Charge l'image de la planète, sinon dessine un rond avec son initiale
        private Image ChargerImage(string nomPlanete)
        {
            string chemin = Path.Combine(
                Application.StartupPath, "images", "planetes", $"{nomPlanete}.png");

            if (File.Exists(chemin))
                return Image.FromFile(chemin);

            Bitmap bmp = new Bitmap(150, 150);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.FillEllipse(Brushes.SlateBlue, 5, 5, 140, 140);
                string initiale = nomPlanete.Length > 0 ? nomPlanete[0].ToString() : "?";
                g.DrawString(initiale, new Font("Arial", 45, FontStyle.Bold),
                             Brushes.White, new PointF(35, 30));
            }
            return bmp;
        }

        // Le filtre relance l'affichage des cartes
        private void cmbFiltreDataBaz_SelectedIndexChanged(object sender, EventArgs e)
        {
            AfficherCartes(cmbFiltreDataBaz.SelectedItem.ToString());
        }

        // Empêche de taper du texte dans le filtre
        private void cmbFiltreDataBaz_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnRetour_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void flpPlanetes_Paint(object sender, PaintEventArgs e) { }

        private void dgvEspeces_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = false;
        }
    }
}