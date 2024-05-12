using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CryptoWallet_3.Model
{
    public class Currencies : INotifyPropertyChanged
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }
        private ICollection<Transaction> transaction;
        public ICollection<Transaction> Transaction
        {
            get { return transaction; }
            set { transaction = value; OnPropertyChanged("Transaction"); }
        }

        private ICollection<UserCurrencies> userCurrencies;
        public ICollection<UserCurrencies> UserCurrencies
        {
            get { return userCurrencies; }
            set { userCurrencies = value; OnPropertyChanged("UserCurrencies"); }
        }

        private ICollection<CurrenciesRate> currenciesRate;
        public ICollection<CurrenciesRate> CurrenciesRate
        {
            get { return currenciesRate; }
            set { currenciesRate = value; OnPropertyChanged("CurrenciesRate"); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
