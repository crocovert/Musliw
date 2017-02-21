using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Musliw
{
    public partial class Carte : Form
    {
        public etude projet;
        public Fenetre fen;
        public int nproj=0;

        public Carte(etude proj, int i)
        {
            InitializeComponent();
            projet = proj;
            nproj = i;
            fen = new Fenetre();

            
        }

        private void Carte_Load(object sender, EventArgs e)
        {
            Graphics page = this.CreateGraphics();
           
            fen.xu = projet.reseaux[nproj].xu;
            fen.xl = projet.reseaux[nproj].xl;
            fen.yu = projet.reseaux[nproj].yu;
            fen.yl = projet.reseaux[nproj].yl;
            fen.xc = 0.5f* (fen.xl + fen.xu);
            fen.yc = 0.5f * (fen.yl + fen.yu);
            fen.taille_texte = 10f;
            fen.couleur_texte = Color.Black;

            fen.ecart = 1f;
            fen.epaisseur = 1f;
            fen.volume_echelle = 100f;
            fen.stylo_couleur = Color.Black;
            fen.brosse_couleur = Color.Red;
            fen.echelle = Math.Max((fen.xu - fen.xl) / this.Width, (fen.yu - fen.yl) / this.Height);
            
            fen.zoom = fen.echelle;
            this.Text = projet.reseaux[nproj].nom+" Carte";

        }

        private void Carte_Paint(object sender, PaintEventArgs e)
        {
            int i;
            float w, h;
            //projet = ((Musliw.MusliW)(this.MdiParent)).projet;
            Graphics page = e.Graphics;
            
            //page.Clear(this.BackColor);

            w = this.Width;
            h = this.Height;
            
            Pen stylo = new Pen(fen.stylo_couleur, fen.epaisseur);
            Font fonte = new Font(FontFamily.GenericSansSerif, 7,FontStyle.Bold);
            this.ForeColor = Color.Black;
            Brush brosse =new SolidBrush(fen.brosse_couleur);
            Brush brosse_texte = new SolidBrush(fen.couleur_texte);
            float dx = w / (projet.reseaux[nproj].xu - projet.reseaux[nproj].xl);
            float dy = h / (projet.reseaux[nproj].yu - projet.reseaux[nproj].yl);
            float deltax,deltay,voldeltax,voldeltay;
            float cx = 0.5f * (projet.reseaux[nproj].xu + projet.reseaux[nproj].xl);
            float cy = 0.5f * (projet.reseaux[nproj].yu + projet.reseaux[nproj].yl);
            
            //MessageBox.Show(xl.ToString() + " " + yu.ToString());
            PointF p1=new PointF();
            PointF p2 = new PointF();
            PointF p3 = new PointF();
            PointF p4 = new PointF();
            PointF p5 = new PointF();
            

            PointF[] points = new PointF[4] ;
            float angle=0,norme=0;
            float sinx = 0, cosx = 1;
            if (fen.volume_echelle < 1e-6f)
            {
                fen.volume_echelle = 1e-6f;
            }
            for (i = 0; i < projet.reseaux[nproj].links.Count; i++)
            {
                norme = fen.norme(projet.reseaux[nproj].nodes[projet.reseaux[nproj].links[i].no].x, projet.reseaux[nproj].nodes[projet.reseaux[nproj].links[i].nd].x, projet.reseaux[nproj].nodes[projet.reseaux[nproj].links[i].no].y, projet.reseaux[nproj].nodes[projet.reseaux[nproj].links[i].nd].y, fen.ecart + 0.5f * fen.epaisseur);
                if ((projet.reseaux[nproj].nodes[projet.reseaux[nproj].links[i].no].is_visible == true && projet.reseaux[nproj].nodes[projet.reseaux[nproj].links[i].nd].is_visible == true && norme > 0) && (projet.reseaux[nproj].nodes[projet.reseaux[nproj].links[i].no].is_valid == true && projet.reseaux[nproj].nodes[projet.reseaux[nproj].links[i].nd].is_valid == true))
                {
 
                    //page.DrawRectangle(stylo, 0f, 0f, 200, 200);
                    //MessageBox.Show(((res.nodes[i].x - res.xl) * delta).ToString() + " " + ((res.yu - res.nodes[i].y) * delta).ToString()+" "+w.ToString()+" "+h.ToString());
                deltax = fen.deltax(projet.reseaux[nproj].nodes[projet.reseaux[nproj].links[i].no].x, projet.reseaux[nproj].nodes[projet.reseaux[nproj].links[i].nd].x, projet.reseaux[nproj].nodes[projet.reseaux[nproj].links[i].no].y, projet.reseaux[nproj].nodes[projet.reseaux[nproj].links[i].nd].y, fen.ecart+0.5f*fen.epaisseur);
                deltay = fen.deltay(projet.reseaux[nproj].nodes[projet.reseaux[nproj].links[i].no].x, projet.reseaux[nproj].nodes[projet.reseaux[nproj].links[i].nd].x, projet.reseaux[nproj].nodes[projet.reseaux[nproj].links[i].no].y, projet.reseaux[nproj].nodes[projet.reseaux[nproj].links[i].nd].y, fen.ecart+0.5f*fen.epaisseur);
                cosx = fen.deltax(projet.reseaux[nproj].nodes[projet.reseaux[nproj].links[i].no].x, projet.reseaux[nproj].nodes[projet.reseaux[nproj].links[i].nd].x, projet.reseaux[nproj].nodes[projet.reseaux[nproj].links[i].no].y, projet.reseaux[nproj].nodes[projet.reseaux[nproj].links[i].nd].y, 1);
                sinx = fen.deltay(projet.reseaux[nproj].nodes[projet.reseaux[nproj].links[i].no].x, projet.reseaux[nproj].nodes[projet.reseaux[nproj].links[i].nd].x, projet.reseaux[nproj].nodes[projet.reseaux[nproj].links[i].no].y, projet.reseaux[nproj].nodes[projet.reseaux[nproj].links[i].nd].y, 1);

                    voldeltax = fen.deltax(projet.reseaux[nproj].nodes[projet.reseaux[nproj].links[i].no].x, projet.reseaux[nproj].nodes[projet.reseaux[nproj].links[i].nd].x, projet.reseaux[nproj].nodes[projet.reseaux[nproj].links[i].no].y, projet.reseaux[nproj].nodes[projet.reseaux[nproj].links[i].nd].y, fen.ecart + 0.5f * fen.epaisseur + projet.reseaux[projet.reseau_actif].links[i].volau / fen.volume_echelle);
                    voldeltay = fen.deltay(projet.reseaux[nproj].nodes[projet.reseaux[nproj].links[i].no].x, projet.reseaux[nproj].nodes[projet.reseaux[nproj].links[i].nd].x, projet.reseaux[nproj].nodes[projet.reseaux[nproj].links[i].no].y, projet.reseaux[nproj].nodes[projet.reseaux[nproj].links[i].nd].y, fen.ecart + 0.5f * fen.epaisseur + projet.reseaux[projet.reseau_actif].links[i].volau / fen.volume_echelle);
                    page.DrawLine(stylo, ((projet.reseaux[nproj].nodes[projet.reseaux[nproj].links[i].no].x - fen.xl) / fen.echelle) + deltay, ((fen.yu - projet.reseaux[nproj].nodes[projet.reseaux[nproj].links[i].no].y) / fen.echelle) + deltax, ((projet.reseaux[nproj].nodes[projet.reseaux[nproj].links[i].nd].x - fen.xl) / fen.echelle) + deltay, ((fen.yu - projet.reseaux[nproj].nodes[projet.reseaux[nproj].links[i].nd].y) / fen.echelle) + deltax);


                    p1.X = ((projet.reseaux[nproj].nodes[projet.reseaux[nproj].links[i].no].x - fen.xl) / fen.echelle) + deltay;
                    p1.Y = ((fen.yu - projet.reseaux[nproj].nodes[projet.reseaux[nproj].links[i].no].y) / fen.echelle) + deltax;
                    p2.X = ((projet.reseaux[nproj].nodes[projet.reseaux[nproj].links[i].no].x - fen.xl) / fen.echelle) + voldeltay;
                    p2.Y = ((fen.yu - projet.reseaux[nproj].nodes[projet.reseaux[nproj].links[i].no].y) / fen.echelle) + voldeltax;
                    p3.X = ((projet.reseaux[nproj].nodes[projet.reseaux[nproj].links[i].nd].x - fen.xl) / fen.echelle) + voldeltay;
                    p3.Y = ((fen.yu - projet.reseaux[nproj].nodes[projet.reseaux[nproj].links[i].nd].y) / fen.echelle) + voldeltax;
                    p4.X = ((projet.reseaux[nproj].nodes[projet.reseaux[nproj].links[i].nd].x - fen.xl) / fen.echelle) + deltay;
                    p4.Y = ((fen.yu - projet.reseaux[nproj].nodes[projet.reseaux[nproj].links[i].nd].y) / fen.echelle) + deltax;


                    System.Drawing.Drawing2D.GraphicsPath epaisseur = new System.Drawing.Drawing2D.GraphicsPath();
                    System.Drawing.Drawing2D.GraphicsPath texte_epaisseur = new System.Drawing.Drawing2D.GraphicsPath();
                    epaisseur.StartFigure();
                    points[0] = p1;
                    points[1] = p2;
                    points[2] = p3;
                    points[3] = p4;
                    
                    
                    epaisseur.AddPolygon(points);
                    epaisseur.CloseFigure();
                    //page.FillPath(brosse, epaisseur);
                    //page.FillPolygon(brosse, points);
                    //page.DrawPolygon(stylo,points);
                    epaisseur.Reset();
                    texte_epaisseur.StartFigure();
                    p5.X = 0.5f * (p3.X + p2.X);
                    p5.Y = 0.5f * (p3.Y + p2.Y);
                    texte_epaisseur.AddString(projet.reseaux[projet.reseau_actif].links[i].volau.ToString("0"), FontFamily.GenericSansSerif, 0, fen.taille_texte, new PointF(p5.X, p5.Y), StringFormat.GenericDefault);
                    RectangleF encombrement = texte_epaisseur.GetBounds();
                    // texte_epaisseur.AddRectangle(encombrement);
                    //texte_epaisseur.AddPie(p5.X,p5.Y,2,2,0,360);

                    page.FillPolygon(brosse, points);
                    page.DrawPolygon(stylo, points);

                    if (encombrement.Width < fen.norme(p1.X, p4.X, p1.Y, p4.Y, 1) && projet.reseaux[projet.reseau_actif].links[i].volau > 0)
                    {
                        System.Drawing.Drawing2D.Matrix rotation = new System.Drawing.Drawing2D.Matrix();

                        if (cosx >= 0 && sinx <= 0)
                        {
                            angle = 180f * ((float)Math.Acos(cosx) / (float)Math.PI);
                            rotation.RotateAt(angle, p5);
                            rotation.Translate(p5.X - encombrement.X, p5.Y - encombrement.Y);
                            System.Drawing.Drawing2D.Matrix trans = new System.Drawing.Drawing2D.Matrix();
                            texte_epaisseur.Transform(rotation);
                            trans.Translate(-0.5f * encombrement.Width * cosx, 0.5f * encombrement.Width * sinx);
                            texte_epaisseur.Transform(trans);
                            texte_epaisseur.CloseFigure();
                            page.FillPath(brosse_texte, texte_epaisseur);

                        }
                        else if (cosx <= 0 && sinx >= 0)
                        {
                            angle = 180f - 180f * ((float)Math.Acos(cosx) / (float)Math.PI);
                            rotation.RotateAt(angle, p5);
                            rotation.Translate(p5.X - encombrement.X, p5.Y - encombrement.Y);
                            System.Drawing.Drawing2D.Matrix trans = new System.Drawing.Drawing2D.Matrix();
                            texte_epaisseur.Transform(rotation);
                            trans.Translate(+0.5f * encombrement.Width * cosx + (encombrement.Height) * sinx, -0.5f * encombrement.Width * sinx + (encombrement.Height) * cosx);
                            texte_epaisseur.Transform(trans);
                            texte_epaisseur.CloseFigure();

                            page.FillPath(brosse_texte, texte_epaisseur);


                        }
                        else if (cosx >= 0 && sinx >= 0)
                        {
                            angle = -180f * (float)Math.Acos(cosx) / (float)Math.PI;
                            rotation.RotateAt(angle, p5);
                            rotation.Translate(p5.X - encombrement.X, p5.Y - encombrement.Y);
                            System.Drawing.Drawing2D.Matrix trans = new System.Drawing.Drawing2D.Matrix();
                            texte_epaisseur.Transform(rotation);
                            trans.Translate(-0.5f * encombrement.Width * cosx, 0.5f * encombrement.Width * sinx);
                            texte_epaisseur.Transform(trans);
                            texte_epaisseur.CloseFigure();

                            page.FillPath(brosse_texte, texte_epaisseur);
                        }
                        else if (cosx <= 0 && sinx <= 0)
                        {
                            angle = 180 + 180f * ((float)Math.Acos(cosx) / (float)Math.PI);
                            rotation.RotateAt(angle, p5);
                            rotation.Translate(p5.X - encombrement.X, p5.Y - encombrement.Y);
                            System.Drawing.Drawing2D.Matrix trans = new System.Drawing.Drawing2D.Matrix();
                            texte_epaisseur.Transform(rotation);
                            trans.Translate(+0.5f * encombrement.Width * cosx + (encombrement.Height) * sinx, -0.5f * encombrement.Width * sinx + (encombrement.Height) * cosx);
                            texte_epaisseur.Transform(trans);
                            texte_epaisseur.CloseFigure();

                            page.FillPath(brosse_texte, texte_epaisseur);
                        }

                    }
                    epaisseur.Dispose();
                    texte_epaisseur.Dispose();
                }
            
            }
        /*        for (i = 0; i < projet.reseaux[nproj].nodes.Count; i++)
            {
                if (projet.reseaux[nproj].nodes[i].i != 0)
                {
                    //page.DrawRectangle(stylo, 0f, 0f, 200, 200);
                    //MessageBox.Show(((res.nodes[i].x - res.xl) * delta).ToString() + " " + ((res.yu - res.nodes[i].y) * delta).ToString()+" "+w.ToString()+" "+h.ToString());
                    page.FillRectangle(brosse, (res.nodes[i].x - res.xl) * delta, (res.yu - res.nodes[i].y) * delta, 30f, 20f);
                    page.DrawRectangle(stylo, (res.nodes[i].x - res.xl) * delta, (res.yu - res.nodes[i].y) * delta, 30f, 20f);
                    page.DrawString(res.nodes[i].i.ToString(), fonte, Brushes.Black, new RectangleF((res.nodes[i].x - res.xl) * delta, (res.yu - res.nodes[i].y) * delta, 30f, 20f));
                }
            }*/
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            float x1, x2, y1, y2;
            x1 = fen.xl;
            x2 = fen.xl+this.Width*fen.echelle; 
            y1 = fen.yl;
            y2 = fen.yl+this.Height*fen.echelle;
            fen.echelle = fen.echelle * 0.5f;
            fen.xl = 0.5f * (x1 + x2) - 0.5f * this.Width * fen.echelle;
            fen.xu = 0.5f * (x1 + x2) + 0.5f * this.Width * fen.echelle;
            fen.yl = 0.5f * (y1 + y2) - 0.5f * this.Height * fen.echelle;
            fen.yu = 0.5f * (y1 + y2) + 0.5f * this.Height * fen.echelle;
            this.Invalidate();

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            float x1, x2, y1, y2;
            x1 = fen.xl;
            x2 = fen.xl + this.Width * fen.echelle;
            y1 = fen.yl;
            y2 = fen.yl + this.Height * fen.echelle;
            fen.echelle = fen.echelle * 2f;
            fen.xl = 0.5f * (x1 + x2) - 0.5f * this.Width * fen.echelle;
            fen.xu = 0.5f * (x1 + x2) + 0.5f * this.Width * fen.echelle;
            fen.yl = 0.5f * (y1 + y2) - 0.5f * this.Height * fen.echelle;
            fen.yu = 0.5f * (y1 + y2) + 0.5f * this.Height * fen.echelle;
            fen.xc = 0.5f * (fen.xl + fen.xu);
            fen.yc = 0.5f * (fen.yl + fen.yu);
            this.Invalidate();
        }



        private void Carte_MouseUp(object sender, MouseEventArgs e)
        {
            fen.xl += (fen.xc - this.PointToClient(new Point(e.X, e.Y)).X) * fen.echelle;
            fen.yl += (-fen.yc + this.PointToClient(new Point(e.X, e.Y)).Y) * fen.echelle;
            fen.xu += (fen.xc - this.PointToClient(new Point(e.X, e.Y)).X) * fen.echelle;
            fen.yu += (-fen.yc + this.PointToClient(new Point(e.X, e.Y)).Y) * fen.echelle;
            fen.xc = 0.5f * (fen.xl + fen.xu);
            fen.yc = 0.5f * (fen.yl + fen.yu);
            this.Invalidate();
        }

        private void Carte_MouseDown(object sender, MouseEventArgs e)
        {
            fen.xc = this.PointToClient(new Point(e.X, e.Y)).X;
            fen.yc = this.PointToClient(new Point(e.X, e.Y)).Y;
        }

        private void Carte_SizeChanged(object sender, EventArgs e)
        {
            fen.xu = fen.xl + this.Width * fen.echelle;
            fen.yl = fen.yu - this.Height * fen.echelle;
            fen.xc = 0.5f * (fen.xl + fen.xu);
            fen.yc = 0.5f * (fen.yl + fen.yu);
            this.Invalidate();

        }

        private void décalageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Affichage affichage = new Affichage(fen);
            affichage.ShowDialog();
            fen = affichage.fenetre;
            fen.echelle = Convert.ToSingle(affichage.textBox1.Text);
            fen.xl = fen.xc - 0.5f * this.Width * fen.echelle;
            fen.xu = fen.xc + 0.5f * this.Width * fen.echelle;
            fen.yl = fen.yc - 0.5f * this.Height * fen.echelle;
            fen.yu = fen.yc + 0.5f * this.Height * fen.echelle;

            


            this.Invalidate();
        }
    }
    public class Fenetre
    {
        public float xu, yu, xl, yl,xc,yc, echelle, ecart, epaisseur, volume_echelle,zoom,taille_texte;
        public Color stylo_couleur = new Color();
        public Color brosse_couleur = new Color();
        public Color couleur_texte = new Color();

        public float deltax(float x1,float x2,float y1,float y2,float ecart)
        {
            return (float) ((x2 - x1) / Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2)))*ecart;
            
        }
        public float deltay(float x1, float x2, float y1, float y2,float ecart)
        {
            return (float) ((y2 - y1) / Math.Sqrt(Math.Pow((x2 - x1),2) + Math.Pow((y2 - y1),2)))*ecart;
        }
        public float norme(float x1, float x2, float y1, float y2, float ecart)
        {
            return (float)(Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2))) * ecart;
        }
    }
    
}