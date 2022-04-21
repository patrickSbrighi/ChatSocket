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
using System.Net.Sockets;
using System.Net;
using System.Windows.Threading;
using System.Threading;
using System.IO;

namespace ChatSimulator
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Socket socket = null;
        Thread thread;
        public List<string> MessaggiRicevuti;
        public List<string> MessaggiInviati;
        const string PATH = "contatti.txt";

        public MainWindow()
        {
            InitializeComponent();
            InizializzaioneChat();
            MessaggiRicevuti = new List<string>();
            MessaggiInviati = new List<string>();
        }

        public MainWindow(IPAddress ip, int porta, List<string> messaggiRice, List<String> messaInvi)
        {
            InitializeComponent();
            InizializzaioneChat();

            if(ip.ToString() != IPAddress.Any.ToString())
                txtIP.Text = ip.ToString();
            if(porta != 0)
                txtPorta.Text = porta.ToString();

            MessaggiRicevuti = messaggiRice;
            MessaggiInviati = messaInvi;
            ScriviLst();
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtMessaggio.Text != "")
                {
                    InviaMessaggi(txtIP.Text, txtPorta.Text, txtMessaggio.Text);
                    AggiungiAStorico(txtIP.Text, txtMessaggio.Text); //salvo il messaggio nello storico
                    txtMessaggio.Text = ""; //svuoto l'interfaccia
                }
                else
                {
                    throw new Exception("Scrivere un messaggio");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InviaMessaggi(string ip, string porta, string testo)
        {
            try
            {
                IPAddress remote_address = IPAddress.Parse(ip); //inserisco l'ip a cui inviare il messaggio prendendolo da interfaccia

                IPEndPoint remote_endpoint = new IPEndPoint(remote_address, int.Parse(porta));//creo un nuovo endpoint prendendo la porta dalla text dell'interfaccia

                byte[] messaggio = Encoding.UTF8.GetBytes(testo); //trasformo il messaggio in un array di byte, perchè andrò a trasferire dei byte

                socket.SendTo(messaggio, remote_endpoint); //invia il messaggio al remote endpoint

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AggiornamentoRicevuti()
        {
            while (true)
            {
                int nBytes = 0; //numero di byte ricevuti


                if ((nBytes = socket.Available) > 0)//controllo quanti sono i byte disponibili, se maggiori di 0 vuol dire che è arrivato qualcosa
                {
                    byte[] buffer = new byte[nBytes]; //creo un vettore lungo n byte

                    EndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0); //do valori di default al nuovo endpoint perchè ancora sono sconosciuti

                    nBytes = socket.ReceiveFrom(buffer, ref remoteEndPoint);//riceve il datagram e si salva l'ip che manda, ritorna n byte ricevuti, nel buffer mette i datagram e nel endpoint l'ip

                    string from = RiconoscimentoIP(((IPEndPoint)remoteEndPoint).Address.ToString());//rendo stringa l ip del mandante e controllo che non sia in rubrica

                    string messaggio = Encoding.UTF8.GetString(buffer, 0, nBytes); //rendo stringa il messaggio, prendendolo nel buffer partendo da 0 fino alla fine (nbytes)

                    //scrivo sul listbox
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        lstMesaggi.Items.Add(from + ": " + messaggio);
                    }));
                }

                Thread.Sleep(250);
            }

        }

        private string RiconoscimentoIP(string ip)
        {
            List<Contatti> persone = LeggiFile();

            foreach (Contatti p in persone)
            {
                if (p.Ip.ToString() == ip)
                    return p.ToString();
            }

            return ip;
        }

        private List<Contatti> LeggiFile()
        {
            List<Contatti> toRet = new List<Contatti>();
            using (StreamReader st = new StreamReader(PATH))
            {
                while (!st.EndOfStream)
                {
                    string line = st.ReadLine();
                    string[] lineSplit = line.Split('|');
                    Contatti contatto = new Contatti(lineSplit[0], lineSplit[1], IPAddress.Parse(lineSplit[2]), int.Parse(lineSplit[3]));
                    toRet.Add(contatto);
                }
            }

            return toRet;
        }
        private void btnRubi_Click(object sender, RoutedEventArgs e)
        {
            Rubrica r = new Rubrica(this);
            btnRubi.IsEnabled = false;
            ListaMessaggi();
            thread.Abort();
            socket.Close();
            r.Show();
        }

        private void InizializzaioneChat()
        {
            try
            {
                //adress family è un enumeratore che specifica quali sono le modalià di lavoro (interNework = ipv4), anche sockettype (dgram = udp)
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                IPAddress local_adress = IPAddress.Any; //inizialmente non metto nessun indirizzo ip nel indirizzo ip locale
                IPEndPoint local_endpoint = new IPEndPoint(local_adress.MapToIPv4(), 64000); //converto il local adress come ipv4 (non più string) e poi dico la porta

                socket.Bind(local_endpoint); //collego la socket all'endpoint, qui prendo l'indirizzo ip vero e proprio

                thread = new Thread(new ThreadStart(AggiornamentoRicevuti)); //crea un thread per controllare se è arrivato un messaggio
                thread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ScriviLst()
        {
            foreach (string s in MessaggiRicevuti)
            {
                lstMesaggi.Items.Add(s);
            }
        }

        private void ListaMessaggi()
        {
            MessaggiRicevuti.Clear();
            foreach (string s in lstMesaggi.Items)
            {
                MessaggiRicevuti.Add(s);
            }
        }

        private void AggiungiAStorico(string ip, string mess)
        {
            string persona = RiconoscimentoIP(ip);
            MessaggiInviati.Add(persona + ": " + mess);
        }

        private void btnStorico_Click(object sender, RoutedEventArgs e)
        {
            Storico stori = new Storico(MessaggiInviati);
            stori.Show();
        }

        private void btnTutti_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtMessaggio.Text != "")
                {
                    List<Contatti> persone = LeggiFile();

                    foreach (Contatti c in persone)
                    {
                        InviaMessaggi(c.Ip.ToString(), c.Porta.ToString(), txtMessaggio.Text);
                    }

                    AggiungiAStorico("All", txtMessaggio.Text);
                    txtMessaggio.Text = "";
                    MessageBox.Show("Il messaggio è stato inoltrato a tutta la rubrica");
                }
                else
                {
                    throw new Exception("Scrivere un messaggio");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
