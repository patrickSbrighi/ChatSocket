using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace ChatSimulator
{
    public class Contatti : IComparable<Contatti>
    {
        public Contatti(string nome, string cognome, IPAddress ip, int porta)
        {
            Nome = nome;
            Cognome = cognome;
            Ip = ip;
            Porta = porta;
        }

        private string _nome;
        public string Nome
        {
            get => _nome;
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                    _nome = value;
                else
                    throw new Exception("Nome non valido");
            }
        }

        private string _cognome;
        public string Cognome
        {
            get => _cognome;
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                    _cognome = value;
                else
                    throw new Exception("Cognome non valido");
            }
        }

        private IPAddress _ip;
        public IPAddress Ip
        {
            get => _ip;
            set
            {
                if (value != null && value.ToString() != IPAddress.Any.ToString())
                    _ip = value;
                else
                    throw new Exception("Ip non valido");
            }
        }

        private int _porta;
        public int Porta
        {
            get => _porta;
            set
            {
                if (value > 0 && value < 65535)
                    _porta = value;
                else
                    throw new Exception("Porta non valida");
            }
        }

        int IComparable<Contatti>.CompareTo(Contatti other)
        {
            if (this.Nome.CompareTo(other.Nome) != 0)
            {
                return this.Nome.CompareTo(other.Nome);
            }
            else
            {
                return this.Cognome.CompareTo(other.Cognome);
            }
        }

        public override string ToString()
        {
            return this.Nome + " " + this.Cognome;
        }
    }
}
