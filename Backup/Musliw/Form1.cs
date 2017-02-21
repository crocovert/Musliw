using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Musliw
{

    public partial class MusliW : Form
    {
        public etude projet = new etude();
        public MusliW()
        {
            InitializeComponent();
            projet.param_affectation_horaire.tboa = 2f;
            projet.param_affectation_horaire.sortie_chemins = false;
            projet.param_affectation_horaire.sortie_temps = false;
            projet.param_affectation_horaire.coef_tmap = 1f;
            projet.param_affectation_horaire.cwait = 3f;
            projet.param_affectation_horaire.cveh = 1f;
            projet.param_affectation_horaire.cmap = 2f;
            projet.param_affectation_horaire.cboa = 5f;
            projet.param_affectation_horaire.algorithme = 0;
            projet.param_affectation_horaire.param_dijkstra = 200f;
            projet.param_affectation_horaire.nb_jours = 0;
        }

        private void importerRéseauEmme2ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            projet.reseaux.Add(new network());
            int i = 0, j, num_res;
            bool ci = false;
            float timau = 0, xi, yi;
            string chaine, carte = "";
            string[] ch;

            //System.IO.StreamWriter resultat = new System.IO.StreamWriter("c:\\temp\\result.txt");
            //network reseau = new network();
            //network reseau = new network();  
            projet.reseau_actif = projet.reseaux.Count - 1;
            num_res = projet.reseaux.Count - 1;
            openFileDialog1.ShowDialog();
            string nom_reseau = openFileDialog1.FileName;
            if (System.IO.File.Exists(nom_reseau))
            {
                System.IO.StreamReader fichier_reseau = new System.IO.StreamReader(nom_reseau);
                projet.reseaux[num_res].matrices.Add(new matrix());
                do
                {

                    projet.reseaux[num_res].nom = System.IO.Path.GetFileNameWithoutExtension(nom_reseau);
                    chaine = fichier_reseau.ReadLine();
                    if (chaine == null) { goto lecture; }
                    if (chaine == "" || chaine.Substring(0, 1) == " ") { goto lecture; }

                    if (chaine.Substring(0, 1) == "c") { goto lecture; }
                    string[] param ={ " " };

                    if (chaine.Substring(0, 7) == "t nodes" || chaine.Substring(0, 7) == "t links")
                    {
                        carte = chaine.Substring(0, 7);
                        goto lecture;

                    }

                    //inserer noeuds// 
                    if (carte == "t nodes")
                    {
                        ch = chaine.Split(param, System.StringSplitOptions.RemoveEmptyEntries);

                        i = (int)System.Convert.ToSingle(ch[1]);
                        xi = System.Convert.ToSingle(ch[2]);
                        yi = System.Convert.ToSingle(ch[3]);
                        //emcombrement du reseau
                        if (xi > projet.reseaux[num_res].xu)
                        {
                            projet.reseaux[num_res].xu = xi;
                        }
                        if (xi < projet.reseaux[num_res].xl)
                        {
                            projet.reseaux[num_res].xl = xi;
                        }
                        if (yi > projet.reseaux[num_res].yu)
                        {

                            projet.reseaux[num_res].yu = yi;

                        }
                        if (yi < projet.reseaux[num_res].yl)
                        {
                            projet.reseaux[num_res].yl = yi;
                        }

                        if (ch[0] == "a*")
                        {
                            ci = true;



                        }
                        else
                        { ci = false; }





                        node ni = new node();
                        node nul = new node();
                        ni.i = i;
                        ni.x = xi;
                        ni.y = yi;
                        ni.ci = ci;
                        ni.is_visible = true;
                        
                        while (projet.reseaux[num_res].nodes.Count < i + 1)
                        {
                            projet.reseaux[num_res].nodes.Add(nul);
                        }
                        projet.reseaux[num_res].nodes[i] = ni;


                    }
                    //inserer liens
                    else if (carte == "t links")
                    {
                        ch = chaine.Split(param, System.StringSplitOptions.RemoveEmptyEntries);

                        link lien = new link();
                        lien.no = (int)System.Convert.ToSingle(ch[1]);
                        lien.nd = (int)System.Convert.ToSingle(ch[2]);
                        lien.longueur = System.Convert.ToSingle(ch[3]); ;
                        lien.modes = ch[4].ToString();
                        lien.type = (int)System.Convert.ToSingle(ch[5]); ;
                        lien.lanes = System.Convert.ToSingle(ch[6]); ;
                        lien.vdf = (int)System.Convert.ToSingle(ch[7]); ;
                        lien.temps = timau;
                        switch (lien.vdf)
                        {
                            case 1:
                                lien.v0 = 130f;
                                lien.a = 1.03f;
                                lien.b = 0.94f;
                                lien.n = 8f;
                                break;
                            case 2:
                                lien.v0 = 110f;
                                lien.a = 1.03f;
                                lien.b = 0.94f;
                                lien.n = 8f;
                                break;
                            case 4:
                                lien.v0 = 120f;
                                lien.a = 1.03f;
                                lien.b = 0.94f;
                                lien.n = 8f;
                                break;
                            case 3:
                                lien.v0 = 90f;
                                lien.a = 1.03f;
                                lien.b = 0.94f;
                                lien.n = 8f;
                                break;
                            case 24:
                                lien.v0 = 90f;
                                lien.a = 1.1f;
                                lien.b = 0.5f;
                                lien.n = 8f;
                                break;
                            case 5:
                                lien.v0 = 90f;
                                lien.a = 1.08f;
                                lien.b = 0.64f;
                                lien.n = 8f;
                                break;
                            case 16:
                                lien.v0 = 80f;
                                lien.a = 1.21f;
                                lien.b = 0.56f;
                                lien.n = 8f;
                                break;
                            case 6:
                                lien.v0 = 70f;
                                lien.a = 1.08f;
                                lien.b = 0.64f;
                                lien.n = 8f;
                                break;
                            case 9:
                                lien.v0 = 70f;
                                lien.a = 1.03f;
                                lien.b = 0.94f;
                                lien.n = 8f;
                                break;
                            case 18:
                                lien.v0 = 70f;
                                lien.a = 1.08f;
                                lien.b = 0.64f;
                                lien.n = 8f;
                                break;
                            case 19:
                                lien.a = 60f;
                                lien.b = 1.08f;
                                lien.n = 0.64f;
                                break;
                            case 14:
                                lien.v0 = 60f;
                                lien.a = 1.08f;
                                lien.b = 0.64f;
                                lien.n = 8f;
                                break;
                            case 13:
                                lien.v0 = 60f;
                                lien.a = 1.08f;
                                lien.b = 0.64f;
                                lien.n = 8f;
                                break;
                            case 10:
                                lien.v0 = 50f;
                                lien.a = 1.08f;
                                lien.b = 0.64f;
                                lien.n = 8f;
                                break;
                            case 12:
                                lien.v0 = 50f;
                                lien.a = 1.08f;
                                lien.b = 0.64f;
                                lien.n = 8f;
                                break;
                            case 32:
                                lien.v0 = 50f;
                                lien.a = 1.21f;
                                lien.b = 0.56f;
                                lien.n = 8f;
                                break;
                            case 7:
                                lien.v0 = 40f;
                                lien.a = 1.21f;
                                lien.b = 0.56f;
                                lien.n = 8f;
                                break;
                            case 28:
                                lien.v0 = 40f;
                                lien.a = 1.1f;
                                lien.b = 0.5f;
                                lien.n = 8f;
                                break;
                            case 31:
                                lien.v0 = 30f;
                                lien.a = 1.21f;
                                lien.b = 0.56f;
                                lien.n = 8f;
                                break;
                            default:
                                lien.v0 = 1f;
                                lien.a = 0f;
                                lien.b = 0f;
                                lien.n = 0f;
                                break;


                        }

                        lien.touche = 0;
                        lien.cout = -1;
                        projet.reseaux[num_res].links.Add(lien);
                        //        MessageBox.Show(lien.no.ToString()+" "+lien.nd.ToString());
                    }


                lecture: ;

                } while (fichier_reseau.EndOfStream == false);
                fichier_reseau.Close();

                //construction du graphe
                // table des prédécesseurs et successeurs de noeuds
                for (i = 0; i < projet.reseaux[projet.reseau_actif].links.Count; i++)
                {
                    turn virage = new turn();
                    virage.numero = i;
                    virage.temps = 0;
                    virage.distance = 0;
                    virage.cout = 0;

                    projet.reseaux[projet.reseau_actif].nodes[projet.reseaux[projet.reseau_actif].links[i].nd].pred.Add(virage);
                    projet.reseaux[projet.reseau_actif].nodes[projet.reseaux[projet.reseau_actif].links[i].no].succ.Add(virage);
                    //                    Console.SetCursorPosition(1, Console.CursorTop-1);

                }
                // table des prédécesseurs et successeurs de tronçons
                Console.WriteLine("création de la topologie des noeuds terminée");
                for (i = 0; i < projet.reseaux[projet.reseau_actif].links.Count; i++)
                {
                    for (j = 0; j < projet.reseaux[projet.reseau_actif].nodes[projet.reseaux[projet.reseau_actif].links[i].no].pred.Count; j++)
                    {
                        turn virage = new turn();
                        projet.reseaux[projet.reseau_actif].links[i].arci.Add(virage);
                        projet.reseaux[projet.reseau_actif].links[i].arci[j].numero = projet.reseaux[projet.reseau_actif].nodes[projet.reseaux[projet.reseau_actif].links[i].no].pred[j].numero;

                    }
                    for (j = 0; j < projet.reseaux[projet.reseau_actif].nodes[projet.reseaux[projet.reseau_actif].links[i].nd].succ.Count; j++)
                    {
                        turn virage = new turn();
                        projet.reseaux[projet.reseau_actif].links[i].arcj.Add(virage);
                        projet.reseaux[projet.reseau_actif].links[i].arcj[j].numero = projet.reseaux[projet.reseau_actif].nodes[projet.reseaux[projet.reseau_actif].links[i].nd].succ[j].numero;

                    }


                }





            }
        }

        private void donnéesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Choix_donnees choix_donnees = new Choix_donnees(projet);
            choix_donnees.MdiParent = this;
            choix_donnees.Show();
        }

        private void carteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Choix_carte choix_carte = new Choix_carte(projet);
            choix_carte.MdiParent = this;
            choix_carte.Show();

        }

        private void importerMatriceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            import_matrice imp_mat = new import_matrice(projet);
            imp_mat.ShowDialog();
            projet.reseau_actif = 0;
            //            projet.reseaux[projet.reseau_actif].matrices.Add();
            string chaine;
            int ori, des;
            string[] ch;
            float mf;
            string[] param ={ " " };
            //lecture matrice
            openFileDialog1.ShowDialog();
            if (System.IO.File.Exists(openFileDialog1.FileName))
            {
                System.IO.StreamReader fichier_matrice = new System.IO.StreamReader(openFileDialog1.FileName);
                while (fichier_matrice.EndOfStream == false)
                {
                    chaine = fichier_matrice.ReadLine();
                    ch = chaine.Split(param, StringSplitOptions.RemoveEmptyEntries);
                    ori = (int)System.Convert.ToSingle(ch[0]);
                    des = (int)System.Convert.ToSingle(ch[1]);
                    mf = System.Convert.ToSingle(ch[2]);


                    while (projet.reseaux[projet.reseau_actif].matrices[0].o.Count <= ori)
                    {
                        projet.reseaux[projet.reseau_actif].matrices[0].o.Add(new vecteur());
                    }
                    while (projet.reseaux[projet.reseau_actif].matrices[0].o[ori].d.Count <= des)
                    {
                        projet.reseaux[projet.reseau_actif].matrices[0].o[ori].d.Add(new float());
                    }

                    projet.reseaux[projet.reseau_actif].matrices[0].o[ori].d[des] = mf;
                }
            }
        }

        private void affectationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Avancement avancement = new Avancement();
            avancement.Show();
            int i, j;
            // Console.WriteLine("création de la topologie des tronçons terminée");
            //plus courts chemins
            Queue<int> touches = new Queue<int>();
            Queue<int> calcules = new Queue<int>();
            List<List<int>> gga_nq = new List<List<int>>();

            System.IO.StreamWriter fichier_sortie = new System.IO.StreamWriter("c:\\result.txt");



            //initilisation
            for (i = 0; i < projet.reseaux[projet.reseau_actif].links.Count; i++)
            {

                projet.reseaux[projet.reseau_actif].links[i].volau = 0;
                projet.reseaux[projet.reseau_actif].links[i].touche = 0;
                projet.reseaux[projet.reseau_actif].links[i].cout = 0;
                projet.reseaux[projet.reseau_actif].links[i].tmap = 0;
                projet.reseaux[projet.reseau_actif].links[i].pivot = -1;
                projet.reseaux[projet.reseau_actif].links[i].is_queue = false;

                //projet.reseaux[projet.reseau_actif].links[i].temps = projet.reseaux[projet.reseau_actif].links[i].fd(projet.reseaux[projet.reseau_actif].links[i].volau, projet.reseaux[projet.reseau_actif].links[i].longueur, 0f, projet.reseaux[projet.reseau_actif].links[i].lanes * 1000, projet.reseaux[projet.reseau_actif].links[i].v0, projet.reseaux[projet.reseau_actif].links[i].a, projet.reseaux[projet.reseau_actif].links[i].b, projet.reseaux[projet.reseau_actif].links[i].n);

            }


            int p, q;
            for (p = 0; p < projet.reseaux[projet.reseau_actif].matrices[0].o.Count; p++)
            {

                if (projet.reseaux[projet.reseau_actif].matrices[0].o[p].d.Count > 0)
                {
                    for (i = 0; i < projet.reseaux[projet.reseau_actif].links.Count; i++)
                    {
                        projet.reseaux[projet.reseau_actif].links[i].touche = 0;
                        projet.reseaux[projet.reseau_actif].links[i].cout = 0;
                        projet.reseaux[projet.reseau_actif].links[i].tmap = 0;
                        projet.reseaux[projet.reseau_actif].links[i].pivot = -1;

                    }
                    int depart = p;
                    int successeur,pivot = -1, bucket, id_bucket = 0; ;

                    for (j = 0; j < projet.reseaux[projet.reseau_actif].nodes[depart].succ.Count; j++)
                    {
                        successeur = projet.reseaux[projet.reseau_actif].nodes[depart].succ[j].numero; 
                        bucket = Convert.ToInt32((Math.Pow(projet.reseaux[projet.reseau_actif].links[successeur].cout, 2) / projet.param_affectation_horaire.param_dijkstra));
                        while (bucket >= gga_nq.Count)
                        {
                            gga_nq.Add(new List<int>());
                        }
                        gga_nq[bucket].Add(successeur);

                        //touches.Enqueue(projet.reseaux[projet.reseau_actif].nodes[depart].succ[j].numero);
                        projet.reseaux[projet.reseau_actif].links[projet.reseaux[projet.reseau_actif].nodes[depart].succ[j].numero].touche = 1;
                        projet.reseaux[projet.reseau_actif].links[projet.reseaux[projet.reseau_actif].nodes[depart].succ[j].numero].cout = projet.reseaux[projet.reseau_actif].links[projet.reseaux[projet.reseau_actif].nodes[depart].succ[j].numero].temps;
                        projet.reseaux[projet.reseau_actif].links[projet.reseaux[projet.reseau_actif].nodes[depart].succ[j].numero].tmap = projet.reseaux[projet.reseau_actif].links[projet.reseaux[projet.reseau_actif].nodes[depart].succ[j].numero].longueur;

                    }

                        while (gga_nq.Count > id_bucket)
                        {

                            while (gga_nq[id_bucket].Count == 0)
                            {
                                id_bucket++;
                                if (id_bucket == gga_nq.Count)
                                {
                                    goto fin_gga;
                                }
                            }
                            pivot = gga_nq[id_bucket][0];
                            gga_nq[id_bucket].RemoveAt(0);

                        
                        for (j = 0; j < projet.reseaux[projet.reseau_actif].links[pivot].arcj.Count; j++)
                        {
                             successeur = projet.reseaux[projet.reseau_actif].links[pivot].arcj[j].numero;

                            if (projet.reseaux[projet.reseau_actif].links[successeur].touche == 0)
                            {
                                projet.reseaux[projet.reseau_actif].links[successeur].touche = 1;
                                projet.reseaux[projet.reseau_actif].links[successeur].cout = projet.reseaux[projet.reseau_actif].links[pivot].cout + projet.reseaux[projet.reseau_actif].links[successeur].temps;
                                projet.reseaux[projet.reseau_actif].links[successeur].tmap = projet.reseaux[projet.reseau_actif].links[pivot].tmap + projet.reseaux[projet.reseau_actif].links[successeur].longueur;
                                bucket = Convert.ToInt32((Math.Pow(projet.reseaux[projet.reseau_actif].links[successeur].cout, 2) / projet.param_affectation_horaire.param_dijkstra));
                                while (bucket >= gga_nq.Count)
                                {
                                    gga_nq.Add(new List<int>());
                                }
                                gga_nq[bucket].Add(successeur);
                                
                                projet.reseaux[projet.reseau_actif].links[successeur].pivot = pivot;
                            }
                            else if (projet.reseaux[projet.reseau_actif].links[successeur].touche == 1 || projet.reseaux[projet.reseau_actif].links[successeur].touche == 2)
                            {
                                if (projet.reseaux[projet.reseau_actif].links[successeur].cout > projet.reseaux[projet.reseau_actif].links[pivot].cout + projet.reseaux[projet.reseau_actif].links[successeur].temps)
                                {
                                    bucket = Convert.ToInt32(Math.Pow(projet.reseaux[projet.reseau_actif].links[successeur].cout, 2) / projet.param_affectation_horaire.param_dijkstra);

                                    projet.reseaux[projet.reseau_actif].links[successeur].touche = 2;
                                    projet.reseaux[projet.reseau_actif].links[successeur].tmap = projet.reseaux[projet.reseau_actif].links[pivot].tmap + projet.reseaux[projet.reseau_actif].links[successeur].longueur;
                                    projet.reseaux[projet.reseau_actif].links[successeur].cout = projet.reseaux[projet.reseau_actif].links[pivot].cout + projet.reseaux[projet.reseau_actif].links[successeur].temps;
                                    projet.reseaux[projet.reseau_actif].links[successeur].pivot = pivot;
                                    gga_nq[bucket].Remove(successeur);
                                    bucket = Convert.ToInt32(Math.Pow(projet.reseaux[projet.reseau_actif].links[successeur].cout, 2) / projet.param_affectation_horaire.param_dijkstra);
                                    gga_nq[bucket].Add(successeur);

                                }
                            }

                        }
                        //projet.reseaux[projet.reseau_actif].links[pivot].touche = 3;
                        //Console.WriteLine((touches.Count+calcules.Count).ToString());
                    }
                fin_gga:

                    avancement.textBox1.Text = p.ToString();
                    avancement.progressBar1.Value = (100 * p / projet.reseaux[projet.reseau_actif].matrices[0].o.Count);                    //Console.SetCursorPosition(1, Console.CursorTop - 1);

                    avancement.Refresh();

                    //Console.WriteLine(p.ToString());
                    for (q = 0; q < projet.reseaux[projet.reseau_actif].matrices[0].o[p].d.Count; q++)
                    {
                        int arrivee = 0;

                        if (projet.reseaux[projet.reseau_actif].matrices[0].o[p].d[q] != 0)
                        {
                            float cout_fin = 1e38f;

                            for (j = 0; j < projet.reseaux[projet.reseau_actif].nodes[q].pred.Count; j++)
                            {
                                int predecesseur = projet.reseaux[projet.reseau_actif].nodes[q].pred[j].numero;
                                if (projet.reseaux[projet.reseau_actif].links[predecesseur].pivot != -1 && projet.reseaux[projet.reseau_actif].links[predecesseur].cout <= cout_fin)
                                {
                                    arrivee = predecesseur;
                                    cout_fin = projet.reseaux[projet.reseau_actif].links[predecesseur].cout;

                                }



                            }
                            pivot = arrivee;
                            while (pivot != -1)
                            {
                                projet.reseaux[projet.reseau_actif].links[pivot].volau += projet.reseaux[projet.reseau_actif].matrices[0].o[p].d[q];
                                pivot = projet.reseaux[projet.reseau_actif].links[pivot].pivot;
                                /*                                if (pivot != -1)
                                                                {
                                                                    fichier_sortie.WriteLine(projet.reseaux[projet.reseau_actif].links[pivot].no.ToString() + " " + projet.reseaux[projet.reseau_actif].links[pivot].nd.ToString() + " " + projet.reseaux[projet.reseau_actif].links[pivot].cout.ToString() + " " + projet.reseaux[projet.reseau_actif].links[pivot].tmap.ToString());
                                                                }*/
                            }
                            if (p != q)
                            {
                                fichier_sortie.WriteLine(p.ToString() + " " + q.ToString() + " " + projet.reseaux[projet.reseau_actif].links[arrivee].cout.ToString() + " " + projet.reseaux[projet.reseau_actif].links[arrivee].tmap.ToString());
                            }
                            else
                            {
                                fichier_sortie.WriteLine(p.ToString() + " " + q.ToString() + " 0 0");
                            }
                        }
                    }
                }

            }
            avancement.Close();
            fichier_sortie.Close();
        }

        private void importerRéseauTCÀHorairesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void affectationTCÀHorairesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i, j;
            Aff_hor aff_hor = new Aff_hor(projet.param_affectation_horaire);
            aff_hor.ShowDialog();
            projet.param_affectation_horaire = aff_hor.param;
            string[] param ={ ";" };
            projet.reseaux.Add(new network());
            int num_res;


            string chaine;
            string[] ch;

            projet.reseau_actif = projet.reseaux.Count - 1;
            num_res = projet.reseaux.Count - 1;
            //openFileDialog1.ShowDialog();

            string nom_reseau = projet.param_affectation_horaire.nom_reseau;
            string nom_matrice = projet.param_affectation_horaire.nom_matrice;
            string carte = "t links";
            if (System.IO.File.Exists(nom_reseau) == true && System.IO.File.Exists(nom_matrice) == true && nom_reseau != null && nom_matrice != null)
            {
                System.IO.StreamReader fichier_reseau = new System.IO.StreamReader(nom_reseau);
                //   projet.reseaux[num_res].matrices.Add(new matrix());

                projet.reseaux[projet.reseau_actif].nom = System.IO.Path.GetFileNameWithoutExtension(nom_reseau);
                while (fichier_reseau.EndOfStream == false)
                {
                lecture:
                    chaine = fichier_reseau.ReadLine();

                    if (chaine.Substring(0,7)=="t nodes")
                    {
                        carte="t nodes";
                        goto lecture;
                    }
                    else if (chaine.Substring(0, 7) == "t links")
                    {
                        carte = "t links";
                        goto lecture;
                    }


                    
                    ch = chaine.Split(param, System.StringSplitOptions.RemoveEmptyEntries);
                    //MessageBox.Show(carte + " " + ch[0]);
                    //if ((Convert.ToSingle(ch[4]) > projet.param_affectation_horaire.deb_per && Convert.ToSingle(ch[4]) < projet.param_affectation_horaire.fin_per) || Convert.ToSingle(ch[4])<0)
                    if (carte == "t nodes")
                    {
                        int ni= int.Parse(ch[0]);
                        float xi, yi;
                        xi = float.Parse(ch[1]);
                        yi = float.Parse(ch[2]);
                        node noeud = new node();
                        node nul = new node();
                        noeud.i = ni;
                        noeud.x = xi;
                        noeud.y = yi;
                        noeud.is_visible = true;
                        if (xi > projet.reseaux[num_res].xu)
                        {
                            projet.reseaux[num_res].xu = xi;
                        }
                        if (xi < projet.reseaux[num_res].xl)
                        {
                            projet.reseaux[num_res].xl = xi;
                        }
                        if (yi > projet.reseaux[num_res].yu)
                        {

                            projet.reseaux[num_res].yu = yi;

                        }
                        if (yi < projet.reseaux[num_res].yl)
                        {
                            projet.reseaux[num_res].yl = yi;
                        }


                        while (projet.reseaux[projet.reseau_actif].nodes.Count < ni + 1)
                        {
                            projet.reseaux[projet.reseau_actif].nodes.Add(nul);
                        }
                        
                        projet.reseaux[projet.reseau_actif].nodes[ni] = noeud;
                    }
                    else if (carte == "t links")
                    {
                        node nul = new node();
                        node nodei = new node();
                        node nodej = new node();

                        int ni = Convert.ToInt32(ch[0]);
                        int line;
                        nodei.i = ni;
                        while (projet.reseaux[projet.reseau_actif].nodes.Count < ni + 1)
                        {
                            projet.reseaux[projet.reseau_actif].nodes.Add(nul);
                        }

                        if (projet.reseaux[projet.reseau_actif].nodes[ni].i == 0)
                        {
                            projet.reseaux[projet.reseau_actif].nodes[ni] = nodei;
                        }
                        
                        int nj = Convert.ToInt32(ch[1]);

                        nodej.i = nj;
                        while (projet.reseaux[projet.reseau_actif].nodes.Count < nj + 1)
                        {
                            projet.reseaux[projet.reseau_actif].nodes.Add(nul);
                        }
                        if (projet.reseaux[projet.reseau_actif].nodes[nj].i == 0)
                        {
                            projet.reseaux[projet.reseau_actif].nodes[nj] = nodej;
                        }


                        link lien = new link();
                        lien.no = ni;
                        lien.nd = nj;
                        line = -1;


                        line = Convert.ToInt32(ch[4]);
                        Service num_service = new Service();
                        num_service.numero = Convert.ToInt32(ch[5]);
                        num_service.hd = Convert.ToSingle(ch[6]);
                        num_service.hf = Convert.ToSingle(ch[7]);
                        if (num_service.hd < 100f && num_service.numero > 0)
                        {
                            // num_service.hd += 1440f;
                        }
                        if (num_service.hf < 100f && num_service.numero >= 0)
                        {
                            //num_service.hf += 1440f;
                        }
                        if (num_service.hf < num_service.hd)
                        {
                            num_service.hf += 1440f;
                        }
                        num_service.regime = Convert.ToString(ch[8]);
                        int nb = projet.reseaux[projet.reseau_actif].links.Count;
                        if (nb > 0)
                        {
                            if (projet.reseaux[projet.reseau_actif].links[nb - 1].no == ni && projet.reseaux[projet.reseau_actif].links[nb - 1].nd == nj && projet.reseaux[projet.reseau_actif].links[nb - 1].ligne == line)
                            {

                                projet.reseaux[projet.reseau_actif].links[nb - 1].services.Add(num_service);

                            }
                            else
                            {
                                lien.ligne = line;
                                lien.temps = Convert.ToSingle(ch[2]) * projet.param_affectation_horaire.coef_tmap;
                                lien.longueur = Convert.ToSingle(ch[3]);
                                lien.services.Add(num_service);
                                if (ch.Length > 9)
                                {
                                    lien.texte = ch[9];
                                }


                                projet.reseaux[projet.reseau_actif].links.Add(lien);
                            }
                        }

                        else
                        {
                            lien.ligne = -1;
                            lien.temps = Convert.ToSingle(ch[2]) * projet.param_affectation_horaire.coef_tmap;
                            lien.longueur = Convert.ToSingle(ch[3]);
                            lien.services.Add(num_service);
                            if (ch.Length > 9)
                            {
                                lien.texte = ch[9];
                            }
                            projet.reseaux[projet.reseau_actif].links.Add(lien);
                        }

                    }
                }
                fichier_reseau.Close();

                //construction du graphe
                // table des prédécesseurs et successeurs de noeuds
                for (i = 0; i < projet.reseaux[projet.reseau_actif].links.Count; i++)
                {
                    turn virage = new turn();
                    virage.numero = i;
                    virage.temps = 0;
                    virage.distance = 0;
                    virage.cout = 0;

                    projet.reseaux[projet.reseau_actif].nodes[projet.reseaux[projet.reseau_actif].links[i].nd].pred.Add(virage);
                    projet.reseaux[projet.reseau_actif].nodes[projet.reseaux[projet.reseau_actif].links[i].no].succ.Add(virage);
                    //                    Console.SetCursorPosition(1, Console.CursorTop-1);

                }


                // table des prédécesseurs et successeurs de tronçons
                //Console.WriteLine("création de la topologie des noeuds terminée");
                for (i = 0; i < projet.reseaux[projet.reseau_actif].links.Count; i++)
                {
                    for (j = 0; j < projet.reseaux[projet.reseau_actif].nodes[projet.reseaux[projet.reseau_actif].links[i].no].pred.Count; j++)
                    {
                        turn virage = new turn();
                        int predecesseur = projet.reseaux[projet.reseau_actif].nodes[projet.reseaux[projet.reseau_actif].links[i].no].pred[j].numero;

                        {
                            virage.numero = predecesseur;
                            projet.reseaux[projet.reseau_actif].links[i].arci.Add(virage);
                        }


                    }
                    for (j = 0; j < projet.reseaux[projet.reseau_actif].nodes[projet.reseaux[projet.reseau_actif].links[i].nd].succ.Count; j++)
                    {
                        turn virage = new turn();
                        int successeur = projet.reseaux[projet.reseau_actif].nodes[projet.reseaux[projet.reseau_actif].links[i].nd].succ[j].numero;
                        {
                            virage.numero = successeur;
                            projet.reseaux[projet.reseau_actif].links[i].arcj.Add(virage);
                        }

                    }

                    /*                    avancement.textBox1.Text = i.ToString() + " " + projet.reseaux[projet.reseau_actif].nodes[projet.reseaux[projet.reseau_actif].links[i].nd].succ.Count.ToString();
                                        avancement.progressBar1.Value = (100 * i / projet.reseaux[projet.reseau_actif].links.Count);
                                        avancement.Refresh();*/

                }







                //affectation tc à horaires algorithme
                // graph growth aglorithm with buckets
                // graph growth aglorithm with buckets
                // graph growth aglorithm with buckets
                // graph growth aglorithm with buckets
                // graph growth aglorithm with buckets
                // graph growth aglorithm with buckets
                // graph growth aglorithm with buckets
                // graph growth aglorithm with buckets
                if (projet.param_affectation_horaire.algorithme == 0)
                {
                    Avancement avancement = new Avancement();
                    avancement.progressBar1.Value = 0;
                    avancement.Show();
                    

                    System.IO.StreamWriter fich_sortie = new System.IO.StreamWriter(projet.param_affectation_horaire.nom_sortie + "_temps.txt");
                    System.IO.StreamWriter fich_sortie2 = new System.IO.StreamWriter(projet.param_affectation_horaire.nom_sortie + "_chemins.txt");
                    System.IO.StreamWriter fich_result = new System.IO.StreamWriter(projet.param_affectation_horaire.nom_sortie + "_aff.txt");
                    System.IO.StreamWriter fich_od = new System.IO.StreamWriter(projet.param_affectation_horaire.nom_sortie + "_od.txt");
                    fich_sortie.WriteLine("o;i;j;jour;heureo;heured;temps;tveh;tmap;tatt;tcorr;cout;pole;volau;texte");

                    // Console.WriteLine("création de la topologie des tronçons terminée");
                    //plus courts chemins
                    Queue<int> touches = new Queue<int>();
                    Queue<int> calcules = new Queue<int>();
                    List<List<int>> gga_nq = new List<List<int>>();




                    //initilisation
                    for (i = 0; i < projet.reseaux[projet.reseau_actif].links.Count; i++)
                    {

                        projet.reseaux[projet.reseau_actif].links[i].volau = 0;
                        projet.reseaux[projet.reseau_actif].links[i].touche = 0;
                        projet.reseaux[projet.reseau_actif].links[i].cout = 0;
                        projet.reseaux[projet.reseau_actif].links[i].pivot = -1;
                        projet.reseaux[projet.reseau_actif].links[i].is_queue = false;
                        //                projet.reseaux[projet.reseau_actif].links[i].temps = projet.reseaux[projet.reseau_actif].links[i].fd(projet.reseaux[projet.reseau_actif].links[i].volau, projet.reseaux[projet.reseau_actif].links[i].longueur, 0f, projet.reseaux[projet.reseau_actif].links[i].lanes * 1000, projet.reseaux[projet.reseau_actif].links[i].v0, projet.reseaux[projet.reseau_actif].links[i].a, projet.reseaux[projet.reseau_actif].links[i].b, projet.reseaux[projet.reseau_actif].links[i].n);

                    }


                    int p, q, sens = 1;
                    System.IO.FileStream flux = new System.IO.FileStream(nom_matrice, System.IO.FileMode.Open);
                    System.IO.StreamReader fichier_matrice = new System.IO.StreamReader(flux);
                    avancement.progressBar1.Maximum = (int) flux.Length;
                    
                    while (fichier_matrice.EndOfStream == false)
                    {
                    lecture:
                        chaine = fichier_matrice.ReadLine();

                        if (chaine == "")
                        {
                            goto lecture;
                        }
                        ch = chaine.Split(param, StringSplitOptions.RemoveEmptyEntries);
                        p = Convert.ToInt32(ch[0]);
                        q = Convert.ToInt32(ch[1]);
                        float od = Convert.ToSingle(ch[2]);
                        int jour = (int)Convert.ToSingle(ch[3]);
                        float horaire = Convert.ToSingle(ch[4]);
                        if (ch.Length > 5)
                        {
                            if (ch[5].ToLower() == "d")
                            {
                                sens = 1;

                            }
                            else if (ch[5].ToLower() == "a")
                            {
                                sens = 2;
                            }
                        }
                        //MessageBox.Show(p.ToString() + " " + q.ToString() + " " + horaire.ToString());
                        avancement.textBox1.Text = p.ToString() + " " + q.ToString() + " " + horaire.ToString();
                        avancement.progressBar1.Value =(int) flux.Position;
                        //             fich_sortie.WriteLine(pivot.ToString() + projet.reseaux[projet.reseaux].links[pivot].cout.ToString());
                        //                flux.Position += chaine.Length;
                        avancement.Refresh();


                        //sens heure de départ//
                        //sens heure de départ//
                        //sens heure de départ//
                        //sens heure de départ//
                        //sens heure de départ//


                        //if (projet.reseaux[projet.reseau_actif].matrices[0].o[p].d.Count > 0)
                        if (sens == 1)
                        {
                            for (i = 0; i < projet.reseaux[projet.reseau_actif].links.Count; i++)
                            {
                                projet.reseaux[projet.reseau_actif].links[i].pole = -1;
                                projet.reseaux[projet.reseau_actif].links[i].touche = 0;
                                projet.reseaux[projet.reseau_actif].links[i].cout = 0;
                                projet.reseaux[projet.reseau_actif].links[i].tatt = 0;
                                projet.reseaux[projet.reseau_actif].links[i].tcor = 0;
                                projet.reseaux[projet.reseau_actif].links[i].tmap = 0;
                                projet.reseaux[projet.reseau_actif].links[i].tveh = 0;
                                projet.reseaux[projet.reseau_actif].links[i].h = 0;
                                for (j = 0; j < projet.reseaux[projet.reseau_actif].links[i].services.Count; j++)
                                {
                                    projet.reseaux[projet.reseau_actif].links[i].services[j].delta = 0;
                                }
                                projet.reseaux[projet.reseau_actif].links[i].pivot = -1;
                                projet.reseaux[projet.reseau_actif].links[i].service = 0;
                                projet.reseaux[projet.reseau_actif].links[i].is_queue = false;



                            }
                            int depart = p;
                            int pivot = -1;
                            int successeur, bucket, id_bucket = 0;
                            for (j = 0; j < projet.reseaux[projet.reseau_actif].nodes[depart].succ.Count; j++)
                            {
                                successeur = projet.reseaux[projet.reseau_actif].nodes[depart].succ[j].numero;

                                if (projet.reseaux[projet.reseau_actif].links[successeur].ligne < 0)
                                {
                                    bucket = Math.Min(Convert.ToInt32((Math.Pow(projet.reseaux[projet.reseau_actif].links[successeur].cout, 2) / projet.param_affectation_horaire.param_dijkstra)), projet.param_affectation_horaire.max_nb_buckets);
                                    while (bucket >= gga_nq.Count)
                                    {
                                        gga_nq.Add(new List<int>());
                                    }
                                    gga_nq[bucket].Add(successeur);
                                    //touches.Enqueue(successeur);
                                    projet.reseaux[projet.reseau_actif].links[successeur].touche = 1;
                                    projet.reseaux[projet.reseau_actif].links[successeur].cout = projet.reseaux[projet.reseau_actif].links[successeur].temps * projet.param_affectation_horaire.cmap;
                                    projet.reseaux[projet.reseau_actif].links[successeur].tmap = projet.reseaux[projet.reseau_actif].links[successeur].temps;
                                    projet.reseaux[projet.reseau_actif].links[successeur].h = horaire + projet.reseaux[projet.reseau_actif].links[successeur].temps;
                                    projet.reseaux[projet.reseau_actif].links[successeur].pivot = -1;
                                    projet.reseaux[projet.reseau_actif].links[successeur].pole = depart;
                                }
                                else
                                {
                                    int ii, jj, num_service = -1, h3 = 0, duree_periode;
                                    float h1 = 1e38f, h2 = 1e38f, cout2 = 1e38f;
                                    for (ii = 0; ii < projet.reseaux[projet.reseau_actif].links[successeur].services.Count; ii++)
                                    {
                                        
                                        duree_periode = projet.reseaux[projet.reseau_actif].links[successeur].services[ii].regime.Length;
                                        if (projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hd + projet.reseaux[projet.reseau_actif].links[successeur].services[ii].delta*1440f < horaire + projet.param_affectation_horaire.tboa || projet.reseaux[projet.reseau_actif].links[successeur].services[ii].regime.Substring(jour,1)== "N")
                                        {

                                            h1 = 1e38f;
                                            h2 = 1e38f;
                                            h3 = -1;
                                            for (jj = jour + 1; jj <= Math.Min(jour + projet.param_affectation_horaire.nb_jours, duree_periode - 1); jj++)
                                            {
                                                if (projet.reseaux[projet.reseau_actif].links[successeur].services[ii].regime.Substring(jj, 1) == "O" && (projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hd + (-jour + jj) * 24f * 60f  < h1))
                                                {
                                                    h1 = projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hd + (-jour + jj) * 24f * 60f ;
                                                    h2 = (-jour + jj) ;
                                                    h3 = jj;
                                                }

                                            }
                                            if (h3 != -1)
                                            {
                                                projet.reseaux[projet.reseau_actif].links[successeur].services[ii].delta = h2;
                                            }
                                            else
                                            {
                                                projet.reseaux[projet.reseau_actif].links[successeur].services[ii].delta = -1;
                                            }


                                        }

                                        if (((projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hf - projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hd) * projet.param_affectation_horaire.cveh + (projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hd + projet.reseaux[projet.reseau_actif].links[successeur].services[ii].delta*1440f - horaire) * projet.param_affectation_horaire.cwait + projet.param_affectation_horaire.tboa * projet.param_affectation_horaire.cboa) < cout2 && projet.reseaux[projet.reseau_actif].links[successeur].services[ii].delta > -1 )
                                        {
                                            cout2 = (projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hf - projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hd) * projet.param_affectation_horaire.cveh + (projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hd + projet.reseaux[projet.reseau_actif].links[successeur].services[ii].delta*1440f - horaire) * projet.param_affectation_horaire.cwait + projet.param_affectation_horaire.tboa * projet.param_affectation_horaire.cboa;
                                            num_service = ii;

                                        }

                                    }
                                    if (num_service != -1)
                                    {
                                        projet.reseaux[projet.reseau_actif].links[successeur].service = num_service;
                                        projet.reseaux[projet.reseau_actif].links[successeur].cout = (projet.reseaux[projet.reseau_actif].links[successeur].services[num_service].hf - projet.reseaux[projet.reseau_actif].links[successeur].services[num_service].hd) * projet.param_affectation_horaire.cveh + (projet.reseaux[projet.reseau_actif].links[successeur].services[num_service].hd + projet.reseaux[projet.reseau_actif].links[successeur].services[num_service].delta*1440f - horaire) * projet.param_affectation_horaire.cwait + (projet.param_affectation_horaire.tboa * projet.param_affectation_horaire.cboa);

                                        projet.reseaux[projet.reseau_actif].links[successeur].touche = 1;

                                        projet.reseaux[projet.reseau_actif].links[successeur].h = projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].hf + projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].delta*1440f;

                                        //                                    projet.reseaux[projet.reseau_actif].links[successeur].tatt = projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].hd + projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].delta - projet.reseaux[projet.reseau_actif].links[pivot].h;
                                        projet.reseaux[projet.reseau_actif].links[successeur].tatt = projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].hd + projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].delta*1440f - horaire;

                                        projet.reseaux[projet.reseau_actif].links[successeur].tveh = projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].hf - projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].hd;
                                        projet.reseaux[projet.reseau_actif].links[successeur].tcor = projet.param_affectation_horaire.tboa;
                                        projet.reseaux[projet.reseau_actif].links[successeur].tmap = 0;
                                        bucket = Convert.ToInt32(Math.Min((Math.Pow(projet.reseaux[projet.reseau_actif].links[successeur].cout, 2) / projet.param_affectation_horaire.param_dijkstra), projet.param_affectation_horaire.max_nb_buckets));
                                        while (bucket >= gga_nq.Count)
                                        {
                                            gga_nq.Add(new List<int>());
                                        }
                                        gga_nq[bucket].Add(successeur);
                                        //                                touches.Enqueue(successeur);
                                        projet.reseaux[projet.reseau_actif].links[successeur].pivot = pivot;
                                        projet.reseaux[projet.reseau_actif].links[successeur].pole = projet.reseaux[projet.reseau_actif].links[successeur].no;
                                        if (projet.reseaux[projet.reseau_actif].links[successeur].tveh < 0)
                                        {
                                            //fich_sortie.WriteLine("30 " + pivot.ToString() + " " + projet.reseaux[projet.reseau_actif].links[pivot].cout.ToString() + " " + projet.reseaux[projet.reseau_actif].links[successeur].cout.ToString() + " " + projet.reseaux[projet.reseau_actif].links[pivot].ligne.ToString() + " " + projet.reseaux[projet.reseau_actif].links[successeur].ligne.ToString() + " " + projet.reseaux[projet.reseau_actif].links[pivot].h.ToString() + " " + projet.reseaux[projet.reseau_actif].links[successeur].h.ToString());
                                        }
                                    }
                                }
                            }

                            while (gga_nq.Count > id_bucket)
                            {

                                while (gga_nq[id_bucket].Count == 0)
                                {
                                    id_bucket++;
                                    if (id_bucket == gga_nq.Count)
                                    {
                                        goto fin_gga;
                                    }
                                }
                                pivot = gga_nq[id_bucket][0];
                                gga_nq[id_bucket].RemoveAt(0);


                                //avancement.textBox1.Text = touches.Count.ToString() + " " + calcules.Count.ToString() + " " + projet.reseaux[projet.reseau_actif].links[pivot].cout;
                                //avancement.textBox1.Refresh();
                                for (j = 0; j < projet.reseaux[projet.reseau_actif].links[pivot].arcj.Count; j++)
                                {
                                    successeur = projet.reseaux[projet.reseau_actif].links[pivot].arcj[j].numero;


                                    //successeurs touches pour la première fois
                                    if (projet.reseaux[projet.reseau_actif].links[successeur].touche == 0)
                                    {
                                        // successeur marche à pied
                                        if (projet.reseaux[projet.reseau_actif].links[successeur].ligne < 0)
                                        {
                                            projet.reseaux[projet.reseau_actif].links[successeur].cout = projet.reseaux[projet.reseau_actif].links[pivot].cout + projet.reseaux[projet.reseau_actif].links[successeur].temps * projet.param_affectation_horaire.cmap;
                                            projet.reseaux[projet.reseau_actif].links[successeur].h = projet.reseaux[projet.reseau_actif].links[pivot].h + projet.reseaux[projet.reseau_actif].links[successeur].temps;
                                            projet.reseaux[projet.reseau_actif].links[successeur].tatt = projet.reseaux[projet.reseau_actif].links[pivot].tatt;
                                            projet.reseaux[projet.reseau_actif].links[successeur].tveh = projet.reseaux[projet.reseau_actif].links[pivot].tveh;
                                            projet.reseaux[projet.reseau_actif].links[successeur].tcor = projet.reseaux[projet.reseau_actif].links[pivot].tcor;
                                            projet.reseaux[projet.reseau_actif].links[successeur].tmap = projet.reseaux[projet.reseau_actif].links[pivot].tmap + projet.reseaux[projet.reseau_actif].links[successeur].temps;
                                            projet.reseaux[projet.reseau_actif].links[successeur].touche = 1;
                                            projet.reseaux[projet.reseau_actif].links[successeur].service = -1;
                                            bucket = Convert.ToInt32(Math.Min((Math.Pow(projet.reseaux[projet.reseau_actif].links[successeur].cout, 2) / projet.param_affectation_horaire.param_dijkstra), projet.param_affectation_horaire.max_nb_buckets));
                                            while (bucket >= gga_nq.Count)
                                            {
                                                gga_nq.Add(new List<int>());
                                            }
                                            gga_nq[bucket].Add(successeur);
                                            //                                        touches.Enqueue(successeur);
                                            projet.reseaux[projet.reseau_actif].links[successeur].pivot = pivot;
                                            projet.reseaux[projet.reseau_actif].links[successeur].pole = projet.reseaux[projet.reseau_actif].links[pivot].pole;
                                        }
                                        //successeur TC même ligne
                                        else if (projet.reseaux[projet.reseau_actif].links[successeur].ligne == projet.reseaux[projet.reseau_actif].links[pivot].ligne)
                                        {
                                            int ii, num_service = -1;
                                            for (ii = 0; ii < projet.reseaux[projet.reseau_actif].links[successeur].services.Count; ii++)
                                            {
                                                if (projet.reseaux[projet.reseau_actif].links[successeur].services[ii].numero == projet.reseaux[projet.reseau_actif].links[pivot].services[projet.reseaux[projet.reseau_actif].links[pivot].service].numero)
                                                {
                                                    if (projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hd >= projet.reseaux[projet.reseau_actif].links[pivot].services[projet.reseaux[projet.reseau_actif].links[pivot].service].hf)
                                                    {
                                                        num_service = ii;
                                                    }
                                                }
                                            }
                                            if (num_service != -1 && projet.reseaux[projet.reseau_actif].links[successeur].services[num_service].hd + projet.reseaux[projet.reseau_actif].links[pivot].services[projet.reseaux[projet.reseau_actif].links[pivot].service].delta*1440f >= projet.reseaux[projet.reseau_actif].links[pivot].h)
                                            {
                                                projet.reseaux[projet.reseau_actif].links[successeur].service = num_service;
                                                projet.reseaux[projet.reseau_actif].links[successeur].services[num_service].delta = projet.reseaux[projet.reseau_actif].links[pivot].services[projet.reseaux[projet.reseau_actif].links[pivot].service].delta;

                                                projet.reseaux[projet.reseau_actif].links[successeur].touche = 1;
                                                projet.reseaux[projet.reseau_actif].links[successeur].cout = projet.reseaux[projet.reseau_actif].links[pivot].cout + (projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].hf + projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].delta*1440f - projet.reseaux[projet.reseau_actif].links[pivot].h) * projet.param_affectation_horaire.cveh;
                                                projet.reseaux[projet.reseau_actif].links[successeur].h = projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].hf + projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].delta*1440f;
                                                projet.reseaux[projet.reseau_actif].links[successeur].tatt = projet.reseaux[projet.reseau_actif].links[pivot].tatt;
                                                projet.reseaux[projet.reseau_actif].links[successeur].tveh = projet.reseaux[projet.reseau_actif].links[pivot].tveh + projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].hf + projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].delta*1440f - projet.reseaux[projet.reseau_actif].links[pivot].h;
                                                projet.reseaux[projet.reseau_actif].links[successeur].tcor = projet.reseaux[projet.reseau_actif].links[pivot].tcor;
                                                projet.reseaux[projet.reseau_actif].links[successeur].tmap = projet.reseaux[projet.reseau_actif].links[pivot].tmap;
                                                bucket = Convert.ToInt32(Math.Min((Math.Pow(projet.reseaux[projet.reseau_actif].links[successeur].cout, 2) / projet.param_affectation_horaire.param_dijkstra), projet.param_affectation_horaire.max_nb_buckets));
                                                while (bucket >= gga_nq.Count)
                                                {
                                                    gga_nq.Add(new List<int>());
                                                }
                                                gga_nq[bucket].Add(successeur);
                                                //touches.Enqueue(successeur);
                                                projet.reseaux[projet.reseau_actif].links[successeur].pivot = pivot;
                                                projet.reseaux[projet.reseau_actif].links[successeur].pole = projet.reseaux[projet.reseau_actif].links[pivot].pole;
                                            }
                                        }

                                            //successeur TC lignes différentes
                                        else if (projet.reseaux[projet.reseau_actif].links[successeur].ligne != projet.reseaux[projet.reseau_actif].links[pivot].ligne)
                                        {
                                            int ii, jj, num_service = -1, h3 = 0, duree_periode;
                                            float h1 = 1e38f, h2 = 1e38f, cout2 = 1e38f;
                                            for (ii = 0; ii < projet.reseaux[projet.reseau_actif].links[successeur].services.Count; ii++)
                                            {
                                        
                                                duree_periode = projet.reseaux[projet.reseau_actif].links[successeur].services[ii].regime.Length;

                                                if (projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hd + projet.reseaux[projet.reseau_actif].links[successeur].services[ii].delta*1440f < projet.reseaux[projet.reseau_actif].links[pivot].h + projet.param_affectation_horaire.tboa || projet.reseaux[projet.reseau_actif].links[successeur].services[ii].regime.Substring(jour,1)== "N")
                                        
                                                {

                                                    h1 = 1e38f;
                                                    h2 = 1e38f;
                                                    h3 = -1;
                                                    for (jj = jour + 1; jj <= Math.Min(jour + projet.param_affectation_horaire.nb_jours, duree_periode - 1); jj++)
                                                    {
                                                        if (projet.reseaux[projet.reseau_actif].links[successeur].services[ii].regime.Substring(jj, 1) == "O" && (projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hd + (-jour + jj) * 24f * 60f  < h1))
                                                        {
                                                            h1 = projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hd + (-jour + jj) * 24f * 60f ;
                                                            h2 = (-jour + jj) ;
                                                            h3 = jj;
                                                        }

                                                    }
                                                    if (h3 != -1)
                                                    {
                                                        projet.reseaux[projet.reseau_actif].links[successeur].services[ii].delta = h2;
                                                    }
                                                    else
                                                    {
                                                        projet.reseaux[projet.reseau_actif].links[successeur].services[ii].delta = -1;
                                                    }


                                                }



                                                if ((projet.reseaux[projet.reseau_actif].links[pivot].cout + (projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hf - projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hd) * projet.param_affectation_horaire.cveh + (projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hd + projet.reseaux[projet.reseau_actif].links[successeur].services[ii].delta*1440f - projet.reseaux[projet.reseau_actif].links[pivot].h) * projet.param_affectation_horaire.cwait + projet.param_affectation_horaire.tboa * projet.param_affectation_horaire.cboa) < cout2 && projet.reseaux[projet.reseau_actif].links[successeur].services[ii].delta > -1 )
                                                {
                                                    cout2 = projet.reseaux[projet.reseau_actif].links[pivot].cout + (projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hf - projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hd) * projet.param_affectation_horaire.cveh + (projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hd + projet.reseaux[projet.reseau_actif].links[successeur].services[ii].delta*1440f - projet.reseaux[projet.reseau_actif].links[pivot].h) * projet.param_affectation_horaire.cwait + projet.param_affectation_horaire.tboa * projet.param_affectation_horaire.cboa;
                                                    num_service = ii;

                                                }

                                            }
                                            if (num_service != -1)
                                            {
                                                projet.reseaux[projet.reseau_actif].links[successeur].service = num_service;
                                                projet.reseaux[projet.reseau_actif].links[successeur].cout = projet.reseaux[projet.reseau_actif].links[pivot].cout + (projet.reseaux[projet.reseau_actif].links[successeur].services[num_service].hf - projet.reseaux[projet.reseau_actif].links[successeur].services[num_service].hd) * projet.param_affectation_horaire.cveh + (projet.reseaux[projet.reseau_actif].links[successeur].services[num_service].hd + projet.reseaux[projet.reseau_actif].links[successeur].services[num_service].delta*1440f - projet.reseaux[projet.reseau_actif].links[pivot].h) * projet.param_affectation_horaire.cwait + (projet.param_affectation_horaire.tboa * projet.param_affectation_horaire.cboa);

                                                projet.reseaux[projet.reseau_actif].links[successeur].touche = 1;

                                                projet.reseaux[projet.reseau_actif].links[successeur].h = projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].hf + projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].delta*1440f;
                                                projet.reseaux[projet.reseau_actif].links[successeur].tatt = projet.reseaux[projet.reseau_actif].links[pivot].tatt + projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].hd + projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].delta*1440f - projet.reseaux[projet.reseau_actif].links[pivot].h;
                                                projet.reseaux[projet.reseau_actif].links[successeur].tveh = projet.reseaux[projet.reseau_actif].links[pivot].tveh + projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].hf - projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].hd;
                                                projet.reseaux[projet.reseau_actif].links[successeur].tcor = projet.reseaux[projet.reseau_actif].links[pivot].tcor + projet.param_affectation_horaire.tboa;
                                                projet.reseaux[projet.reseau_actif].links[successeur].tmap = projet.reseaux[projet.reseau_actif].links[pivot].tmap;
                                                bucket = Convert.ToInt32(Math.Min((Math.Pow(projet.reseaux[projet.reseau_actif].links[successeur].cout, 2) / projet.param_affectation_horaire.param_dijkstra), projet.param_affectation_horaire.max_nb_buckets));
                                                while (bucket >= gga_nq.Count)
                                                {
                                                    gga_nq.Add(new List<int>());
                                                }
                                                gga_nq[bucket].Add(successeur);
                                                //                                        touches.Enqueue(successeur);
                                                projet.reseaux[projet.reseau_actif].links[successeur].pivot = pivot;

                                                if (projet.reseaux[projet.reseau_actif].links[pivot].pole == depart)
                                                {
                                                    projet.reseaux[projet.reseau_actif].links[successeur].pole = projet.reseaux[projet.reseau_actif].links[successeur].no;
                                                }
                                                else
                                                {
                                                    projet.reseaux[projet.reseau_actif].links[successeur].pole = projet.reseaux[projet.reseau_actif].links[pivot].pole;
                                                }
                                               /* if (projet.reseaux[projet.reseau_actif].links[successeur].tveh < 0)
                                                {
                                                    //fich_sortie.WriteLine("30 " + pivot.ToString() + " " + projet.reseaux[projet.reseau_actif].links[pivot].cout.ToString() + " " + projet.reseaux[projet.reseau_actif].links[successeur].cout.ToString() + " " + projet.reseaux[projet.reseau_actif].links[pivot].ligne.ToString() + " " + projet.reseaux[projet.reseau_actif].links[successeur].ligne.ToString() + " " + projet.reseaux[projet.reseau_actif].links[pivot].h.ToString() + " " + projet.reseaux[projet.reseau_actif].links[successeur].h.ToString());
                                                }*/
                                            }
                                        }
                                    }


        //eléments déjà touchés
                                    else if (projet.reseaux[projet.reseau_actif].links[successeur].touche == 1 || projet.reseaux[projet.reseau_actif].links[successeur].touche == 2)
                                    {
                                        bucket = Convert.ToInt32(Math.Min((Math.Pow(projet.reseaux[projet.reseau_actif].links[successeur].cout, 2) / projet.param_affectation_horaire.param_dijkstra), projet.param_affectation_horaire.max_nb_buckets));
                                        //successeurs marche à pied
                                        if (projet.reseaux[projet.reseau_actif].links[successeur].ligne < 0)
                                        {
                                            if (projet.reseaux[projet.reseau_actif].links[successeur].cout > projet.reseaux[projet.reseau_actif].links[pivot].cout + projet.reseaux[projet.reseau_actif].links[successeur].temps * projet.param_affectation_horaire.cmap)
                                            {
                                                projet.reseaux[projet.reseau_actif].links[successeur].cout = projet.reseaux[projet.reseau_actif].links[pivot].cout + projet.reseaux[projet.reseau_actif].links[successeur].temps * projet.param_affectation_horaire.cmap;
                                                projet.reseaux[projet.reseau_actif].links[successeur].h = projet.reseaux[projet.reseau_actif].links[pivot].h + projet.reseaux[projet.reseau_actif].links[successeur].temps;
                                                projet.reseaux[projet.reseau_actif].links[successeur].tatt = projet.reseaux[projet.reseau_actif].links[pivot].tatt;
                                                projet.reseaux[projet.reseau_actif].links[successeur].tveh = projet.reseaux[projet.reseau_actif].links[pivot].tveh;
                                                projet.reseaux[projet.reseau_actif].links[successeur].tcor = projet.reseaux[projet.reseau_actif].links[pivot].tcor;
                                                projet.reseaux[projet.reseau_actif].links[successeur].tmap = projet.reseaux[projet.reseau_actif].links[pivot].tmap + projet.reseaux[projet.reseau_actif].links[successeur].temps;
                                                projet.reseaux[projet.reseau_actif].links[successeur].touche = 2;

                                                projet.reseaux[projet.reseau_actif].links[successeur].pivot = pivot;
                                                projet.reseaux[projet.reseau_actif].links[successeur].pole = projet.reseaux[projet.reseau_actif].links[pivot].pole;
                                                gga_nq[bucket].Remove(successeur);
                                                bucket = Convert.ToInt32(Math.Min((Math.Pow(projet.reseaux[projet.reseau_actif].links[successeur].cout, 2) / projet.param_affectation_horaire.param_dijkstra), projet.param_affectation_horaire.max_nb_buckets));
                                                gga_nq[bucket].Add(successeur);

                                            }

                                        }
                                        //successeurs TC même ligne
                                        else if ((projet.reseaux[projet.reseau_actif].links[successeur].ligne == projet.reseaux[projet.reseau_actif].links[pivot].ligne) && ((projet.reseaux[projet.reseau_actif].links[pivot].h <= projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].hd + projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].delta*1440f)))
                                        {
                                            int ii, num_service = -1;
                                            for (ii = 0; ii < projet.reseaux[projet.reseau_actif].links[successeur].services.Count; ii++)
                                            {
                                                if (projet.reseaux[projet.reseau_actif].links[successeur].services[ii].numero == projet.reseaux[projet.reseau_actif].links[pivot].services[projet.reseaux[projet.reseau_actif].links[pivot].service].numero)
                                                {
                                                    if (projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hd >= projet.reseaux[projet.reseau_actif].links[pivot].services[projet.reseaux[projet.reseau_actif].links[pivot].service].hf)
                                                    {
                                                        num_service = ii;
                                                    }
                                                }


                                            }

                                            if (num_service != -1 && projet.reseaux[projet.reseau_actif].links[successeur].services[num_service].hd + projet.reseaux[projet.reseau_actif].links[pivot].services[projet.reseaux[projet.reseau_actif].links[pivot].service].delta >= projet.reseaux[projet.reseau_actif].links[pivot].h)
                                            {

                                                if (projet.reseaux[projet.reseau_actif].links[successeur].cout > projet.reseaux[projet.reseau_actif].links[pivot].cout + (projet.reseaux[projet.reseau_actif].links[successeur].services[num_service].hf + projet.reseaux[projet.reseau_actif].links[pivot].services[projet.reseaux[projet.reseau_actif].links[pivot].service].delta*1440f - projet.reseaux[projet.reseau_actif].links[pivot].h) * projet.param_affectation_horaire.cveh && (projet.reseaux[projet.reseau_actif].links[successeur].services[num_service].hd >= projet.reseaux[projet.reseau_actif].links[pivot].services[projet.reseaux[projet.reseau_actif].links[pivot].service].hf))
                                                {
                                                    projet.reseaux[projet.reseau_actif].links[successeur].services[num_service].delta = projet.reseaux[projet.reseau_actif].links[pivot].services[projet.reseaux[projet.reseau_actif].links[pivot].service].delta;
                                                    projet.reseaux[projet.reseau_actif].links[successeur].service = num_service;
                                                    projet.reseaux[projet.reseau_actif].links[successeur].touche = 2;
                                                    projet.reseaux[projet.reseau_actif].links[successeur].cout = projet.reseaux[projet.reseau_actif].links[pivot].cout + (projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].hf + projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].delta*1440f - projet.reseaux[projet.reseau_actif].links[pivot].h) * projet.param_affectation_horaire.cveh;
                                                    projet.reseaux[projet.reseau_actif].links[successeur].h = projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].hf + projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].delta*1440f;
                                                    projet.reseaux[projet.reseau_actif].links[successeur].tatt = projet.reseaux[projet.reseau_actif].links[pivot].tatt;
                                                    projet.reseaux[projet.reseau_actif].links[successeur].tveh = projet.reseaux[projet.reseau_actif].links[pivot].tveh + projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].hf + projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].delta *1440f- projet.reseaux[projet.reseau_actif].links[pivot].h;
                                                    projet.reseaux[projet.reseau_actif].links[successeur].tcor = projet.reseaux[projet.reseau_actif].links[pivot].tcor;
                                                    projet.reseaux[projet.reseau_actif].links[successeur].tmap = projet.reseaux[projet.reseau_actif].links[pivot].tmap;

                                                    projet.reseaux[projet.reseau_actif].links[successeur].pivot = pivot;
                                                    projet.reseaux[projet.reseau_actif].links[successeur].pole = projet.reseaux[projet.reseau_actif].links[pivot].pole;
                                                    gga_nq[bucket].Remove(successeur);
                                                    bucket = Convert.ToInt32(Math.Min((Math.Pow(projet.reseaux[projet.reseau_actif].links[successeur].cout, 2) / projet.param_affectation_horaire.param_dijkstra), projet.param_affectation_horaire.max_nb_buckets));
                                                    gga_nq[bucket].Add(successeur);
                                                }
                                            }
                                        }
                                        //successeurs TC lignes différentes
                                        else if ((projet.reseaux[projet.reseau_actif].links[successeur].ligne != projet.reseaux[projet.reseau_actif].links[pivot].ligne) && (projet.reseaux[projet.reseau_actif].links[pivot].h + projet.param_affectation_horaire.tboa <= projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].hd + projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].delta))
                                        {
                                            int ii, jj, num_service = -1, h3 = 0, duree_periode;
                                            float h1 = 1e38f, h2 = 1e38f, cout2 = 1e38f;
                                            for (ii = 0; ii < projet.reseaux[projet.reseau_actif].links[successeur].services.Count; ii++)
                                            {
                                        
                                                duree_periode = projet.reseaux[projet.reseau_actif].links[successeur].services[ii].regime.Length;

                                                if (projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hd + projet.reseaux[projet.reseau_actif].links[successeur].services[ii].delta*1440f < projet.reseaux[projet.reseau_actif].links[pivot].h + projet.param_affectation_horaire.tboa || projet.reseaux[projet.reseau_actif].links[successeur].services[ii].regime.Substring(jour,1)=="N")
                                                {

                                                    h1 = 1e38f;
                                                    h2 = 1e38f;
                                                    h3 = -1;
                                                    for (jj = jour + 1; jj <= Math.Min(jour + projet.param_affectation_horaire.nb_jours, duree_periode - 1); jj++)
                                                    {
                                                        if (projet.reseaux[projet.reseau_actif].links[successeur].services[ii].regime.Substring(jj, 1) == "O" && (projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hd + (-jour + jj) * 24f * 60f + projet.reseaux[projet.reseau_actif].links[successeur].services[ii].regime.Substring(jour,1)=="N"))
                                                        {
                                                            h1 = projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hd + (-jour + jj) * 24f * 60f ;
                                                            h2 = (-jour + jj) ;
                                                            h3 = jj;
                                                        }

                                                    }
                                                    if (h3 != -1)
                                                    {
                                                        projet.reseaux[projet.reseau_actif].links[successeur].services[ii].delta = h2;
                                                    }
                                                    else
                                                    {
                                                        projet.reseaux[projet.reseau_actif].links[successeur].services[ii].delta = -1;
                                                    }


                                                }
                                                if (projet.reseaux[projet.reseau_actif].links[pivot].cout + (projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hf - projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hd) * projet.param_affectation_horaire.cveh + (projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hd + projet.reseaux[projet.reseau_actif].links[successeur].services[ii].delta*1440f - projet.reseaux[projet.reseau_actif].links[pivot].h) * projet.param_affectation_horaire.cwait + (projet.param_affectation_horaire.tboa * projet.param_affectation_horaire.cboa) < cout2 && projet.reseaux[projet.reseau_actif].links[successeur].services[ii].delta > -1)
                                                {
                                                    cout2 = projet.reseaux[projet.reseau_actif].links[pivot].cout + (projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hf - projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hd) * projet.param_affectation_horaire.cveh + (projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hd + projet.reseaux[projet.reseau_actif].links[successeur].services[ii].delta*1440f - projet.reseaux[projet.reseau_actif].links[pivot].h) * projet.param_affectation_horaire.cwait + (projet.param_affectation_horaire.tboa * projet.param_affectation_horaire.cboa);
                                                    num_service = ii;
                                                }

                                            }
                                            if (num_service != -1)
                                            {
                                                if (projet.reseaux[projet.reseau_actif].links[successeur].cout > projet.reseaux[projet.reseau_actif].links[pivot].cout + (projet.reseaux[projet.reseau_actif].links[successeur].services[num_service].hf - projet.reseaux[projet.reseau_actif].links[successeur].services[num_service].hd) * projet.param_affectation_horaire.cveh + (projet.reseaux[projet.reseau_actif].links[successeur].services[num_service].hd + projet.reseaux[projet.reseau_actif].links[successeur].services[num_service].delta*1440f - projet.reseaux[projet.reseau_actif].links[pivot].h) * projet.param_affectation_horaire.cwait + (projet.param_affectation_horaire.tboa * projet.param_affectation_horaire.cboa))
                                                {
                                                    projet.reseaux[projet.reseau_actif].links[successeur].service = num_service;
                                                    projet.reseaux[projet.reseau_actif].links[successeur].cout = projet.reseaux[projet.reseau_actif].links[pivot].cout + (projet.reseaux[projet.reseau_actif].links[successeur].services[num_service].hf - projet.reseaux[projet.reseau_actif].links[successeur].services[num_service].hd) * projet.param_affectation_horaire.cveh + (projet.reseaux[projet.reseau_actif].links[successeur].services[num_service].hd + projet.reseaux[projet.reseau_actif].links[successeur].services[num_service].delta*1440f - projet.reseaux[projet.reseau_actif].links[pivot].h) * projet.param_affectation_horaire.cwait + (projet.param_affectation_horaire.tboa * projet.param_affectation_horaire.cboa);
                                                    projet.reseaux[projet.reseau_actif].links[successeur].touche = 2;

                                                    projet.reseaux[projet.reseau_actif].links[successeur].h = projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].hf + projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].delta*1440f;
                                                    projet.reseaux[projet.reseau_actif].links[successeur].tatt = projet.reseaux[projet.reseau_actif].links[pivot].tatt + projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].hd + projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].delta*1440f - projet.reseaux[projet.reseau_actif].links[pivot].h;
                                                    projet.reseaux[projet.reseau_actif].links[successeur].tveh = projet.reseaux[projet.reseau_actif].links[pivot].tveh + projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].hf - projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].hd;
                                                    projet.reseaux[projet.reseau_actif].links[successeur].tcor = projet.reseaux[projet.reseau_actif].links[pivot].tcor + projet.param_affectation_horaire.tboa;
                                                    projet.reseaux[projet.reseau_actif].links[successeur].tmap = projet.reseaux[projet.reseau_actif].links[pivot].tmap;

                                                    projet.reseaux[projet.reseau_actif].links[successeur].pivot = pivot;
                                                    if (projet.reseaux[projet.reseau_actif].links[pivot].pole == depart)
                                                    {
                                                        projet.reseaux[projet.reseau_actif].links[successeur].pole = projet.reseaux[projet.reseau_actif].links[successeur].no;
                                                    }
                                                    else
                                                    {
                                                        projet.reseaux[projet.reseau_actif].links[successeur].pole = projet.reseaux[projet.reseau_actif].links[pivot].pole;
                                                    }

                                                    gga_nq[bucket].Remove(successeur);
                                                    bucket = Convert.ToInt32(Math.Min((Math.Pow(projet.reseaux[projet.reseau_actif].links[successeur].cout, 2) / projet.param_affectation_horaire.param_dijkstra), projet.param_affectation_horaire.max_nb_buckets));
                                                    gga_nq[bucket].Add(successeur);
                                                }
                                            }
                                        }


                                    }
                                }
                                //projet.reseaux[projet.reseau_actif].links[pivot].touche = 3;
                                //Console.WriteLine((touches.Count+calcules.Count).ToString());
                            }
                        fin_gga:
                            //Console.WriteLine(p.ToString());

                            int arrivee = 0;
                            float cout_fin = 1e8f;

                            for (j = 0; j < projet.reseaux[projet.reseau_actif].nodes[q].pred.Count; j++)
                            {
                                int predecesseur = projet.reseaux[projet.reseau_actif].nodes[q].pred[j].numero;
                                if (projet.reseaux[projet.reseau_actif].links[predecesseur].pivot != -1 && projet.reseaux[projet.reseau_actif].links[predecesseur].cout < cout_fin)
                                {
                                    arrivee = predecesseur;
                                    cout_fin = projet.reseaux[projet.reseau_actif].links[predecesseur].cout;

                                }




                            }
                            pivot = arrivee;
                            string itineraire = "",texte;
                            while (pivot != -1)
                            {
                                projet.reseaux[projet.reseau_actif].links[pivot].volau += od;
                                if (projet.param_affectation_horaire.sortie_chemins == true)
                                {
                                    texte = p.ToString("0")+";"+q.ToString("0")+";"+jour.ToString("0")+";"+horaire.ToString("0.000");

                                    texte += ";"+projet.reseaux[projet.reseau_actif].links[pivot].no.ToString("0");
                                    texte += ";" + projet.reseaux[projet.reseau_actif].links[pivot].nd.ToString("0");
                                    texte += ";" + projet.reseaux[projet.reseau_actif].links[pivot].ligne.ToString("0");
                                    if (projet.reseaux[projet.reseau_actif].links[pivot].service != -1)
                                    {
                                        texte += ";" + projet.reseaux[projet.reseau_actif].links[pivot].services[projet.reseaux[projet.reseau_actif].links[pivot].service].numero.ToString("0");
                                    }
                                    else
                                    {
                                        texte += ";-1";
                                    }
                                    texte += ";" + (projet.reseaux[projet.reseau_actif].links[pivot].h - horaire).ToString("0.000");
                                    texte += ";" + projet.reseaux[projet.reseau_actif].links[pivot].h.ToString("0.000");
                                    texte += ";" + projet.reseaux[projet.reseau_actif].links[pivot].tveh.ToString("0.000");
                                    texte += ";" + projet.reseaux[projet.reseau_actif].links[pivot].tmap.ToString("0.000");
                                    texte += ";" + projet.reseaux[projet.reseau_actif].links[pivot].tatt.ToString("0.000");
                                    texte += ";" + projet.reseaux[projet.reseau_actif].links[pivot].tcor.ToString("0.000");
                                    texte += ";" + projet.reseaux[projet.reseau_actif].links[pivot].cout.ToString("0.000");
                                    texte += ";" + projet.reseaux[projet.reseau_actif].links[pivot].pole.ToString("0"); 
                                    texte += ";" + projet.reseaux[projet.reseau_actif].links[pivot].volau.ToString("0.00");
                                    texte += ";" + projet.reseaux[projet.reseau_actif].links[pivot].texte;
                                    fich_sortie2.WriteLine(texte);

                                }
                                if (projet.reseaux[projet.reseau_actif].links[pivot].pivot != -1)
                                {
                                    if (projet.reseaux[projet.reseau_actif].links[pivot].ligne != projet.reseaux[projet.reseau_actif].links[projet.reseaux[projet.reseau_actif].links[pivot].pivot].ligne)
                                    {
                                        string[] param2 = { "-" }, lignes_corr;
                                        lignes_corr = projet.reseaux[projet.reseau_actif].links[pivot].texte.Split(param2, StringSplitOptions.RemoveEmptyEntries);
                                        if (projet.reseaux[projet.reseau_actif].links[pivot].ligne > 0)
                                        {
                                            itineraire = lignes_corr[0] + "," + itineraire;
                                        }
                                        else
                                        {
                                            itineraire = "MAP," + itineraire;
                                        }
                                    }
                                }
                                pivot = projet.reseaux[projet.reseau_actif].links[pivot].pivot;
                            }
                            //fich_sortie.WriteLine("o;i;j;jour;heureo;heured;temps;tveh;tmap;tcorr;cout;volau;texte" );
                            
                             texte = p.ToString("0") + ";" + q.ToString("0");
                            texte += ";" + jour.ToString("0.000");
                            texte += ";" + horaire.ToString("0.000");
                            texte += ";" + projet.reseaux[projet.reseau_actif].links[arrivee].h.ToString("0.000");
                            texte += ";" + (-horaire + projet.reseaux[projet.reseau_actif].links[arrivee].h).ToString("0.000");
                            texte += ";" + projet.reseaux[projet.reseau_actif].links[arrivee].tveh.ToString("0.000");
                            texte += ";" + projet.reseaux[projet.reseau_actif].links[arrivee].tmap.ToString("0.000");
                            texte += ";" + projet.reseaux[projet.reseau_actif].links[arrivee].tatt.ToString("0.000");
                            texte += ";" + projet.reseaux[projet.reseau_actif].links[arrivee].tcor.ToString("0.000");
                            texte += ";" + projet.reseaux[projet.reseau_actif].links[arrivee].cout.ToString("0.000");
                            texte += ";" + projet.reseaux[projet.reseau_actif].links[arrivee].pole.ToString("0"); 
                            texte += ";" + projet.reseaux[projet.reseau_actif].links[arrivee].volau.ToString("0.00");
                            texte += ";" + projet.reseaux[projet.reseau_actif].links[arrivee].texte;
                            itineraire = "MAP," + itineraire;
                            texte += ";" + itineraire;
                            fich_od.WriteLine(texte);
                            if (projet.param_affectation_horaire.sortie_temps == true)
                            {

                                for (i = 0; i < projet.reseaux[projet.reseau_actif].links.Count; i++)
                                {
                                    arrivee = i;
                                    if (projet.reseaux[projet.reseau_actif].links[arrivee].h != 0 && projet.reseaux[projet.reseau_actif].links[arrivee].ligne < 0)
                                    {
                                        texte = p.ToString("0");
                                        texte += ";" + (projet.reseaux[projet.reseau_actif].links[arrivee].no).ToString("0");
                                        texte += ";" + (projet.reseaux[projet.reseau_actif].links[arrivee].nd).ToString("0");
                                        texte += ";" + jour.ToString("0.000");
                                        texte += ";" + horaire.ToString("0.000");
                                        texte += ";" + projet.reseaux[projet.reseau_actif].links[arrivee].h.ToString("0.000");
                                        texte += ";" + (-horaire + projet.reseaux[projet.reseau_actif].links[arrivee].h).ToString("0.000");
                                        texte += ";" + projet.reseaux[projet.reseau_actif].links[arrivee].tveh.ToString("0.000");
                                        texte += ";" + projet.reseaux[projet.reseau_actif].links[arrivee].tmap.ToString("0.000");
                                        texte += ";" + projet.reseaux[projet.reseau_actif].links[arrivee].tatt.ToString("0.000");
                                        texte += ";" + projet.reseaux[projet.reseau_actif].links[arrivee].tcor.ToString("0.000");
                                        texte += ";" + projet.reseaux[projet.reseau_actif].links[arrivee].cout.ToString("0.000");
                                        texte += ";" + projet.reseaux[projet.reseau_actif].links[arrivee].pole.ToString("0"); 
                                        texte += ";" + projet.reseaux[projet.reseau_actif].links[arrivee].volau.ToString("0.00");
                                        texte += ";" + projet.reseaux[projet.reseau_actif].links[arrivee].texte;
                                        //                                itineraire = "MAP," + itineraire;
                                        //texte += ";" + itineraire;
                                        fich_sortie.WriteLine(texte);
                                    }
                                }
                            }
                        }

                        // sens heure d'arrivée
                        // sens heure d'arrivée
                        // sens heure d'arrivée
                        // sens heure d'arrivée
                        // sens heure d'arrivée
                        // sens heure d'arrivée
                        // sens heure d'arrivée
                        if (sens == 2)
                        {
                            for (i = 0; i < projet.reseaux[projet.reseau_actif].links.Count; i++)
                            {
                                projet.reseaux[projet.reseau_actif].links[i].touche = 0;
                                projet.reseaux[projet.reseau_actif].links[i].cout = 0;
                                projet.reseaux[projet.reseau_actif].links[i].tatt = 0;
                                projet.reseaux[projet.reseau_actif].links[i].tcor = 0;
                                projet.reseaux[projet.reseau_actif].links[i].tmap = 0;
                                projet.reseaux[projet.reseau_actif].links[i].tveh = 0;
                                projet.reseaux[projet.reseau_actif].links[i].h = 0;
                                for (j = 0; j < projet.reseaux[projet.reseau_actif].links[i].services.Count; j++)
                                {
                                    projet.reseaux[projet.reseau_actif].links[i].services[j].delta = 0;
                                }
                                projet.reseaux[projet.reseau_actif].links[i].pivot = -1;
                                projet.reseaux[projet.reseau_actif].links[i].pole = -1; 
                                projet.reseaux[projet.reseau_actif].links[i].service = -1;
                                projet.reseaux[projet.reseau_actif].links[i].is_queue = false;



                            }
                            int depart = q;
                            int pivot = -1;
                            int bucket, id_bucket = 0, predecesseur;
                            for (j = 0; j < projet.reseaux[projet.reseau_actif].nodes[depart].pred.Count; j++)
                            {
                                predecesseur = projet.reseaux[projet.reseau_actif].nodes[depart].pred[j].numero;

                                if (projet.reseaux[projet.reseau_actif].links[predecesseur].ligne < 0)
                                {
                                    bucket = Convert.ToInt32(Math.Min((Math.Pow(projet.reseaux[projet.reseau_actif].links[predecesseur].cout, 2) / projet.param_affectation_horaire.param_dijkstra), projet.param_affectation_horaire.max_nb_buckets));
                                    while (bucket >= gga_nq.Count)
                                    {
                                        gga_nq.Add(new List<int>());
                                    }
                                    gga_nq[bucket].Add(predecesseur);
                                    //touches.Enqueue(successeur);
                                    projet.reseaux[projet.reseau_actif].links[predecesseur].touche = 1;
                                    projet.reseaux[projet.reseau_actif].links[predecesseur].cout = projet.reseaux[projet.reseau_actif].links[predecesseur].temps * projet.param_affectation_horaire.cmap;
                                    projet.reseaux[projet.reseau_actif].links[predecesseur].tmap = projet.reseaux[projet.reseau_actif].links[predecesseur].temps;
                                    projet.reseaux[projet.reseau_actif].links[predecesseur].h = horaire - projet.reseaux[projet.reseau_actif].links[predecesseur].temps;
                                    projet.reseaux[projet.reseau_actif].links[predecesseur].pivot = -1;
                                    projet.reseaux[projet.reseau_actif].links[predecesseur].pole = depart;
                                }
                                else
                                {
                                    int ii, jj, num_service = -1, h3 = 0, duree_periode;
                                    float h1 = 1e38f, h2 = 1e38f, cout2 = 1e38f;
                                    for (ii = 0; ii < projet.reseaux[projet.reseau_actif].links[predecesseur].services.Count; ii++)
                                    {
                                        duree_periode = projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].regime.Length;

                                        if (projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].hf  > horaire || projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].regime.Substring(jour, 1) == "N" )
                                        {

                                            h1 = -1e38f;
                                            h2 = 1e38f;
                                            h3 = -1;
                                            for (jj = jour-1; jj >= Math.Max(jour - projet.param_affectation_horaire.nb_jours, 0); jj--)
                                            {
                                                if (projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].regime.Substring(jj, 1) == "O" && (projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].hf + (-jour + jj) * 24f * 60f) > h1 && (projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].hf + (-jour + jj) * 24f * 60f)<horaire)
                                                {
                                                    h1 = projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].hf + (-jour + jj) * 24f * 60f ;
                                                    h2 = (-jour + jj)  ;
                                                    h3 = jj;
                                                }

                                            }
                                            if (h3 != -1)
                                            {
                                                projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].delta = h2;
                                            }
                                            else
                                            {
                                                projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].delta = 1;
                                            }


                                        }

                                        if (((projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].hf - projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].hd) * projet.param_affectation_horaire.cveh + (-projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].hf - projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].delta * 60f * 24f + horaire) * projet.param_affectation_horaire.cwait) < cout2 && projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].delta < 1)
                                        {
                                            cout2 = (projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].hf - projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].hd) * projet.param_affectation_horaire.cveh + (-projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].hf - projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].delta*60f*24f + horaire) * projet.param_affectation_horaire.cwait;
                                            num_service = ii;

                                        }

                                    }
                                    if (num_service != -1)
                                    {
                                        projet.reseaux[projet.reseau_actif].links[predecesseur].service = num_service;
                                        projet.reseaux[projet.reseau_actif].links[predecesseur].cout = (projet.reseaux[projet.reseau_actif].links[predecesseur].services[num_service].hf - projet.reseaux[projet.reseau_actif].links[predecesseur].services[num_service].hd) * projet.param_affectation_horaire.cveh + (-projet.reseaux[projet.reseau_actif].links[predecesseur].services[num_service].hf - projet.reseaux[projet.reseau_actif].links[predecesseur].services[num_service].delta* 1440f + horaire) * projet.param_affectation_horaire.cwait;

                                        projet.reseaux[projet.reseau_actif].links[predecesseur].touche = 1;

                                        projet.reseaux[projet.reseau_actif].links[predecesseur].h = projet.reseaux[projet.reseau_actif].links[predecesseur].services[projet.reseaux[projet.reseau_actif].links[predecesseur].service].hd + projet.reseaux[projet.reseau_actif].links[predecesseur].services[projet.reseaux[projet.reseau_actif].links[predecesseur].service].delta*60f*24f;
                                        projet.reseaux[projet.reseau_actif].links[predecesseur].tatt = -projet.reseaux[projet.reseau_actif].links[predecesseur].services[projet.reseaux[projet.reseau_actif].links[predecesseur].service].hf - projet.reseaux[projet.reseau_actif].links[predecesseur].services[projet.reseaux[projet.reseau_actif].links[predecesseur].service].delta*1440f + horaire;
                                        projet.reseaux[projet.reseau_actif].links[predecesseur].tveh = projet.reseaux[projet.reseau_actif].links[predecesseur].services[projet.reseaux[projet.reseau_actif].links[predecesseur].service].hf - projet.reseaux[projet.reseau_actif].links[predecesseur].services[projet.reseaux[projet.reseau_actif].links[predecesseur].service].hd;
                                        projet.reseaux[projet.reseau_actif].links[predecesseur].tcor = 0;
                                        projet.reseaux[projet.reseau_actif].links[predecesseur].tmap = 0;
                                        bucket = Convert.ToInt32(Math.Min((Math.Pow(projet.reseaux[projet.reseau_actif].links[predecesseur].cout, 2) / projet.param_affectation_horaire.param_dijkstra), projet.param_affectation_horaire.max_nb_buckets));
                                        while (bucket >= gga_nq.Count)
                                        {
                                            gga_nq.Add(new List<int>());
                                        }
                                        gga_nq[bucket].Add(predecesseur);
                                        //                                touches.Enqueue(successeur);
                                        projet.reseaux[projet.reseau_actif].links[predecesseur].pivot = pivot;
                                        projet.reseaux[projet.reseau_actif].links[predecesseur].pole = projet.reseaux[projet.reseau_actif].links[predecesseur].nd;
                                    }
                                }
                            }

                            while (gga_nq.Count > id_bucket)
                            {

                                while (gga_nq[id_bucket].Count == 0)
                                {
                                    id_bucket++;
                                    if (id_bucket == gga_nq.Count)
                                    {
                                        goto fin_gga2;
                                    }
                                }
                                pivot = gga_nq[id_bucket][0];
                                gga_nq[id_bucket].RemoveAt(0);


                                //avancement.textBox1.Text = touches.Count.ToString() + " " + calcules.Count.ToString() + " " + projet.reseaux[projet.reseau_actif].links[pivot].cout;
                                //avancement.textBox1.Refresh();
                                for (j = 0; j < projet.reseaux[projet.reseau_actif].links[pivot].arci.Count; j++)
                                {
                                    predecesseur = projet.reseaux[projet.reseau_actif].links[pivot].arci[j].numero;


                                    //successeurs touches pour la première fois
                                    if (projet.reseaux[projet.reseau_actif].links[predecesseur].touche == 0)
                                    {
                                        // predecesseur marche à pied pivot marche
                                        if (projet.reseaux[projet.reseau_actif].links[predecesseur].ligne < 0 && projet.reseaux[projet.reseau_actif].links[pivot].ligne < 0)
                                        {
                                            projet.reseaux[projet.reseau_actif].links[predecesseur].cout = projet.reseaux[projet.reseau_actif].links[pivot].cout + projet.reseaux[projet.reseau_actif].links[predecesseur].temps * projet.param_affectation_horaire.cmap;
                                            projet.reseaux[projet.reseau_actif].links[predecesseur].h = projet.reseaux[projet.reseau_actif].links[pivot].h - projet.reseaux[projet.reseau_actif].links[predecesseur].temps;
                                            projet.reseaux[projet.reseau_actif].links[predecesseur].tatt = projet.reseaux[projet.reseau_actif].links[pivot].tatt;
                                            projet.reseaux[projet.reseau_actif].links[predecesseur].tveh = projet.reseaux[projet.reseau_actif].links[pivot].tveh;
                                            projet.reseaux[projet.reseau_actif].links[predecesseur].tcor = projet.reseaux[projet.reseau_actif].links[pivot].tcor;
                                            projet.reseaux[projet.reseau_actif].links[predecesseur].tmap = projet.reseaux[projet.reseau_actif].links[pivot].tmap + projet.reseaux[projet.reseau_actif].links[predecesseur].temps;
                                            projet.reseaux[projet.reseau_actif].links[predecesseur].touche = 1;
                                            projet.reseaux[projet.reseau_actif].links[predecesseur].service = -1;
                                            bucket = Convert.ToInt32(Math.Min((Math.Pow(projet.reseaux[projet.reseau_actif].links[predecesseur].cout, 2) / projet.param_affectation_horaire.param_dijkstra), projet.param_affectation_horaire.max_nb_buckets));
                                            while (bucket >= gga_nq.Count)
                                            {
                                                gga_nq.Add(new List<int>());
                                            }
                                            gga_nq[bucket].Add(predecesseur);
                                            //                                        touches.Enqueue(successeur);
                                            projet.reseaux[projet.reseau_actif].links[predecesseur].pivot = pivot;
                                            projet.reseaux[projet.reseau_actif].links[predecesseur].pole = projet.reseaux[projet.reseau_actif].links[pivot].pole;
                                        }
                                        // predecesseur marche à pied pivot TC
                                        else if (projet.reseaux[projet.reseau_actif].links[predecesseur].ligne < 0 && projet.reseaux[projet.reseau_actif].links[pivot].ligne > 0)
                                        {
                                            projet.reseaux[projet.reseau_actif].links[predecesseur].cout = projet.reseaux[projet.reseau_actif].links[pivot].cout + projet.reseaux[projet.reseau_actif].links[predecesseur].temps * projet.param_affectation_horaire.cmap + projet.param_affectation_horaire.cboa * projet.param_affectation_horaire.tboa;
                                            projet.reseaux[projet.reseau_actif].links[predecesseur].h = projet.reseaux[projet.reseau_actif].links[pivot].h - projet.reseaux[projet.reseau_actif].links[predecesseur].temps - projet.param_affectation_horaire.tboa;
                                            projet.reseaux[projet.reseau_actif].links[predecesseur].tatt = projet.reseaux[projet.reseau_actif].links[pivot].tatt;
                                            projet.reseaux[projet.reseau_actif].links[predecesseur].tveh = projet.reseaux[projet.reseau_actif].links[pivot].tveh;
                                            projet.reseaux[projet.reseau_actif].links[predecesseur].tcor = projet.reseaux[projet.reseau_actif].links[pivot].tcor + projet.param_affectation_horaire.tboa;
                                            projet.reseaux[projet.reseau_actif].links[predecesseur].tmap = projet.reseaux[projet.reseau_actif].links[pivot].tmap + projet.reseaux[projet.reseau_actif].links[predecesseur].temps;
                                            projet.reseaux[projet.reseau_actif].links[predecesseur].touche = 1;
                                            projet.reseaux[projet.reseau_actif].links[predecesseur].service = -1;
                                            bucket = Convert.ToInt32(Math.Min((Math.Pow(projet.reseaux[projet.reseau_actif].links[predecesseur].cout, 2) / projet.param_affectation_horaire.param_dijkstra), projet.param_affectation_horaire.max_nb_buckets));
                                            while (bucket >= gga_nq.Count)
                                            {
                                                gga_nq.Add(new List<int>());
                                            }
                                            gga_nq[bucket].Add(predecesseur);
                                            //                                        touches.Enqueue(successeur);
                                            projet.reseaux[projet.reseau_actif].links[predecesseur].pivot = pivot;
                                            projet.reseaux[projet.reseau_actif].links[predecesseur].pole = projet.reseaux[projet.reseau_actif].links[predecesseur].nd;
                                        }
                                        //predecesseurs TC même ligne
                                        else if (projet.reseaux[projet.reseau_actif].links[predecesseur].ligne == projet.reseaux[projet.reseau_actif].links[pivot].ligne)
                                        {
                                            int ii, num_service = -1;
                                            for (ii = 0; ii < projet.reseaux[projet.reseau_actif].links[predecesseur].services.Count; ii++)
                                            {
                                                if (projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].numero == projet.reseaux[projet.reseau_actif].links[pivot].services[projet.reseaux[projet.reseau_actif].links[pivot].service].numero)
                                                {
                                                    if (projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].hf <= projet.reseaux[projet.reseau_actif].links[pivot].services[projet.reseaux[projet.reseau_actif].links[pivot].service].hd)
                                                    {
                                                        num_service = ii;
                                                    }
                                                }
                                            }
                                            if (num_service != -1 && projet.reseaux[projet.reseau_actif].links[predecesseur].services[num_service].hf + projet.reseaux[projet.reseau_actif].links[pivot].services[projet.reseaux[projet.reseau_actif].links[pivot].service].delta*1440f<= projet.reseaux[projet.reseau_actif].links[pivot].h)
                                            {
                                                projet.reseaux[projet.reseau_actif].links[predecesseur].service = num_service;
                                                projet.reseaux[projet.reseau_actif].links[predecesseur].services[num_service].delta = projet.reseaux[projet.reseau_actif].links[pivot].services[projet.reseaux[projet.reseau_actif].links[pivot].service].delta;

                                                projet.reseaux[projet.reseau_actif].links[predecesseur].touche = 1;
                                                projet.reseaux[projet.reseau_actif].links[predecesseur].cout = projet.reseaux[projet.reseau_actif].links[pivot].cout + (-projet.reseaux[projet.reseau_actif].links[predecesseur].services[projet.reseaux[projet.reseau_actif].links[predecesseur].service].hd - projet.reseaux[projet.reseau_actif].links[predecesseur].services[projet.reseaux[projet.reseau_actif].links[predecesseur].service].delta*1440f + projet.reseaux[projet.reseau_actif].links[pivot].h) * projet.param_affectation_horaire.cveh;
                                                projet.reseaux[projet.reseau_actif].links[predecesseur].h = projet.reseaux[projet.reseau_actif].links[predecesseur].services[projet.reseaux[projet.reseau_actif].links[predecesseur].service].hd + projet.reseaux[projet.reseau_actif].links[predecesseur].services[projet.reseaux[projet.reseau_actif].links[predecesseur].service].delta*60f*24f;
                                                projet.reseaux[projet.reseau_actif].links[predecesseur].tatt = projet.reseaux[projet.reseau_actif].links[pivot].tatt;
                                                projet.reseaux[projet.reseau_actif].links[predecesseur].tveh = projet.reseaux[projet.reseau_actif].links[pivot].tveh - projet.reseaux[projet.reseau_actif].links[predecesseur].services[projet.reseaux[projet.reseau_actif].links[predecesseur].service].hd - projet.reseaux[projet.reseau_actif].links[predecesseur].services[projet.reseaux[projet.reseau_actif].links[predecesseur].service].delta*1440f + projet.reseaux[projet.reseau_actif].links[pivot].h;
                                                projet.reseaux[projet.reseau_actif].links[predecesseur].tcor = projet.reseaux[projet.reseau_actif].links[pivot].tcor;
                                                projet.reseaux[projet.reseau_actif].links[predecesseur].tmap = projet.reseaux[projet.reseau_actif].links[pivot].tmap;
                                                bucket = Convert.ToInt32(Math.Min((Math.Pow(projet.reseaux[projet.reseau_actif].links[predecesseur].cout, 2) / projet.param_affectation_horaire.param_dijkstra), projet.param_affectation_horaire.max_nb_buckets));
                                                while (bucket >= gga_nq.Count)
                                                {
                                                    gga_nq.Add(new List<int>());
                                                }
                                                gga_nq[bucket].Add(predecesseur);
                                                //touches.Enqueue(successeur);
                                                projet.reseaux[projet.reseau_actif].links[predecesseur].pivot = pivot;
                                                projet.reseaux[projet.reseau_actif].links[predecesseur].pole = projet.reseaux[projet.reseau_actif].links[pivot].pole;
                                            }
                                        }

                                            //successeur TC lignes différentes
                                        else if (projet.reseaux[projet.reseau_actif].links[predecesseur].ligne != projet.reseaux[projet.reseau_actif].links[pivot].ligne && projet.reseaux[projet.reseau_actif].links[pivot].ligne > 0)
                                        {
                                            int ii, jj, num_service = -1, h3 = 0, duree_periode;
                                            float h1 = -1e38f, h2 = 1e38f, cout2 = 1e38f;
                                            for (ii = 0; ii < projet.reseaux[projet.reseau_actif].links[predecesseur].services.Count; ii++)
                                            {
                                                
                                                duree_periode = projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].regime.Length;

                                                if (projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].hf + projet.param_affectation_horaire.tboa > projet.reseaux[projet.reseau_actif].links[pivot].h || projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].regime.Substring(jour, 1) == "N")
                                                {

                                                    h1 = -1e38f;
                                                    h2 = 1e38f;
                                                    h3 = -1;
                                                    for (jj = jour-1 ; jj >= Math.Max(jour - projet.param_affectation_horaire.nb_jours, 0); jj--)
                                                    {
                                                        if (projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].regime.Substring(jj, 1) == "O" && (projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].hf + (-jour + jj) * 24f * 60f) > h1 && (projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].hf + (-jour + jj) * 24f * 60f) < projet.reseaux[projet.reseau_actif].links[pivot].h)
                                                        {
                                                            h1 = projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].hf + (-jour + jj) * 24f * 60f;
                                                            h2 = (-jour + jj) ;
                                                            h3 = jj;
                                                        }

                                                    }
                                                    if (h3 != -1)
                                                    {
                                                        projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].delta = h2;
                                                    }
                                                    else
                                                    {
                                                        projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].delta = 1;
                                                    }


                                                }

                                                if ((projet.reseaux[projet.reseau_actif].links[pivot].cout + (projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].hf - projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].hd) * projet.param_affectation_horaire.cveh + (-projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].hf - projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].delta * 60f * 24f + projet.reseaux[projet.reseau_actif].links[pivot].h) * projet.param_affectation_horaire.cwait + projet.param_affectation_horaire.tboa * projet.param_affectation_horaire.cboa) < cout2 && projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].delta < 1)
                                                {
                                                    cout2 = projet.reseaux[projet.reseau_actif].links[pivot].cout + (projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].hf - projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].hd) * projet.param_affectation_horaire.cveh + (-projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].hf - projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].delta*60f*24f + projet.reseaux[projet.reseau_actif].links[pivot].h) * projet.param_affectation_horaire.cwait + projet.param_affectation_horaire.tboa * projet.param_affectation_horaire.cboa;
                                                    num_service = ii;

                                                }

                                            }
                                            if (num_service != -1)
                                            {
                                                projet.reseaux[projet.reseau_actif].links[predecesseur].service = num_service;
                                                projet.reseaux[projet.reseau_actif].links[predecesseur].cout = projet.reseaux[projet.reseau_actif].links[pivot].cout + (projet.reseaux[projet.reseau_actif].links[predecesseur].services[num_service].hf - projet.reseaux[projet.reseau_actif].links[predecesseur].services[num_service].hd) * projet.param_affectation_horaire.cveh + (-projet.reseaux[projet.reseau_actif].links[predecesseur].services[num_service].hf - projet.reseaux[projet.reseau_actif].links[predecesseur].services[num_service].delta*1440f + projet.reseaux[projet.reseau_actif].links[pivot].h) * projet.param_affectation_horaire.cwait + (projet.param_affectation_horaire.tboa * projet.param_affectation_horaire.cboa);

                                                projet.reseaux[projet.reseau_actif].links[predecesseur].touche = 1;

                                                projet.reseaux[projet.reseau_actif].links[predecesseur].h = projet.reseaux[projet.reseau_actif].links[predecesseur].services[projet.reseaux[projet.reseau_actif].links[predecesseur].service].hd + projet.reseaux[projet.reseau_actif].links[predecesseur].services[projet.reseaux[projet.reseau_actif].links[predecesseur].service].delta*60f*24f;
                                                projet.reseaux[projet.reseau_actif].links[predecesseur].tatt = projet.reseaux[projet.reseau_actif].links[pivot].tatt - projet.reseaux[projet.reseau_actif].links[predecesseur].services[projet.reseaux[projet.reseau_actif].links[predecesseur].service].hf - projet.reseaux[projet.reseau_actif].links[predecesseur].services[projet.reseaux[projet.reseau_actif].links[predecesseur].service].delta*1440f + projet.reseaux[projet.reseau_actif].links[pivot].h;
                                                projet.reseaux[projet.reseau_actif].links[predecesseur].tveh = projet.reseaux[projet.reseau_actif].links[pivot].tveh + projet.reseaux[projet.reseau_actif].links[predecesseur].services[projet.reseaux[projet.reseau_actif].links[predecesseur].service].hf - projet.reseaux[projet.reseau_actif].links[predecesseur].services[projet.reseaux[projet.reseau_actif].links[predecesseur].service].hd;
                                                projet.reseaux[projet.reseau_actif].links[predecesseur].tcor = projet.reseaux[projet.reseau_actif].links[pivot].tcor + projet.param_affectation_horaire.tboa;
                                                projet.reseaux[projet.reseau_actif].links[predecesseur].tmap = projet.reseaux[projet.reseau_actif].links[pivot].tmap;
                                                bucket = Convert.ToInt32(Math.Min((Math.Pow(projet.reseaux[projet.reseau_actif].links[predecesseur].cout, 2) / projet.param_affectation_horaire.param_dijkstra), projet.param_affectation_horaire.max_nb_buckets));
                                                while (bucket >= gga_nq.Count)
                                                {
                                                    gga_nq.Add(new List<int>());
                                                }
                                                gga_nq[bucket].Add(predecesseur);
                                                //                                        touches.Enqueue(successeur);
                                                projet.reseaux[projet.reseau_actif].links[predecesseur].pivot = pivot;
                                                projet.reseaux[projet.reseau_actif].links[predecesseur].pole = projet.reseaux[projet.reseau_actif].links[predecesseur].nd;
                                            }
                                        }

                                                //predecesseur TC lignes différentes pivot MAP
                                        else if (projet.reseaux[projet.reseau_actif].links[predecesseur].ligne != projet.reseaux[projet.reseau_actif].links[pivot].ligne && projet.reseaux[projet.reseau_actif].links[pivot].ligne < 0)
                                        {
                                            int ii, jj, num_service = -1, h3 = 0, duree_periode;
                                            float h1 = 1e38f, h2 = 1e38f, cout2 = 1e38f;
                                            for (ii = 0; ii < projet.reseaux[projet.reseau_actif].links[predecesseur].services.Count; ii++)
                                            {
                                                
                                                duree_periode = projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].regime.Length;
                                                if (projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].hf + projet.param_affectation_horaire.tboa > projet.reseaux[projet.reseau_actif].links[pivot].h || projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].regime.Substring(jour, 1) == "N")
                                                {
                                                    h1 = -1e38f;
                                                    h2 = 1e38f;
                                                    h3 = -1;

                                                    for (jj = jour-1 ; jj >= Math.Max(jour - projet.param_affectation_horaire.nb_jours, 0); jj--)
                                                    {

                                                        if (projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].regime.Substring(jj, 1) == "O" && (projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].hf + (-jour + jj) * 24f * 60f) > h1 && (projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].hf + (-jour + jj) * 24f * 60f) < projet.reseaux[projet.reseau_actif].links[pivot].h)
                                                        {
                                                            h1 = projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].hf + (-jour + jj) * 24f * 60f;
                                                            h2 = (-jour + jj) ;
                                                            h3 = jj;
                                                        }

                                                    }
                                                    if (h3 != -1)
                                                    {
                                                        projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].delta = h2;
                                                    }
                                                    else
                                                    {
                                                        projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].delta = 1;
                                                    }


                                                }

                                                if ((projet.reseaux[projet.reseau_actif].links[pivot].cout + (projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].hf - projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].hd) * projet.param_affectation_horaire.cveh + (-projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].hf - projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].delta * 60f * 24f + projet.reseaux[projet.reseau_actif].links[pivot].h) * projet.param_affectation_horaire.cwait) < cout2 && projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].delta < 1)
                                                {
                                                    cout2 = projet.reseaux[projet.reseau_actif].links[pivot].cout + (projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].hf - projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].hd) * projet.param_affectation_horaire.cveh + (-projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].hf - projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].delta *60f*24f+ projet.reseaux[projet.reseau_actif].links[pivot].h) * projet.param_affectation_horaire.cwait;
                                                    num_service = ii;

                                                }

                                            }
                                            if (num_service != -1)
                                            {
                                                projet.reseaux[projet.reseau_actif].links[predecesseur].service = num_service;
                                                projet.reseaux[projet.reseau_actif].links[predecesseur].cout = projet.reseaux[projet.reseau_actif].links[pivot].cout + (projet.reseaux[projet.reseau_actif].links[predecesseur].services[num_service].hf - projet.reseaux[projet.reseau_actif].links[predecesseur].services[num_service].hd) * projet.param_affectation_horaire.cveh + (-projet.reseaux[projet.reseau_actif].links[predecesseur].services[num_service].hf - projet.reseaux[projet.reseau_actif].links[predecesseur].services[num_service].delta*1440f + projet.reseaux[projet.reseau_actif].links[pivot].h) * projet.param_affectation_horaire.cwait;

                                                projet.reseaux[projet.reseau_actif].links[predecesseur].touche = 1;

                                                projet.reseaux[projet.reseau_actif].links[predecesseur].h = projet.reseaux[projet.reseau_actif].links[predecesseur].services[projet.reseaux[projet.reseau_actif].links[predecesseur].service].hd + projet.reseaux[projet.reseau_actif].links[predecesseur].services[projet.reseaux[projet.reseau_actif].links[predecesseur].service].delta*24f*60f;
                                                projet.reseaux[projet.reseau_actif].links[predecesseur].tatt = projet.reseaux[projet.reseau_actif].links[pivot].tatt - projet.reseaux[projet.reseau_actif].links[predecesseur].services[projet.reseaux[projet.reseau_actif].links[predecesseur].service].hf - projet.reseaux[projet.reseau_actif].links[predecesseur].services[projet.reseaux[projet.reseau_actif].links[predecesseur].service].delta*1440f + projet.reseaux[projet.reseau_actif].links[pivot].h;
                                                projet.reseaux[projet.reseau_actif].links[predecesseur].tveh = projet.reseaux[projet.reseau_actif].links[pivot].tveh + projet.reseaux[projet.reseau_actif].links[predecesseur].services[projet.reseaux[projet.reseau_actif].links[predecesseur].service].hf - projet.reseaux[projet.reseau_actif].links[predecesseur].services[projet.reseaux[projet.reseau_actif].links[predecesseur].service].hd;
                                                projet.reseaux[projet.reseau_actif].links[predecesseur].tcor = projet.reseaux[projet.reseau_actif].links[pivot].tcor;
                                                projet.reseaux[projet.reseau_actif].links[predecesseur].tmap = projet.reseaux[projet.reseau_actif].links[pivot].tmap;
                                                bucket = Convert.ToInt32(Math.Min((Math.Pow(projet.reseaux[projet.reseau_actif].links[predecesseur].cout, 2) / projet.param_affectation_horaire.param_dijkstra), projet.param_affectation_horaire.max_nb_buckets));
                                                while (bucket >= gga_nq.Count)
                                                {
                                                    gga_nq.Add(new List<int>());
                                                }
                                                gga_nq[bucket].Add(predecesseur);
                                                //                                        touches.Enqueue(successeur);
                                                projet.reseaux[projet.reseau_actif].links[predecesseur].pivot = pivot;
                                                projet.reseaux[projet.reseau_actif].links[predecesseur].pole = projet.reseaux[projet.reseau_actif].links[pivot].pole;
                                            }
                                        }

                                    }


            //eléments déjà touchés
                                    else if (projet.reseaux[projet.reseau_actif].links[predecesseur].touche == 1 || projet.reseaux[projet.reseau_actif].links[predecesseur].touche == 2)
                                    {
                                        bucket = Convert.ToInt32(Math.Min((Math.Pow(projet.reseaux[projet.reseau_actif].links[predecesseur].cout, 2) / projet.param_affectation_horaire.param_dijkstra), projet.param_affectation_horaire.max_nb_buckets));
                                        //successeurs marche à pied pivot MAP
                                        if (projet.reseaux[projet.reseau_actif].links[predecesseur].ligne < 0 && projet.reseaux[projet.reseau_actif].links[pivot].ligne < 0)
                                        {
                                            if (projet.reseaux[projet.reseau_actif].links[predecesseur].cout > projet.reseaux[projet.reseau_actif].links[pivot].cout + projet.reseaux[projet.reseau_actif].links[predecesseur].temps * projet.param_affectation_horaire.cmap)
                                            {
                                                projet.reseaux[projet.reseau_actif].links[predecesseur].cout = projet.reseaux[projet.reseau_actif].links[pivot].cout + projet.reseaux[projet.reseau_actif].links[predecesseur].temps * projet.param_affectation_horaire.cmap;
                                                projet.reseaux[projet.reseau_actif].links[predecesseur].h = projet.reseaux[projet.reseau_actif].links[pivot].h - projet.reseaux[projet.reseau_actif].links[predecesseur].temps;
                                                projet.reseaux[projet.reseau_actif].links[predecesseur].tatt = projet.reseaux[projet.reseau_actif].links[pivot].tatt;
                                                projet.reseaux[projet.reseau_actif].links[predecesseur].tveh = projet.reseaux[projet.reseau_actif].links[pivot].tveh;
                                                projet.reseaux[projet.reseau_actif].links[predecesseur].tcor = projet.reseaux[projet.reseau_actif].links[pivot].tcor;
                                                projet.reseaux[projet.reseau_actif].links[predecesseur].tmap = projet.reseaux[projet.reseau_actif].links[pivot].tmap + projet.reseaux[projet.reseau_actif].links[predecesseur].temps;
                                                projet.reseaux[projet.reseau_actif].links[predecesseur].touche = 2;

                                                projet.reseaux[projet.reseau_actif].links[predecesseur].pivot = pivot;
                                                projet.reseaux[projet.reseau_actif].links[predecesseur].pole = projet.reseaux[projet.reseau_actif].links[pivot].pole;
                                                gga_nq[bucket].Remove(predecesseur);
                                                bucket = Convert.ToInt32(Math.Min((Math.Pow(projet.reseaux[projet.reseau_actif].links[predecesseur].cout, 2) / projet.param_affectation_horaire.param_dijkstra), projet.param_affectation_horaire.max_nb_buckets));
                                                gga_nq[bucket].Add(predecesseur);

                                            }

                                        }
                                        //predecesseurs marche à pied pivot TC
                                        else if (projet.reseaux[projet.reseau_actif].links[predecesseur].ligne < 0 && projet.reseaux[projet.reseau_actif].links[pivot].ligne > 0)
                                        {
                                            if (projet.reseaux[projet.reseau_actif].links[predecesseur].cout > projet.reseaux[projet.reseau_actif].links[pivot].cout + projet.reseaux[projet.reseau_actif].links[predecesseur].temps * projet.param_affectation_horaire.cmap + projet.param_affectation_horaire.cboa * projet.param_affectation_horaire.tboa)
                                            {
                                                projet.reseaux[projet.reseau_actif].links[predecesseur].cout = projet.reseaux[projet.reseau_actif].links[pivot].cout + projet.reseaux[projet.reseau_actif].links[predecesseur].temps * projet.param_affectation_horaire.cmap + projet.param_affectation_horaire.cboa * projet.param_affectation_horaire.tboa;
                                                projet.reseaux[projet.reseau_actif].links[predecesseur].h = projet.reseaux[projet.reseau_actif].links[pivot].h - projet.reseaux[projet.reseau_actif].links[predecesseur].temps - projet.param_affectation_horaire.tboa;
                                                projet.reseaux[projet.reseau_actif].links[predecesseur].tatt = projet.reseaux[projet.reseau_actif].links[pivot].tatt;
                                                projet.reseaux[projet.reseau_actif].links[predecesseur].tveh = projet.reseaux[projet.reseau_actif].links[pivot].tveh;
                                                projet.reseaux[projet.reseau_actif].links[predecesseur].tcor = projet.reseaux[projet.reseau_actif].links[pivot].tcor + projet.param_affectation_horaire.tboa;
                                                projet.reseaux[projet.reseau_actif].links[predecesseur].tmap = projet.reseaux[projet.reseau_actif].links[pivot].tmap + projet.reseaux[projet.reseau_actif].links[predecesseur].temps;
                                                projet.reseaux[projet.reseau_actif].links[predecesseur].touche = 2;

                                                projet.reseaux[projet.reseau_actif].links[predecesseur].pivot = pivot;
                                                projet.reseaux[projet.reseau_actif].links[predecesseur].pole = projet.reseaux[projet.reseau_actif].links[predecesseur].nd; 
                                                
                                                gga_nq[bucket].Remove(predecesseur);
                                                bucket = Convert.ToInt32(Math.Min((Math.Pow(projet.reseaux[projet.reseau_actif].links[predecesseur].cout, 2) / projet.param_affectation_horaire.param_dijkstra), projet.param_affectation_horaire.max_nb_buckets));
                                                gga_nq[bucket].Add(predecesseur);

                                            }

                                        }
                                        //successeurs TC même ligne
                                        else if ((projet.reseaux[projet.reseau_actif].links[predecesseur].ligne == projet.reseaux[projet.reseau_actif].links[pivot].ligne && projet.reseaux[projet.reseau_actif].links[pivot].ligne > 0) && ((projet.reseaux[projet.reseau_actif].links[pivot].h >= projet.reseaux[projet.reseau_actif].links[predecesseur].services[projet.reseaux[projet.reseau_actif].links[predecesseur].service].hf + projet.reseaux[projet.reseau_actif].links[predecesseur].services[projet.reseaux[projet.reseau_actif].links[predecesseur].service].delta*1440f)))
                                        {
                                            int ii, num_service = -1;
                                            for (ii = 0; ii < projet.reseaux[projet.reseau_actif].links[predecesseur].services.Count; ii++)
                                            {
                                                if (projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].numero == projet.reseaux[projet.reseau_actif].links[pivot].services[projet.reseaux[projet.reseau_actif].links[pivot].service].numero)
                                                {
                                                    if (projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].hf <= projet.reseaux[projet.reseau_actif].links[pivot].services[projet.reseaux[projet.reseau_actif].links[pivot].service].hd)
                                                    {
                                                        num_service = ii;
                                                    }
                                                }


                                            }

                                            if (num_service != -1 && projet.reseaux[projet.reseau_actif].links[predecesseur].services[num_service].hf + projet.reseaux[projet.reseau_actif].links[pivot].services[projet.reseaux[projet.reseau_actif].links[pivot].service].delta*1440f <= projet.reseaux[projet.reseau_actif].links[pivot].h)
                                            {

                                                if (projet.reseaux[projet.reseau_actif].links[predecesseur].cout > projet.reseaux[projet.reseau_actif].links[pivot].cout + (-projet.reseaux[projet.reseau_actif].links[predecesseur].services[num_service].hd - projet.reseaux[projet.reseau_actif].links[pivot].services[projet.reseaux[projet.reseau_actif].links[pivot].service].delta*1440f + projet.reseaux[projet.reseau_actif].links[pivot].h) * projet.param_affectation_horaire.cveh && (projet.reseaux[projet.reseau_actif].links[predecesseur].services[projet.reseaux[projet.reseau_actif].links[predecesseur].service].hf <= projet.reseaux[projet.reseau_actif].links[pivot].services[projet.reseaux[projet.reseau_actif].links[pivot].service].hd))
                                                {
                                                    projet.reseaux[projet.reseau_actif].links[predecesseur].service = num_service;
                                                    projet.reseaux[projet.reseau_actif].links[predecesseur].services[num_service].delta = projet.reseaux[projet.reseau_actif].links[pivot].services[projet.reseaux[projet.reseau_actif].links[pivot].service].delta;

                                                    projet.reseaux[projet.reseau_actif].links[predecesseur].touche = 2;
                                                    projet.reseaux[projet.reseau_actif].links[predecesseur].cout = projet.reseaux[projet.reseau_actif].links[pivot].cout + (-projet.reseaux[projet.reseau_actif].links[predecesseur].services[projet.reseaux[projet.reseau_actif].links[predecesseur].service].hd - projet.reseaux[projet.reseau_actif].links[predecesseur].services[projet.reseaux[projet.reseau_actif].links[predecesseur].service].delta*1440f + projet.reseaux[projet.reseau_actif].links[pivot].h) * projet.param_affectation_horaire.cveh;
                                                    projet.reseaux[projet.reseau_actif].links[predecesseur].h = projet.reseaux[projet.reseau_actif].links[predecesseur].services[projet.reseaux[projet.reseau_actif].links[predecesseur].service].hd + projet.reseaux[projet.reseau_actif].links[predecesseur].services[projet.reseaux[projet.reseau_actif].links[predecesseur].service].delta*60f*24f;
                                                    projet.reseaux[projet.reseau_actif].links[predecesseur].tatt = projet.reseaux[projet.reseau_actif].links[pivot].tatt;
                                                    projet.reseaux[projet.reseau_actif].links[predecesseur].tveh = projet.reseaux[projet.reseau_actif].links[pivot].tveh - projet.reseaux[projet.reseau_actif].links[predecesseur].services[projet.reseaux[projet.reseau_actif].links[predecesseur].service].hd - projet.reseaux[projet.reseau_actif].links[predecesseur].services[projet.reseaux[projet.reseau_actif].links[predecesseur].service].delta*1440f + projet.reseaux[projet.reseau_actif].links[pivot].h;
                                                    projet.reseaux[projet.reseau_actif].links[predecesseur].tcor = projet.reseaux[projet.reseau_actif].links[pivot].tcor;
                                                    projet.reseaux[projet.reseau_actif].links[predecesseur].tmap = projet.reseaux[projet.reseau_actif].links[pivot].tmap;

                                                    projet.reseaux[projet.reseau_actif].links[predecesseur].pivot = pivot;
                                                    projet.reseaux[projet.reseau_actif].links[predecesseur].pole = projet.reseaux[projet.reseau_actif].links[pivot].pole;
                                                    gga_nq[bucket].Remove(predecesseur);
                                                    bucket = Convert.ToInt32(Math.Min((Math.Pow(projet.reseaux[projet.reseau_actif].links[predecesseur].cout, 2) / projet.param_affectation_horaire.param_dijkstra), projet.param_affectation_horaire.max_nb_buckets));
                                                    gga_nq[bucket].Add(predecesseur);
                                                }
                                            }
                                        }
                                        //successeurs TC lignes différentes
                                        else if ((projet.reseaux[projet.reseau_actif].links[predecesseur].ligne != projet.reseaux[projet.reseau_actif].links[pivot].ligne && projet.reseaux[projet.reseau_actif].links[pivot].ligne > 0) && (projet.reseaux[projet.reseau_actif].links[pivot].h - projet.param_affectation_horaire.tboa >= projet.reseaux[projet.reseau_actif].links[predecesseur].services[projet.reseaux[projet.reseau_actif].links[predecesseur].service].hf + projet.reseaux[projet.reseau_actif].links[predecesseur].services[projet.reseaux[projet.reseau_actif].links[predecesseur].service].delta*1440f))
                                        {
                                            int ii, jj, num_service = -1, h3 = -1, duree_periode;
                                            float h1 = 1e38f, h2 = 1e38f, cout2 = 1e38f;
                                            for (ii = 0; ii < projet.reseaux[projet.reseau_actif].links[predecesseur].services.Count; ii++)
                                            {
                                                duree_periode = projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].regime.Length;

                                                if (projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].hf + projet.param_affectation_horaire.tboa > projet.reseaux[projet.reseau_actif].links[pivot].h || projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].regime.Substring(jour, 1) == "N")
                                                {

                                                    h1 = -1e38f;
                                                    h2 = 1e38f;
                                                    h3 = -1;
                                                    for (jj = jour-1 ; jj >= Math.Max(jour - projet.param_affectation_horaire.nb_jours, 0); jj--)
                                                    {
                                                        if (projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].regime.Substring(jj, 1) == "O" && (projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].hf + (-jour + jj) * 24f * 60f) > h1 && (projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].hf + (-jour + jj) * 24f * 60f) < projet.reseaux[projet.reseau_actif].links[pivot].h)
                                                        {
                                                            h1 = projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].hf + (-jour + jj) * 24f * 60f;
                                                            h2 = (-jour + jj) ;
                                                            h3 = jj;
                                                        }

                                                    }
                                                    if (h3 != -1)
                                                    {
                                                        projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].delta = h2;
                                                    }
                                                    else
                                                    {
                                                        projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].delta = 1;
                                                    }


                                                }
                                                if (projet.reseaux[projet.reseau_actif].links[pivot].cout + (projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].hf - projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].hd) * projet.param_affectation_horaire.cveh + (-projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].hf - projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].delta * 60f * 24f + projet.reseaux[projet.reseau_actif].links[pivot].h) * projet.param_affectation_horaire.cwait + (projet.param_affectation_horaire.tboa * projet.param_affectation_horaire.cboa) < cout2 && projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].delta < 1)
                                                {
                                                    cout2 = projet.reseaux[projet.reseau_actif].links[pivot].cout + (projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].hf - projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].hd) * projet.param_affectation_horaire.cveh + (-projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].hf - projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].delta*60f*24f+ projet.reseaux[projet.reseau_actif].links[pivot].h) * projet.param_affectation_horaire.cwait + (projet.param_affectation_horaire.tboa * projet.param_affectation_horaire.cboa);
                                                    num_service = ii;
                                                }

                                            }

                                            if (num_service != -1)
                                            {
                                                if (projet.reseaux[projet.reseau_actif].links[predecesseur].cout > projet.reseaux[projet.reseau_actif].links[pivot].cout + (projet.reseaux[projet.reseau_actif].links[predecesseur].services[num_service].hf - projet.reseaux[projet.reseau_actif].links[predecesseur].services[num_service].hd) * projet.param_affectation_horaire.cveh + (-projet.reseaux[projet.reseau_actif].links[predecesseur].services[num_service].hf - projet.reseaux[projet.reseau_actif].links[predecesseur].services[num_service].delta*1440f + projet.reseaux[projet.reseau_actif].links[pivot].h) * projet.param_affectation_horaire.cwait + (projet.param_affectation_horaire.tboa * projet.param_affectation_horaire.cboa))
                                                {
                                                    projet.reseaux[projet.reseau_actif].links[predecesseur].service = num_service;
                                                    projet.reseaux[projet.reseau_actif].links[predecesseur].cout = projet.reseaux[projet.reseau_actif].links[pivot].cout + (projet.reseaux[projet.reseau_actif].links[predecesseur].services[num_service].hf - projet.reseaux[projet.reseau_actif].links[predecesseur].services[num_service].hd) * projet.param_affectation_horaire.cveh + (-projet.reseaux[projet.reseau_actif].links[predecesseur].services[num_service].hf - projet.reseaux[projet.reseau_actif].links[predecesseur].services[num_service].delta*1440f + projet.reseaux[projet.reseau_actif].links[pivot].h) * projet.param_affectation_horaire.cwait + (projet.param_affectation_horaire.tboa * projet.param_affectation_horaire.cboa);
                                                    projet.reseaux[projet.reseau_actif].links[predecesseur].touche = 2;

                                                    projet.reseaux[projet.reseau_actif].links[predecesseur].h = projet.reseaux[projet.reseau_actif].links[predecesseur].services[projet.reseaux[projet.reseau_actif].links[predecesseur].service].hd + projet.reseaux[projet.reseau_actif].links[predecesseur].services[projet.reseaux[projet.reseau_actif].links[predecesseur].service].delta*60f*24f;
                                                    projet.reseaux[projet.reseau_actif].links[predecesseur].tatt = projet.reseaux[projet.reseau_actif].links[pivot].tatt - projet.reseaux[projet.reseau_actif].links[predecesseur].services[projet.reseaux[projet.reseau_actif].links[predecesseur].service].hf - projet.reseaux[projet.reseau_actif].links[predecesseur].services[projet.reseaux[projet.reseau_actif].links[predecesseur].service].delta*1440f + projet.reseaux[projet.reseau_actif].links[pivot].h;
                                                    projet.reseaux[projet.reseau_actif].links[predecesseur].tveh = projet.reseaux[projet.reseau_actif].links[pivot].tveh + projet.reseaux[projet.reseau_actif].links[predecesseur].services[projet.reseaux[projet.reseau_actif].links[predecesseur].service].hf - projet.reseaux[projet.reseau_actif].links[predecesseur].services[projet.reseaux[projet.reseau_actif].links[predecesseur].service].hd;
                                                    projet.reseaux[projet.reseau_actif].links[predecesseur].tcor = projet.reseaux[projet.reseau_actif].links[pivot].tcor + projet.param_affectation_horaire.tboa;
                                                    projet.reseaux[projet.reseau_actif].links[predecesseur].tmap = projet.reseaux[projet.reseau_actif].links[pivot].tmap;

                                                    projet.reseaux[projet.reseau_actif].links[predecesseur].pivot = pivot;
                                                    projet.reseaux[projet.reseau_actif].links[predecesseur].pole = projet.reseaux[projet.reseau_actif].links[predecesseur].nd;
                                                    gga_nq[bucket].Remove(predecesseur);
                                                    bucket = Convert.ToInt32(Math.Min((Math.Pow(projet.reseaux[projet.reseau_actif].links[predecesseur].cout, 2) / projet.param_affectation_horaire.param_dijkstra), projet.param_affectation_horaire.max_nb_buckets));
                                                    gga_nq[bucket].Add(predecesseur);
                                                }
                                            }

                                        }
                                        //predecesseurs TC lignes différentes pivot MAP
                                        else if ((projet.reseaux[projet.reseau_actif].links[predecesseur].ligne != projet.reseaux[projet.reseau_actif].links[pivot].ligne && projet.reseaux[projet.reseau_actif].links[pivot].ligne < 0) && (projet.reseaux[projet.reseau_actif].links[pivot].h >= projet.reseaux[projet.reseau_actif].links[predecesseur].services[projet.reseaux[projet.reseau_actif].links[predecesseur].service].hf + projet.reseaux[projet.reseau_actif].links[predecesseur].services[projet.reseaux[projet.reseau_actif].links[predecesseur].service].delta*1440f))
                                        {
                                            int ii, jj, num_service = -1, h3 = -1, duree_periode;
                                            float h1 = 1e38f, h2 = 1e38f, cout2 = 1e38f;
                                            for (ii = 0; ii < projet.reseaux[projet.reseau_actif].links[predecesseur].services.Count; ii++)
                                            {
                                                
                                                duree_periode = projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].regime.Length;

                                                if (projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].hf > projet.reseaux[projet.reseau_actif].links[pivot].h || projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].regime.Substring(jour, 1) == "N")
                                                {

                                                    h1 = -1e38f;
                                                    h2 = 1e38f;
                                                    h3 = -1;
                                                    for (jj = jour-1 ; jj >= Math.Max(jour - projet.param_affectation_horaire.nb_jours, 0); jj--)
                                                    {
                                                        if (projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].regime.Substring(jj, 1) == "O" && (projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].hf + (-jour + jj) * 24f * 60f) > h1 && (projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].hf + (-jour + jj) * 24f * 60f) < projet.reseaux[projet.reseau_actif].links[pivot].h)
                                                        {
                                                            h1 = projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].hf + (-jour + jj) * 24f * 60f ;
                                                            h2 = (-jour + jj) ;
                                                            h3 = jj;
                                                        }

                                                    }
                                                    if (h3 != -1)
                                                    {
                                                        projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].delta = h2;
                                                    }
                                                    else
                                                    {
                                                        projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].delta = 1;
                                                    }


                                                }
                                                if (projet.reseaux[projet.reseau_actif].links[pivot].cout + (projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].hf - projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].hd) * projet.param_affectation_horaire.cveh + (-projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].hf - projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].delta * 60f * 24f + projet.reseaux[projet.reseau_actif].links[pivot].h) * projet.param_affectation_horaire.cwait < cout2 && projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].regime.Substring(jour + (int)(projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].delta), 1) == "O" && projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].delta < 1)
                                                {
                                                    num_service = ii;
                                                    cout2 = projet.reseaux[projet.reseau_actif].links[pivot].cout + (projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].hf - projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].hd) * projet.param_affectation_horaire.cveh + (-projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].hf - projet.reseaux[projet.reseau_actif].links[predecesseur].services[ii].delta*60f*24f + projet.reseaux[projet.reseau_actif].links[pivot].h) * projet.param_affectation_horaire.cwait;
                                                }

                                            }

                                            if (num_service != -1)
                                            {
                                                if (projet.reseaux[projet.reseau_actif].links[predecesseur].cout > projet.reseaux[projet.reseau_actif].links[pivot].cout + (projet.reseaux[projet.reseau_actif].links[predecesseur].services[num_service].hf - projet.reseaux[projet.reseau_actif].links[predecesseur].services[num_service].hd) * projet.param_affectation_horaire.cveh + (-projet.reseaux[projet.reseau_actif].links[predecesseur].services[num_service].hf - projet.reseaux[projet.reseau_actif].links[predecesseur].services[num_service].delta*1440f + projet.reseaux[projet.reseau_actif].links[pivot].h) * projet.param_affectation_horaire.cwait)
                                                {

                                                    projet.reseaux[projet.reseau_actif].links[predecesseur].service = num_service;
                                                    projet.reseaux[projet.reseau_actif].links[predecesseur].cout = projet.reseaux[projet.reseau_actif].links[pivot].cout + (projet.reseaux[projet.reseau_actif].links[predecesseur].services[num_service].hf - projet.reseaux[projet.reseau_actif].links[predecesseur].services[num_service].hd) * projet.param_affectation_horaire.cveh + (-projet.reseaux[projet.reseau_actif].links[predecesseur].services[num_service].hf - projet.reseaux[projet.reseau_actif].links[predecesseur].services[num_service].delta*1440f + projet.reseaux[projet.reseau_actif].links[pivot].h) * projet.param_affectation_horaire.cwait;
                                                    projet.reseaux[projet.reseau_actif].links[predecesseur].touche = 2;

                                                    projet.reseaux[projet.reseau_actif].links[predecesseur].h = projet.reseaux[projet.reseau_actif].links[predecesseur].services[projet.reseaux[projet.reseau_actif].links[predecesseur].service].hd + projet.reseaux[projet.reseau_actif].links[predecesseur].services[projet.reseaux[projet.reseau_actif].links[predecesseur].service].delta*60f*24f;
                                                    projet.reseaux[projet.reseau_actif].links[predecesseur].tatt = projet.reseaux[projet.reseau_actif].links[pivot].tatt - projet.reseaux[projet.reseau_actif].links[predecesseur].services[projet.reseaux[projet.reseau_actif].links[predecesseur].service].hf - projet.reseaux[projet.reseau_actif].links[predecesseur].services[projet.reseaux[projet.reseau_actif].links[predecesseur].service].delta*1440f + projet.reseaux[projet.reseau_actif].links[pivot].h;
                                                    projet.reseaux[projet.reseau_actif].links[predecesseur].tveh = projet.reseaux[projet.reseau_actif].links[pivot].tveh + projet.reseaux[projet.reseau_actif].links[predecesseur].services[projet.reseaux[projet.reseau_actif].links[predecesseur].service].hf - projet.reseaux[projet.reseau_actif].links[predecesseur].services[projet.reseaux[projet.reseau_actif].links[predecesseur].service].hd;
                                                    projet.reseaux[projet.reseau_actif].links[predecesseur].tcor = projet.reseaux[projet.reseau_actif].links[pivot].tcor;
                                                    projet.reseaux[projet.reseau_actif].links[predecesseur].tmap = projet.reseaux[projet.reseau_actif].links[pivot].tmap;

                                                    projet.reseaux[projet.reseau_actif].links[predecesseur].pivot = pivot;
                                                    projet.reseaux[projet.reseau_actif].links[predecesseur].pole = projet.reseaux[projet.reseau_actif].links[pivot].pole;
                                                    gga_nq[bucket].Remove(predecesseur);
                                                    bucket = Convert.ToInt32(Math.Min((Math.Pow(projet.reseaux[projet.reseau_actif].links[predecesseur].cout, 2) / projet.param_affectation_horaire.param_dijkstra), projet.param_affectation_horaire.max_nb_buckets));
                                                    gga_nq[bucket].Add(predecesseur);
                                                }
                                            }
                                        }


                                    }
                                }
                                //projet.reseaux[projet.reseau_actif].links[pivot].touche = 3;
                                //Console.WriteLine((touches.Count+calcules.Count).ToString());
                            }
                        fin_gga2:
                            //Console.WriteLine(p.ToString());

                            int arrivee = 0;
                            float cout_fin = 1e8f;

                            for (j = 0; j < projet.reseaux[projet.reseau_actif].nodes[p].succ.Count; j++)
                            {
                                predecesseur = projet.reseaux[projet.reseau_actif].nodes[p].succ[j].numero;
                                if (projet.reseaux[projet.reseau_actif].links[predecesseur].pivot != -1 && projet.reseaux[projet.reseau_actif].links[predecesseur].cout < cout_fin)
                                {
                                    arrivee = predecesseur;
                                    cout_fin = projet.reseaux[projet.reseau_actif].links[predecesseur].cout;

                                }




                            }
                            pivot = arrivee;
                            string itineraire = "",texte;
                            while (pivot != -1)
                            {
                                projet.reseaux[projet.reseau_actif].links[pivot].volau += od;
                                if (projet.param_affectation_horaire.sortie_chemins == true)
                                {
                                    texte = p.ToString("0") + ";" + q.ToString("0") + ";"+jour.ToString("0") + ";"+ horaire.ToString("0.000"); 
                                    texte += ";"+ projet.reseaux[projet.reseau_actif].links[pivot].no.ToString("0");
                                    texte += ";" + projet.reseaux[projet.reseau_actif].links[pivot].nd.ToString("0");
                                    texte += ";" + projet.reseaux[projet.reseau_actif].links[pivot].ligne.ToString("0");
                                    if (projet.reseaux[projet.reseau_actif].links[pivot].service != -1)
                                    {
                                        texte += ";" + projet.reseaux[projet.reseau_actif].links[pivot].services[projet.reseaux[projet.reseau_actif].links[pivot].service].numero.ToString("0");
                                    }
                                    else
                                    {
                                        texte += ";-1";
                                    }
                                    texte += ";" + (-projet.reseaux[projet.reseau_actif].links[pivot].h + horaire).ToString("0.000");
                                    texte += ";" + projet.reseaux[projet.reseau_actif].links[pivot].h.ToString("0.000");
                                    texte += ";" + projet.reseaux[projet.reseau_actif].links[pivot].tveh.ToString("0.000");
                                    texte += ";" + projet.reseaux[projet.reseau_actif].links[pivot].tmap.ToString("0.000");
                                    texte += ";" + projet.reseaux[projet.reseau_actif].links[pivot].tatt.ToString("0.000");
                                    texte += ";" + projet.reseaux[projet.reseau_actif].links[pivot].tcor.ToString("0.000");
                                    texte += ";" + projet.reseaux[projet.reseau_actif].links[pivot].cout.ToString("0.000");
                                    texte += ";" + projet.reseaux[projet.reseau_actif].links[pivot].pole.ToString("0"); 
                                    texte += ";" + projet.reseaux[projet.reseau_actif].links[pivot].volau.ToString("0.00");
                                    texte += ";" + projet.reseaux[projet.reseau_actif].links[pivot].texte;
                                    fich_sortie2.WriteLine(texte);

                                }
                                if (projet.reseaux[projet.reseau_actif].links[pivot].pivot != -1)
                                {
                                    if (projet.reseaux[projet.reseau_actif].links[pivot].ligne != projet.reseaux[projet.reseau_actif].links[projet.reseaux[projet.reseau_actif].links[pivot].pivot].ligne)
                                    {
                                        string[] param2 = { "-" }, lignes_corr;
                                        lignes_corr = projet.reseaux[projet.reseau_actif].links[pivot].texte.Split(param2, StringSplitOptions.RemoveEmptyEntries);
                                        if (projet.reseaux[projet.reseau_actif].links[pivot].ligne > 0)
                                        {
                                            itineraire = lignes_corr[0] + "," + itineraire;
                                        }
                                        else
                                        {
                                            itineraire = "MAP," + itineraire;
                                        }
                                    }
                                }
                                pivot = projet.reseaux[projet.reseau_actif].links[pivot].pivot;
                            }
                            texte = p.ToString("0") + ";" + q.ToString("0");
                            texte += ";" + jour.ToString("0.000");
                            texte += ";" + projet.reseaux[projet.reseau_actif].links[arrivee].h.ToString("0.000");
                            texte += ";" + horaire.ToString("0.000");
                            texte += ";" + (horaire - projet.reseaux[projet.reseau_actif].links[arrivee].h).ToString("0.000");
                            texte += ";" + projet.reseaux[projet.reseau_actif].links[arrivee].tveh.ToString("0.000");
                            texte += ";" + projet.reseaux[projet.reseau_actif].links[arrivee].tmap.ToString("0.000");
                            texte += ";" + projet.reseaux[projet.reseau_actif].links[arrivee].tatt.ToString("0.000");
                            texte += ";" + projet.reseaux[projet.reseau_actif].links[arrivee].tcor.ToString("0.000");
                            texte += ";" + projet.reseaux[projet.reseau_actif].links[arrivee].cout.ToString("0.000");
                            texte += ";" + projet.reseaux[projet.reseau_actif].links[arrivee].pole.ToString("0"); 
                            texte += ";" + projet.reseaux[projet.reseau_actif].links[arrivee].volau.ToString("0.00");
                            texte += ";" + projet.reseaux[projet.reseau_actif].links[arrivee].texte;
                            itineraire = "MAP," + itineraire;
                            texte += ";" + itineraire;
                            fich_od.WriteLine(texte);

                            if (projet.param_affectation_horaire.sortie_temps == true)
                            {
                                for (i = 0; i < projet.reseaux[projet.reseau_actif].links.Count; i++)
                                {

                                    arrivee = i;
                                    if (projet.reseaux[projet.reseau_actif].links[arrivee].h != 0 && projet.reseaux[projet.reseau_actif].links[arrivee].ligne < 0)
                                    {
                                        texte = q.ToString("0");
                                        texte += ";" + (projet.reseaux[projet.reseau_actif].links[arrivee].no).ToString("0");
                                        texte += ";" + (projet.reseaux[projet.reseau_actif].links[arrivee].nd).ToString("0");
                                        texte += ";" + jour.ToString("0.000");
                                        texte += ";" + projet.reseaux[projet.reseau_actif].links[arrivee].h.ToString("0.000");
                                        texte += ";" + horaire.ToString("0.000");
                                        texte += ";" + (horaire - projet.reseaux[projet.reseau_actif].links[arrivee].h).ToString("0.000");
                                        texte += ";" + projet.reseaux[projet.reseau_actif].links[arrivee].tveh.ToString("0.000");
                                        texte += ";" + projet.reseaux[projet.reseau_actif].links[arrivee].tmap.ToString("0.000");
                                        texte += ";" + projet.reseaux[projet.reseau_actif].links[arrivee].tatt.ToString("0.000");
                                        texte += ";" + projet.reseaux[projet.reseau_actif].links[arrivee].tcor.ToString("0.000");
                                        texte += ";" + projet.reseaux[projet.reseau_actif].links[arrivee].cout.ToString("0.000");
                                        texte += ";" + projet.reseaux[projet.reseau_actif].links[arrivee].pole.ToString("0"); 
                                        texte += ";" + projet.reseaux[projet.reseau_actif].links[arrivee].volau.ToString("0.00");
                                        texte += ";" + projet.reseaux[projet.reseau_actif].links[arrivee].texte;
                                        //                                itineraire = "MAP," + itineraire;
                                        fich_sortie.WriteLine(texte);
                                    }
                                }
                            }
                        }

                    }
                    avancement.Close();
                    fich_sortie.Close();
                    fich_sortie2.Close();
                    fich_od.Close();
                    for (i = 0; i < projet.reseaux[projet.reseau_actif].links.Count; i++)
                    {
                        string texte = projet.reseaux[projet.reseau_actif].links[i].no.ToString("0");
                        texte += ";" + projet.reseaux[projet.reseau_actif].links[i].nd.ToString("0") + ";" + projet.reseaux[projet.reseau_actif].links[i].ligne.ToString("0");
                        texte += ";" + projet.reseaux[projet.reseau_actif].links[i].volau.ToString("0.000000") + ";" + projet.reseaux[projet.reseau_actif].links[i].texte;
                        fich_result.WriteLine(texte);
                    }
                    fich_result.Close();
                    //projet.reseaux.Remove(projet.reseaux[projet.reseau_actif]);


                }


                    //algorithme de Dijkstra

                    //algorithme de Dijkstra
                //algorithme de Dijkstra
                //algorithme de Dijkstra
                //algorithme de Dijkstra
                //algorithme de Dijkstra
                //algorithme de Dijkstra
                //algorithme de Dijkstra
                //algorithme de Dijkstra
                //algorithme de Dijkstra
                //algorithme de Dijkstra
                //algorithme de Dijkstra
                //algorithme de Dijkstra
                //algorithme de Dijkstra
                //algorithme de Dijkstra
                //algorithme de Dijkstra
                //algorithme de Dijkstra
                //algorithme de Dijkstra
                //algorithme de Dijkstra
                //algorithme de Dijkstra
                //algorithme de Dijkstra


                    //algorithme de Dijkstra

                else if (projet.param_affectation_horaire.algorithme == 1)
                {

                    Avancement avancement = new Avancement();
                    avancement.Show();


                    System.IO.StreamWriter fich_sortie = new System.IO.StreamWriter("c:\\temp\\tc.txt");
                    System.IO.StreamWriter fich_sortie2 = new System.IO.StreamWriter("c:\\temp\\tc_chemins.txt");

                    // Console.WriteLine("création de la topologie des tronçons terminée");
                    //plus courts chemins

                    List<suivant> dijkstra_touches = new List<suivant>(0);




                    //initilisation
                    for (i = 0; i < projet.reseaux[projet.reseau_actif].links.Count; i++)
                    {

                        projet.reseaux[projet.reseau_actif].links[i].volau = 0;
                        projet.reseaux[projet.reseau_actif].links[i].touche = 0;
                        projet.reseaux[projet.reseau_actif].links[i].cout = 0;
                        projet.reseaux[projet.reseau_actif].links[i].pivot = -1;
                        projet.reseaux[projet.reseau_actif].links[i].is_queue = false;
                        //                projet.reseaux[projet.reseau_actif].links[i].temps = projet.reseaux[projet.reseau_actif].links[i].fd(projet.reseaux[projet.reseau_actif].links[i].volau, projet.reseaux[projet.reseau_actif].links[i].longueur, 0f, projet.reseaux[projet.reseau_actif].links[i].lanes * 1000, projet.reseaux[projet.reseau_actif].links[i].v0, projet.reseaux[projet.reseau_actif].links[i].a, projet.reseaux[projet.reseau_actif].links[i].b, projet.reseaux[projet.reseau_actif].links[i].n);

                    }


                    int p, q;
                    System.IO.FileStream flux = new System.IO.FileStream(nom_matrice, System.IO.FileMode.Open);
                    System.IO.StreamReader fichier_matrice = new System.IO.StreamReader(flux);

                    while (fichier_matrice.EndOfStream == false)
                    {
                    lecture:
                        chaine = fichier_matrice.ReadLine();

                        if (chaine == "")
                        {
                            goto lecture;
                        }
                        ch = chaine.Split(param, StringSplitOptions.RemoveEmptyEntries);
                        p = Convert.ToInt32(ch[0]);
                        q = Convert.ToInt32(ch[1]);
                        float od = Convert.ToSingle(ch[2]);
                        int jour = (int)Convert.ToSingle(ch[3]);
                        float horaire = Convert.ToSingle(ch[4]);
                        //MessageBox.Show(p.ToString() + " " + q.ToString() + " " + horaire.ToString());
                        avancement.textBox1.Text = p.ToString() + " " + q.ToString() + " " + horaire.ToString();
                        //             fich_sortie.WriteLine(pivot.ToString() + projet.reseaux[projet.reseaux].links[pivot].cout.ToString());
                        //                flux.Position += chaine.Length;
                        avancement.Refresh();

                        //if (projet.reseaux[projet.reseau_actif].matrices[0].o[p].d.Count > 0)
                        {
                            for (i = 0; i < projet.reseaux[projet.reseau_actif].links.Count; i++)
                            {
                                projet.reseaux[projet.reseau_actif].links[i].touche = 0;
                                projet.reseaux[projet.reseau_actif].links[i].cout = 1e38f;
                                projet.reseaux[projet.reseau_actif].links[i].tatt = 0;
                                projet.reseaux[projet.reseau_actif].links[i].tcor = 0;
                                projet.reseaux[projet.reseau_actif].links[i].tmap = 0;
                                projet.reseaux[projet.reseau_actif].links[i].tveh = 0;
                                projet.reseaux[projet.reseau_actif].links[i].h = 0;
                                for (j = 0; j < projet.reseaux[projet.reseau_actif].links[i].services.Count; j++)
                                {
                                    projet.reseaux[projet.reseau_actif].links[i].services[j].delta = 0;
                                }
                                projet.reseaux[projet.reseau_actif].links[i].pivot = -1;
                                projet.reseaux[projet.reseau_actif].links[i].service = 0;
                                projet.reseaux[projet.reseau_actif].links[i].is_queue = false;



                            }
                            int depart = p;
                            int pivot = -1;
                            int successeur, bucket, id_bucket = 0;

                            for (j = 0; j < projet.reseaux[projet.reseau_actif].nodes[depart].succ.Count; j++)
                            {
                                successeur = projet.reseaux[projet.reseau_actif].nodes[depart].succ[j].numero;
                                if (projet.reseaux[projet.reseau_actif].links[successeur].ligne < 0)
                                {

                                    projet.reseaux[projet.reseau_actif].links[successeur].touche = 1;
                                    projet.reseaux[projet.reseau_actif].links[successeur].cout = projet.reseaux[projet.reseau_actif].links[successeur].temps * projet.param_affectation_horaire.cmap;
                                    projet.reseaux[projet.reseau_actif].links[successeur].tmap = projet.reseaux[projet.reseau_actif].links[successeur].temps;
                                    projet.reseaux[projet.reseau_actif].links[successeur].h = horaire + projet.reseaux[projet.reseau_actif].links[successeur].temps;
                                    projet.reseaux[projet.reseau_actif].links[successeur].pivot = -1;
                                    bucket = Convert.ToInt32((Math.Pow(projet.reseaux[projet.reseau_actif].links[successeur].cout, 2) / projet.param_affectation_horaire.param_dijkstra));
                                    while (bucket >= dijkstra_touches.Count)
                                    {
                                        dijkstra_touches.Add(new suivant());
                                    }
                                    dijkstra_touches[bucket].classe.Add(successeur);
                                }
                                else
                                {
                                    int ii, jj, num_service = -1, h3 = 0, duree_periode;
                                    float h1 = 1e38f, h2 = 1e38f, cout2 = 1e38f;
                                    for (ii = 0; ii < projet.reseaux[projet.reseau_actif].links[successeur].services.Count; ii++)
                                    {
                                        projet.reseaux[projet.reseau_actif].links[successeur].services[ii].delta = 0;
                                        duree_periode = projet.reseaux[projet.reseau_actif].links[successeur].services[ii].regime.Length;

                                        while (projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hd + projet.reseaux[projet.reseau_actif].links[successeur].services[ii].delta < horaire + projet.param_affectation_horaire.tboa)
                                        {

                                            h1 = 1e38f;
                                            h2 = 1e38f;
                                            h3 = 0;
                                            for (jj = jour + 1; jj <= Math.Min(jour + projet.param_affectation_horaire.nb_jours, duree_periode - 1); jj++)
                                            {
                                                if (projet.reseaux[projet.reseau_actif].links[successeur].services[ii].regime.Substring(jj, 1) == "O" && (projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hd + projet.reseaux[projet.reseau_actif].links[successeur].services[ii].delta + (-jour + jj) * 24f * 60f) < h1)
                                                {
                                                    h1 = projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hd + (-jour + jj) * 24f * 60f + projet.reseaux[projet.reseau_actif].links[successeur].services[ii].delta;
                                                    h2 = projet.reseaux[projet.reseau_actif].links[successeur].services[ii].delta + (-jour + jj) * 24f * 60f;
                                                    h3 = jj;
                                                }
                                            }

                                            projet.reseaux[projet.reseau_actif].links[successeur].services[ii].delta = h2;
                                        }

                                        if (((projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hf - projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hd) * projet.param_affectation_horaire.cveh + (projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hd + projet.reseaux[projet.reseau_actif].links[successeur].services[ii].delta - horaire) * projet.param_affectation_horaire.cwait + projet.param_affectation_horaire.tboa * projet.param_affectation_horaire.cboa) < cout2)
                                        {
                                            cout2 = (projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hf - projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hd) * projet.param_affectation_horaire.cveh + (projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hd + projet.reseaux[projet.reseau_actif].links[successeur].services[ii].delta - horaire) * projet.param_affectation_horaire.cwait + projet.param_affectation_horaire.tboa * projet.param_affectation_horaire.cboa;
                                            num_service = ii;

                                        }

                                    }
                                    projet.reseaux[projet.reseau_actif].links[successeur].service = num_service;
                                    projet.reseaux[projet.reseau_actif].links[successeur].cout = (projet.reseaux[projet.reseau_actif].links[successeur].services[num_service].hf - projet.reseaux[projet.reseau_actif].links[successeur].services[num_service].hd) * projet.param_affectation_horaire.cveh + (projet.reseaux[projet.reseau_actif].links[successeur].services[num_service].hd + projet.reseaux[projet.reseau_actif].links[successeur].services[num_service].delta - horaire) * projet.param_affectation_horaire.cwait + (projet.param_affectation_horaire.tboa * projet.param_affectation_horaire.cboa);

                                    projet.reseaux[projet.reseau_actif].links[successeur].touche = 1;

                                    projet.reseaux[projet.reseau_actif].links[successeur].h = projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].hf + projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].delta;
                                    projet.reseaux[projet.reseau_actif].links[successeur].tatt = projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].hd + projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].delta - horaire;
                                    projet.reseaux[projet.reseau_actif].links[successeur].tveh = projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].hf - projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].hd;
                                    projet.reseaux[projet.reseau_actif].links[successeur].tcor = projet.param_affectation_horaire.tboa;
                                    projet.reseaux[projet.reseau_actif].links[successeur].tmap = 0;
                                    bucket = Convert.ToInt32((Math.Pow(projet.reseaux[projet.reseau_actif].links[successeur].cout, 2) / projet.param_affectation_horaire.param_dijkstra));

                                    while (bucket >= dijkstra_touches.Count)
                                    {
                                        dijkstra_touches.Add(new suivant());
                                    }
                                    dijkstra_touches[bucket].classe.Add(successeur);


                                    projet.reseaux[projet.reseau_actif].links[successeur].pivot = pivot;

                                }
                            }
                            int k;
                            while (dijkstra_touches.Count > id_bucket)
                            {
                                float cout_max = 1e38f;
                                int id_pivot = -1;
                                //avancement.textBox1.Text = id_bucket + " " + dijkstra_touches.Count + " " + pivot;
                                //                        avancement.Refresh();


                                while (dijkstra_touches[id_bucket].classe.Count == 0)
                                {
                                    id_bucket++;
                                    if (id_bucket == dijkstra_touches.Count)
                                    {
                                        goto fin;
                                    }
                                }
                                for (k = 0; k < dijkstra_touches[id_bucket].classe.Count; k++)
                                {
                                    if (projet.reseaux[projet.reseau_actif].links[dijkstra_touches[id_bucket].classe[k]].cout < cout_max)
                                    {
                                        cout_max = projet.reseaux[projet.reseau_actif].links[dijkstra_touches[id_bucket].classe[k]].cout;
                                        id_pivot = k;
                                    }
                                }
                                pivot = dijkstra_touches[id_bucket].classe[id_pivot];
                                dijkstra_touches[id_bucket].classe.RemoveAt(id_pivot);




                                //                            avancement.textBox1.Text = id_bucket+ " " + dijkstra_touches.Count+ " " + pivot;
                                //avancement.Refresh();
                                for (j = 0; j < projet.reseaux[projet.reseau_actif].links[pivot].arcj.Count; j++)
                                {
                                    successeur = projet.reseaux[projet.reseau_actif].links[pivot].arcj[j].numero;


                                    //successeurs touches pour la première fois
                                    if (projet.reseaux[projet.reseau_actif].links[successeur].touche == 0)
                                    {
                                        // successeur marche à pied
                                        if (projet.reseaux[projet.reseau_actif].links[successeur].ligne < 0)
                                        {
                                            projet.reseaux[projet.reseau_actif].links[successeur].cout = projet.reseaux[projet.reseau_actif].links[pivot].cout + projet.reseaux[projet.reseau_actif].links[successeur].temps * projet.param_affectation_horaire.cmap;
                                            projet.reseaux[projet.reseau_actif].links[successeur].h = projet.reseaux[projet.reseau_actif].links[pivot].h + projet.reseaux[projet.reseau_actif].links[successeur].temps;
                                            projet.reseaux[projet.reseau_actif].links[successeur].tatt = projet.reseaux[projet.reseau_actif].links[pivot].tatt;
                                            projet.reseaux[projet.reseau_actif].links[successeur].tveh = projet.reseaux[projet.reseau_actif].links[pivot].tveh;
                                            projet.reseaux[projet.reseau_actif].links[successeur].tcor = projet.reseaux[projet.reseau_actif].links[pivot].tcor;
                                            projet.reseaux[projet.reseau_actif].links[successeur].tmap = projet.reseaux[projet.reseau_actif].links[pivot].tmap + projet.reseaux[projet.reseau_actif].links[successeur].temps;
                                            projet.reseaux[projet.reseau_actif].links[successeur].touche = 1;
                                            projet.reseaux[projet.reseau_actif].links[successeur].service = -1;
                                            bucket = Convert.ToInt32(Math.Pow(projet.reseaux[projet.reseau_actif].links[successeur].cout, 2) / projet.param_affectation_horaire.param_dijkstra);
                                            while (bucket >= dijkstra_touches.Count)
                                            {
                                                dijkstra_touches.Add(new suivant());
                                            }
                                            dijkstra_touches[bucket].classe.Add(successeur);
                                            projet.reseaux[projet.reseau_actif].links[successeur].pivot = pivot;
                                        }
                                        //successeur TC même ligne
                                        else if (projet.reseaux[projet.reseau_actif].links[successeur].ligne == projet.reseaux[projet.reseau_actif].links[pivot].ligne)
                                        {
                                            int ii, num_service = -1;
                                            for (ii = 0; ii < projet.reseaux[projet.reseau_actif].links[successeur].services.Count; ii++)
                                            {
                                                if (projet.reseaux[projet.reseau_actif].links[successeur].services[ii].numero == projet.reseaux[projet.reseau_actif].links[pivot].services[projet.reseaux[projet.reseau_actif].links[pivot].service].numero)
                                                {
                                                    if (projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hd >= projet.reseaux[projet.reseau_actif].links[pivot].services[projet.reseaux[projet.reseau_actif].links[pivot].service].hf)
                                                    {
                                                        num_service = ii;
                                                    }
                                                }
                                            }
                                            if (num_service != -1 && projet.reseaux[projet.reseau_actif].links[successeur].services[num_service].hd + projet.reseaux[projet.reseau_actif].links[pivot].services[projet.reseaux[projet.reseau_actif].links[pivot].service].delta >= projet.reseaux[projet.reseau_actif].links[pivot].h)
                                            {
                                                projet.reseaux[projet.reseau_actif].links[successeur].service = num_service;
                                                projet.reseaux[projet.reseau_actif].links[successeur].services[num_service].delta = projet.reseaux[projet.reseau_actif].links[pivot].services[projet.reseaux[projet.reseau_actif].links[pivot].service].delta;

                                                projet.reseaux[projet.reseau_actif].links[successeur].touche = 1;
                                                projet.reseaux[projet.reseau_actif].links[successeur].cout = projet.reseaux[projet.reseau_actif].links[pivot].cout + (projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].hf + projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].delta - projet.reseaux[projet.reseau_actif].links[pivot].h) * projet.param_affectation_horaire.cveh;
                                                projet.reseaux[projet.reseau_actif].links[successeur].h = projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].hf + projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].delta;
                                                projet.reseaux[projet.reseau_actif].links[successeur].tatt = projet.reseaux[projet.reseau_actif].links[pivot].tatt;
                                                projet.reseaux[projet.reseau_actif].links[successeur].tveh = projet.reseaux[projet.reseau_actif].links[pivot].tveh + projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].hf + projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].delta - projet.reseaux[projet.reseau_actif].links[pivot].h;
                                                projet.reseaux[projet.reseau_actif].links[successeur].tcor = projet.reseaux[projet.reseau_actif].links[pivot].tcor;
                                                projet.reseaux[projet.reseau_actif].links[successeur].tmap = projet.reseaux[projet.reseau_actif].links[pivot].tmap;
                                                bucket = Convert.ToInt32(Math.Pow(projet.reseaux[projet.reseau_actif].links[successeur].cout, 2) / projet.param_affectation_horaire.param_dijkstra);
                                                while (bucket >= dijkstra_touches.Count)
                                                {
                                                    dijkstra_touches.Add(new suivant());
                                                }
                                                dijkstra_touches[bucket].classe.Add(successeur);
                                                projet.reseaux[projet.reseau_actif].links[successeur].pivot = pivot;

                                            }
                                        }

                                            //successeur TC lignes différentes
                                        else if (projet.reseaux[projet.reseau_actif].links[successeur].ligne != projet.reseaux[projet.reseau_actif].links[pivot].ligne)
                                        {
                                            int ii, jj, num_service = -1, h3 = 0, duree_periode;
                                            float h1 = 1e38f, h2 = 1e38f, cout2 = 1e38f;
                                            for (ii = 0; ii < projet.reseaux[projet.reseau_actif].links[successeur].services.Count; ii++)
                                            {
                                                projet.reseaux[projet.reseau_actif].links[successeur].services[ii].delta = 0;
                                                duree_periode = projet.reseaux[projet.reseau_actif].links[successeur].services[ii].regime.Length;

                                                while (projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hd + projet.reseaux[projet.reseau_actif].links[successeur].services[ii].delta < projet.reseaux[projet.reseau_actif].links[pivot].h + projet.param_affectation_horaire.tboa)
                                                {

                                                    h1 = 1e38f;
                                                    h2 = 1e38f;
                                                    h3 = 0;
                                                    for (jj = jour + 1; jj <= Math.Min(jour + projet.param_affectation_horaire.nb_jours, duree_periode - 1); jj++)
                                                    {
                                                        if (projet.reseaux[projet.reseau_actif].links[successeur].services[ii].regime.Substring(jj, 1) == "O" && (projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hd + projet.reseaux[projet.reseau_actif].links[successeur].services[ii].delta + (-jour + jj) * 24f * 60f) < h1)
                                                        {
                                                            h1 = projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hd + (-jour + jj) * 24f * 60f + projet.reseaux[projet.reseau_actif].links[successeur].services[ii].delta;
                                                            h2 = projet.reseaux[projet.reseau_actif].links[successeur].services[ii].delta + (-jour + jj) * 24f * 60f;
                                                            h3 = jj;
                                                        }
                                                    }

                                                    projet.reseaux[projet.reseau_actif].links[successeur].services[ii].delta = h2;
                                                }

                                                if ((projet.reseaux[projet.reseau_actif].links[pivot].cout + (projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hf - projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hd) * projet.param_affectation_horaire.cveh + (projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hd + projet.reseaux[projet.reseau_actif].links[successeur].services[ii].delta - projet.reseaux[projet.reseau_actif].links[pivot].h) * projet.param_affectation_horaire.cwait + projet.param_affectation_horaire.tboa * projet.param_affectation_horaire.cboa) < cout2)
                                                {
                                                    cout2 = projet.reseaux[projet.reseau_actif].links[pivot].cout + (projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hf - projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hd) * projet.param_affectation_horaire.cveh + (projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hd + projet.reseaux[projet.reseau_actif].links[successeur].services[ii].delta - projet.reseaux[projet.reseau_actif].links[pivot].h) * projet.param_affectation_horaire.cwait + projet.param_affectation_horaire.tboa * projet.param_affectation_horaire.cboa;
                                                    num_service = ii;

                                                }

                                            }
                                            projet.reseaux[projet.reseau_actif].links[successeur].service = num_service;
                                            projet.reseaux[projet.reseau_actif].links[successeur].cout = projet.reseaux[projet.reseau_actif].links[pivot].cout + (projet.reseaux[projet.reseau_actif].links[successeur].services[num_service].hf - projet.reseaux[projet.reseau_actif].links[successeur].services[num_service].hd) * projet.param_affectation_horaire.cveh + (projet.reseaux[projet.reseau_actif].links[successeur].services[num_service].hd + projet.reseaux[projet.reseau_actif].links[successeur].services[num_service].delta - projet.reseaux[projet.reseau_actif].links[pivot].h) * projet.param_affectation_horaire.cwait + (projet.param_affectation_horaire.tboa * projet.param_affectation_horaire.cboa);

                                            projet.reseaux[projet.reseau_actif].links[successeur].touche = 1;

                                            projet.reseaux[projet.reseau_actif].links[successeur].h = projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].hf + projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].delta;
                                            projet.reseaux[projet.reseau_actif].links[successeur].tatt = projet.reseaux[projet.reseau_actif].links[pivot].tatt + projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].hd + projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].delta - projet.reseaux[projet.reseau_actif].links[pivot].h;
                                            projet.reseaux[projet.reseau_actif].links[successeur].tveh = projet.reseaux[projet.reseau_actif].links[pivot].tveh + projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].hf - projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].hd;
                                            projet.reseaux[projet.reseau_actif].links[successeur].tcor = projet.reseaux[projet.reseau_actif].links[pivot].tcor + projet.param_affectation_horaire.tboa;
                                            projet.reseaux[projet.reseau_actif].links[successeur].tmap = projet.reseaux[projet.reseau_actif].links[pivot].tmap;
                                            bucket = Convert.ToInt32(Math.Pow(projet.reseaux[projet.reseau_actif].links[successeur].cout, 2) / projet.param_affectation_horaire.param_dijkstra);
                                            while (bucket >= dijkstra_touches.Count)
                                            {
                                                dijkstra_touches.Add(new suivant());
                                            }
                                            dijkstra_touches[bucket].classe.Add(successeur);
                                            projet.reseaux[projet.reseau_actif].links[successeur].pivot = pivot;
                                            if (projet.reseaux[projet.reseau_actif].links[successeur].tveh < 0)
                                            {
                                                //fich_sortie.WriteLine("30 " + pivot.ToString() + " " + projet.reseaux[projet.reseau_actif].links[pivot].cout.ToString() + " " + projet.reseaux[projet.reseau_actif].links[successeur].cout.ToString() + " " + projet.reseaux[projet.reseau_actif].links[pivot].ligne.ToString() + " " + projet.reseaux[projet.reseau_actif].links[successeur].ligne.ToString() + " " + projet.reseaux[projet.reseau_actif].links[pivot].h.ToString() + " " + projet.reseaux[projet.reseau_actif].links[successeur].h.ToString());
                                            }
                                        }
                                    }


        //eléments déjà touchés
                                    else if (projet.reseaux[projet.reseau_actif].links[successeur].touche == 1 || projet.reseaux[projet.reseau_actif].links[successeur].touche == 2)
                                    {
                                        bucket = Convert.ToInt32(Math.Pow(projet.reseaux[projet.reseau_actif].links[successeur].cout, 2) / projet.param_affectation_horaire.param_dijkstra);
                                        //successeurs marche à pied
                                        if (projet.reseaux[projet.reseau_actif].links[successeur].ligne < 0)
                                        {
                                            if (projet.reseaux[projet.reseau_actif].links[successeur].cout > projet.reseaux[projet.reseau_actif].links[pivot].cout + projet.reseaux[projet.reseau_actif].links[successeur].temps * projet.param_affectation_horaire.cmap)
                                            {
                                                projet.reseaux[projet.reseau_actif].links[successeur].cout = projet.reseaux[projet.reseau_actif].links[pivot].cout + projet.reseaux[projet.reseau_actif].links[successeur].temps * projet.param_affectation_horaire.cmap;
                                                projet.reseaux[projet.reseau_actif].links[successeur].h = projet.reseaux[projet.reseau_actif].links[pivot].h + projet.reseaux[projet.reseau_actif].links[successeur].temps;
                                                projet.reseaux[projet.reseau_actif].links[successeur].tatt = projet.reseaux[projet.reseau_actif].links[pivot].tatt;
                                                projet.reseaux[projet.reseau_actif].links[successeur].tveh = projet.reseaux[projet.reseau_actif].links[pivot].tveh;
                                                projet.reseaux[projet.reseau_actif].links[successeur].tcor = projet.reseaux[projet.reseau_actif].links[pivot].tcor;
                                                projet.reseaux[projet.reseau_actif].links[successeur].tmap = projet.reseaux[projet.reseau_actif].links[pivot].tmap + projet.reseaux[projet.reseau_actif].links[successeur].temps;
                                                projet.reseaux[projet.reseau_actif].links[successeur].touche = 2;

                                                dijkstra_touches[bucket].classe.Remove(successeur);
                                                bucket = Convert.ToInt32(Math.Pow(projet.reseaux[projet.reseau_actif].links[successeur].cout, 2) / projet.param_affectation_horaire.param_dijkstra);
                                                dijkstra_touches[bucket].classe.Add(successeur);

                                                projet.reseaux[projet.reseau_actif].links[successeur].pivot = pivot;

                                            }

                                        }
                                        //successeurs TC même ligne
                                        else if ((projet.reseaux[projet.reseau_actif].links[successeur].ligne == projet.reseaux[projet.reseau_actif].links[pivot].ligne) && ((projet.reseaux[projet.reseau_actif].links[pivot].h <= projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].hd + projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].delta)))
                                        {
                                            int ii, num_service = -1;
                                            for (ii = 0; ii < projet.reseaux[projet.reseau_actif].links[successeur].services.Count; ii++)
                                            {
                                                if (projet.reseaux[projet.reseau_actif].links[successeur].services[ii].numero == projet.reseaux[projet.reseau_actif].links[pivot].services[projet.reseaux[projet.reseau_actif].links[pivot].service].numero)
                                                {
                                                    if (projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hd >= projet.reseaux[projet.reseau_actif].links[pivot].services[projet.reseaux[projet.reseau_actif].links[pivot].service].hf)
                                                    {
                                                        num_service = ii;
                                                    }
                                                }


                                            }

                                            if (num_service != -1 && projet.reseaux[projet.reseau_actif].links[successeur].services[num_service].hd + projet.reseaux[projet.reseau_actif].links[pivot].services[projet.reseaux[projet.reseau_actif].links[pivot].service].delta >= projet.reseaux[projet.reseau_actif].links[pivot].h)
                                            {
                                                projet.reseaux[projet.reseau_actif].links[successeur].service = num_service;
                                                projet.reseaux[projet.reseau_actif].links[successeur].services[num_service].delta = projet.reseaux[projet.reseau_actif].links[pivot].services[projet.reseaux[projet.reseau_actif].links[pivot].service].delta;

                                                if (projet.reseaux[projet.reseau_actif].links[successeur].cout > projet.reseaux[projet.reseau_actif].links[pivot].cout + (projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].hf + projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].delta - projet.reseaux[projet.reseau_actif].links[pivot].h) * projet.param_affectation_horaire.cveh && (projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].hd >= projet.reseaux[projet.reseau_actif].links[pivot].services[projet.reseaux[projet.reseau_actif].links[pivot].service].hf))
                                                {
                                                    projet.reseaux[projet.reseau_actif].links[successeur].touche = 2;
                                                    projet.reseaux[projet.reseau_actif].links[successeur].cout = projet.reseaux[projet.reseau_actif].links[pivot].cout + (projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].hf + projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].delta - projet.reseaux[projet.reseau_actif].links[pivot].h) * projet.param_affectation_horaire.cveh;
                                                    projet.reseaux[projet.reseau_actif].links[successeur].h = projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].hf + projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].delta;
                                                    projet.reseaux[projet.reseau_actif].links[successeur].tatt = projet.reseaux[projet.reseau_actif].links[pivot].tatt;
                                                    projet.reseaux[projet.reseau_actif].links[successeur].tveh = projet.reseaux[projet.reseau_actif].links[pivot].tveh + projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].hf + projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].delta - projet.reseaux[projet.reseau_actif].links[pivot].h;
                                                    projet.reseaux[projet.reseau_actif].links[successeur].tcor = projet.reseaux[projet.reseau_actif].links[pivot].tcor;
                                                    projet.reseaux[projet.reseau_actif].links[successeur].tmap = projet.reseaux[projet.reseau_actif].links[pivot].tmap;

                                                    dijkstra_touches[bucket].classe.Remove(successeur);

                                                    bucket = Convert.ToInt32(Math.Pow(projet.reseaux[projet.reseau_actif].links[successeur].cout, 2) / projet.param_affectation_horaire.param_dijkstra);
                                                    dijkstra_touches[bucket].classe.Add(successeur);

                                                    projet.reseaux[projet.reseau_actif].links[successeur].pivot = pivot;

                                                }
                                            }
                                        }
                                        //successeurs TC lignes différentes
                                        else if ((projet.reseaux[projet.reseau_actif].links[successeur].ligne != projet.reseaux[projet.reseau_actif].links[pivot].ligne) && (projet.reseaux[projet.reseau_actif].links[pivot].h + projet.param_affectation_horaire.tboa <= projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].hd + projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].delta))
                                        {
                                            int ii, jj, num_service = -1, h3 = 0, duree_periode;
                                            float h1 = 1e38f, h2 = 1e38f, cout2 = 1e38f;
                                            for (ii = 0; ii < projet.reseaux[projet.reseau_actif].links[successeur].services.Count; ii++)
                                            {
                                                projet.reseaux[projet.reseau_actif].links[successeur].services[ii].delta = 0;
                                                duree_periode = projet.reseaux[projet.reseau_actif].links[successeur].services[ii].regime.Length;

                                                while (projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hd + projet.reseaux[projet.reseau_actif].links[successeur].services[ii].delta < projet.reseaux[projet.reseau_actif].links[pivot].h + projet.param_affectation_horaire.tboa || projet.reseaux[projet.reseau_actif].links[successeur].services[ii].delta == -1)
                                                {

                                                    h1 = 1e38f;
                                                    h2 = 1e38f;
                                                    h3 = -1;
                                                    for (jj = jour + 1; jj <= Math.Min(jour + projet.param_affectation_horaire.nb_jours, duree_periode - 1); jj++)
                                                    {
                                                        if (projet.reseaux[projet.reseau_actif].links[successeur].services[ii].regime.Substring(jj, 1) == "O" && (projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hd + (-jour + jj) * 24f * 60f + projet.reseaux[projet.reseau_actif].links[successeur].services[ii].delta) < h1)
                                                        {
                                                            h1 = projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hd + (-jour + jj) * 24f * 60f + projet.reseaux[projet.reseau_actif].links[successeur].services[ii].delta;
                                                            h2 = (-jour + jj) * 24f * 60f + projet.reseaux[projet.reseau_actif].links[successeur].services[ii].delta;
                                                            h3 = jj;
                                                        }

                                                    }
                                                    if (h3 != -1)
                                                    {
                                                        projet.reseaux[projet.reseau_actif].links[successeur].services[ii].delta = h2;
                                                    }
                                                    else
                                                    {
                                                        projet.reseaux[projet.reseau_actif].links[successeur].services[ii].delta = -1;
                                                    }


                                                }

                                                if (projet.reseaux[projet.reseau_actif].links[pivot].cout + (projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hf - projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hd) * projet.param_affectation_horaire.cveh + (projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hd + projet.reseaux[projet.reseau_actif].links[successeur].services[ii].delta - projet.reseaux[projet.reseau_actif].links[pivot].h) * projet.param_affectation_horaire.cwait + (projet.param_affectation_horaire.tboa * projet.param_affectation_horaire.cboa) < cout2)
                                                {
                                                    cout2 = projet.reseaux[projet.reseau_actif].links[pivot].cout + (projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hf - projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hd) * projet.param_affectation_horaire.cveh + (projet.reseaux[projet.reseau_actif].links[successeur].services[ii].hd + projet.reseaux[projet.reseau_actif].links[successeur].services[ii].delta - projet.reseaux[projet.reseau_actif].links[pivot].h) * projet.param_affectation_horaire.cwait + (projet.param_affectation_horaire.tboa * projet.param_affectation_horaire.cboa);
                                                    num_service = ii;
                                                    projet.reseaux[projet.reseau_actif].links[successeur].service = ii;
                                                }

                                            }

                                            if (projet.reseaux[projet.reseau_actif].links[successeur].cout > projet.reseaux[projet.reseau_actif].links[pivot].cout + (projet.reseaux[projet.reseau_actif].links[successeur].services[num_service].hf - projet.reseaux[projet.reseau_actif].links[successeur].services[num_service].hd) * projet.param_affectation_horaire.cveh + (projet.reseaux[projet.reseau_actif].links[successeur].services[num_service].hd + projet.reseaux[projet.reseau_actif].links[successeur].services[num_service].delta - projet.reseaux[projet.reseau_actif].links[pivot].h) * projet.param_affectation_horaire.cwait + (projet.param_affectation_horaire.tboa * projet.param_affectation_horaire.cboa))
                                            {
                                                projet.reseaux[projet.reseau_actif].links[successeur].cout = projet.reseaux[projet.reseau_actif].links[pivot].cout + (projet.reseaux[projet.reseau_actif].links[successeur].services[num_service].hf - projet.reseaux[projet.reseau_actif].links[successeur].services[num_service].hd) * projet.param_affectation_horaire.cveh + (projet.reseaux[projet.reseau_actif].links[successeur].services[num_service].hd + projet.reseaux[projet.reseau_actif].links[successeur].services[num_service].delta - projet.reseaux[projet.reseau_actif].links[pivot].h) * projet.param_affectation_horaire.cwait + (projet.param_affectation_horaire.tboa * projet.param_affectation_horaire.cboa);
                                                projet.reseaux[projet.reseau_actif].links[successeur].touche = 2;

                                                projet.reseaux[projet.reseau_actif].links[successeur].h = projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].hf + projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].delta;
                                                projet.reseaux[projet.reseau_actif].links[successeur].tatt = projet.reseaux[projet.reseau_actif].links[pivot].tatt + projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].hd + projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].delta - projet.reseaux[projet.reseau_actif].links[pivot].h;
                                                projet.reseaux[projet.reseau_actif].links[successeur].tveh = projet.reseaux[projet.reseau_actif].links[pivot].tveh + projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].hf - projet.reseaux[projet.reseau_actif].links[successeur].services[projet.reseaux[projet.reseau_actif].links[successeur].service].hd;
                                                projet.reseaux[projet.reseau_actif].links[successeur].tcor = projet.reseaux[projet.reseau_actif].links[pivot].tcor + projet.param_affectation_horaire.tboa;
                                                projet.reseaux[projet.reseau_actif].links[successeur].tmap = projet.reseaux[projet.reseau_actif].links[pivot].tmap;

                                                dijkstra_touches[bucket].classe.Remove(successeur);

                                                bucket = Convert.ToInt32(Math.Pow(projet.reseaux[projet.reseau_actif].links[successeur].cout, 2) / projet.param_affectation_horaire.param_dijkstra);
                                                dijkstra_touches[bucket].classe.Add(successeur);

                                                projet.reseaux[projet.reseau_actif].links[successeur].pivot = pivot;

                                            }

                                        }


                                    }
                                }
                                //projet.reseaux[projet.reseau_actif].links[pivot].touche = 3;
                                //Console.WriteLine((touches.Count+calcules.Count).ToString());
                            }

                            //Console.WriteLine(p.ToString());
                        fin:
                            int arrivee = 0;
                            float cout_fin = 1e8f;

                            for (j = 0; j < projet.reseaux[projet.reseau_actif].nodes[q].pred.Count; j++)
                            {
                                int predecesseur = projet.reseaux[projet.reseau_actif].nodes[q].pred[j].numero;
                                if (projet.reseaux[projet.reseau_actif].links[predecesseur].pivot != -1 && projet.reseaux[projet.reseau_actif].links[predecesseur].cout < cout_fin)
                                {
                                    arrivee = predecesseur;
                                    cout_fin = projet.reseaux[projet.reseau_actif].links[predecesseur].cout;

                                }




                            }
                            pivot = arrivee;
                            string itineraire = "";
                            while (pivot != -1)
                            {
                                projet.reseaux[projet.reseau_actif].links[pivot].volau += od;
                                if (projet.param_affectation_horaire.sortie_chemins == true)
                                {
                                    string texte = projet.reseaux[projet.reseau_actif].links[pivot].no.ToString("0");
                                    texte += ";" + projet.reseaux[projet.reseau_actif].links[pivot].nd.ToString("0");
                                    texte += ";" + projet.reseaux[projet.reseau_actif].links[pivot].ligne.ToString("0");
                                    if (projet.reseaux[projet.reseau_actif].links[pivot].service != -1)
                                    {
                                        texte += ";" + projet.reseaux[projet.reseau_actif].links[pivot].services[projet.reseaux[projet.reseau_actif].links[pivot].service].numero.ToString("0");
                                    }
                                    else
                                    {
                                        texte += ";-1";
                                    }
                                    texte += ";" + (projet.reseaux[projet.reseau_actif].links[pivot].h - horaire).ToString("0.000");
                                    texte += ";" + projet.reseaux[projet.reseau_actif].links[pivot].h.ToString("0.000");
                                    texte += ";" + projet.reseaux[projet.reseau_actif].links[pivot].tveh.ToString("0.000");
                                    texte += ";" + projet.reseaux[projet.reseau_actif].links[pivot].tmap.ToString("0.000");
                                    texte += ";" + projet.reseaux[projet.reseau_actif].links[pivot].tatt.ToString("0.000");
                                    texte += ";" + projet.reseaux[projet.reseau_actif].links[pivot].tcor.ToString("0.000");
                                    texte += ";" + projet.reseaux[projet.reseau_actif].links[pivot].cout.ToString("0.000");
                                    texte += ";" + projet.reseaux[projet.reseau_actif].links[pivot].volau.ToString("0.00");
                                    texte += ";" + projet.reseaux[projet.reseau_actif].links[pivot].texte;
                                    fich_sortie2.WriteLine(texte);

                                }
                                if (projet.reseaux[projet.reseau_actif].links[pivot].pivot != -1)
                                {
                                    if (projet.reseaux[projet.reseau_actif].links[pivot].ligne != projet.reseaux[projet.reseau_actif].links[projet.reseaux[projet.reseau_actif].links[pivot].pivot].ligne)
                                    {
                                        string[] param2 = { "-" }, lignes_corr;
                                        lignes_corr = projet.reseaux[projet.reseau_actif].links[pivot].texte.Split(param2, StringSplitOptions.RemoveEmptyEntries);
                                        if (projet.reseaux[projet.reseau_actif].links[pivot].ligne > 0)
                                        {
                                            itineraire = lignes_corr[0] + "," + itineraire;
                                        }
                                        else
                                        {
                                            itineraire = "MAP," + itineraire;
                                        }
                                    }
                                }
                                pivot = projet.reseaux[projet.reseau_actif].links[pivot].pivot;
                            }
                            //for (i = 0; i < projet.reseaux[projet.reseau_actif].links.Count; i++)
                            {
                                string texte = p.ToString("0");
                                texte += ";" + q.ToString("0") + ";" + jour.ToString() + ";" + horaire.ToString();
                                texte += ";" + (projet.reseaux[projet.reseau_actif].links[arrivee].h - horaire).ToString("0.000");
                                texte += ";" + projet.reseaux[projet.reseau_actif].links[arrivee].h.ToString("0.000");
                                texte += ";" + projet.reseaux[projet.reseau_actif].links[arrivee].tveh.ToString("0.000");
                                texte += ";" + projet.reseaux[projet.reseau_actif].links[arrivee].tmap.ToString("0.000");
                                texte += ";" + projet.reseaux[projet.reseau_actif].links[arrivee].tatt.ToString("0.000");
                                texte += ";" + projet.reseaux[projet.reseau_actif].links[arrivee].tcor.ToString("0.000");
                                texte += ";" + projet.reseaux[projet.reseau_actif].links[arrivee].cout.ToString("0.000");
                                texte += ";" + projet.reseaux[projet.reseau_actif].links[arrivee].volau.ToString("0.00");
                                texte += ";" + itineraire;
                                fich_sortie.WriteLine(texte);
                            }
                        }


                        /*for (i = 0; i < projet.reseaux[projet.reseau_actif].links.Count; i++)
                        {
                            if (projet.reseaux[projet.reseau_actif].links[i].volau > 0)
                            {
                            }
                        }*/
                    }
                    avancement.Close();
                    fich_sortie.Close();
                    fich_sortie2.Close();
