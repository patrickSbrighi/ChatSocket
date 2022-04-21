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
    /// Logica di interazione per AggiungiContatto.xaml
    /// </summary>
    public partial class AggiungiContatto : Window
    {
        private List<Contatti> _contatti;
        private MainWindow _main;
        private const string PATH = "contatti.txt";
        public AggiungiContatto(List<Contatti> contatti, MainWindow main)
        {
            InitializeComponent();
            _contatti = contatti;
            _main = main;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Contatti nuovo = new Contatti(txtNome.Text, txtCognome.Text, IPAddress.Parse(txtIP.Text), int.Parse(txtPorta.Text));
                bool ipEsistente = false;
                foreach (Contatti c in _contatti)
                {
                    if (c.Ip.ToString() == nuovo.Ip.ToString())
                        ipEsistente = true;
                }

                if (!ipEsistente)
                {
                    _contatti.Add(nuovo);
                    Scrivi();
                    MessageBox.Show("Contatto salvato correttamente");
                    SvuotaInterfaccia();
                }
                else
                    MessageBox.Show("Esiste già un contatto con questo indirizzo ip");
            }
            catch (Exception)
            {
                MessageBox.Show("I parametri inseriti non somo validi");
            }
        }

        private void Scrivi()
        {
            _contatti.Sort();
            using (StreamWriter sw = new StreamWriter(PATH))
            {
                foreach (Contatti c in _contatti)
                {
                    string line = c.Nome + "|" + c.Cognome + "|" + c.Ip.ToString() + "|" + c.Porta.ToString();
                    sw.WriteLine(line);
                }
            }
        }

        private void SvuotaInterfaccia()
        {
            txtCognome.Text = "";
            txtNome.Text = "";
            txtIP.Text = "";
            txtPorta.Text = "";
        }

        private void btnRubrica_Click(object sender, RoutedEventArgs e)
        {
            Rubrica r = new Rubrica(_main);
            r.Show();
            this.Close();
        }
    }
}
