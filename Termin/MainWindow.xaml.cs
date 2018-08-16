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
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Windows.Forms;
using Termin;




namespace Terminverwaltung
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlDataAdapter sad;
        DataTable dt;
       // SqlConnection cn = new SqlConnection(@"Server=DIMITRIK-PC\SQLEXPRESS; Database=Tdata;  Integrated Security= sspi");
        SqlConnection cn = new SqlConnection(@"Server=DESKTOP-A1S2399\SQLEXPRESS; Database=Tdata;  Integrated Security= sspi");
        public MainWindow()
        {
            InitializeComponent();
            lb.Content = "Willkommen !";
        }

        private void Appointments_Click(object sender, RoutedEventArgs e)
        {

            sad = new SqlDataAdapter("select TerminID AS Nr, Datum, Uhrzeit, KontaktArt, Inhalt from Termine", cn);
            cn.Open();
            SqlCommandBuilder cb = new SqlCommandBuilder(sad);
            dt = new DataTable();
            sad.Fill(dt);
            dg.ItemsSource = dt.DefaultView;
            lb.Content = "Zum Speichern bitte die Taste 'Speichern' auswählen";
            cn.Close();
        }

        private void Contacts_Click(object sender, RoutedEventArgs e)
        {

            sad = new SqlDataAdapter("select KontaktID AS Nr, Name, Vorname, Firma, Strasse, Plz, Ort, Telefon, Email  from Kontakte", cn);
            cn.Open();
            SqlCommandBuilder cb = new SqlCommandBuilder(sad);
            dt = new DataTable();
            sad.Fill(dt);
            dg.ItemsSource = dt.DefaultView;
            lb.Content = "Zum Speichern bitte die Taste 'Speichern' auswählen";
            cn.Close();
        }

        private void NewContact_Click(object sender, RoutedEventArgs e)
        {
            Contact cont = new Contact();
            cont.ShowDialog();
        }

        private void NewAppoint_Click(object sender, RoutedEventArgs e)
        {
            Term ter = new Term();
            ter.ShowDialog();
        }

        private void Together_Click(object sender, RoutedEventArgs e)
        {
            sad = new SqlDataAdapter("SELECT Kontakte.Name, Kontakte.Vorname, Termine.Datum, Termine.Uhrzeit, Termine.Inhalt FROM Kontakte INNER JOIN" +
                " Termine ON Kontakte.KontaktID = Termine.KontaktID", cn);
            cn.Open();
            SqlCommandBuilder cb = new SqlCommandBuilder(sad);
            dt = new DataTable();
            sad.Fill(dt);
            dg.ItemsSource = dt.DefaultView;
            lb.Content = " 'JOIN' Kkontakte mit terminen";
            cn.Close();
        }

        private void Backup_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowDialog();
            fbd.ShowNewFolderButton = true;
            fbd.Description = "Verzeichnis auswählen";


            FileStream ausgabe;
            StreamWriter schreiben;

            string folder;
            folder = fbd.SelectedPath;

            ausgabe = new FileStream(folder + "\\Sicherung.txt", FileMode.Create, FileAccess.Write);
            schreiben = new StreamWriter(ausgabe);
            schreiben.WriteLine("Sicherung\n");
            if (dg.Items.Count > 1)
            {
                for (int i = 0; i < dg.Items.Count; i++)
                {
                    DataGridRow row = (DataGridRow)dg.ItemContainerGenerator.ContainerFromIndex(i);
                    for (int j = 0; j < dg.Columns.Count; j++)
                    {
                        TextBlock cellContent = dg.Columns[j].GetCellContent(row) as TextBlock;

                        schreiben.Write(cellContent.Text);

                    }
                    schreiben.WriteLine();
                }
                schreiben.Close();
                lb.Content = "Sicherung erstellt";
            }
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            dg.ItemsSource = null;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            sad.Update(dt);
            lb.Content = "Änderungen wurden gespeichert";
            cn.Close();
        }

    }
}