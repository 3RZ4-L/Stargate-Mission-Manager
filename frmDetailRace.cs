using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace projetStarGate
{
    public partial class frmDetailRace : Form
    {
        // La ligne de l'espèce sur laquelle on a cliqué dans frmRaces
        private DataRow _race;

        public frmDetailRace(DataRow race)
        {
            InitializeComponent();
            _race = race; // on garde l'espèce pour l'afficher dans le Load
        }

        private void frmDetailRace_Load(object sender, EventArgs e)
        {
            // On récupère les infos de l'espèce
            string nom = _race["NomRace"].ToString();
            string couleur = _race["Couleur"].ToString();
            string type = _race["TypeRace"].ToString();
            int id = Convert.ToInt32(_race["Id"]);

            // Réglages de la fenêtre (taille fixe, centrée, pas de maximize)
            this.Text = $"Espèce : {nom}";
            this.Size = new Size(800, 700);
            this.MaximumSize = new Size(800, 700);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.FromArgb(56, 56, 56);

            // Vert si alliée, rouge si ennemie
            Color couleurTheme = (type == "Alliée")
                ? Color.FromArgb(0, 180, 90)
                : Color.FromArgb(210, 60, 50);

            // --- TITRE ---
            panelTitre.BackColor = couleurTheme;
            panelTitre.Height = 90;

            Label lblTitre = new Label
            {
                Text = $"➤ {nom}",
                Font = new Font("Segoe UI", 24f, FontStyle.Bold),
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            panelTitre.Controls.Add(lblTitre);

            // --- IMAGE ---
            panelContenu.Padding = new Padding(20);

            PictureBox pb = new PictureBox
            {
                Width = 180,
                Height = 180,
                Left = (panelContenu.Width - 180) / 2, // centré horizontalement
                Top = 10,
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = ChargerImage(couleur)
            };
            panelContenu.Controls.Add(pb);

            // --- INFOS ---
            // posY descend à chaque info ajoutée pour les empiler
            int posY = 205;

            AjouterSeparateur(panelContenu, posY, couleurTheme);
            posY += 20;

            AjouterInfo(panelContenu, "🎨  Couleur", couleur, posY, couleurTheme);
            posY += 50;

            AjouterInfo(panelContenu, "🌍  Planètes", ObtenirPlanetes(id), posY, couleurTheme);
            posY += 50;

            AjouterSeparateur(panelContenu, posY, couleurTheme);
            posY += 20;

            // Infos différentes selon allié ou ennemi
            if (type == "Alliée")
            {
                AjouterInfo(panelContenu, "🤝  Bienveillance",
                    _race["Bienveillance"]?.ToString() ?? "?", posY, couleurTheme);
                posY += 50;

                AjouterInfo(panelContenu, "📅  Premier contact",
                    _race["DateContact"]?.ToString() ?? "?", posY, couleurTheme);
                posY += 50;

                AjouterInfo(panelContenu, "🎵  Instrument favori",
                    _race["Instrument"]?.ToString() ?? "?", posY, couleurTheme);
            }
            else if (type == "Ennemie")
            {
                AjouterInfo(panelContenu, "⚔️  Type d'arme",
                    _race["TypeArme"]?.ToString() ?? "?", posY, couleurTheme);
                posY += 50;

                AjouterInfo(panelContenu, "💢  Agressivité",
                    _race["Agressivite"]?.ToString() ?? "?", posY, couleurTheme);
            }

            // --- BOUTON FERMER ---
            btnFermer.BackColor = couleurTheme;
            btnFermer.ForeColor = Color.White;
            btnFermer.Font = new Font("Segoe UI", 13f, FontStyle.Bold);
            btnFermer.FlatStyle = FlatStyle.Flat;
            btnFermer.FlatAppearance.BorderSize = 0;
            btnFermer.Cursor = Cursors.Hand;
            btnFermer.Height = 55;
            btnFermer.Text = "Fermer";
        }

        // Ajoute une ligne "titre : valeur" à la position posY
        private void AjouterInfo(Panel parent, string titre, string valeur, int posY, Color couleur)
        {
            Label lblTitre = new Label
            {
                Text = titre,
                Font = new Font("Segoe UI", 12f, FontStyle.Bold),
                ForeColor = couleur,
                Left = 20,
                Top = posY,
                Width = 200,
                Height = 35,
                TextAlign = ContentAlignment.MiddleLeft
            };
            parent.Controls.Add(lblTitre);

            Label lblValeur = new Label
            {
                Text = valeur,
                Font = new Font("Segoe UI", 12f),
                ForeColor = System.Drawing.SystemColors.Info,
                Left = 230,
                Top = posY,
                Width = 520,
                Height = 35,
                TextAlign = ContentAlignment.MiddleLeft
            };
            parent.Controls.Add(lblValeur);
        }

        // Petite barre de séparation colorée
        private void AjouterSeparateur(Panel parent, int posY, Color couleur)
        {
            Panel sep = new Panel
            {
                Left = 20,
                Top = posY,
                Width = parent.Width - 40,
                Height = 2,
                BackColor = couleur
            };
            parent.Controls.Add(sep);
        }

        // Récupère les planètes de l'espèce depuis le DataSet déjà chargé
        private string ObtenirPlanetes(int idEspece)
        {
            if (!MesDatas.DsGlobal.Tables.Contains("EspecePlanetes"))
                return "Origine inconnue";

            DataRow[] rows = MesDatas.DsGlobal.Tables["EspecePlanetes"]
                              .Select($"IdEspece = {idEspece}");

            if (rows.Length == 0) return "Origine inconnue";

            string planetes = rows[0]["Planetes"]?.ToString() ?? "";
            return string.IsNullOrEmpty(planetes) ? "Origine inconnue" : planetes;
        }

        // Charge l'image de l'espèce, sinon dessine un rond avec un "?"
        private Image ChargerImage(string couleur)
        {
            string chemin = Path.Combine(
                Application.StartupPath, "images", "especes", $"{couleur}.png");

            if (File.Exists(chemin))
                return Image.FromFile(chemin);

            // Image par défaut si le fichier n'existe pas
            Bitmap bmp = new Bitmap(180, 180);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.FillEllipse(Brushes.LightGray, 10, 10, 160, 160);
                g.DrawString("?", new Font("Arial", 55, FontStyle.Bold),
                             Brushes.White, new PointF(50, 45));
            }
            return bmp;
        }

        // Empêche de déplacer la fenêtre (on bloque la commande "déplacer" de Windows)
        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MOVE = 0xF010;

            if (m.Msg == WM_SYSCOMMAND && ((int)m.WParam & 0xFFF0) == SC_MOVE)
                return;

            base.WndProc(ref m);
        }

        private void panelContenu_Paint(object sender, PaintEventArgs e) { }

        private void btnFermer_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}