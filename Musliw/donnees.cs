using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Musliw
{
    public partial class Donnees : Form
    {
        public etude projet;
        public int nproj=0;

        public Donnees(etude proj, int i)
        {
            InitializeComponent();
            projet = proj;
            nproj = i;
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void donnees_Load(object sender, EventArgs e)
        {
            int i;
            

            this.Text = projet.reseaux[nproj].nom + " Données";

            /*for (i = 0; i < projet.reseaux[nproj].nodes.Count; i++)
            {
                if (projet.reseaux[nproj].nodes[i].i != 0)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1["i", n].Value = projet.reseaux[nproj].nodes[i].i;
                    dataGridView1["x", n].Value = projet.reseaux[nproj].nodes[i].x;
                    dataGridView1["y", n].Value = projet.reseaux[nproj].nodes[i].y;
                    dataGridView1["ci", n].Value = projet.reseaux[nproj].nodes[i].ci;
                    n++;
                }
                
            }*/
            for (i = 0; i < projet.reseaux[nproj].nodes.Count; i++)
                {
                    dataSet11.Nodes.AddNodesRow(i, projet.reseaux[nproj].nodes[i].i, projet.reseaux[nproj].nodes[i].x, projet.reseaux[nproj].nodes[i].y, projet.reseaux[nproj].nodes[i].texte);
                            
                        
                }
                for (i = 0; i < projet.reseaux[nproj].links.Count; i++)
                {
                        dataSet11.Links.AddLinksRow(i,projet.reseaux[nproj].links[i].no, projet.reseaux[nproj].links[i].nd, projet.reseaux[nproj].links[i].ligne, projet.reseaux[nproj].links[i].longueur, projet.reseaux[nproj].links[i].volau, projet.reseaux[nproj].links[i].h,projet.reseaux[nproj].links[i].texte);

                }

            }

        private void dataSet11BindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }
        }
    
}