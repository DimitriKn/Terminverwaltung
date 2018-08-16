using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.Data.SqlClient;
using System.Data;

namespace Termin
{
    /// <summary>
    /// Interaktionslogik für Window2.xaml
    /// </summary>
    public partial class Term : Window
    {
        SqlDataAdapter sad;
        DataTable dt;
        
        SqlConnection cn = new SqlConnection(@"Server=DESKTOP-A1S2399\SQLEXPRESS; Database=Tdata;  Integrated Security= sspi");
        public Term()
        {
            InitializeComponent();
            fill_combo();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            
            sad = new SqlDataAdapter("insert into Termine (Datum, Uhrzeit, KontaktArt, KontaktID, Inhalt) values (' " + this.Datum.Text + "',' " + this.Uhrzeit.Text + "'," +
                "' " + this.KontaktArt.Text + "',' " + this.Kontakt.Text.Substring(0, 2) + "',' " + this.Inhalt.Text + "')", cn);
            cn.Open();
            SqlCommandBuilder cb = new SqlCommandBuilder(sad);
            dt = new DataTable();
            sad.Fill(dt);
            cn.Close();
            //string a = this.Datum.Text;
            //string b = this.Uhrzeit.Text;
            //string c = this.KontaktArt.Text;
            //string d = this.Kontakt.Text.Substring(0,2);
            //string f = this.Inhalt.Text;
            string der = "Daten wurden gespeichert";
            //string mes = string.Concat(a, b, c, d, f);
            System.Windows.MessageBox.Show(der);
            this.Close();
        }

        private void fill_combo()
        {
            
            cn.Open();
            
            sad = new SqlDataAdapter("select * from Kontakte ", cn);
       
            SqlCommandBuilder cb = new SqlCommandBuilder(sad);
            dt = new DataTable();
            sad.Fill(dt);
            foreach (DataRow dt in dt.Rows)
            {
                string KontID = dt["KontaktID"].ToString();
                string Name = dt["Name"].ToString();
                string Vorname = dt["Vorname"].ToString();
                Kontakt.Items.Add(string.Concat(KontID, Name, Vorname)) ;
               
            }

        }


        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
