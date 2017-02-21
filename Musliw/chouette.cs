using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Npgsql;

namespace Musliw
{
    public partial class chouette : Form
    {
        public chouette()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            String nom_fichier_musliw = saveFileDialog1.FileName;
            System.IO.StreamWriter fichier_musliw = new System.IO.StreamWriter(nom_fichier_musliw);
            
            String chaine;
            chaine =  "Server=" + textBox1.Text + ";Port="+textBox5.Text+";User Id=" + textBox4.Text + ";Password=" + textBox3.Text + ";Database=" + textBox2.Text + ";" ;
//            MessageBox.Show(chaine);
            NpgsqlConnection chouette_plus = new NpgsqlConnection(chaine);
            chouette_plus.Open();


            DateTime debut_cal, fin_cal;
            debut_cal = dateTimePicker1.Value;
            fin_cal = dateTimePicker2.Value;


            int longueur_cal = (dateTimePicker2.Value - dateTimePicker1.Value).Days;
            Dictionary<int, String> regime = new Dictionary<int, string>();
            String texte_requete = "select * FROM chouette.timetable_date;";

            NpgsqlCommand requete = new NpgsqlCommand(texte_requete, chouette_plus);
            NpgsqlDataReader dr = requete.ExecuteReader();
            int cle;
            while (dr.Read())
            {
                cle = int.Parse(dr[0].ToString());
                //MessageBox.Show(dr[0].ToString());

                if (regime.ContainsKey(cle) == false)
                {
                    regime[cle] = new String('N', longueur_cal);

                }

                DateTime h = (DateTime)dr[1];
                if (h >= dateTimePicker1.Value && h <= dateTimePicker2.Value)
                {
                    int nb = (h - dateTimePicker1.Value).Days;
                    regime[cle] = regime[cle].Substring(0, (nb - 1)) + "O" + regime[cle].Substring(nb);
                }



            //    fichier_musliw.WriteLine(cle.ToString() + ":" + regime[cle]);

            }





            texte_requete="SELECT "+
  " stoparea.parentid, "+
" date_part('hour',vehiclejourneyatstop.arrivaltime)*60+ date_part('minute',vehiclejourneyatstop.arrivaltime )+ date_part('second',vehiclejourneyatstop.arrivaltime)/60 as arrival, "+
"  date_part('hour',vehiclejourneyatstop.departuretime )*60+ date_part('minute',vehiclejourneyatstop.departuretime )+date_part('second',vehiclejourneyatstop.departuretime)/60 as departure, "+
  " stoppoint.\"position\"," +
  " vehiclejourneyatstop.vehiclejourneyid, "+
  " vehiclejourney.routeid, "+
  " line.\"name\"," +
  " line.transportmodename,"+
  " timetablevehiclejourney.timetableid"+
" FROM "+
  " chouette.vehiclejourneyatstop, "+
  " chouette.stoparea, "+
  " chouette.stoppoint, "+
  " chouette.vehiclejourney, "+
  " chouette.line, "+
  " chouette.route,"+
  " chouette.timetablevehiclejourney"+
" WHERE "+
  " stoparea.id = stoppoint.stopareaid AND"+
  " stoppoint.id = vehiclejourneyatstop.stoppointid AND"+
  " vehiclejourney.id = vehiclejourneyatstop.vehiclejourneyid AND"+
  " line.id = route.lineid AND"+
  " route.id = vehiclejourney.routeid AND "+
  " timetablevehiclejourney.vehiclejourneyid = vehiclejourneyatstop.vehiclejourneyid"+
" ORDER BY"+
 " vehiclejourneyatstop.id ASC, "+
 " stoppoint.\"position\" ASC;";
            //MessageBox.Show(texte_requete);
            requete=  new NpgsqlCommand(texte_requete,chouette_plus);
             dr= requete.ExecuteReader();
           
            String stop_p = "", depart = "",arrivee="",line_p="";
            while(dr.Read())
            {String ligne="";
            if (line_p !=  dr[4].ToString())
            {
                stop_p = dr[0].ToString();
                arrivee = dr[1].ToString();
                depart = dr[2].ToString();
                line_p = dr[4].ToString();

            }
            else
            {
                
                
                ligne = stop_p + ";" + dr[0].ToString() + ";" + "-1;0;"+dr[5].ToString() + ";" + dr[4].ToString()+";"+depart.ToString()+";"+dr[1].ToString()+";"+regime[int.Parse(dr[8].ToString())]+";"+dr[6].ToString()+";0;0";
                fichier_musliw.WriteLine(ligne);
                stop_p = dr[0].ToString();
                arrivee = dr[1].ToString();
                depart = dr[2].ToString();
                line_p =
                    dr[4].ToString();
            }
            }

            texte_requete = "SELECT " +
            " connectionlink.departureid, connectionlink.arrivalid," +
          " date_part('hour',connectionlink.defaultduration)*60+ date_part('minute',connectionlink.defaultduration )+ date_part('second',connectionlink.defaultduration)/60 as duree, " +
            " connectionlink.linkdistance, connectionlink.name" + " FROM chouette.connectionlink";
            requete=  new NpgsqlCommand(texte_requete,chouette_plus);
             dr= requete.ExecuteReader();
            while(dr.Read())
            {
                string ligne = dr[0].ToString() + ";" + dr[1].ToString() + ";" + dr[2].ToString() + ";" + dr[3].ToString()+"-1;-1;-1;-1;-1;"+dr[4].ToString() + ";0;0";
                fichier_musliw.WriteLine(ligne);

                
            }

            //"where periodstart>=TIMESTAMP " + "\'" + dateTimePicker1.Value + "\'" +
            //"and periodend=<TIMESTAMP " + "\'" + dateTimePicker2.Value + "\'";

            
            fichier_musliw.Close();
            chouette_plus.Close();
            this.Close();
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void chouette_Load(object sender, EventArgs e)
        {

        }
    }
}
