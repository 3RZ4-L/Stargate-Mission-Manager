using System;
using System.Drawing;
using System.Windows.Forms;

namespace projetStarGate
{
    public partial class Alien : UserControl
    {
        public bool Selectionne { get; private set; } = false;

        public Alien()
        {
            InitializeComponent();
        }

        public Alien(string nom, string arme, string nb, string nomImage)
        {
            InitializeComponent();

            lblNom.Text = nom;
            lblArme.Text = arme;
            lblNB.Text = nb;

            if (!string.IsNullOrEmpty(nomImage) && System.IO.File.Exists(nomImage))
            {
                pictureBox1.Image = Image.FromFile(nomImage);
            }
        }

        private void Alien_Click(object sender, EventArgs e)
        {
            if (Selectionne == false)
            {
                Selectionne = true;
                BackColor = Color.LightGray;
            }
            else
            {
                Selectionne = false;
                BackColor = Color.Transparent;
            }
        }
    }
}