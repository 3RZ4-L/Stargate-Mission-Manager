using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projetStarGate
{
    public partial class Login : Form
    {
        private Label lblLoading;
        private bool _loading;

        public Login()
        {
            InitializeComponent(); // Initialise les composants du formulaire
            CreateLoadingUI();     // Crée le label de chargement
        }

        private void CreateLoadingUI()
        {
            // Création du label de chargement
            lblLoading = new Label
            {
                Text = "Connexion",
                Font = new Font("Rubik", 32, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Visible = false
            };

            lblLoading.Location = new Point(176, 197);
            this.Controls.Add(lblLoading);
            lblLoading.BringToFront();
        }

        private async Task ShowLoading()
        {
            int dots = 0;

            // Boucle tant que le chargement est actif
            while (_loading)
            {
                dots = (dots + 1) % 4;
                lblLoading.Text = "Connexion" + new string('.', dots);
                await Task.Delay(300);
            }
        }

        private async void btnValider_Click(object sender, EventArgs e)
        {
            // Vérifie les identifiants
            if (ValiderLogin())
            {
                // Affiche le chargement
                lblLoading.Visible = true;
                _loading = true;

                // Démarre l'animation
                var loadingTask = ShowLoading();
                await Task.Delay(1200);
                _loading = false;
                lblLoading.Visible = false;
                await loadingTask;

                // Ouvre la fenêtre suivante
                new Creation_Mission().Show();
                this.Hide();
            }
            else
            {
                // Fait vibrer la fenêtre
                await Vibrer();
            }
        }

        private bool ValiderLogin()
        {
            try
            {
                // Ouvre la connexion si nécessaire
                if (Connexion.Connec.State != ConnectionState.Open)
                    Connexion.Connec.Open();

                // Requête SQL
                string sql = "SELECT mdp FROM Admin WHERE login = @login";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, Connexion.Connec))
                {
                    // Remplace @login par le texte saisi
                    cmd.Parameters.AddWithValue("@login", txtLogin.Text.Trim());

                    // Exécute la requête
                    object result = cmd.ExecuteScalar();

                    // Login introuvable
                    if (result == null || result == DBNull.Value)
                        return false;


                    string mdpStocke = result.ToString();
                    return BCrypt.Net.BCrypt.Verify(txtMdp.Text, mdpStocke);
                }
            }
            catch (Exception ex)
            {
                // Affiche l'erreur
                MessageBox.Show("Erreur lors de la connexion : " + ex.Message);
                return false;
            }
        }

        private async Task Vibrer()
        {
            // Sauvegarde la position d'origine
            var originalLocation = this.Location;
            int pixels = 10; // Distance du déplacement
            int count = 6;   // Nombre d'allers-retours

            for (int i = 0; i < count; i++)
            {
                // Déplacement à droite
                this.Location = new Point(originalLocation.X + pixels, originalLocation.Y);
                await Task.Delay(30);
                // Déplacement à gauche
                this.Location = new Point(originalLocation.X - pixels, originalLocation.Y);
                await Task.Delay(30);
            }
            // Retour à la position initiale
            this.Location = originalLocation;
        }

        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            // Cache le formulaire
            this.Hide();
        }
    }
}