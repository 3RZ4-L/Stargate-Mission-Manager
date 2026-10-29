using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace projetStarGate
{
    public partial class frmJournal : Form
    {
        // ============================================================
        // ATTRIBUTS DE LA FORME
        // ============================================================

        private string m_planet;
        private string m_numMission;
        private string m_matChef;
        private string m_numGlobal;
        private string m_nomTableBilanCapture;

        // Blocs principaux de l'interface
        GroupBoxCustom grpEvents;
        GroupBoxCustom grpContacts;
        GroupBoxCustom grpDepenses;
        GroupBoxCustom grpBilCapt;

        // Tableau du bilan des captures
        DataGridView dgvBilanCaptures;

        // Contrôles pour les événements du journal
        Label lblDate;
        RichTextBox rtxtCommentaire;
        Label lblSelectedComm;

        // Contrôles pour les dépenses
        Label lblDateDepense;
        Label lblMontantDepense;
        Label lblTypeDepense;
        RichTextBox rtxtMotifDepense;
        Label lblSelectedDepense;

        // Contrôles pour les contacts
        Label lblDateContact;
        Label lblSommeContact;
        Label lblInformateurContact;
        RichTextBox rtxtAppreciationContact;
        Label lblSelectedContact;

        // Sources de données utilisées pour la navigation
        private BindingSource bsJournal = new BindingSource();
        private BindingSource bsDepenses = new BindingSource();
        private BindingSource bsContacts = new BindingSource();


        // ============================================================
        // CONSTRUCTEUR ET CHARGEMENT DE LA FORME
        // ============================================================

        // Initialise la forme avec les informations de la mission sélectionnée.
        public frmJournal(string planet, string numMission, string matChef, string numGlobal, string nomTableBilanCapture)
        {
            InitializeComponent();

            m_planet = planet;
            m_numMission = numMission;
            m_matChef = matChef;
            m_numGlobal = numGlobal;
            m_nomTableBilanCapture = nomTableBilanCapture;
        }

        // Crée l'interface puis charge toutes les données affichées.
        private void frmJournal_Load(object sender, EventArgs e)
        {
            creerUI();
            completeJDB();
            completeDepenses();
            completeContacts();
            completeBilanCaptures();
            creerBoutonExit();
        }


        // ============================================================
        // CRÉATION DES ÉLÉMENTS GRAPHIQUES GÉNÉRIQUES
        // ============================================================

        // Crée un bouton avec les propriétés principales.
        // Le style est appliqué automatiquement.
        private Button creerButton(string name, string text, int x, int y, int sizeX, int sizeY, EventHandler click, int fontSize)
        {
            Button btn = new Button();
            btn.Name = name;
            btn.Text = text;
            btn.Location = new Point(x, y);
            btn.Size = new Size(sizeX, sizeY);
            btn.AutoSize = true;
            btn.Font = new Font(btn.Font.FontFamily, fontSize);
            btn.Click += click;

            styleButton(btn);

            return btn;
        }

        // Crée un bouton de navigation lié à un BindingSource.
        // Le BindingSource est stocké dans le Tag du bouton.
        private Button creerButtonNavigation(string name, string text, int x, int y, BindingSource bs, EventHandler click)
        {
            Button btn = creerButton(name, text, x, y, 50, 50, click, 14);
            btn.Tag = bs;
            return btn;
        }

        // Crée un GroupBox personnalisé.
        private GroupBoxCustom creerGroupBox(string name, string text, int x, int y, int sizeX, int sizeY, int fontSize)
        {
            GroupBoxCustom grp = new GroupBoxCustom();
            grp.Name = name;
            grp.Text = text;
            grp.Location = new Point(x, y);
            grp.Size = new Size(sizeX, sizeY);
            grp.Font = new Font(grp.Font.FontFamily, fontSize);
            grp.TabStop = false;

            return grp;
        }

        // Crée un Label avec le style de base de l'application.
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


        // ============================================================
        // STYLES DES ÉLÉMENTS
        // ============================================================

        // Applique le style commun aux boutons.
        private void styleButton(Button btn)
        {
            btn.BackColor = SystemColors.Info;
            btn.ForeColor = Color.Gray;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderColor = Color.Gray;
            btn.FlatAppearance.BorderSize = 1;
        }

        // Applique le style commun aux RichTextBox.
        private void styleRichTextBox(RichTextBox rtxt)
        {
            rtxt.BackColor = Color.FromArgb(40, 40, 40);
            rtxt.ForeColor = SystemColors.Info;
            rtxt.BorderStyle = BorderStyle.FixedSingle;
        }

        // Applique le style du tableau du bilan des captures.
        // Le tableau est en lecture seule et les colonnes s'adaptent.
        private void styleDataGridView(DataGridView dgv)
        {
            dgv.BackgroundColor = Color.FromArgb(40, 40, 40);
            dgv.GridColor = Color.Gray;
            dgv.BorderStyle = BorderStyle.FixedSingle;

            dgv.DefaultCellStyle.BackColor = Color.FromArgb(40, 40, 40);
            dgv.DefaultCellStyle.ForeColor = SystemColors.Info;
            dgv.DefaultCellStyle.SelectionBackColor = Color.Gray;
            dgv.DefaultCellStyle.SelectionForeColor = SystemColors.Info;
            dgv.DefaultCellStyle.Font = new Font(dgv.Font.FontFamily, 13);

            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 30, 30);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.Info;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font(dgv.Font.FontFamily, 13, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            dgv.EnableHeadersVisualStyles = false;

            dgv.RowHeadersVisible = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.ReadOnly = true;

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dgv.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgv.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
        }


        // ============================================================
        // CRÉATION DE L'INTERFACE PRINCIPALE
        // ============================================================

        // Crée tous les blocs de la fenêtre :
        // journal, contacts, dépenses et bilan des captures.
        private void creerUI()
        {
            creerBlocEvenements();
            creerBlocContacts();
            creerBlocDepenses();
            creerBlocBilanCaptures();
        }

        // Crée le bloc d'affichage des événements du journal.
        private void creerBlocEvenements()
        {
            grpEvents = creerGroupBox(
                "grpEvents",
                "Evénements du journal : " + m_planet + " mission : " + m_numMission,
                10, 10, 700, 300, 18
            );
            Controls.Add(grpEvents);

            lblDate = creerLabel("lblDate", "", 10, 95, 16);
            grpEvents.Controls.Add(lblDate);

            rtxtCommentaire = new RichTextBox();
            rtxtCommentaire.Location = new Point(grpEvents.Width - 10 - 550, 40);
            rtxtCommentaire.Size = new Size(550, 150);
            rtxtCommentaire.Name = "rtxtCommenataires";
            rtxtCommentaire.Font = new Font(rtxtCommentaire.Font.FontFamily, 14);
            rtxtCommentaire.ReadOnly = true;
            rtxtCommentaire.TabStop = false;
            rtxtCommentaire.WordWrap = true;
            rtxtCommentaire.ScrollBars = RichTextBoxScrollBars.Vertical;
            rtxtCommentaire.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            styleRichTextBox(rtxtCommentaire);
            grpEvents.Controls.Add(rtxtCommentaire);

            Button btnJToLeft = creerButtonNavigation("btnJToLeft", "<<", grpEvents.Width - 10 - 550, 210, bsJournal, buttonToLeft_Click);
            grpEvents.Controls.Add(btnJToLeft);

            Button btnJOneLeft = creerButtonNavigation("btnJOneLeft", "<", grpEvents.Width - 10 - 550 + 60, 210, bsJournal, buttonOneLeft_Click);
            grpEvents.Controls.Add(btnJOneLeft);

            Button btnJOneRight = creerButtonNavigation("btnJOneRight", ">", grpEvents.Width - 10 - 110, 210, bsJournal, buttonOneRight_Click);
            grpEvents.Controls.Add(btnJOneRight);

            Button btnJToRight = creerButtonNavigation("btnJToRight", ">>", grpEvents.Width - 10 - 50, 210, bsJournal, buttonToRight_Click);
            grpEvents.Controls.Add(btnJToRight);

            lblSelectedComm = creerLabel("lblSelectedComm", "", rtxtCommentaire.Width / 2 + 110, 225, 14);
            grpEvents.Controls.Add(lblSelectedComm);
        }

        // Crée le bloc d'affichage des dépenses.
        private void creerBlocDepenses()
        {
            grpDepenses = creerGroupBox("grpDepenses", "Dépenses effectuées", 720, 10, 700, 450, 18);
            Controls.Add(grpDepenses);

            lblDateDepense = creerLabel("lblDateDepense", "", 10, 50, 14);
            grpDepenses.Controls.Add(lblDateDepense);

            lblMontantDepense = creerLabel("lblMontantDepense", "", 10, 85, 14);
            grpDepenses.Controls.Add(lblMontantDepense);

            lblTypeDepense = creerLabel("lblTypeDepense", "", 10, 120, 14);
            grpDepenses.Controls.Add(lblTypeDepense);

            rtxtMotifDepense = new RichTextBox();
            rtxtMotifDepense.Location = new Point(10, 160);
            rtxtMotifDepense.Size = new Size(grpDepenses.Width - 20, 200);
            rtxtMotifDepense.Name = "rtxtMotifDepense";
            rtxtMotifDepense.Font = new Font(rtxtMotifDepense.Font.FontFamily, 14);
            rtxtMotifDepense.ReadOnly = true;
            rtxtMotifDepense.TabStop = false;
            rtxtMotifDepense.WordWrap = true;
            rtxtMotifDepense.ScrollBars = RichTextBoxScrollBars.Vertical;
            styleRichTextBox(rtxtMotifDepense);
            grpDepenses.Controls.Add(rtxtMotifDepense);

            Button btnDepFirst = creerButtonNavigation("btnDepToLeft", "<<", 10, 390, bsDepenses, buttonToLeft_Click);
            grpDepenses.Controls.Add(btnDepFirst);

            Button btnDepPrev = creerButtonNavigation("btnDepOneLeft", "<", 70, 390, bsDepenses, buttonOneLeft_Click);
            grpDepenses.Controls.Add(btnDepPrev);

            Button btnDepNext = creerButtonNavigation("btnDepOneRight", ">", grpDepenses.Width - 120, 390, bsDepenses, buttonOneRight_Click);
            grpDepenses.Controls.Add(btnDepNext);

            Button btnDepLast = creerButtonNavigation("btnDepToRight", ">>", grpDepenses.Width - 60, 390, bsDepenses, buttonToRight_Click);
            grpDepenses.Controls.Add(btnDepLast);

            lblSelectedDepense = creerLabel("lblSelectedDepense", "", grpDepenses.Width / 2 - 30, 405, 14);
            grpDepenses.Controls.Add(lblSelectedDepense);
        }

        // Crée le bloc d'affichage des contacts avec les informateurs.
        private void creerBlocContacts()
        {
            grpContacts = creerGroupBox("grpContacts", "Contacts avec les informateurs", 10, 320, 700, 400, 18);
            Controls.Add(grpContacts);

            lblDateContact = creerLabel("lblDateContact", "", 10, 50, 14);
            grpContacts.Controls.Add(lblDateContact);

            lblSommeContact = creerLabel("lblSommeContact", "", 10, 85, 14);
            grpContacts.Controls.Add(lblSommeContact);

            lblInformateurContact = creerLabel("lblInformateurContact", "", 10, 120, 14);
            grpContacts.Controls.Add(lblInformateurContact);

            rtxtAppreciationContact = new RichTextBox();
            rtxtAppreciationContact.Location = new Point(10, 160);
            rtxtAppreciationContact.Size = new Size(grpContacts.Width - 20, 130);
            rtxtAppreciationContact.Name = "rtxtAppreciationContact";
            rtxtAppreciationContact.Font = new Font(rtxtAppreciationContact.Font.FontFamily, 14);
            rtxtAppreciationContact.ReadOnly = true;
            rtxtAppreciationContact.TabStop = false;
            rtxtAppreciationContact.WordWrap = true;
            rtxtAppreciationContact.ScrollBars = RichTextBoxScrollBars.Vertical;
            styleRichTextBox(rtxtAppreciationContact);
            grpContacts.Controls.Add(rtxtAppreciationContact);

            Button btnContactFirst = creerButtonNavigation("btnContactFirst", "<<", 10, 320, bsContacts, buttonToLeft_Click);
            grpContacts.Controls.Add(btnContactFirst);

            Button btnContactPrev = creerButtonNavigation("btnContactPrev", "<", 70, 320, bsContacts, buttonOneLeft_Click);
            grpContacts.Controls.Add(btnContactPrev);

            Button btnContactNext = creerButtonNavigation("btnContactNext", ">", grpContacts.Width - 120, 320, bsContacts, buttonOneRight_Click);
            grpContacts.Controls.Add(btnContactNext);

            Button btnContactLast = creerButtonNavigation("btnContactLast", ">>", grpContacts.Width - 60, 320, bsContacts, buttonToRight_Click);
            grpContacts.Controls.Add(btnContactLast);

            lblSelectedContact = creerLabel("lblSelectedContact", "", grpContacts.Width / 2 - 30, 335, 14);
            grpContacts.Controls.Add(lblSelectedContact);
        }

        // Crée le bloc contenant le tableau du bilan des captures.
        private void creerBlocBilanCaptures()
        {
            grpBilCapt = creerGroupBox("grpBilCapt", "Bilan de capture", 720, 470, 700, 195, 18);
            Controls.Add(grpBilCapt);

            dgvBilanCaptures = new DataGridView();
            dgvBilanCaptures.Name = "dgvBilanCaptures";
            dgvBilanCaptures.Location = new Point(10, 35);
            dgvBilanCaptures.Size = new Size(grpBilCapt.Width - 20, grpBilCapt.Height - 45);

            styleDataGridView(dgvBilanCaptures);

            grpBilCapt.Controls.Add(dgvBilanCaptures);
        }

        // Crée le bouton permettant de revenir à la page précédente.
        private void creerBoutonExit()
        {
            Button btnExit = new Button();
            btnExit.Location = new Point(ClientSize.Width - 380, ClientSize.Height - 54);
            btnExit.Size = new Size(50, 50);
            btnExit.AutoSize = true;
            btnExit.Name = "btnExit";
            btnExit.Text = "Retour à la page des détails des missions";
            btnExit.Click += buttonExit_Click;

            styleButton(btnExit);

            Controls.Add(btnExit);
        }


        // ============================================================
        // CHARGEMENT DES DONNÉES
        // ============================================================

        // Charge une table du DataSet dans un BindingSource.
        // Applique un filtre, un tri et connecte l'événement de navigation.
        private void chargerBindingSource(string nomTable, string filtre, string tri, BindingSource bs, EventHandler positionChanged, Action afficher)
        {
            DataTable table = MesDatas.DsGlobal.Tables[nomTable];

            if (table == null)
            {
                MessageBox.Show("La table " + nomTable + " est introuvable dans le DataSet.");
                return;
            }

            DataView dv = new DataView(table);
            dv.RowFilter = filtre;
            dv.Sort = tri;

            bs.DataSource = dv;

            bs.PositionChanged -= positionChanged;
            bs.PositionChanged += positionChanged;

            if (bs.Count > 0)
            {
                bs.MoveFirst();
            }

            afficher();
        }

        // Charge les événements du journal de bord de la mission.
        private void completeJDB()
        {
            string filtre = "nomPlanete = '" + m_planet.Replace("'", "''") + "' and numero = " + m_numMission;

            chargerBindingSource(
                "JournalDeBord",
                filtre,
                "dateJ ASC",
                bsJournal,
                bsJournal_PositionChanged,
                afficherEventJournal
            );
        }

        // Charge les dépenses de la mission.
        private void completeDepenses()
        {
            string filtre = "nomPlanete = '" + m_planet.Replace("'", "''") + "' and numeroMission = " + m_numMission;

            chargerBindingSource(
                "Depense",
                filtre,
                "dateD ASC",
                bsDepenses,
                bsDepenses_PositionChanged,
                afficherDepense
            );
        }

        // Charge les contacts avec les informateurs de la mission.
        private void completeContacts()
        {
            string filtre = "nomPlanete = '" + m_planet.Replace("'", "''") + "' and numeroMission = " + m_numMission;

            chargerBindingSource(
                "Contact",
                filtre,
                "dateC ASC",
                bsContacts,
                bsContacts_PositionChanged,
                afficherContact
            );
        }

        // Charge la table locale contenant le bilan des captures.
        private void completeBilanCaptures()
        {
            if (string.IsNullOrWhiteSpace(m_nomTableBilanCapture))
            {
                MessageBox.Show("Nom de table bilan capture introuvable.");
                return;
            }

            DataTable table = MesDatas.DsGlobal.Tables[m_nomTableBilanCapture];

            if (table == null)
            {
                MessageBox.Show("La table " + m_nomTableBilanCapture + " est introuvable dans le DataSet.");
                return;
            }

            dgvBilanCaptures.DataSource = table;

            dgvBilanCaptures.Columns["Nom de l'espèce"].FillWeight = 30;
            dgvBilanCaptures.Columns["Objectif initial"].FillWeight = 20;
            dgvBilanCaptures.Columns["Nombre de captures réalisées"].FillWeight = 30;
            dgvBilanCaptures.Columns["Taux de réussite (%)"].FillWeight = 20;
        }


        // ============================================================
        // AFFICHAGE DES DONNÉES
        // ============================================================

        // Affiche l'événement actuellement sélectionné dans le journal.
        private void afficherEventJournal()
        {
            if (bsJournal.Count == 0)
            {
                lblDate.Text = "";
                rtxtCommentaire.Text = "";
                lblSelectedComm.Text = "0/0";
                return;
            }

            DataRowView ligne = bsJournal.Current as DataRowView;
            if (ligne == null)
            {
                return;
            }

            string dateJ = ligne["dateJ"].ToString();
            string commentaire = ligne["commentaires"].ToString();

            lblDate.Text = convertirDate(dateJ).ToString("dd/MM/yyyy");
            rtxtCommentaire.Text = commentaire;
            lblSelectedComm.Text = (bsJournal.Position + 1) + "/" + bsJournal.Count;
        }

        // Affiche la dépense actuellement sélectionnée.
        private void afficherDepense()
        {
            if (bsDepenses.Count == 0)
            {
                lblDateDepense.Text = "";
                lblMontantDepense.Text = "";
                lblTypeDepense.Text = "";
                rtxtMotifDepense.Text = "";
                lblSelectedDepense.Text = "0/0";
                return;
            }

            DataRowView ligne = bsDepenses.Current as DataRowView;

            if (ligne == null)
            {
                return;
            }

            string dateD = ligne["dateD"].ToString();
            string montant = ligne["montant"].ToString();
            string motif = ligne["motif"].ToString();
            string idTypeDepense = ligne["idTypeDepense"].ToString();

            lblDateDepense.Text = "Date : " + convertirDate(dateD).ToString("dd/MM/yyyy");
            lblMontantDepense.Text = "Montant : " + montant + "$";
            lblTypeDepense.Text = "Type : " + getLibelleTypeDepense(idTypeDepense);
            rtxtMotifDepense.Text = motif;

            lblSelectedDepense.Text = (bsDepenses.Position + 1) + "/" + bsDepenses.Count;
        }

        // Affiche le contact actuellement sélectionné.
        private void afficherContact()
        {
            if (bsContacts.Count == 0)
            {
                lblDateContact.Text = "";
                lblSommeContact.Text = "";
                lblInformateurContact.Text = "";
                rtxtAppreciationContact.Text = "";
                lblSelectedContact.Text = "0/0";
                return;
            }

            DataRowView ligne = bsContacts.Current as DataRowView;

            if (ligne == null)
            {
                return;
            }

            string dateC = ligne["dateC"].ToString();
            string somme = ligne["sommeVersee"].ToString();
            string appreciation = ligne["appreciation"].ToString();
            string nomCodeInformateur = ligne["nomCodeInformateur"].ToString();

            lblDateContact.Text = "Date : " + convertirDate(dateC).ToString("dd/MM/yyyy");
            lblSommeContact.Text = "Somme versée : " + somme + "$";
            lblInformateurContact.Text = "Informateur : " + getNomInformateur(nomCodeInformateur);
            rtxtAppreciationContact.Text = appreciation;

            lblSelectedContact.Text = (bsContacts.Position + 1) + "/" + bsContacts.Count;
        }


        // ============================================================
        // NAVIGATION ENTRE LES ENREGISTREMENTS
        // ============================================================

        // Va au premier élément du BindingSource associé au bouton.
        private void buttonToLeft_Click(object sender, EventArgs e)
        {
            BindingSource bs = getBindingSourceFromButton(sender);

            if (bs == null || bs.Count == 0)
            {
                return;
            }

            bs.MoveFirst();
        }

        // Va à l'élément précédent du BindingSource associé au bouton.
        private void buttonOneLeft_Click(object sender, EventArgs e)
        {
            BindingSource bs = getBindingSourceFromButton(sender);

            if (bs == null || bs.Count == 0)
            {
                return;
            }

            bs.MovePrevious();
        }

        // Va à l'élément suivant du BindingSource associé au bouton.
        private void buttonOneRight_Click(object sender, EventArgs e)
        {
            BindingSource bs = getBindingSourceFromButton(sender);

            if (bs == null || bs.Count == 0)
            {
                return;
            }

            bs.MoveNext();
        }

        // Va au dernier élément du BindingSource associé au bouton.
        private void buttonToRight_Click(object sender, EventArgs e)
        {
            BindingSource bs = getBindingSourceFromButton(sender);

            if (bs == null || bs.Count == 0)
            {
                return;
            }

            bs.MoveLast();
        }

        // Récupère le BindingSource stocké dans le Tag d'un bouton.
        private BindingSource getBindingSourceFromButton(object sender)
        {
            Button btn = sender as Button;

            if (btn == null || btn.Tag == null)
            {
                return null;
            }

            return btn.Tag as BindingSource;
        }


        // ============================================================
        // ÉVÉNEMENTS DES BINDINGSOURCES
        // ============================================================

        // Met à jour l'affichage quand l'événement sélectionné change.
        private void bsJournal_PositionChanged(object sender, EventArgs e)
        {
            afficherEventJournal();
        }

        // Met à jour l'affichage quand la dépense sélectionnée change.
        private void bsDepenses_PositionChanged(object sender, EventArgs e)
        {
            afficherDepense();
        }

        // Met à jour l'affichage quand le contact sélectionné change.
        private void bsContacts_PositionChanged(object sender, EventArgs e)
        {
            afficherContact();
        }


        // ============================================================
        // RECHERCHES DANS LES TABLES DU DATASET
        // ============================================================

        // Retourne le libellé d'un type de dépense à partir de son identifiant.
        private string getLibelleTypeDepense(string idTypeDepense)
        {
            DataTable tableType = MesDatas.DsGlobal.Tables["TypeDepense"];

            if (tableType == null)
            {
                return idTypeDepense;
            }

            DataRow[] lignes = tableType.Select("id = " + idTypeDepense);

            if (lignes.Length == 0)
            {
                return idTypeDepense;
            }

            return lignes[0]["libelle"].ToString();
        }

        // Retourne le nom d'un informateur à partir de son nom de code.
        private string getNomInformateur(string nomCodeInformateur)
        {
            DataTable tableInformateur = MesDatas.DsGlobal.Tables["Informateur"];

            if (tableInformateur == null)
            {
                return nomCodeInformateur;
            }

            DataRow[] lignes = tableInformateur.Select("nomCode = '" + nomCodeInformateur.Replace("'", "''") + "'");

            if (lignes.Length == 0)
            {
                return nomCodeInformateur;
            }

            return lignes[0]["nom"].ToString();
        }


        // ============================================================
        // ACTIONS DE LA FENÊTRE
        // ============================================================

        // Cache la fenêtre du journal et revient à la page précédente.
        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Hide();
        }


        // ============================================================
        // OUTILS
        // ============================================================

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