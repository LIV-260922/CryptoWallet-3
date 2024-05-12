using CryptoWallet_3.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CryptoWallet_3.ViewModel
{
    public class HistoryViewModel : INotifyPropertyChanged
    {
        WalletDbContext dbContext = new WalletDbContext();
        private MainWindowViewModel mainWindowViewModel;
        public HistoryViewModel(MainWindowViewModel mainWindowViewModel)
        {
            dbContext = new WalletDbContext();
            this.mainWindowViewModel = mainWindowViewModel;
            if (dbContext.Transaction.Count() == 0)
                SelectedTransaction = new Transaction();
            else
                SelectedTransaction = _Transaction.FirstOrDefault();
        }
        public ObservableCollection<Transaction> _Transaction
        {
            get { return new ObservableCollection<Transaction>(dbContext.Transaction.ToList()); }
            set
            {
                OnPropertyChanged("_Transaction");
            }
        }
        private Transaction selectedTransaction;
        public Transaction SelectedTransaction
        {
            get { return selectedTransaction; }
            set { selectedTransaction = value; OnPropertyChanged("SelectedTransaction"); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
