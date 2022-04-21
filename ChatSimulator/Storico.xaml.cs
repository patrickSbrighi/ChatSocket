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

namespace ChatSimulator
{
    /// <summary>
    /// Logica di interazione per Storico.xaml
    /// </summary>
    public partial class Storico : Window
    {
        public Storico(List<string> messaggi)
        {
            InitializeComponent();

            foreach (string s in messaggi)
            {
                lstMess.Items.Add(s);
            }
        }
    }
}
