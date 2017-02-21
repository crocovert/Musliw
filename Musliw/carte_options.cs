using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Musliw
{
    public partial class Affichage : Form
    {
        public Fenetre fenetre = new Fenetre();

        public Affichage(Fenetre fen)
        {
            InitializeComponent();
            
            fenetre = fen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            colorDialog1.ShowDialog();
            button1.BackColor = colorDialog1.Color;
        }

        private void Affichage_Shown(object sender, EventArgs e)
        {

        }

        private void Affichage_Load(object sender, EventArgs e)
        {
            numericUpDown1.Value = (decimal)fenetre.epaisseur;
            numericUpDown2.Value = (decimal)fenetre.ecart;
            numericUpDown3.Value = (decimal)fenetre.volume_echelle;
            numericUpDown4.Value = (decimal)fenetre.taille_texte;
            button3.BackColor = fenetre.brosse_couleur;
            button4.BackColor = fenetre.couleur_texte;
            button1.BackColor = fenetre.stylo_couleur;
            textBox1.Text = fenetre.echelle.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fenetre.epaisseur = (float)numericUpDown1.Value;
            fenetre.ecart = (float)numericUpDown2.Value;
            fenetre.volume_echelle = (float)numericUpDown3.Value;
            fenetre.taille_texte = (float)numericUpDown4.Value;          
            fenetre.stylo_couleur = button1.BackColor;
            fenetre.brosse_couleur = button3.BackColor;
            fenetre.couleur_texte = button4.BackColor;


            fenetre.echelle = Convert.ToSingle(textBox1.Text);
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            button3.BackColor = colorDialog1.Color;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            button4.BackColor = colorDialog1.Color;
        }

        
    }
}