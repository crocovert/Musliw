using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Musliw
{
    public partial class Aff_hor : Form
    {public Param_affectation_horaire param ;


        public Aff_hor(Param_affectation_horaire parametres)
        {
            InitializeComponent();
            param = parametres;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            textBox4.Text = openFileDialog1.FileName;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            textBox3.Text = openFileDialog1.FileName;
        }

        private void Aff_hor_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = param.sortie_chemins;
            checkBox2.Checked = param.sortie_temps;
            textBox2.Text = param.coef_tmap.ToString();
            textBox3.Text = param.nom_reseau;
            textBox4.Text = param.nom_matrice;
            textBox5.Text = param.cveh.ToString();
            textBox6.Text = param.cmap.ToString();
            textBox7.Text = param.cwait.ToString();
            textBox8.Text = param.cboa.ToString();
            textBox9.Text = param.tboa.ToString();
            textBox10.Text = param.max_nb_buckets.ToString();
            textBox11.Text = param.nb_jours.ToString();
            
            textBox1.Text = param.param_dijkstra.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            param.sortie_chemins = checkBox1.Checked;
            param.sortie_temps = checkBox2.Checked;
            param.coef_tmap = Convert.ToSingle(textBox2.Text);
            param.nom_reseau = textBox3.Text;
            param.nom_matrice = textBox4.Text;
            param.cveh = Convert.ToSingle(textBox5.Text);
             param.cmap= Convert.ToSingle(textBox6.Text);
             param.cwait= Convert.ToSingle(textBox7.Text);
             param.cboa= Convert.ToSingle(textBox8.Text);
             param.tboa= Convert.ToSingle(textBox9.Text);
             param.nb_jours = Convert.ToInt32(textBox11.Text);
             param.max_nb_buckets = Convert.ToInt32(textBox10.Text);
             param.algorithme = 0;
             param.param_dijkstra = Convert.ToSingle(textBox1.Text);
             saveFileDialog1.ShowDialog();
             param.nom_sortie = saveFileDialog1.FileName;
             if (param.nom_sortie != null)
             {
                 this.Close();
             }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }
    }
}