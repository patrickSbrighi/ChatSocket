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
using System.Net;

namespace ChatSimulator
{
    /// <summary>
    /// Logica di interazione per Rubrica.xaml
    /// </summary>
    public partial class Rubrica : Window
    {
        List<Contatti> persone;
        const string PATH = "contatti.txt";
        MainWindow mainAttuale;
        public Rubrica(MainWindow main)
        {
            InitializeComponent();
            persone = new List<Contatti>();
            LeggiContatti();
            mainAttuale = main;
            ScriviLst();
            btnRemuve.IsEnabled = false;
            btnSeleziona.IsEnabled = false;
        }

        private void btnAggiungi_Click(object sender, RoutedEventArgs e)
        {
            AggiungiContatto add = new AggiungiContatto(persone, mainAttuale);
            add.Show();
            this.Close();
        }

        private void btnSeleziona_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lstContatti.SelectedItem != null)
                {
                    Contatti selected = (Contatti)lstContatti.SelectedItem;
                    MainWindow main = new MainWindow(selected.Ip, selected.Porta, mainAttuale.MessaggiRicevuti, mainAttuale.MessaggiInviati);
                    mainAttuale.Close();
                    main.Show();
                    this.Close();
                }
                else
                    throw new Exception("Selezionare un contatto");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LeggiContatti()
        {
            using (StreamReader st = new StreamReader(PATH))
            {
                while (!st.EndOfStream)
                {
                    string line = st.ReadLine();
                    string[] lineSplit = line.Split('|');
                    Contatti contatto = new Contatti(lineSplit[0], lineSplit[1], IPAddress.Parse(lineSplit[2]), int.Parse(lineSplit[3]));
                    persone.Add(contatto);
                }
            }
        }

        private void ScriviLst()
        {
            lstContatti.Items.Clear();
            foreach (Contatti c in persone)
            {
                lstContatti.Items.Add(c);
            }
        }

        private void btnChiudi_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow(IPAddress.Any, 0, mainAttuale.MessaggiRicevuti, mainAttuale.MessaggiInviati);
            mainAttuale.Close();
            main.Show();
            this.Close();
        }

        private void lstContatti_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lstContatti.SelectedIndex >= 0)
            {
                btnRemuve.IsEnabled = true;
                btnSeleziona.IsEnabled = true;
            }
            else
            {
                btnRemuve.IsEnabled = false;
                btnSeleziona.IsEnabled = false;
            }
        }

        private void btnRemuve_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                persone.RemoveAt(lstContatti.SelectedIndex);
                ScriviLst();
                ScriviFile();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ScriviFile()
        {
            persone.Sort();
            using (StreamWriter sw = new StreamWriter(PATH))
            {
                foreach (Contatti c in persone)
                {
                    string line = c.Nome + "|" + c.Cognome + "|" + c.Ip.ToString() + "|" + c.Porta.ToString();
                    sw.WriteLine(line);
                }
            }
        }
    }
}
