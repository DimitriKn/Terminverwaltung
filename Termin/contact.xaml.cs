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

using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Windows.Forms;
using Termin;


namespace Termin
{
    /// <summary>
    /// Interaktionslogik für Window1.xaml
    /// </summary>
    public partial class Contact : Window
    {
        SqlDataAdapter sad;
        DataTable dt;
        SqlConnection cn = new SqlConnection(@"Server=DESKTOP-A1S2399\SQLEXPRESS; Database=Tdata;  Integrated Security= sspi");

        public Contact()
        {
            InitializeComponent();
        }

        private void saves_Click(object sender, RoutedEventArgs e)
        {
            
            sad = new SqlDataAdapter("insert into Kontakte (Name, Vorname, Firma, Strasse, Plz, Ort, Telefon, Email) values (' " + this.Name.Text+ "',' " + this.Vorname.Text + "'," +
                "' " + this.Firma.Text + "',' " + this.Strasse.Text + "',' " + this.Plz.Text + "',' " + this.Ort.Text + "',' " + this.Telefon.Text + "',' " + this.Email.Text + "')", cn);
            cn.Open();
            SqlCommandBuilder cb = new SqlCommandBuilder(sad);
            dt = new DataTable();
            sad.Fill(dt);
            cn.Close();
            System.Windows.MessageBox.Show("Daten wurden gespeichert");
            this.Close();
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
