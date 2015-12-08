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
    {
        public Param_affectation_horaire param;


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


        public void afficher_parametres(Param_affectation_horaire param)
        {
            checkBox1.Checked = param.sortie_chemins;
            checkBox2.Checked = param.demitours;
            checkBox3.Checked = param.sortie_services;
            checkBox4.Checked = param.sortie_turns;
            checkBox5.Checked = param.sortie_noeuds;
            comboBox1.SelectedIndex = param.sortie_temps;
            comboBox2.SelectedIndex = param.algorithme;
            textBox2.Text = param.texte_coef_tmap;
            textBox3.Text = param.nom_reseau;
            textBox4.Text = param.nom_matrice;
            textBox5.Text = param.texte_cveh;
            textBox6.Text = param.texte_cmap;
            textBox7.Text = param.texte_cwait;
            textBox8.Text = param.texte_cboa;
            textBox9.Text = param.texte_tboa;
            textBox10.Text = param.max_nb_buckets.ToString();
            textBox11.Text = param.nb_jours.ToString();

            textBox1.Text = param.param_dijkstra.ToString();
            textBox12.Text = param.pu.ToString();
            textBox13.Text = param.nom_penalites;
            textBox14.Text = param.texte_tboa_max;
            textBox15.Text = param.tmapmax.ToString();
            textBox16.Text = param.texte_toll.ToString();
            textBox17.Text = param.texte_filtre_sortie.ToString();
            textBox18.Text = param.temps_max.ToString();

        }

        private void Aff_hor_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = param.sortie_chemins;
            checkBox2.Checked = param.demitours;
            checkBox3.Checked = param.sortie_services;
            checkBox4.Checked = param.sortie_turns;
            checkBox5.Checked = param.sortie_noeuds;
            comboBox1.SelectedIndex = param.sortie_temps;
            comboBox2.SelectedIndex = param.algorithme;
            textBox2.Text = param.texte_coef_tmap;
            textBox3.Text = param.nom_reseau;
            textBox4.Text = param.nom_matrice;
            textBox5.Text = param.texte_cveh;
            textBox6.Text = param.texte_cmap;
            textBox7.Text = param.texte_cwait;
            textBox8.Text = param.texte_cboa;
            textBox9.Text = param.texte_tboa;
            textBox10.Text = param.max_nb_buckets.ToString();
            textBox11.Text = param.nb_jours.ToString();

            textBox1.Text = param.param_dijkstra.ToString();
            textBox12.Text = param.pu.ToString();
            textBox13.Text = param.nom_penalites;
            textBox14.Text = param.texte_tboa_max;
            textBox15.Text = param.tmapmax.ToString();
            textBox16.Text = param.texte_toll.ToString();
            textBox17.Text = param.texte_filtre_sortie.ToString();
            textBox18.Text = param.temps_max.ToString();
            ToolTip info_bulle = new ToolTip();
            info_bulle.SetToolTip(checkBox1, "Cocher pour obtenir le détail des itinéraires");
            info_bulle.SetToolTip(checkBox2, "Cocher pour interdire les demi-tours");
            info_bulle.SetToolTip(checkBox3, "Cocher pour obtenir les volumes par service");
            info_bulle.SetToolTip(checkBox4, "Cocher pour obtenir les volumes au carrefour et le détail des correspondances");
            info_bulle.SetToolTip(button3, "Cliquer pour sélectionner le fichier réseau");
            info_bulle.SetToolTip(button4, "Cliquer pour sélectionner le fichier matrice");
            info_bulle.SetToolTip(button5, "Cliquer pour sélectionner le fichier pénalités et correspondances personnalisées");
            info_bulle.SetToolTip(textBox5, "Coefficient de pondération du temps à bord du véhicule");
            info_bulle.SetToolTip(textBox6, "Coefficient de pondération du temps en transport individuel");
            info_bulle.SetToolTip(textBox8, "Coefficient de pondération du temps de correspondance");
            info_bulle.SetToolTip(textBox2, "Coefficient multiplicateur du temps de transport individuel");
            info_bulle.SetToolTip(textBox9, "Temps de correspondance minimum (minutes)");
            info_bulle.SetToolTip(textBox14, "Temps de correspondance maximum (minutes)");
            info_bulle.SetToolTip(textBox11, "Nombre de jours en plus de celui en cours à prendre en compte pour utiliser les services TC");
            info_bulle.SetToolTip(comboBox1, "Quel niveau détail dans la sortie des temps détaillés");
            info_bulle.SetToolTip(comboBox2, "Sélection de l'algorithme de calcul du plus court chemin");
            info_bulle.SetToolTip(textBox1, "Paramètre de l'algorithme. Cela peut influer de manière non négligeable sur les temps de calcul");
            info_bulle.SetToolTip(textBox10, "Nombre d'intervalles dans l'algorithme. Prendre des valeurs plus grandes pour de grands réseaux");
            info_bulle.SetToolTip(textBox12, "Exposant pour la taille des intervalles. En général 2 pour les réseaux surfaciques, 3 pour les volumiques");
            info_bulle.SetToolTip(textBox16, "Coefficient de pondération du péage");
            info_bulle.SetToolTip(textBox17, "Filtre type de tronçon temps détaillés");
            info_bulle.SetToolTip(textBox18, "cout maximum fichier résultat");
            info_bulle.SetToolTip(checkBox5, "Sortie résultats par noeuds");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            param.sortie_chemins = checkBox1.Checked;
            param.demitours = checkBox2.Checked;
            param.sortie_temps = comboBox1.SelectedIndex;
            param.sortie_turns = checkBox4.Checked;
            param.sortie_services = checkBox3.Checked;

            param.nom_reseau = textBox3.Text;
            param.nom_matrice = textBox4.Text;
            param.nom_penalites = textBox13.Text;


            param.texte_coef_tmap = textBox2.Text;

            param.texte_cveh = textBox5.Text;
            param.texte_cmap = textBox6.Text;
            param.tmapmax = Convert.ToSingle(textBox15.Text);
            param.texte_cwait = textBox7.Text;
            param.texte_cboa = textBox8.Text;
            param.texte_tboa = textBox9.Text;
            param.texte_tboa_max = textBox14.Text;
            param.texte_toll = textBox16.Text;
            param.temps_max = float.Parse(textBox18.Text);
            param.nb_jours = int.Parse(textBox11.Text);
            param.max_nb_buckets = Convert.ToInt32(textBox10.Text);
            param.algorithme = Convert.ToInt32(comboBox2.SelectedIndex);
            param.param_dijkstra = Convert.ToSingle(textBox1.Text);
            param.pu = Convert.ToSingle(textBox12.Text);
            param.texte_filtre_sortie = textBox17.Text;
            param.sortie_noeuds = checkBox5.Checked;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {

                param.nom_sortie = saveFileDialog1.FileName;
                param.test_OK = true;
                if (param.nom_sortie != null)
                {
                    this.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            param.test_OK = false;
            this.Close();
        }


        private void button5_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            textBox13.Text = openFileDialog1.FileName;
        }



        private void textBox14_TextChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {






        }

        private void button7_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            String nom_param = openFileDialog1.FileName;
            Param_affectation_horaire param = new Param_affectation_horaire();
            if (System.IO.File.Exists(nom_param))
            {
                param.Lit_parametres(nom_param);
                afficher_parametres(param);
            }
        }
    }
        
    
}