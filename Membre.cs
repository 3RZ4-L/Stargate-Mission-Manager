using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace projetStarGate
{
    public partial class Membre : UserControl
    {
        public bool Selectionne { get; private set; } = false;

        public Membre()
        {
            InitializeComponent();
        }

        public Membre(string nom, string metier, string nomImage)
        {
            InitializeComponent();

            lblNom.Text = nom;
            lblMetier.Text = metier;

            if (!string.IsNullOrEmpty(nomImage) && File.Exists(nomImage))
            {
                pictureBox1.Image = Image.FromFile(nomImage);
            }
        }

        private void Membre_Click(object sender, EventArgs e)
        {
            if (!Selectionne)
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