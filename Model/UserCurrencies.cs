using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
namespace CryptoWallet_3.Model
{
    public class UserCurrencies : INotifyPropertyChanged
    {

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }

        private Currencies currency;
        public Currencies Currency
        {
            get { return currency; }
            set { currency = value; OnPropertyChanged("Currency"); }
        }

        private Users user;
        public Users User
        {
            get { return user; }
            set { user = value; OnPropertyChanged("User"); }
        }

        private decimal amountCurrencies;
        public decimal AmountCurrencies
        {
            get { return amountCurrencies; }
            set { amountCurrencies = value; OnPropertyChanged("AmountCurrencies"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

}