//                    projet.reseaux.Remove(projet.reseaux[projet.reseau_actif]);

                }
            }
        }

        private void importerRéseauAccessibilitéToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i, j;
            string[] param ={ " " };
            projet.reseaux.Add(new network());
            int num_res;


            string chaine;
            string[] ch;

            projet.reseau_actif = projet.reseaux.Count - 1;
            num_res = projet.reseaux.Count - 1;
            openFileDialog1.ShowDialog();
            string nom_reseau = openFileDialog1.FileName;


            if (System.IO.File.Exists(nom_reseau))
            {
                System.IO.StreamReader fichier_reseau = new System.IO.StreamReader(nom_reseau);
                projet.reseaux[num_res].matrices.Add(new matrix());

                projet.reseaux[projet.reseau_actif].nom = System.IO.Path.GetFileNameWithoutExtension(nom_reseau);
                while (fichier_reseau.EndOfStream == false)
                {

                    chaine = fichier_reseau.ReadLine();




                    ch = chaine.Split(param, System.StringSplitOptions.RemoveEmptyEntries);

                    {
                        node nul = new node();
                        node nodei = new node();
                        node nodej = new node();
                        int ni = Convert.ToInt32(ch[0]);
                        nodei.i = ni;
                        while (projet.reseaux[projet.reseau_actif].nodes.Count < ni + 1)
                        {
                            projet.reseaux[projet.reseau_actif].nodes.Add(nul);
                        }
                        projet.reseaux[projet.reseau_actif].nodes[ni] = nodei;
                        int nj = Convert.ToInt32(ch[1]);

                        nodej.i = nj;
                        while (projet.reseaux[projet.reseau_actif].nodes.Count < nj + 1)
                        {
                            projet.reseaux[projet.reseau_actif].nodes.Add(nul);
                        }
                        projet.reseaux[projet.reseau_actif].nodes[nj] = nodej;


                        link lien = new link();
                        lien.no = ni;
                        lien.nd = nj;
                        lien.temps = Convert.ToSingle(ch[2]);
                        lien.longueur = Convert.ToSingle(ch[3]);
                        projet.reseaux[projet.reseau_actif].links.Add(lien);

                    }
                }
                fichier_reseau.Close();

                //construction du graphe
                // table des prédécesseurs et successeurs de noeuds
                for (i = 0; i < projet.reseaux[projet.reseau_actif].links.Count; i++)
                {
                    turn virage = new turn();
                    virage.numero = i;
                    virage.temps = 0;
                    virage.distance = 0;
                    virage.cout = 0;

                    projet.reseaux[projet.reseau_actif].nodes[projet.reseaux[projet.reseau_actif].links[i].nd].pred.Add(virage);
                    projet.reseaux[projet.reseau_actif].nodes[projet.reseaux[projet.reseau_actif].links[i].no].succ.Add(virage);
                    //                    Console.SetCursorPosition(1, Console.CursorTop-1);

                }


                // table des prédécesseurs et successeurs de tronçons
                //Console.WriteLine("création de la topologie des noeuds terminée");
                for (i = 0; i < projet.reseaux[projet.reseau_actif].links.Count; i++)
                {
                    for (j = 0; j < projet.reseaux[projet.reseau_actif].nodes[projet.reseaux[projet.reseau_actif].links[i].no].pred.Count; j++)
                    {
                        turn virage = new turn();
                        int predecesseur = projet.reseaux[projet.reseau_actif].nodes[projet.reseaux[projet.reseau_actif].links[i].no].pred[j].numero;
                        {
                            virage.numero = predecesseur;
                            projet.reseaux[projet.reseau_actif].links[i].arci.Add(virage);
                        }


                    }
                    for (j = 0; j < projet.reseaux[projet.reseau_actif].nodes[projet.reseaux[projet.reseau_actif].links[i].nd].succ.Count; j++)
                    {
                        turn virage = new turn();
                        int successeur = projet.reseaux[projet.reseau_actif].nodes[projet.reseaux[projet.reseau_actif].links[i].nd].succ[j].numero;
                        {
                            virage.numero = successeur;
                            projet.reseaux[projet.reseau_actif].links[i].arcj.Add(virage);
                        }

                    }

                    /*                    avancement.textBox1.Text = i.ToString() + " " + projet.reseaux[projet.reseau_actif].nodes[projet.reseaux[projet.reseau_actif].links[i].nd].succ.Count.ToString();
                                        avancement.progressBar1.Value = (100 * i / projet.reseaux[projet.reseau_actif].links.Count);
                                        avancement.Refresh();*/

                }


            }


        }

        private void simplifierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            Choix_reseau choix_carte = new Choix_reseau(projet);
            choix_carte.ShowDialog();
            network reseau = projet.reseaux[choix_carte.comboBox1.SelectedIndex];

            int i,j,k,narcj,narci,successeur=0,id_arc,predecesseur=0;
            int nblinks;
            
           /*Avancement avancement = new Avancement();
            avancement.Show();*/

            for (i = 0; i < reseau.links.Count; i++)
            {
                reseau.links[i].pivot = 0;
            }

            iteration:
            nblinks = reseau.links.Count;

            for (i = 0; i < reseau.links.Count; i++)
            {
                /*avancement.textBox1.Text = i.ToString() + " " + nblinks.ToString();
                avancement.progressBar1.Value = (int) (100 * i) / nblinks;
                avancement.Refresh();*/
                

                if (reseau.links[i].is_valid==true) 
                    {
                narcj = 0;
                    narci=0;
                    for (j = 0; j < reseau.links[i].arcj.Count; j++)
                    {

                        if (reseau.links[reseau.links[i].arcj[j].numero].nd != reseau.links[i].no && reseau.links[reseau.links[i].arcj[j].numero].is_valid == true && reseau.nodes[reseau.links[i].nd].ci == false)
                        {
                            narcj++;
                            successeur = reseau.links[i].arcj[j].numero;
                        }
                    }



                    if (narcj == 1 )
                    {
                        for (j = 0; j < reseau.links[successeur].arci.Count; j++)
                        {

                            if (reseau.links[reseau.links[successeur].arci[j].numero].no != reseau.links[successeur].nd && reseau.links[reseau.links[successeur].arci[j].numero].is_valid == true && reseau.nodes[reseau.links[successeur].no].ci == false)
                            {
                                narci++;
                                predecesseur = reseau.links[successeur].arci[j].numero;
                            }

                        }
                    }
                        if (narci==1 && narcj==1)
                {
                    link arc = new link();
                    link arca = reseau.links[i];
                    link arcb = reseau.links[successeur];
//préparation du tronçon fusionné
                    arc.no = arca.no;
                    arc.nd = arcb.nd;
                    arc.longueur = arca.longueur + arcb.longueur;
                    arc.temps = arca.temps + arcb.temps;
                    arc.vdf = arcb.vdf;
                    arc.lanes = arcb.lanes;
                    arc.modes = arcb.modes;
                    arc.type = arcb.type;
                    arc.volau = arcb.volau;
                    arc.is_valid = true;
                    // invalisation des arcs intermédiaires
                    reseau.links[i].is_valid = false;
                    reseau.links[i].pivot = reseau.links.Count;
                    reseau.links[successeur].is_valid = false;
                    reseau.links[successeur].pivot = reseau.links.Count ;
              
                    id_arc = nblinks;

//ajout des successeurs au tronçon fusionné                    
                    for (j = 0; j < arca.arci.Count; j++)
                    {
                            turn mvt = arca.arci[j];
                            if (reseau.links[arca.arci[j].numero].is_valid == true)
                            {
                                arc.arci.Add(mvt);
                                turn mvt2 = new turn();
                                mvt2.numero = reseau.links.Count;
                                mvt2.is_valid = true;
                                reseau.links[arca.arci[j].numero].arcj.Add(mvt2);
                                for (k = 0; k < reseau.links[arca.arci[j].numero].arcj.Count; k++)
                                {
                                    if (reseau.links[arca.arci[j].numero].arcj[k].numero == i)
                                    {
                                        reseau.links[arca.arci[j].numero].arcj.RemoveAt(k);
                                    }
                                }
                            }
                    }
//ajout des predecesseurs au tronçon fusionné
                    for (j = 0; j < arcb.arcj.Count; j++)
                    {
                            turn mvt = arcb.arcj[j];
                            if (reseau.links[arcb.arcj[j].numero].is_valid == true)
                            {
                                arc.arcj.Add(mvt);
                                turn mvt2 = new turn();
                                mvt2.numero = reseau.links.Count;
                                mvt2.is_valid = true;
                                reseau.links[arcb.arcj[j].numero].arci.Add(mvt2);
                                for (k = 0; k < reseau.links[arcb.arcj[j].numero].arci.Count; k++)
                                {
                                    if (reseau.links[arcb.arcj[j].numero].arci[k].numero == successeur)
                                    {
                                        reseau.links[arcb.arcj[j].numero].arci.RemoveAt(k);
                                    }
                                }
                            }

                    }
//ajout de l'arc fusionné                    
                    reseau.links.Add(arc);

// invalidation du noeud intermédiaire
                    //reseau.nodes[arca.nd].is_valid = false;

                    
                    
                }
                }
            }

            if (nblinks < reseau.links.Count)
            {
                goto iteration;
            }
            System.IO.StreamWriter sortie = new System.IO.StreamWriter("c:\\d211.in");
            sortie.WriteLine("t nodes");
            for (i = 0; i < reseau.nodes.Count;i++ )
            {
                if (reseau.nodes[i].is_valid == true && reseau.nodes[i].i>0)
                {
                    if (reseau.nodes[i].ci == true)
                    {
                        sortie.WriteLine("a* " + reseau.nodes[i].i + " " + reseau.nodes[i].x + " " + reseau.nodes[i].y + " 0 0 0");
                    }
                    else
                    {
                        sortie.WriteLine("a " + reseau.nodes[i].i + " " + reseau.nodes[i].x + " " + reseau.nodes[i].y + " 0 0 0");

                    }
                }
            }
            sortie.WriteLine("t links");

            for (i = 0; i < reseau.links.Count; i++)
            {
                //if (reseau.links[i].is_valid == true)
                {
                    if (reseau.links[i].pivot != 0)
                    {
                        while (reseau.links[reseau.links[i].pivot].pivot != 0)
                        {
                            reseau.links[i].pivot = reseau.links[reseau.links[i].pivot].pivot;
                        }
                        //sortie.WriteLine("cd " + reseau.links[i].no + " " + reseau.links[i].nd + " " + reseau.links[i].longueur + " " + reseau.links[i].modes + " " + reseau.links[i].type + " " + reseau.links[i].lanes + " " + reseau.links[i].vdf + " " + reseau.links[i].temps + " " + reseau.links[i].pivot + " 0");
                    }
                    else
                    {
                        reseau.links[i].pivot = i;
                        sortie.WriteLine("a " + reseau.links[i].no + " " + reseau.links[i].nd + " " + reseau.links[i].longueur + " " + reseau.links[i].modes + " " + reseau.links[i].type + " " + reseau.links[i].lanes + " " + reseau.links[i].vdf + " " + reseau.links[i].temps + " " + reseau.links[i].pivot + " 0");
                    }
                    
                }

                    
            }
            //avancement.Close();
            sortie.Close();
                
        }

        private void affectationVPÀHorairesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //importer réseau


            projet.reseaux.Add(new network());
            int i = 0, j, num_res;
            bool ci = false;
            float  xi, yi;
            string chaine, carte = "";
            string[] ch;

            //System.IO.StreamWriter resultat = new System.IO.StreamWriter("c:\\temp\\result.txt");
            //network reseau = new network();
            //network reseau = new network();  
            projet.reseau_actif = projet.reseaux.Count - 1;
            num_res = projet.reseaux.Count - 1;
            openFileDialog1.ShowDialog();
            string nom_reseau = openFileDialog1.FileName;
            if (System.IO.File.Exists(nom_reseau))
            {
                System.IO.StreamReader fichier_reseau = new System.IO.StreamReader(nom_reseau);
                projet.reseaux[num_res].matrices.Add(new matrix());
                do
                {

                    projet.reseaux[num_res].nom = System.IO.Path.GetFileNameWithoutExtension(nom_reseau);
                    chaine = fichier_reseau.ReadLine();
                    if (chaine == null) { goto lecture; }
                    if (chaine == "" || chaine.Substring(0, 1) == " ") { goto lecture; }

                    if (chaine.Substring(0, 1) == "c") { goto lecture; }
                    string[] param ={ " " };

                    if (chaine.Substring(0, 7) == "t nodes" || chaine.Substring(0, 7) == "t links")
                    {
                        carte = chaine.Substring(0, 7);
                        goto lecture;

                    }

                    //inserer noeuds// 
                    if (carte == "t nodes")
                    {
                        ch = chaine.Split(param, System.StringSplitOptions.RemoveEmptyEntries);

                        i = (int)System.Convert.ToSingle(ch[1]);
                        xi = System.Convert.ToSingle(ch[2]);
                        yi = System.Convert.ToSingle(ch[3]);
                        //emcombrement du reseau
                        if (xi > projet.reseaux[num_res].xu)
                        {
                            projet.reseaux[num_res].xu = xi;
                        }
                        if (xi < projet.reseaux[num_res].xl)
                        {
                            projet.reseaux[num_res].xl = xi;
                        }
                        if (yi > projet.reseaux[num_res].yu)
                        {

                            projet.reseaux[num_res].yu = yi;

                        }
                        if (yi < projet.reseaux[num_res].yl)
                        {
                            projet.reseaux[num_res].yl = yi;
                        }

                        if (ch[0] == "a*")
                        {
                            ci = true;



                        }
                        else
                        { ci = false; }





                        node ni = new node();
                        node nul = new node();
                        ni.i = i;
                        ni.x = xi;
                        ni.y = yi;
                        ni.ci = ci;

                        while (projet.reseaux[num_res].nodes.Count < i + 1)
                        {
                            projet.reseaux[num_res].nodes.Add(nul);
                        }
                        projet.reseaux[num_res].nodes[i] = ni;


                    }
                    //inserer liens
                    else if (carte == "t links")
                    {
                        ch = chaine.Split(param, System.StringSplitOptions.RemoveEmptyEntries);

                        link lien = new link();
                        int ii,periode=0;
                        for (ii=0;ii<(int)(1440f/periode);ii++)
                        {
                            lien.ul.Add(0);
                        }
                        lien.no = (int)System.Convert.ToSingle(ch[1]);
                        lien.nd = (int)System.Convert.ToSingle(ch[2]);
                        lien.longueur = System.Convert.ToSingle(ch[3]); ;
                        lien.modes = ch[4].ToString();
                        lien.type = (int)System.Convert.ToSingle(ch[5]); ;
                        lien.lanes = System.Convert.ToSingle(ch[6]); ;
                        lien.vdf = (int)System.Convert.ToSingle(ch[7]);
                        lien.v0 = (int)System.Convert.ToSingle(ch[8]);
                        lien.vsat = (int)System.Convert.ToSingle(ch[9]);
                        lien.n = (int)System.Convert.ToSingle(ch[10]);
                        lien.nb_voies = (int)Math.Max(lien.lanes/1.8f,1f);
                        lien.temps = lien.v0;

                        lien.touche = 0;
                        lien.cout = -1;
                        projet.reseaux[num_res].links.Add(lien);
                        //        MessageBox.Show(lien.no.ToString()+" "+lien.nd.ToString());
                    }


                lecture: ;

                } while (fichier_reseau.EndOfStream == false);
                fichier_reseau.Close();

                //construction du graphe
                // table des prédécesseurs et successeurs de noeuds
                for (i = 0; i < projet.reseaux[projet.reseau_actif].links.Count; i++)
                {
                    turn virage = new turn();
                    virage.numero = i;
                    virage.temps = 0;
                    virage.distance = 0;
                    virage.cout = 0;

                    projet.reseaux[projet.reseau_actif].nodes[projet.reseaux[projet.reseau_actif].links[i].nd].pred.Add(virage);
                    projet.reseaux[projet.reseau_actif].nodes[projet.reseaux[projet.reseau_actif].links[i].no].succ.Add(virage);
                    //                    Console.SetCursorPosition(1, Console.CursorTop-1);

                }
                // table des prédécesseurs et successeurs de tronçons
                Console.WriteLine("création de la topologie des noeuds terminée");
                for (i = 0; i < projet.reseaux[projet.reseau_actif].links.Count; i++)
                {
                    for (j = 0; j < projet.reseaux[projet.reseau_actif].nodes[projet.reseaux[projet.reseau_actif].links[i].no].pred.Count; j++)
                    {
                        turn virage = new turn();
                        projet.reseaux[projet.reseau_actif].links[i].arci.Add(virage);
                        projet.reseaux[projet.reseau_actif].links[i].arci[j].numero = projet.reseaux[projet.reseau_actif].nodes[projet.reseaux[projet.reseau_actif].links[i].no].pred[j].numero;

                    }
                    for (j = 0; j < projet.reseaux[projet.reseau_actif].nodes[projet.reseaux[projet.reseau_actif].links[i].nd].succ.Count; j++)
                    {
                        turn virage = new turn();
                        projet.reseaux[projet.reseau_actif].links[i].arcj.Add(virage);
                        projet.reseaux[projet.reseau_actif].links[i].arcj[j].numero = projet.reseaux[projet.reseau_actif].nodes[projet.reseaux[projet.reseau_actif].links[i].nd].succ[j].numero;

                    }


                }





            }







            //importer matrices


            import_matrice imp_mat = new import_matrice(projet);
            imp_mat.ShowDialog();
            projet.reseau_actif = 0;
            //            projet.reseaux[projet.reseau_actif].matrices.Add();
            
            int ori, des;
            
            float mf;
            string[] param2 ={ " " };
            //lecture matrice
            openFileDialog1.ShowDialog();
            if (System.IO.File.Exists(openFileDialog1.FileName))
            {
                System.IO.StreamReader fichier_matrice = new System.IO.StreamReader(openFileDialog1.FileName);
                while (fichier_matrice.EndOfStream == false)
                {
                    chaine = fichier_matrice.ReadLine();
                    ch = chaine.Split(param2, StringSplitOptions.RemoveEmptyEntries);
                    ori = (int)System.Convert.ToSingle(ch[0]);
                    des = (int)System.Convert.ToSingle(ch[1]);
                    mf = System.Convert.ToSingle(ch[2]);


                    while (projet.reseaux[projet.reseau_actif].matrices[0].o.Count <= ori)
                    {
                        projet.reseaux[projet.reseau_actif].matrices[0].o.Add(new vecteur());
                    }
                    while (projet.reseaux[projet.reseau_actif].matrices[0].o[ori].d.Count <= des)
                    {
                        projet.reseaux[projet.reseau_actif].matrices[0].o[ori].d.Add(new float());
                    }

                    projet.reseaux[projet.reseau_actif].matrices[0].o[ori].d[des] = mf;
                }

            }









            Avancement avancement = new Avancement();
            avancement.Show();
            //int i, j;
            // Console.WriteLine("création de la topologie des tronçons terminée");
            //plus courts chemins
          //  Queue<int> touches = new Queue<int>();
           // Queue<int> calcules = new Queue<int>();
            List<List<int>> gga_nq = new List<List<int>>();
            int nb_periode = 12;

            System.IO.StreamWriter fichier_sortie = new System.IO.StreamWriter("c:\\result.txt");


            int per;
            //initilisation
            for (i = 0; i < projet.reseaux[projet.reseau_actif].links.Count; i++)
            {
                for (per = 0; per < projet.reseaux[projet.reseau_actif].links[i].ul.Count; per++)
                {
                    projet.reseaux[projet.reseau_actif].links[i].ul[per] = 0;
                }
                projet.reseaux[projet.reseau_actif].links[i].volau = 0;
                projet.reseaux[projet.reseau_actif].links[i].touche = 0;
                projet.reseaux[projet.reseau_actif].links[i].cout = 0;
                projet.reseaux[projet.reseau_actif].links[i].tmap = 0;
                projet.reseaux[projet.reseau_actif].links[i].pivot = -1;
                projet.reseaux[projet.reseau_actif].links[i].is_queue = false;
                for (j = 0; j < nb_periode; j++)
                {
                    projet.reseaux[projet.reseau_actif].links[i].ul.Add(0);
                }

                //projet.reseaux[projet.reseau_actif].links[i].temps = projet.reseaux[projet.reseau_actif].links[i].fd(projet.reseaux[projet.reseau_actif].links[i].volau, projet.reseaux[projet.reseau_actif].links[i].longueur, 0f, projet.reseaux[projet.reseau_actif].links[i].lanes * 1000, projet.reseaux[projet.reseau_actif].links[i].v0, projet.reseaux[projet.reseau_actif].links[i].a, projet.reseaux[projet.reseau_actif].links[i].b, projet.reseaux[projet.reseau_actif].links[i].n);

            }


            int p, q;
            for (p = 0; p < projet.reseaux[projet.reseau_actif].matrices[0].o.Count; p++)
            {

                if (projet.reseaux[projet.reseau_actif].matrices[0].o[p].d.Count > 0)
                {
                    for (i = 0; i < projet.reseaux[projet.reseau_actif].links.Count; i++)
                    {
                        projet.reseaux[projet.reseau_actif].links[i].touche = 0;
                        projet.reseaux[projet.reseau_actif].links[i].cout = 0;
                        projet.reseaux[projet.reseau_actif].links[i].tmap = 0;
                        projet.reseaux[projet.reseau_actif].links[i].pivot = -1;

                    }
                    int depart = p;
                    int successeur, pivot = -1, bucket, id_bucket = 0; ;

                    for (j = 0; j < projet.reseaux[projet.reseau_actif].nodes[depart].succ.Count; j++)
                    {
                        successeur = projet.reseaux[projet.reseau_actif].nodes[depart].succ[j].numero;
                        bucket = Convert.ToInt32((Math.Pow(projet.reseaux[projet.reseau_actif].links[successeur].cout, 2) / projet.param_affectation_horaire.param_dijkstra));
                        while (bucket >= gga_nq.Count)
                        {
                            gga_nq.Add(new List<int>());
                        }
                        gga_nq[bucket].Add(successeur);

                        //touches.Enqueue(projet.reseaux[projet.reseau_actif].nodes[depart].succ[j].numero);
                        projet.reseaux[projet.reseau_actif].links[projet.reseaux[projet.reseau_actif].nodes[depart].succ[j].numero].touche = 1;
                        projet.reseaux[projet.reseau_actif].links[projet.reseaux[projet.reseau_actif].nodes[depart].succ[j].numero].cout = projet.reseaux[projet.reseau_actif].links[projet.reseaux[projet.reseau_actif].nodes[depart].succ[j].numero].temps;
                        projet.reseaux[projet.reseau_actif].links[projet.reseaux[projet.reseau_actif].nodes[depart].succ[j].numero].tmap = projet.reseaux[projet.reseau_actif].links[projet.reseaux[projet.reseau_actif].nodes[depart].succ[j].numero].longueur;

                    }

                    while (gga_nq.Count > id_bucket)
                    {

                        while (gga_nq[id_bucket].Count == 0)
                        {
                            id_bucket++;
                            if (id_bucket == gga_nq.Count)
                            {
                                goto fin_gga;
                            }
                        }
                        pivot = gga_nq[id_bucket][0];
                        gga_nq[id_bucket].RemoveAt(0);


                        for (j = 0; j < projet.reseaux[projet.reseau_actif].links[pivot].arcj.Count; j++)
                        {
                            successeur = projet.reseaux[projet.reseau_actif].links[pivot].arcj[j].numero;

                            if (projet.reseaux[projet.reseau_actif].links[successeur].touche == 0)
                            {
                                projet.reseaux[projet.reseau_actif].links[successeur].touche = 1;
                                projet.reseaux[projet.reseau_actif].links[successeur].cout = projet.reseaux[projet.reseau_actif].links[pivot].cout + projet.reseaux[projet.reseau_actif].links[successeur].temps;
                                projet.reseaux[projet.reseau_actif].links[successeur].tmap = projet.reseaux[projet.reseau_actif].links[pivot].tmap + projet.reseaux[projet.reseau_actif].links[successeur].longueur;
                                bucket = Convert.ToInt32((Math.Pow(projet.reseaux[projet.reseau_actif].links[successeur].cout, 2) / projet.param_affectation_horaire.param_dijkstra));
                                while (bucket >= gga_nq.Count)
                                {
                                    gga_nq.Add(new List<int>());
                                }
                                gga_nq[bucket].Add(successeur);

                                projet.reseaux[projet.reseau_actif].links[successeur].pivot = pivot;
                            }
                            else if (projet.reseaux[projet.reseau_actif].links[successeur].touche == 1 || projet.reseaux[projet.reseau_actif].links[successeur].touche == 2)
                            {
                                if (projet.reseaux[projet.reseau_actif].links[successeur].cout > projet.reseaux[projet.reseau_actif].links[pivot].cout + projet.reseaux[projet.reseau_actif].links[successeur].temps)
                                {
                                    bucket = Convert.ToInt32(Math.Pow(projet.reseaux[projet.reseau_actif].links[successeur].cout, 2) / projet.param_affectation_horaire.param_dijkstra);

                                    projet.reseaux[projet.reseau_actif].links[successeur].touche = 2;
                                    projet.reseaux[projet.reseau_actif].links[successeur].tmap = projet.reseaux[projet.reseau_actif].links[pivot].tmap + projet.reseaux[projet.reseau_actif].links[successeur].longueur;
                                    projet.reseaux[projet.reseau_actif].links[successeur].cout = projet.reseaux[projet.reseau_actif].links[pivot].cout + projet.reseaux[projet.reseau_actif].links[successeur].temps;
                                    projet.reseaux[projet.reseau_actif].links[successeur].pivot = pivot;
                                    gga_nq[bucket].Remove(successeur);
                                    bucket = Convert.ToInt32(Math.Pow(projet.reseaux[projet.reseau_actif].links[successeur].cout, 2) / projet.param_affectation_horaire.param_dijkstra);
                                    gga_nq[bucket].Add(successeur);

                                }
                            }

                        }
                        //projet.reseaux[projet.reseau_actif].links[pivot].touche = 3;
                        //Console.WriteLine((touches.Count+calcules.Count).ToString());
                    }
                fin_gga:

                    avancement.textBox1.Text = p.ToString();
                    avancement.progressBar1.Value = (100 * p / projet.reseaux[projet.reseau_actif].matrices[0].o.Count);                    //Console.SetCursorPosition(1, Console.CursorTop - 1);

                    avancement.Refresh();

                    //Console.WriteLine(p.ToString());
                    for (q = 0; q < projet.reseaux[projet.reseau_actif].matrices[0].o[p].d.Count; q++)
                    {
                        int arrivee = 0;

                        if (projet.reseaux[projet.reseau_actif].matrices[0].o[p].d[q] != 0)
                        {
                            float cout_fin = 1e8f;

                            for (j = 0; j < projet.reseaux[projet.reseau_actif].nodes[q].pred.Count; j++)
                            {
                                int predecesseur = projet.reseaux[projet.reseau_actif].nodes[q].pred[j].numero;
                                if (projet.reseaux[projet.reseau_actif].links[predecesseur].pivot != -1 && projet.reseaux[projet.reseau_actif].links[predecesseur].cout <= cout_fin)
                                {
                                    arrivee = predecesseur;
                                    cout_fin = projet.reseaux[projet.reseau_actif].links[predecesseur].cout;

                                }



                            }
                            pivot = arrivee;
                            while (pivot != -1)
                            {
                                projet.reseaux[projet.reseau_actif].links[pivot].volau += projet.reseaux[projet.reseau_actif].matrices[0].o[p].d[q];
                                pivot = projet.reseaux[projet.reseau_actif].links[pivot].pivot;
                                /*                                if (pivot != -1)
                                                                {
                                                                    fichier_sortie.WriteLine(projet.reseaux[projet.reseau_actif].links[pivot].no.ToString() + " " + projet.reseaux[projet.reseau_actif].links[pivot].nd.ToString() + " " + projet.reseaux[projet.reseau_actif].links[pivot].cout.ToString() + " " + projet.reseaux[projet.reseau_actif].links[pivot].tmap.ToString());
                                                                }*/
                            }
                            if (p != q)
                            {
                                fichier_sortie.WriteLine(p.ToString() + " " + q.ToString() + " " + projet.reseaux[projet.reseau_actif].links[arrivee].cout.ToString() + " " + projet.reseaux[projet.reseau_actif].links[arrivee].tmap.ToString());
                            }
                            else
                            {
                                fichier_sortie.WriteLine(p.ToString() + " " + q.ToString() + " 0 0");
                            }
                        }
                    }
                }

            }
            avancement.Close();
            fichier_sortie.Close();

        }

        private void MusliW_FormClosed(object sender, FormClosedEventArgs e)
        {
            projet.reseaux.Clear();
            
        }

        private void aProposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Application.ProductName + "\nVersion " + Application.ProductVersion + "\n©CETE Nord-Picardie\n");
        }
            
    }

        


    
    public class vecteur
    {
        public List<float> d = new List<float>(0);

    }


    public class turn
    {
        public int numero;
        public float temps, cout, distance;
        public bool is_valid = true;

    }

    public class node
    {
        public float x, y;
        public int i;
        public bool ci=false,is_valid=true,is_visible=false;
        public List<turn> pred = new List<turn>(10);
        public List<turn> succ = new List<turn>(10);
        public List<float> ui = new List<float>();
        public string texte;
    }
    public class link
    {
        public float longueur, temps, cout, v0, vsat,tatt, tcor, tveh, tmap, a, b, n, heured, volau, lanes, h, heuref;
        public int no, nd, service, type, vdf, touche, pivot, ligne,pole,nb_voies;
        public bool is_queue,is_valid=true;
        public List<turn> arci = new List<turn>(10);
        public List<turn> arcj = new List<turn>(10);
        public List<float> ul = new List<float>();
        public List<Service> services = new List<Service>(10);
        public string texte, modes;
        public float fd(float volau, float len, float precha, float cap, float v0, float a, float b, float n)
        {
            float vc, t0, delay;
            t0 = len / v0;
            vc = (volau + precha) / cap;
            if (vc < 1)
            {
                delay = t0 * (a - b * vc) / (a - vc);
            }
            else
            {
                delay = t0 * ((a * (1f - b)) / (n * (a - 1f) * (a - 1f))) * (float)Math.Pow(vc, n) + (n * (a - b) * (a - 1f) - a * (1f - b)) / (n * (a - 1f) * (a - 1f));
            }
            return delay;
        }
    }

        
    public class matrix
    {
        public string nom;
        public List<vecteur> o = new List<vecteur>(0);
        
    }

    public class network
    {
        public string nom;
        public List<link> links = new List<link>(20000);
        public List<node> nodes = new List<node>(10000);
        public float xl = 1e38f, xu = -1e38f, yl = 1e38f, yu = -1e38f;
        public List<matrix> matrices = new List<matrix>();
    }

    public class etude
    {
        public string nom;
        public int reseau_actif;
        public List<network> reseaux = new List<network>();
        public Param_affectation_horaire param_affectation_horaire = new Param_affectation_horaire();

    }

    public class Param_affectation_horaire
    {
        public string nom_reseau, nom_matrice,nom_sortie;
        public float coef_tmap, cmap, cwait, cboa, tboa, cveh,param_dijkstra;
        public bool sortie_chemins,sortie_temps;
        public int algorithme = 1,max_nb_buckets=10000;
        public int nb_jours;

    }

    public class Service
    {
        public int numero;
        public float hd, hf,delta;
        public string regime;
    }

    public class suivant
    {
        public List<int> classe=new List<int>();
        
    }
    
}