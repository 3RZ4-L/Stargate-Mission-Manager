using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projetStarGate
{
    public partial class Creation_Mission : Form
    {
        string nomPlanete;
        int numero;
        string matriculeChef;

        List<string> membres = new List<string>();
        Dictionary<string, int> capture = new Dictionary<string, int>();

        public Creation_Mission()
        {
            InitializeComponent();
        }

        private void Infos_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar));
        }

        private void cbo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void Creation_Mission_Load(object sender, EventArgs e)
        {
            errDate.SetError(dtpRetour, "La date de départ doit être antérieure à la date de retour.");
            grpEtape2.Visible = false;
            grpEtape3.Visible = false;
            btnRetour.Visible = false;

            try
            {
                string sql = "SELECT nom FROM Planete;";
                SQLiteCommand cmd = new SQLiteCommand(sql, Connexion.Connec);
                SQLiteDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                    cboPlanete.Items.Add(dr.GetString(0));

                dr.Close();
            }
            catch (SQLiteException err)
            {
                MessageBox.Show(err.Message);
            }

            txtBudget.Text = "1000";
            txtDataBaz.Text = "0";
            txtEquipage.Text = "1";

            cboFiltreMembres.Items.Clear();
            cboFiltreMembres.Items.Add("Tous");

            try
            {
                // Select les metier pour le filtre du choix des memebres
                string sql = "SELECT DISTINCT grade AS metier " +
                    "FROM Militaire " +
                    "UNION " +
                    "SELECT DISTINCT specialite " +
                    "FROM Civil " +
                    "ORDER BY metier ";

                SQLiteCommand cmd = new SQLiteCommand(sql, Connexion.Connec);
                SQLiteDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                    cboFiltreMembres.Items.Add(dr["metier"].ToString());

                dr.Close();

                cboFiltreMembres.SelectedIndex = 0;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void cboPlanete_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPlanete.SelectedIndex >= 0)
            {
                // Select le nombre de mission pour 1 planete et ajoute 1
                string sql = "SELECT IFNULL(MAX(numero), 0) FROM Mission WHERE nomPlanete = @nomPlanete";
                SQLiteCommand cmd = new SQLiteCommand(sql, Connexion.Connec);
                cmd.Parameters.AddWithValue("@nomPlanete", cboPlanete.SelectedItem.ToString());

                numero = Convert.ToInt32(cmd.ExecuteScalar()) + 1;
                lblNomMission.Text = "Nom de la mission : " + cboPlanete.SelectedItem + " " + numero;
                nomPlanete = cboPlanete.SelectedItem.ToString();
            }
        }

        private void cboChef_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboChef.SelectedItem != null)
            {
                DataRowView ligne = (DataRowView)cboChef.SelectedItem;
                matriculeChef = ligne["matricule"].ToString();

                // Chargement des membres disponibles
                ChargerMembres("Tous");
            }
            else
            {
                MessageBox.Show("Aucun chef sélectionné.");
            }
        }

        private void btnValiderPlanete_Click(object sender, EventArgs e)
        {
            //Gestion d'erreur
            errDate.Clear();
            if (dtpDepart.Value.Date >= dtpRetour.Value.Date)
            {
                errDate.SetError(dtpRetour, "La date de départ doit être antérieure à la date de retour.");
                return;
            }
            if (dtpDepart.Value.Date < DateTime.Today) 
            {
                errDate.SetError(dtpDepart, "La date de départ doit ne doit pas être avant aujourd'hui.");
                return;
            }
            
            grpEtape2.Visible = true;

            membres.Clear();
            capture.Clear();
            flpMembres.Controls.Clear();
            flpCaptures.Controls.Clear();

            try
            {
                // Select des chefs disponibles
                string sql = "SELECT a.matricule AS matricule, a.nom || ' ' || a.prenom AS nomComplet " +
                    "FROM Membre a " +
                    "WHERE a.matricule LIKE 'M%' " +
                    "AND NOT EXISTS ( SELECT a.matricule " +
                    "FROM Composer c " +
                    "JOIN Mission m ON m.numero = c.numeroMission " +
                    "AND m.nomPlanete = c.nomPlanete " +
                    "WHERE c.matriculeMembre = a.matricule " +
                    "AND m.dateDepart <= @dateRetour " +
                    "AND m.dateRetour >= @dateDepart);";

                SQLiteCommand cmd = new SQLiteCommand(sql, Connexion.Connec);
                cmd.Parameters.AddWithValue("@dateDepart", dtpDepart.Value.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@dateRetour", dtpRetour.Value.ToString("yyyy-MM-dd"));

                // Chargement de la comboBox
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                cboChef.DataSource = dt;
                cboChef.DisplayMember = "nomComplet";
                cboChef.ValueMember = "matricule";

                // On ne peut plus changer les données de l'étape 1
                btnValiderPlanete.Visible = false;
                cboPlanete.Enabled = false;
                dtpDepart.Enabled = false;
                dtpRetour.Enabled = false;
            }
            catch (SQLiteException err)
            {
                MessageBox.Show(err.Message);
            }

            try
            {
                // Select des espèces ennemies
                string sql = "SELECT e.id AS id, e.nom AS nom " +
                    "FROM Espece e " +
                    "JOIN Habiter h ON e.id = h.idEspece " +
                    "JOIN Ennemi en ON en.idEspece = e.id " +
                    "WHERE h.nomPlanete = @nomPlanete;";
                SQLiteCommand cmd = new SQLiteCommand(sql, Connexion.Connec);
                cmd.Parameters.AddWithValue("@nomPlanete", nomPlanete);

                // Chargement de la comboBox
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                cboCapture.DataSource = dt;
                cboCapture.DisplayMember = "nom";
                cboCapture.ValueMember = "id";

                if (cboCapture.Items.Count == 0)
                {
                    btnSuppriemrAlien.Visible = false;
                    lblObjectifs.Visible = false;
                    cboCapture.Visible = false;
                    lblNBAlien.Visible = false;
                    txtObjectif.Visible = false;
                    btnAjouterCapture.Visible = false;
                    flpCaptures.Visible = false;
                }

            }
            catch (SQLiteException err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void ChargerMembres(string filtre)
        {
            try
            {
                // Chargement des membres disponibles
                string sql =
                    "SELECT a.matricule, " +
                    "a.nom || ' ' || a.prenom AS nomComplet, " +
                    "IFNULL(civ.specialite, mil.grade) AS metier " +
                    "FROM Membre a " +
                    "LEFT JOIN Civil civ ON civ.matriculeMembre = a.matricule " +
                    "LEFT JOIN Militaire mil ON mil.matriculeMembre = a.matricule " +
                    "WHERE NOT EXISTS ( " +
                    "SELECT 1 " +
                    "FROM Composer c " +
                    "JOIN Mission m ON m.numero = c.numeroMission " +
                    "AND m.nomPlanete = c.nomPlanete " +
                    "WHERE c.matriculeMembre = a.matricule " +
                    "AND m.dateDepart <= @dateRetour " +
                    "AND m.dateRetour >= @dateDepart )" +
                    "AND a.matricule != @matriculeChef";

                if (filtre != "Tous")
                {
                    sql += " AND IFNULL(civ.specialite, mil.grade) = @filtre";
                }
                SQLiteCommand cmd = new SQLiteCommand(sql, Connexion.Connec);

                cmd.Parameters.AddWithValue("@dateDepart", dtpDepart.Value.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@dateRetour", dtpRetour.Value.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@matriculeChef", matriculeChef);

                if (filtre != "Tous")
                {
                    cmd.Parameters.AddWithValue("@filtre", filtre);
                }

                // Chargement de la comboBox
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                cboMembres.DataSource = dt;
                cboMembres.DisplayMember = "nomComplet";
                cboMembres.ValueMember = "matricule";
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void btnAjouterMembre_Click(object sender, EventArgs e)
        {
            if (cboMembres.SelectedItem != null)
            {
                string matricule = cboMembres.SelectedValue.ToString();
                if (!membres.Contains(matricule))
                {
                    string nomComplet = cboMembres.Text;
                    string metier = "";

                    // si militaire
                    if (matricule[0] == 'M')
                    {                        
                        string sql = "SELECT grade FROM Militaire WHERE matriculeMembre = @matricule";
                        SQLiteCommand cmd = new SQLiteCommand(sql, Connexion.Connec);
                        cmd.Parameters.AddWithValue("@matricule", matricule);

                        SQLiteDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                            metier = dr.GetString(0);
                        dr.Close();

                        Membre nouveauMembre = new Membre(nomComplet, metier, "images/grades/" + metier + ".png");
                        nouveauMembre.Name = matricule;

                        flpMembres.Controls.Add(nouveauMembre);
                        membres.Add(matricule);
                    }
                    // si civil
                    else
                    {
                        string sql = "SELECT specialite FROM Civil WHERE matriculeMembre = @matricule";
                        SQLiteCommand cmd = new SQLiteCommand(sql, Connexion.Connec);
                        cmd.Parameters.AddWithValue("@matricule", matricule);

                        SQLiteDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                            metier = dr.GetString(0);
                        dr.Close();

                        Membre nouveauMembre = new Membre(nomComplet, metier, "images/specs/" + metier + ".png");
                        nouveauMembre.Name = matricule;

                        flpMembres.Controls.Add(nouveauMembre);
                        membres.Add(matricule);
                    }                   
                }
            }
        }

        private void btnAjouterCapture_Click(object sender, EventArgs e)
        {
            if (cboCapture.SelectedItem != null && !string.IsNullOrEmpty(txtObjectif.Text) && txtObjectif.Text != "0")
            {
                string idEspece = cboCapture.SelectedValue.ToString();
                if (!capture.ContainsKey(idEspece))
                {
                    try
                    {
                        // Select arme et couleur pour le label arme et l'image
                        string sql = "SELECT typeArme, couleur " +
                            "FROM Ennemi " +
                            "JOIN Espece ON id = idEspece " +
                            "WHERE idEspece = @id;";
                        SQLiteCommand cmd = new SQLiteCommand(sql, Connexion.Connec);
                        cmd.Parameters.AddWithValue("@id", idEspece);

                        SQLiteDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            string nom = cboCapture.Text;
                            string arme = dr.GetString(0);
                            int nb = int.Parse(txtObjectif.Text);
                            string couleur = dr.GetString(1);

                            Alien newAlien = new Alien(nom, arme, nb.ToString(), "images/especes/" + couleur + ".png");
                            flpCaptures.Controls.Add(newAlien);
                            capture.Add(idEspece, nb);

                            grpEtape3.Enabled = true;
                        }
                        dr.Close();
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show(err.Message);
                    }
                }
            }
        }

        private void btnSuppriemrAlien_Click(object sender, EventArgs e)
        {
            // On regarde tous les aliens dans la liste
            List<Alien> aliensASupprimer = new List<Alien>();
            foreach (Alien alien in flpCaptures.Controls)
            {
                if (alien.Selectionne == true)
                {
                    aliensASupprimer.Add(alien);
                }
            }

            // On supprime chaque alien sélectionné
            foreach (Alien alien in aliensASupprimer)
            {
                string idEspece = cboCapture.SelectedValue.ToString();

                if (capture.ContainsKey(idEspece))
                {
                    capture.Remove(idEspece);
                }
                flpCaptures.Controls.Remove(alien);
                alien.Dispose();
            }
        }

        private void btnSupprimerMembre_Click(object sender, EventArgs e)
        {
            // On regarde tous les membres dans la liste
            List<Membre> membresASupprimer = new List<Membre>();

            foreach (Membre membre in flpMembres.Controls)
            {
                if (membre.Selectionne == true)
                {
                    membresASupprimer.Add(membre);
                }
            }

            // On supprime chaque membre sélectionné
            foreach (Membre membre in membresASupprimer)
            {
                membres.Remove(membre.Name);
                flpMembres.Controls.Remove(membre);
                membre.Dispose();
            }
        }

        private async void btnValider_Click(object sender, EventArgs e)
        {
            bool succes = true;

            // Gestion erreurs
            bool entreesValides = true;
            errNBMembres.Clear();
            if (membres.Count != int.Parse(txtEquipage.Text) - 1)
            {
                errNBMembres.SetError(flpMembres, "Le nombre d'équipiers requis doit être égal à "+txtEquipage.Text+".\nVous pouvez changer cette valeur à la page 1");
                entreesValides = false;
            }
            errBudget.Clear();
            errPage1.Clear();
            if (int.Parse(txtBudget.Text) < 1000) 
            {
                errPage1.SetError(btnRetour,"Votre budget est incorrècte");
                errBudget.SetError(txtBudget, "Votre Budget est trop bas...");
                entreesValides = false;
            }

            // Animation (vibrement) lors d'une erreur d'entré
            if (!entreesValides) 
            {
                var originalLocation = this.Location;
                int pixels = 10;
                int count = 6;

                for (int i = 0; i < count; i++)
                {
                    this.Location = new Point(originalLocation.X + pixels, originalLocation.Y);
                    await Task.Delay(30);
                    this.Location = new Point(originalLocation.X - pixels, originalLocation.Y);
                    await Task.Delay(30);
                }

                this.Location = originalLocation;
                return; 
            }

            try
            {
                // Insert Mission
                string sql = "INSERT INTO Mission " +
                    "(nomPlanete, numero, nbMembreRequis, dateDepart, dateRetour, matriculeChef, feuilleDeRoute, objectifDatabaz, budget) " +
                    "VALUES (@nomPlanete, @numero, @nbMembreRequis, @dateDepart, @dateRetour, @matriculeChef, @feuilleDeRoute, @objectifDatabaz, @budget)";
                
                SQLiteCommand cmd = new SQLiteCommand(sql, Connexion.Connec);
                cmd.Parameters.AddWithValue("@nomPlanete", nomPlanete);
                cmd.Parameters.AddWithValue("@numero", numero);
                cmd.Parameters.AddWithValue("@nbMembreRequis", txtEquipage.Text);
                cmd.Parameters.AddWithValue("@dateDepart", dtpDepart.Value.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@dateRetour", dtpRetour.Value.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@matriculeChef", matriculeChef);
                cmd.Parameters.AddWithValue("@feuilleDeRoute", txtFeuille.Text);
                cmd.Parameters.AddWithValue("@objectifDatabaz", txtDataBaz.Text);
                cmd.Parameters.AddWithValue("@budget", txtBudget.Text);

                int ajout = cmd.ExecuteNonQuery();
                if (ajout == 1)
                    succes = true;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                succes = false;
            }

            try
            {
                // Insert Chef
                string sql = "INSERT INTO Composer VALUES(@nomPlanete, @numero, @matriculeMembre)";
                SQLiteCommand cmd = new SQLiteCommand(sql, Connexion.Connec);
                cmd.Parameters.AddWithValue("@nomPlanete", nomPlanete);
                cmd.Parameters.AddWithValue("@numero", numero);
                cmd.Parameters.AddWithValue("@matriculeMembre", matriculeChef);
                cmd.ExecuteNonQuery();

                // Insert membres dans Composer
                foreach (string matricule in membres)
                {
                    sql = "INSERT INTO Composer VALUES(@nomPlanete, @numero, @matriculeMembre)";
                    cmd = new SQLiteCommand(sql, Connexion.Connec);
                    cmd.Parameters.AddWithValue("@nomPlanete", nomPlanete);
                    cmd.Parameters.AddWithValue("@numero", numero);
                    cmd.Parameters.AddWithValue("@matriculeMembre", matricule);
                    cmd.ExecuteNonQuery();
                }

                // Insert objectifCapturr
                foreach (var kv in capture)
                {
                    sql = "INSERT INTO ObjectifCapture VALUES(@nomPlanete, @numero, @idEspeceEnnemi, @nb)";
                    cmd = new SQLiteCommand(sql, Connexion.Connec);
                    cmd.Parameters.AddWithValue("@nomPlanete", nomPlanete);
                    cmd.Parameters.AddWithValue("@numero", numero);
                    cmd.Parameters.AddWithValue("@idEspeceEnnemi", kv.Key);
                    cmd.Parameters.AddWithValue("@nb", kv.Value);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                succes = false;
            }

            // Affiche le "Feedback" et ferme le form
            if (succes)
            {
                grpEtape3.Visible = false;
                btnRetour.Visible = false;
                pictureBox1.Visible = false;

                lblSucces.Visible = true;
                await Task.Delay(3000);
                this.Hide();
            }

        }

        private void btnValiderEtape1_Click(object sender, EventArgs e)
        {
            grpEtape2.Visible = false;
            grpEtape1.Visible = false;
            grpEtape3.Visible = true;
            btnRetour.Visible = true;
            btnValiderEtape1.Visible = false;
        }

        private void cboFiltreMembres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFiltreMembres.SelectedItem != null)
            {
                ChargerMembres(cboFiltreMembres.SelectedItem.ToString());
            }
        }

        private void btnRetour_Click(object sender, EventArgs e)
        {
            errPage1.Clear();
            btnRetour.Visible = false;
            grpEtape3.Visible = false;
            grpEtape1.Visible = true;
            grpEtape2.Visible = true;
            btnValiderEtape1.Visible = true;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}