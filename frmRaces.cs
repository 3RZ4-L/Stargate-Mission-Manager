using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace projetStarGate
{
    public partial class frmRaces : Form
    {
        public frmRaces()
        {
            InitializeComponent();
        }

        private void frmRaces_Load(object sender, EventArgs e)
        {
            this.Text = "Découverte des races extraterrestres";
            this.WindowState = FormWindowState.Maximized;
            ChargerDonnees();   // requêtes SQL -> DataSet
            RemplirFiltres();   // remplit les combobox couleur et type
            AfficherCartes();   // affiche les cartes
        }

        private void ChargerDonnees()
        {
            // On ne charge qu'une fois (le DataSet est global)
            if (!MesDatas.DsGlobal.Tables.Contains("Races"))
            {
                // CASE WHEN : on déduit le type (Alliée/Ennemie) selon la table où l'espèce existe
                string sql = "SELECT e.id AS Id, e.nom AS NomRace, e.couleur AS Couleur, " +
                             "CASE WHEN a.idEspece IS NOT NULL THEN 'Alliée' " +
                             "WHEN n.idEspece IS NOT NULL THEN 'Ennemie' " +
                             "ELSE 'Inconnue' END AS TypeRace, " +
                             "a.degreBienveillance AS Bienveillance, " +
                             "a.instrumentMusique AS Instrument, " +
                             "a.datePremierContact AS DateContact, " +
                             "n.typeArme AS TypeArme, " +
                             "n.degreAgressivite AS Agressivite " +
                             "FROM Espece e " +
                             "LEFT JOIN Allie a ON a.idEspece = e.id " +
                             "LEFT JOIN Ennemi n ON n.idEspece = e.id " +
                             "ORDER BY e.nom";
                MesDatas.ChargerTable("Races", sql);
            }

            if (!MesDatas.DsGlobal.Tables.Contains("EspecePlanetes"))
            {
                // GROUP_CONCAT : regroupe toutes les planètes d'une espèce en une seule chaîne
                string sql = "SELECT h.idEspece, GROUP_CONCAT(h.nomPlanete, '/') AS Planetes " +
                             "FROM Habiter h GROUP BY h.idEspece";
                MesDatas.ChargerTable("EspecePlanetes", sql);
            }
        }

        private void RemplirFiltres()
        {
            // Filtre couleur : "Toutes" + une fois chaque couleur trouvée
            cmbCouleur.Items.Add("Toutes");
            foreach (DataRow row in MesDatas.DsGlobal.Tables["Races"].Rows)
            {
                string c = row["Couleur"].ToString();
                if (!cmbCouleur.Items.Contains(c)) // évite les doublons
                    cmbCouleur.Items.Add(c);
            }
            cmbCouleur.SelectedIndex = 0;

            // Filtre type
            cmbType.Items.Add("Tous");
            cmbType.Items.Add("Alliée");
            cmbType.Items.Add("Ennemie");
            cmbType.SelectedIndex = 0;
        }

        private void AfficherCartes()
        {
            flpRaces.Controls.Clear(); // on vide avant de réafficher

            foreach (DataRow row in MesDatas.DsGlobal.Tables["Races"].Rows)
            {
                string nom = row["NomRace"].ToString();
                string couleur = row["Couleur"].ToString();
                string type = row["TypeRace"].ToString();

                // Filtre par nom (recherche "contient")
                if (txtNom.Text.Trim().Length > 0 &&
                    !nom.ToLower().Contains(txtNom.Text.Trim().ToLower()))
                    continue;

                // Filtre par couleur
                if (cmbCouleur.SelectedItem?.ToString() != "Toutes" &&
                    couleur != cmbCouleur.SelectedItem?.ToString())
                    continue;

                // Filtre par type
                if (cmbType.SelectedItem?.ToString() != "Tous" &&
                    type != cmbType.SelectedItem?.ToString())
                    continue;

                flpRaces.Controls.Add(CreerCarte(row));
            }

            // Compteur "trouvées / total"
            Espece_Trouver.Text = $"Espèces trouvées : {flpRaces.Controls.Count} / {MesDatas.DsGlobal.Tables["Races"].Rows.Count}";
        }

        private Panel CreerCarte(DataRow row)
        {
            int id = Convert.ToInt32(row["Id"]);
            string nom = row["NomRace"].ToString();
            string couleur = row["Couleur"].ToString();
            string type = row["TypeRace"].ToString();

            // Récupère les planètes de l'espèce
            DataRow[] lignesPlanetes = MesDatas.DsGlobal.Tables["EspecePlanetes"]
                                       .Select($"IdEspece = {id}");
            string planetes = lignesPlanetes.Length > 0
                              ? lignesPlanetes[0]["Planetes"].ToString()
                              : "";

            // Vert = allié, rouge = ennemi
            Color couleurType = (type == "Alliée") ? Color.LimeGreen : Color.OrangeRed;
            Color fond = Color.FromArgb(70, 70, 70);
            Color survol = Color.FromArgb(90, 90, 90);

            // --- CARTE ---
            // Tag = row : permet de retrouver l'espèce au clic
            Panel carte = new Panel
            {
                Width = 220,
                Height = 300,
                BackColor = fond,
                BorderStyle = BorderStyle.None,
                Margin = new Padding(10),
                Cursor = Cursors.Hand,
                Tag = row
            };

            // Dessine la bordure colorée autour de la carte
            carte.Paint += (s, e) =>
            {
                using (Pen pen = new Pen(couleurType, 3))
                    e.Graphics.DrawRectangle(pen, 1, 1, carte.Width - 3, carte.Height - 3);
            };

            // Effet survol : la carte s'éclaircit
            carte.MouseEnter += (s, e) => carte.BackColor = survol;
            carte.MouseLeave += (s, e) => carte.BackColor = fond;
            carte.Click += Carte_Click;

            // --- IMAGE ---
            PictureBox pb = new PictureBox
            {
                Width = 150,
                Height = 150,
                Left = 35,
                Top = 10,
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = ChargerImage(couleur),
                Tag = row,
                Cursor = Cursors.Hand
            };
            pb.MouseEnter += (s, e) => carte.BackColor = survol;
            pb.MouseLeave += (s, e) => carte.BackColor = fond;
            pb.Click += Carte_Click;
            carte.Controls.Add(pb);

            // --- NOM ---
            Label lblNom = new Label
            {
                Text = nom,
                Font = new Font("Segoe UI", 12f, FontStyle.Bold),
                ForeColor = System.Drawing.SystemColors.Info,
                Left = 5,
                Top = 168,
                Width = 210,
                Height = 30,
                TextAlign = ContentAlignment.MiddleCenter,
                Tag = row,
                Cursor = Cursors.Hand
            };
            lblNom.MouseEnter += (s, e) => carte.BackColor = survol;
            lblNom.MouseLeave += (s, e) => carte.BackColor = fond;
            lblNom.Click += Carte_Click;
            carte.Controls.Add(lblNom);

            // --- BADGE TYPE ---
            Label lblType = new Label
            {
                Text = type,
                Font = new Font("Segoe UI", 11f),
                ForeColor = Color.Black,
                BackColor = couleurType,
                Left = 35,
                Top = 203,
                Width = 150,
                Height = 28,
                TextAlign = ContentAlignment.MiddleCenter,
                Tag = row,
                Cursor = Cursors.Hand
            };
            lblType.MouseEnter += (s, e) => carte.BackColor = survol;
            lblType.MouseLeave += (s, e) => carte.BackColor = fond;
            lblType.Click += Carte_Click;
            carte.Controls.Add(lblType);

            // --- PLANÈTE ---
            bool origineInconnue = string.IsNullOrEmpty(planetes);
            Label lblPlanete = new Label
            {
                Text = origineInconnue ? "Origine inconnue" : planetes,
                Font = new Font("Segoe UI", 11f,
                            origineInconnue ? FontStyle.Italic : FontStyle.Regular),
                ForeColor = origineInconnue ? Color.OrangeRed : Color.LightGray,
                Left = 5,
                Top = 238,
                Width = 210,
                Height = 35,
                TextAlign = ContentAlignment.MiddleCenter,
                Tag = row,
                Cursor = Cursors.Hand
            };
            lblPlanete.MouseEnter += (s, e) => carte.BackColor = survol;
            lblPlanete.MouseLeave += (s, e) => carte.BackColor = fond;
            lblPlanete.Click += Carte_Click;
            carte.Controls.Add(lblPlanete);

            return carte;
        }

        private void Carte_Click(object sender, EventArgs e)
        {
            // On remonte les parents jusqu'à trouver le contrôle qui a la DataRow
            // (utile car on peut cliquer sur l'image, le nom, le badge...)
            Control ctrl = (Control)sender;
            while (ctrl != null && !(ctrl.Tag is DataRow))
                ctrl = ctrl.Parent;

            if (ctrl == null) return;

            // Ouvre le détail de l'espèce en popup
            DataRow row = (DataRow)ctrl.Tag;
            frmDetailRace f = new frmDetailRace(row);
            f.ShowDialog();
        }

        // Charge l'image de l'espèce, sinon dessine un rond avec un "?"
        private Image ChargerImage(string couleur)
        {
            string chemin = Path.Combine(
                Application.StartupPath, "images", "especes", $"{couleur}.png");

            if (File.Exists(chemin))
                return Image.FromFile(chemin);

            Bitmap bmp = new Bitmap(150, 150);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.FillEllipse(Brushes.LightGray, 10, 10, 130, 130);
                g.DrawString("?", new Font("Arial", 45, FontStyle.Bold),
                             Brushes.White, new PointF(40, 35));
            }
            return bmp;
        }

        // Le bouton recherche relance l'affichage avec les filtres
        private void btnRechercher_Click(object sender, EventArgs e) { AfficherCartes(); }
        private void btnRetour_Click_1(object sender, EventArgs e) { this.Close(); }

        // Réinitialise les filtres et réaffiche tout
        private void res_Click(object sender, EventArgs e)
        {
            txtNom.Text = "";
            cmbCouleur.SelectedIndex = 0;
            cmbType.SelectedIndex = 0;
            AfficherCartes();
        }

        // N'autorise que les lettres et la touche Retour dans la recherche
        private void txtNom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                e.Handled = true;
        }

        // Bloque le copier/coller (Ctrl+C/V/X/A) dans la recherche
        private void txtNom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.V ||
                               e.KeyCode == Keys.X || e.KeyCode == Keys.A))
                e.SuppressKeyPress = true;
        }

        // Empêche de taper du texte dans les combobox
        private void cmbCouleur_KeyPress(object sender, KeyPressEventArgs e) { e.Handled = true; }
        private void cmbType_KeyPress(object sender, KeyPressEventArgs e) { e.Handled = true; }
    }
}