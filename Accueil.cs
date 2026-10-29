using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projetStarGate
{
    public partial class frmAccueil : Form
    {
        // ============================================================
        // ATTRIBUTS DE LA FORME
        // ============================================================

        private FlowLayoutPanel flplMission;
        private RadioButton rdbTous;
        private RadioButton rdbActive;
        private RadioButton rdbTermine;

        private List<MissionItems> allMissions = new List<MissionItems>();
        private frmTableauDeBord frmTDB;

        private Label lblLoading;
        private bool loading;


        // ============================================================
        // CLASSE INTERNE POUR STOCKER UNE MISSION
        // ============================================================

        // Représente une mission affichée dans la liste de l'accueil.
        public class MissionItems
        {
            public int numGlobal { get; set; }
            public int numParPlanete { get; set; }
            public string nomPlanete { get; set; }
            public string dtDepart { get; set; }
            public string dtRetour { get; set; }
            public string chef { get; set; }
            public int numMission { get; set; }
            public string matChef { get; set; }
        }


        // ============================================================
        // CONSTRUCTEUR ET CHARGEMENT DE LA FORME
        // ============================================================

        // Initialise la page d'accueil en plein écran.
        public frmAccueil()
        {
            InitializeComponent();

            WindowState = FormWindowState.Maximized;
            CreateLoadingUI();
        }

        // Charge les données et construit l'interface au démarrage.
        private void frmAccueil_Load(object sender, EventArgs e)
        {
            clearFillDB();
            filtreMission();
            completeFlpl();
            fillFlpl();
            afficherMissions();
        }


        // ============================================================
        // INTERFACE DE CHARGEMENT
        // ============================================================

        // Crée le label utilisé pour afficher le chargement.
        private void CreateLoadingUI()
        {
            lblLoading = new Label();
            lblLoading.Text = "Chargement";
            lblLoading.Font = new Font("Rubik", 32, FontStyle.Bold);
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

                lblLoading.Location = new Point(
                    ClientSize.Width / 2 - lblLoading.Width / 2,
                    ClientSize.Height / 2 - lblLoading.Height / 2
                );

                await Task.Delay(300);
            }
        }

        // Affiche un faux chargement avant d'exécuter une action.
        private async Task ShowLoadingBeforeAction(string text, int delay)
        {
            lblLoading.Visible = true;
            lblLoading.BringToFront();
            loading = true;

            Task animationTask = AnimateLoading(text);

            await Task.Delay(delay);

            loading = false;
            await animationTask;

            lblLoading.Visible = false;
        }


        // ============================================================
        // CRÉATION DES ÉLÉMENTS GRAPHIQUES GÉNÉRIQUES
        // ============================================================

        // Crée un label avec le style principal de l'application.
        private Label creerLabel(string name, string text, int x, int y, int fontSize)
        {
            Label lbl = new Label();
            lbl.Name = name;
            lbl.Text = text;
            lbl.Location = new Point(x, y);
            lbl.AutoSize = true;
            lbl.Font = new Font(lbl.Font.FontFamily, fontSize);
            lbl.ForeColor = SystemColors.Info;
            lbl.BackColor = Color.Transparent;

            return lbl;
        }

        // Applique le style commun aux boutons.
        private void styleButton(Button btn)
        {
            btn.BackColor = SystemColors.Info;
            btn.ForeColor = Color.Gray;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderColor = Color.Gray;
        }


        // ============================================================
        // CRÉATION DE L'INTERFACE PRINCIPALE
        // ============================================================

        // Crée le bloc de filtres des missions.
        private void filtreMission()
        {
            GroupBoxCustom grpFiltre = new GroupBoxCustom();
            grpFiltre.Location = new Point(ClientSize.Width - 500 - 12, 12);
            grpFiltre.Size = new Size(500, 50);
            grpFiltre.Name = "grpFiltre";
            grpFiltre.Text = "État des missions";
            grpFiltre.Font = new Font(grpFiltre.Font.FontFamily, 14);
            grpFiltre.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            grpFiltre.BorderColor = SystemColors.Info;
            grpFiltre.TextColor = SystemColors.Info;
            grpFiltre.ForeColor = SystemColors.Info;
            grpFiltre.BackColor = Color.Transparent;

            Controls.Add(grpFiltre);

            rdbTous = new RadioButton();
            rdbTous.Location = new Point(15, 22);
            rdbTous.Size = new Size(100, 25);
            rdbTous.Name = "rdbTous";
            rdbTous.Text = "Toutes";
            rdbTous.Font = new Font(rdbTous.Font.FontFamily, 14);
            rdbTous.Checked = true;
            rdbTous.CheckedChanged += radioFiltre_CheckedChanged;
            rdbTous.ForeColor = SystemColors.Info;
            grpFiltre.Controls.Add(rdbTous);

            rdbActive = new RadioButton();
            rdbActive.Location = new Point(150, 22);
            rdbActive.AutoSize = true;
            rdbActive.Name = "rdbActive";
            rdbActive.Text = "Actives/Futures";
            rdbActive.Font = new Font(rdbActive.Font.FontFamily, 14);
            rdbActive.CheckedChanged += radioFiltre_CheckedChanged;
            rdbActive.ForeColor = SystemColors.Info;
            grpFiltre.Controls.Add(rdbActive);

            rdbTermine = new RadioButton();
            rdbTermine.Location = new Point(335, 22);
            rdbTermine.Size = new Size(120, 25);
            rdbTermine.Name = "rdbTermine";
            rdbTermine.Text = "Terminées";
            rdbTermine.Font = new Font(rdbTermine.Font.FontFamily, 14);
            rdbTermine.CheckedChanged += radioFiltre_CheckedChanged;
            rdbTermine.ForeColor = SystemColors.Info;
            grpFiltre.Controls.Add(rdbTermine);
        }

        // Crée le FlowLayoutPanel qui contient les cartes des missions.
        private void completeFlpl()
        {
            flplMission = new FlowLayoutPanel();
            flplMission.AutoScroll = true;
            flplMission.FlowDirection = FlowDirection.TopDown;
            flplMission.Location = new Point(ClientSize.Width - 500 - 22, 68);
            flplMission.Name = "flpMissions";
            flplMission.BackColor = Color.Transparent;
            flplMission.Size = new Size(520, ClientSize.Height - 90);
            flplMission.WrapContents = false;
            flplMission.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;

            Controls.Add(flplMission);
        }


        // ============================================================
        // CHARGEMENT DES DONNÉES
        // ============================================================

        // Recharge toutes les tables SQLite dans le DataSet global.
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

        // Charge toutes les missions depuis la base dans une liste locale.
        private void fillFlpl()
        {
            allMissions.Clear();

            try
            {
                string sql = @"select mi.nomPlanete, mi.dateDepart, mi.dateRetour,
                                      mem.nom, mem.prenom, mi.numero, mi.matriculeChef
                               from mission mi
                               join membre mem on mem.matricule = mi.matriculeChef
                               order by mi.dateRetour, mi.nomPlanete, mi.numero";

                SQLiteCommand cmd = new SQLiteCommand(sql, Connexion.Connec);
                SQLiteDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    int numGlobal = 0;
                    Dictionary<string, int> numParPl = new Dictionary<string, int>();

                    while (dr.Read())
                    {
                        numGlobal++;

                        string planet = dr.GetString(0);

                        if (!numParPl.ContainsKey(planet))
                        {
                            numParPl[planet] = 0;
                        }

                        numParPl[planet]++;

                        MissionItems mission = new MissionItems();
                        mission.numGlobal = numGlobal;
                        mission.numParPlanete = numParPl[planet];
                        mission.nomPlanete = planet;
                        mission.dtDepart = dateNormalized(dr.GetString(1));
                        mission.dtRetour = dateNormalized(dr.GetString(2));
                        mission.chef = dr.GetString(3) + " " + dr.GetString(4);
                        mission.numMission = dr.GetInt32(5);
                        mission.matChef = dr.GetString(6);

                        allMissions.Add(mission);
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }


        // ============================================================
        // AFFICHAGE DES MISSIONS
        // ============================================================

        // Affiche les missions selon le filtre sélectionné.
        private void afficherMissions()
        {
            if (flplMission == null)
            {
                MessageBox.Show("Error : flplMission is null");
                return;
            }

            flplMission.Controls.Clear();

            foreach (MissionItems mission in allMissions)
            {
                bool afficher = true;

                DateTime dtRetour = convertirDate(mission.dtRetour);

                if (rdbActive != null && rdbActive.Checked)
                {
                    afficher = dtRetour > DateTime.Today;
                }
                else if (rdbTermine != null && rdbTermine.Checked)
                {
                    afficher = dtRetour <= DateTime.Today;
                }

                if (afficher)
                {
                    GroupBoxCustom grp = insertInfoMission(mission);
                    flplMission.Controls.Add(grp);
                }
            }
        }

        // Crée une carte graphique pour une mission.
        public GroupBoxCustom insertInfoMission(MissionItems mission)
        {
            int xGrpSize = 490;
            int yGrpSize = 290;

            GroupBoxCustom grp = new GroupBoxCustom();
            grp.Name = "grpMissionInfo_" + mission.numGlobal;
            grp.Size = new Size(xGrpSize, yGrpSize);
            grp.TabStop = false;
            grp.Text = "";
            grp.Margin = new Padding(5);
            grp.Font = new Font(grp.Font.FontFamily, 20);
            grp.BorderColor = SystemColors.Info;
            grp.TextColor = SystemColors.Info;
            grp.ForeColor = SystemColors.Info;
            grp.BackColor = Color.Transparent;

            Label lblPlName = creerLabel(
                "lblPlanetName_" + mission.numGlobal,
                mission.nomPlanete + " " + mission.numParPlanete,
                20, 20, 18
            );
            lblPlName.Font = new Font(lblPlName.Font.FontFamily, 18, FontStyle.Bold);

            Label lblStatus = creerLabel("lblStatus_" + mission.numGlobal, "", grp.Width - 120, 20, 16);
            DateTime dtRetour = convertirDate(mission.dtRetour);

            if (dtRetour > DateTime.Today)
            {
                lblStatus.Text = "Active";
                lblStatus.ForeColor = Color.Green;
            }
            else
            {
                lblStatus.Text = "Terminée";
                lblStatus.ForeColor = Color.Red;
            }

            PictureBox pctrPl = new PictureBox();
            pctrPl.Location = new Point(xGrpSize / 2 - 75, 50);
            pctrPl.Name = "pctrMission_" + mission.numGlobal;
            pctrPl.Size = new Size(150, 150);
            pctrPl.Image = chargerImage("images/planetes/" + mission.nomPlanete + ".png");
            pctrPl.SizeMode = PictureBoxSizeMode.Zoom;
            pctrPl.TabStop = false;

            Label lblDtDep = creerLabel(
                "lblMissionDateStart_" + mission.numGlobal,
                mission.dtDepart + "    -",
                xGrpSize - 390, 210, 16
            );

            Label lblDtFin = creerLabel(
                "lblMissionDateEnd_" + mission.numGlobal,
                mission.dtRetour,
                xGrpSize - 235, 210, 16
            );

            Label lblChefName = creerLabel(
                "lblMissionChef_" + mission.numGlobal,
                "Chef : " + mission.chef,
                20, yGrpSize - 43, 16
            );

            Button btnInfo = new Button();
            btnInfo.AutoSize = true;
            btnInfo.Location = new Point(xGrpSize - 85, yGrpSize - 43);
            btnInfo.Name = "btnMissionInfo_" + mission.numGlobal;
            btnInfo.Size = new Size(75, 23);
            btnInfo.Text = "Details";
            btnInfo.Font = new Font(btnInfo.Font.FontFamily, 14);
            styleButton(btnInfo);

            string missionTag = mission.nomPlanete + ";" +
                                mission.numMission + ";" +
                                mission.matChef + ";" +
                                mission.numGlobal;

            lierControlMission(grp, missionTag);
            lierControlMission(lblPlName, missionTag);
            lierControlMission(lblStatus, missionTag);
            lierControlMission(pctrPl, missionTag);
            lierControlMission(lblDtDep, missionTag);
            lierControlMission(lblDtFin, missionTag);
            lierControlMission(lblChefName, missionTag);
            lierControlMission(btnInfo, missionTag);

            grp.Controls.Add(lblStatus);
            grp.Controls.Add(lblChefName);
            grp.Controls.Add(lblDtFin);
            grp.Controls.Add(lblDtDep);
            grp.Controls.Add(pctrPl);
            grp.Controls.Add(lblPlName);
            grp.Controls.Add(btnInfo);

            return grp;
        }

        // Associe un contrôle à une mission grâce à son Tag.
        private void lierControlMission(Control ctrl, string missionTag)
        {
            ctrl.Tag = missionTag;
            ctrl.Click += buttonInflo_Click;
        }


        // ============================================================
        // FILTRES
        // ============================================================

        // Relance l'affichage quand un filtre est sélectionné.
        private void radioFiltre_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdb = sender as RadioButton;

            if (rdb != null && rdb.Checked)
            {
                afficherMissions();
            }
        }


        // ============================================================
        // OUVERTURE DES DÉTAILS D'UNE MISSION
        // ============================================================

        // Ouvre ou recharge la fenêtre des détails d'une mission.
        private async void buttonInflo_Click(object sender, EventArgs e)
        {
            if (loading)
            {
                return;
            }

            Control ctrl = sender as Control;

            if (ctrl == null || ctrl.Tag == null)
            {
                MessageBox.Show("Erreur : informations de mission introuvables.");
                return;
            }

            string tag = ctrl.Tag.ToString();
            string[] parts = tag.Split(';');

            if (parts.Length < 4)
            {
                MessageBox.Show("Erreur : Tag mission invalide.");
                return;
            }

            string planet = parts[0];
            string numMission = parts[1];
            string matChef = parts[2];
            string numGlobal = parts[3];

            await ShowLoadingBeforeAction("Chargement de la mission", 1000);

            if (frmTDB == null || frmTDB.IsDisposed)
            {
                frmTDB = new frmTableauDeBord(planet, numMission, matChef, numGlobal);
                frmTDB.Show(this);
            }
            else
            {
                if (!frmTDB.same(planet, numMission, matChef, numGlobal))
                {
                    frmTDB.loadNewMission(planet, numMission, matChef, numGlobal);
                }

                frmTDB.Show();
                frmTDB.WindowState = FormWindowState.Maximized;
                frmTDB.BringToFront();
                frmTDB.Activate();
            }

            this.Hide();
        }


        // ============================================================
        // OUTILS
        // ============================================================

        // Convertit une date en format français pour l'affichage.
        private string dateNormalized(string date)
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
                return dt.ToString("dd/MM/yyyy");
            }

            return date;
        }

        // Charge une image depuis le dossier de l'application.
        // Si l'image n'existe pas, l'image par défaut est utilisée.
        private Image chargerImage(string cheminRelatif)
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
                return Image.FromStream(fs);
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

        private void btnCreationMission_Click(object sender, EventArgs e)
        {
            Creation_Mission fe = new Creation_Mission();
            fe.Show();
        }

        private void btnStats_Click(object sender, EventArgs e)
        {
            Statistiques fe = new Statistiques();
            fe.Show();

        }

        private void btnAliens_Click(object sender, EventArgs e)
        {
            frmRaces fe = new frmRaces();
            fe.Show();
        }

        private void btnPlanetes_Click(object sender, EventArgs e)
        {
            frmPlanetes fe = new frmPlanetes();
            fe.Show();
        }
    }
}