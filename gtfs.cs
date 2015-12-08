using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Musliw
{
    public partial class gtfs : Form
    {
        public gtfs()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            
            string rep = "";
            
            if (folderBrowserDialog1.ShowDialog()==DialogResult.OK)
            {
                rep = folderBrowserDialog1.SelectedPath+"/";
                this.Cursor = Cursors.WaitCursor;
                //string rep="F:/offre_tc/Bordeaux/2011_2012/";
                DateTime debut_cal, fin_cal;
                debut_cal = dateTimePicker1.Value.Date;
                fin_cal = dateTimePicker2.Value.Date;
                string nom_musliw;// = rep + "test_musliw.txt";

                Dictionary<string, Google_Stop> google_stops = lit_google_stops(rep + "stops.txt");
                Dictionary<string, Google_Route> google_routes = lit_google_routes(rep + "routes.txt");
                Dictionary<string, Google_Trip> google_trips = lit_google_trips(rep + "trips.txt");
                Dictionary<string, List<Google_Calendar_Date>> google_calendar_dates = lit_google_calendar_dates(rep + "calendar_dates.txt");
                Dictionary<string, Google_Calendar> google_calendars = lit_google_calendars(rep + "calendar.txt", debut_cal, fin_cal, google_calendar_dates);
                Dictionary<string, SortedList<int, Google_Stop_Time>> google_stop_times = lit_google_stop_times(rep + "stop_times.txt");
                Dictionary<string, List<Google_Trip>> google_chainages = cree_chainages(google_routes, google_trips, google_calendars, google_stop_times);

                
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    this.Cursor = Cursors.WaitCursor;
                    nom_musliw = saveFileDialog1.FileName;
                    cree_musliw(nom_musliw, google_routes, google_trips, google_calendars, google_stop_times, google_chainages, google_stops);
                    this.Cursor = Cursors.Default;
                }
            }
        }



        public Dictionary<string, Google_Stop> lit_google_stops(string nom_stops)
        {
            Dictionary<string, Google_Stop> google_stops = new Dictionary<string, Google_Stop>();
            System.IO.StreamReader fichier_stops = new System.IO.StreamReader(nom_stops);

            List<string> header =  new List<string>((string[])(fichier_stops.ReadLine().Split(','))); 
            Dictionary<string, int> headers = new Dictionary<string, int>();
            int ii;
            for (ii = 0; ii < header.Count; ii++)
            {
                headers[header[ii].Trim('"')] = ii;
            }
            while (fichier_stops.EndOfStream == false)
            {
                
                string[] delim={"\""};
                //List<string> head = new List<string>((string[])(fichier_stops.ReadLine()).Split(delim,StringSplitOptions.None));
                List<string> elements =new List<string>((string[])System.Text.RegularExpressions.Regex.Split(fichier_stops.ReadLine(),",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)"));
                

                for (int ch=0; ch< elements.Count;ch++)
                {
                    elements[ch]=elements[ch].Replace("\"", "");

                    
                }
                
                
            
                //string[] elements = chaine.Split(',');
                Google_Stop google_stop = new Google_Stop();
                google_stop.numero = elements[headers["stop_id"]];
                google_stop.nom = elements[headers["stop_name"]];
                if (System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator == ",")
                {
                    google_stop.x = float.Parse(elements[headers["stop_lon"]].Replace(".", ","));
                    google_stop.y = float.Parse(elements[headers["stop_lat"]].Replace(".", ","));
                }
                else
                {
                    google_stop.x = float.Parse(elements[headers["stop_lon"]]);
                    google_stop.y = float.Parse(elements[headers["stop_lat"]]);
                }
                google_stops[google_stop.numero] = google_stop;
                

            }
            fichier_stops.Close();

            return google_stops;
        }

        public Dictionary<string, Google_Route> lit_google_routes(string nom_routes)
        {
            Dictionary<string, Google_Route> google_routes = new Dictionary<string, Google_Route>();
            System.IO.StreamReader fichier_routes = new System.IO.StreamReader(nom_routes);

            List<string> header = new List<string>((string[])(fichier_routes.ReadLine().Split(',')));
            Dictionary<string, int> headers = new Dictionary<string, int>();
            int ii;
            for (ii = 0; ii < header.Count; ii++)
            {
                headers[header[ii].Trim('"')] = ii;
            }
            while (fichier_routes.EndOfStream == false)
            {
                List<string> h = new List<string>();
                string[] delim = { "\"" };
                List<string> head = new List<string>((string[])(fichier_routes.ReadLine()).Split(delim, StringSplitOptions.None));
                foreach (string ch in head)
                {
                    h.Add(ch.Replace(", ", "_ "));


                }
                string chaine = string.Join(@"", h.ToArray());


                string[] elements = chaine.Split(',');

                Google_Route google_route = new Google_Route();
                google_route.numero = elements[headers["route_id"]];
                if (headers.ContainsKey("route_short_name"))
                {
                    google_route.nom = elements[headers["route_short_name"]];
                }
                else if (headers.ContainsKey("route_long_name"))
                {
                    google_route.nom = elements[headers["route_long_name"]];

                }
                else
                {
                    google_route.nom = " ";
                }
                google_routes[google_route.numero] = google_route;


            }
            fichier_routes.Close();

            return google_routes;
        }

        public Dictionary<string, Google_Trip> lit_google_trips(string nom_trips)
        {
            Dictionary<string, Google_Trip> google_trips = new Dictionary<string, Google_Trip>();
            System.IO.StreamReader fichier_trips = new System.IO.StreamReader(nom_trips);

            List<string> header = new List<string>((string[])(fichier_trips.ReadLine().Split(',')));
            Dictionary<string, int> headers = new Dictionary<string, int>();
            int ii;
            for (ii = 0; ii < header.Count; ii++)
            {
                headers[header[ii].Trim('"')] = ii;
            }
            while (fichier_trips.EndOfStream == false)
            {
                List<string> h = new List<string>();
                string[] delim = { "\"" };
                List<string> head = new List<string>((string[])(fichier_trips.ReadLine()).Split(delim, StringSplitOptions.None));
                foreach (string ch in head)
                {
                    h.Add(ch.Replace(", ", "_ "));


                }
                string chaine = string.Join(@"", h.ToArray());


                string[] elements = chaine.Split(',');


                Google_Trip google_trip = new Google_Trip();
                google_trip.route_id = elements[headers["route_id"]];
                google_trip.service_id = elements[headers["service_id"]];
                google_trip.trip_id = elements[headers["trip_id"]];
                google_trips[google_trip.trip_id] = google_trip;


            }
            fichier_trips.Close();

            return google_trips;
        }


        public Dictionary<string, List<Google_Calendar_Date>> lit_google_calendar_dates(string nom_calendar_dates)
        {
            Dictionary<string, List<Google_Calendar_Date>> google_calendar_dates= new Dictionary<string,List<Google_Calendar_Date>>();
            System.IO.StreamReader fichier_calendar_dates = new System.IO.StreamReader(nom_calendar_dates);

            List<string> header = new List<string>((string[])(fichier_calendar_dates.ReadLine().Split(',')));
            Dictionary<string, int> headers = new Dictionary<string, int>();
            int ii;
            for (ii = 0; ii < header.Count; ii++)
            {
                headers[header[ii].Trim('"')] = ii;
            }
            while (fichier_calendar_dates.EndOfStream == false)
            {
                List<string> h = new List<string>();
                string[] delim = { "\"" };
                List<string> head = new List<string>((string[])(fichier_calendar_dates.ReadLine()).Split(delim, StringSplitOptions.None));
                foreach (string ch in head)
                {
                    h.Add(ch.Replace(", ", "_ "));


                }
                string chaine = string.Join(@"", h.ToArray());


                string[] elements = chaine.Split(',');
                Google_Calendar_Date google_calendar_date = new Google_Calendar_Date();
                google_calendar_date.date = new DateTime(int.Parse(elements[headers["date"]].Substring(0, 4)), int.Parse(elements[headers["date"]].Substring(4, 2)), int.Parse(elements[headers["date"]].Substring(6, 2)));
                google_calendar_date.type = int.Parse(elements[headers["exception_type"]]);
                string service_id = elements[headers["service_id"]];
                
                if (google_calendar_dates.ContainsKey(service_id) == true)
                {
                    google_calendar_dates[service_id].Add(google_calendar_date);
                }
                else
                {
                    google_calendar_dates.Add(service_id,new List<Google_Calendar_Date>());
                    google_calendar_dates[service_id].Add(google_calendar_date);
                }

            }
            return google_calendar_dates;
        }

        public Dictionary<string, Google_Calendar> lit_google_calendars(string nom_calendars,DateTime debut_cal, DateTime fin_cal,Dictionary<string, List<Google_Calendar_Date>> google_calendar_dates)
        {
            Dictionary<string, Google_Calendar> google_calendars = new Dictionary<string, Google_Calendar>();
            if (System.IO.File.Exists(nom_calendars))
            {
                System.IO.StreamReader fichier_calendars = new System.IO.StreamReader(nom_calendars);

                List<string> header = new List<string>((string[])(fichier_calendars.ReadLine().Split(',')));
                Dictionary<string, int> headers = new Dictionary<string, int>();
                int ii;
                for (ii = 0; ii < header.Count; ii++)
                {
                    headers[header[ii].Trim('"')] = ii;
                }
                while (fichier_calendars.EndOfStream == false)
                {
                    List<string> h = new List<string>();
                    string[] delim = { "\"" };
                    List<string> head = new List<string>((string[])(fichier_calendars.ReadLine()).Split(delim, StringSplitOptions.None));
                    foreach (string ch in head)
                    {
                        h.Add(ch.Replace(", ", "_ "));


                    }
                    string chaine = string.Join(@"", h.ToArray());


                    string[] elements = chaine.Split(',');

                    string[] toto = new string[8];
                    string service_id = elements[headers["service_id"]], calendrier = "";
                    Array.Copy(elements, 1, toto, 0, 7);
                    Google_Calendar google_calendar = new Google_Calendar();
                    google_calendar.semaine = string.Join("", toto);
                    google_calendar.debut = new DateTime(int.Parse(elements[headers["start_date"]].Substring(0, 4)), int.Parse(elements[headers["start_date"]].Substring(4, 2)), int.Parse(elements[headers["start_date"]].Substring(6, 2)));
                    google_calendar.fin = new DateTime(int.Parse(elements[headers["end_date"]].Substring(0, 4)), int.Parse(elements[headers["end_date"]].Substring(4, 2)), int.Parse(elements[headers["end_date"]].Substring(6, 2)));
                    int duree_cal = Math.Max((fin_cal - debut_cal).Days, 1);
                    DateTime jour = debut_cal;
                    String semaine = google_calendar.semaine.Substring(6, 1) + google_calendar.semaine.Substring(0, 6);
                    for (int i = 0; i <= duree_cal; i++)
                    {
                        int jour_semaine = (int)jour.DayOfWeek;
                        if (jour >= google_calendar.debut && jour <= google_calendar.fin && semaine[jour_semaine] == '1')
                        {
                            calendrier += "O";
                        }
                        else
                        {
                            calendrier += "N";
                        }
                        jour = jour.AddDays(1);


                    }
                    google_calendar.calendrier = calendrier;
                    //MessageBox.Show(google_calendar.debut.ToString());
                    google_calendars[service_id] = google_calendar;



                }
                fichier_calendars.Close();
            }
            foreach (string cal in google_calendar_dates.Keys)
            {
                if (google_calendars.ContainsKey(cal) == false)
                {
                    int duree_cal=Math.Max((fin_cal - debut_cal).Days+1,1);
                    string cal_sem=new string('N',duree_cal);
                    Google_Calendar gc=new Google_Calendar();
                    gc.calendrier=cal_sem;
                    gc.debut=debut_cal;
                    gc.fin=fin_cal;
                    gc.semaine=new string('N',7);
                    google_calendars.Add(cal,gc);
                }
                if (google_calendar_dates.ContainsKey(cal))
                {
                    foreach (Google_Calendar_Date caldate in google_calendar_dates[cal])
                    {
                        DateTime date_jour=caldate.date;
                        int typjour = caldate.type;
                        if (date_jour >= debut_cal && date_jour <= fin_cal)
                        {
                            int delta=(date_jour-debut_cal).Days;
                            if (typjour == 1)
                            {
                                google_calendars[cal].calendrier = google_calendars[cal].calendrier.Substring(0, delta) + "O" + google_calendars[cal].calendrier.Substring(delta + 1);
                            }
                            else if (typjour == 2)
                            {
                                google_calendars[cal].calendrier = google_calendars[cal].calendrier.Substring(0, delta) + "N" + google_calendars[cal].calendrier.Substring(delta + 1);

                            }
                        }


                    }
                }
            }

            return google_calendars;
        }




        public Dictionary<string, SortedList<int,Google_Stop_Time> > lit_google_stop_times(string nom_stop_times)
        {
            
            Dictionary<string, SortedList<int,Google_Stop_Time> > google_stop_times = new Dictionary<string, SortedList<int,Google_Stop_Time>>();
            System.IO.StreamReader fichier_stop_times = new System.IO.StreamReader(nom_stop_times);
            List<string> header = new List<string>((string[])(fichier_stop_times.ReadLine().Split(',')));
            Dictionary<string, int> headers = new Dictionary<string, int>();
            int ii;
            for (ii = 0; ii < header.Count; ii++)
            {
                headers[header[ii].Trim('"')] = ii;
            }
            while (fichier_stop_times.EndOfStream == false)
            {
                List<string> h = new List<string>();
                string[] delim = { "\"" };
                List<string> head = new List<string>((string[])(fichier_stop_times.ReadLine()).Split(delim, StringSplitOptions.None));
                foreach (string ch in head)
                {
                    h.Add(ch.Replace(", ", "_ "));


                }
                string chaine = string.Join(@"", h.ToArray());


                string[] elements = chaine.Split(',');
                Google_Stop_Time passage = new Google_Stop_Time();
               
                passage.trip_id=elements[headers["trip_id"]];
                string[] h1 = elements[headers["arrival_time"]].Split(':');
                string[] h2 = elements[headers["departure_time"]].Split(':');
                passage.heure_arr = float.Parse(h1[0]) * 60f + float.Parse(h1[1]) + float.Parse(h1[2]) / 60f;
                passage.heure_dep = float.Parse(h2[0]) * 60f + float.Parse(h2[1]) + float.Parse(h2[2]) / 60f;
                passage.num_arret=elements[headers["stop_id"]];
                passage.num_ordre= int.Parse(elements[headers["stop_sequence"]]);
                if (google_stop_times.ContainsKey(passage.trip_id)==false)
                {
                    google_stop_times[passage.trip_id]=new SortedList<int,Google_Stop_Time>();
                }
                google_stop_times[passage.trip_id].Add(passage.num_ordre,passage);
                
                //MessageBox.Show(DateTime.Parse(elements[1]).ToString());
               

            }
            fichier_stop_times.Close();

            return google_stop_times;
        }

        public Dictionary<string, List<Google_Trip>>  cree_chainages(Dictionary<string,Google_Route> google_routes, Dictionary<string,Google_Trip> google_trips, Dictionary<string,Google_Calendar> google_calendars, Dictionary<string,SortedList<int,Google_Stop_Time>> google_stop_times)
        {
            Dictionary<string, List<Google_Trip>> chainages = new Dictionary<string, List<Google_Trip>>();
            foreach (string service in google_stop_times.Keys)
            {
                string chaine = "";
                foreach (int num_ordre in google_stop_times[service].Keys)
                {
                    chaine += google_stop_times[service][num_ordre].num_arret.ToString() + ";";

                }
                if (chainages.ContainsKey(chaine) == false)
                {
                    chainages[chaine] = new List<Google_Trip>();
                }
                Google_Trip trip=new Google_Trip();
                
                chainages[chaine].Add(google_trips[service]);
                
                
            }
            return chainages;
        }

    

        public void cree_musliw(string nom_musliw,Dictionary<string,Google_Route> google_routes, Dictionary<string,Google_Trip> google_trips,Dictionary<string,Google_Calendar> google_calendars, Dictionary<string,SortedList<int,Google_Stop_Time>> google_stop_times, Dictionary<string,List<Google_Trip>> google_chainages,Dictionary<string,Google_Stop> google_stops)
        {
            int i=0,n;
            System.IO.StreamWriter fichier_musliw = new System.IO.StreamWriter(nom_musliw);
   /*         fichier_musliw.WriteLine("t nodes");
            foreach (Google_Stop arret in google_stops.Values)
            {
                fichier_musliw.WriteLine(arret.numero.ToString() + ";" + arret.x.ToString() + ";" + arret.y.ToString());
            }
            fichier_musliw.WriteLine("t links");*/
            foreach (string chaine in google_chainages.Keys)
            {
                i++;
                int j = 0;
                foreach (Google_Trip mission in google_chainages[chaine])

                {
                    if (google_routes.ContainsKey(mission.route_id) == true)
                    {
                        SortedList<int, Google_Stop_Time> elements = google_stop_times[mission.trip_id];
                        n = elements.Count;
                        int k;
                        j++;
                        String textel = "";
                        for (k = 0; k < n - 1; k++)
                        {
                            if (google_stops.ContainsKey(elements.Values[k].num_arret) == false)
                            {
                                Google_Stop arret = new Google_Stop();
                                arret.nom = elements.Values[k].num_arret;
                                arret.x = 0;
                                arret.y = 0;
                                google_stops[elements.Values[k].num_arret] = arret;

                            }
                            if (google_stops.ContainsKey(elements.Values[k + 1].num_arret) == false)
                            {
                                Google_Stop arret = new Google_Stop();
                                arret.nom = elements.Values[k + 1].num_arret;
                                arret.x = 0;
                                arret.y = 0;
                                google_stops[elements.Values[k + 1].num_arret] = arret;

                            }
                            textel = textBox1.Text + elements.Values[k].num_arret.ToString();
                            textel += ";" + textBox1.Text + elements.Values[k + 1].num_arret.ToString();
                            textel += ";0;0";
                            textel += ";" + i.ToString() + ";" + j.ToString();
                            textel += ";" + elements.Values[k].heure_dep + ";" + elements.Values[k + 1].heure_arr;
                            textel += ";" + google_calendars[mission.service_id].calendrier + ";";
                            textel += google_routes[mission.route_id].nom + "|" + google_stops[elements.Values[k].num_arret].nom + "-" + google_stops[elements.Values[k + 1].num_arret].nom + ";0;0";
                            if (google_calendars[mission.service_id].calendrier.Split('O').Length > 1)
                            {
                                fichier_musliw.WriteLine(textel);

                            }

                        }

                    }
                }



            }
            fichier_musliw.Close();
            this.Cursor = Cursors.Default;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

         
    }


    public class Google_Stop
    {
        public string numero, nom;
        public float x, y;

    }



    public class Google_Route
    {
        public string numero,nom;
        

     }
 
    
    public class Google_Trip
    {
        public string route_id, service_id,trip_id;

    }



    public class Google_Calendar
    {
        public string semaine,calendrier;
        public DateTime debut,fin;




    }

    public class Google_Calendar_Date
    {
        public DateTime date;
        public int type;

    }

    public class Google_Stop_Time

    {

        public string trip_id, num_arret;
        public float heure_arr, heure_dep;
        public int num_ordre;

    }
}
