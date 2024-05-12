using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CryptoWallet_3.Model
{
    public class Transaction : INotifyPropertyChanged
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }
        private decimal amount;
        public decimal Amount
        {
            get { return amount; }
            set { amount = value; OnPropertyChanged("Amount"); }
        }
        private DateTime date;
        public DateTime Date
        {
            get { return date; }
            set { date = value; OnPropertyChanged("Date"); }
        }
        private string recipient;
        public string Recipient
        {
            get { return recipient; }
            set { recipient = value; OnPropertyChanged("Recipient"); }
        }
        private Operation operation;
        public Operation Operation
        {
            get { return operation; }
            set { operation = value; OnPropertyChanged("Operation"); }
        }
        private Users users;
        public Users Users
        {
            get { return users; }
            set { users = value; OnPropertyChanged("Users"); }
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
