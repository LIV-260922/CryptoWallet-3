using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CryptoWallet_3.Model
{
    public class CurrenciesRate : INotifyPropertyChanged
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }
        private decimal rate;
        public decimal Rate
        {
            get { return rate; }
            set { rate = value; OnPropertyChanged("Rate"); }
        }
        private Unit unit;
        public Unit Unit
        {
            get { return unit; }
            set { unit = value; OnPropertyChanged("Unit"); }
        }
        private Currencies currencies;
        public Currencies Currencies
        {
            get { return currencies; }
            set { currencies = value; OnPropertyChanged("Currencies"); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
