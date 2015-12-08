using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Musliw
{
    public partial class import_matrice : Form
    {
        public etude projet;


        public import_matrice(etude proj)
        {
            InitializeComponent();
            int i;
            projet = proj;
            for (i = 0; i < proj.reseaux.Count; i++)
            {
                this.comboBox1.Items.Add(projet.reseaux[i].nom);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {


            projet.reseau_actif = comboBox1.SelectedIndex;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}