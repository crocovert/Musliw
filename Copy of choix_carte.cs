using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Musliw
{
    public partial class Choix_reseau : Form
    {
        public int nproj = 0;
        public etude projet;

        public Choix_reseau(etude proj)
        {
            int i;
            InitializeComponent();
            projet = proj;
            for (i = 0; i < proj.reseaux.Count; i++)
            {
                this.comboBox1.Items.Add(projet.reseaux[i].nom);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i=this.comboBox1.SelectedIndex;
            Carte carte = new Carte(projet,i);
            carte.MdiParent = this.MdiParent;
            carte.Show();
            this.Close();
        }
    }
}