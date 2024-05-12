using CryptoWallet_3.Model;
using CryptoWallet_3.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Linq;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Data.Entity;
using System.Windows.Data;

namespace CryptoWallet_3.ViewModel
{

    public class CryptoWalletViewModel : INotifyPropertyChanged
    {
        WalletDbContext dbContext = new WalletDbContext();
        private MainWindowViewModel mainWindowViewModel;
        public CryptoWalletViewModel(MainWindowViewModel mainWindowViewModel)
        {
            dbContext = new WalletDbContext();
            this.mainWindowViewModel = mainWindowViewModel;
            if (dbContext.Currencies.Count() == 0)
                SelectedCurrencies = new Currencies();
            else
                SelectedCurrencies = _Currencies.FirstOrDefault();
            if (dbContext.CurrenciesRate.Count() == 0)
                SelectedCurrenciesRate = new CurrenciesRate();
            else
                SelectedCurrenciesRate = _Rate.FirstOrDefault();
            if (dbContext.UserCurrencies.Count() == 0)
                SelectedUserCurrencies = new UserCurrencies();
            else
                SelectedUserCurrencies = _Amount.FirstOrDefault();

        }
        public ObservableCollection<Currencies> _Currencies
        {
            get { return new ObservableCollection<Currencies>(dbContext.Currencies.ToList()); }
            set
            {
                OnPropertyChanged("_Currencies");
            }
        }
        private Currencies selectedCurrencies;
        public Currencies SelectedCurrencies
        {
            get { return selectedCurrencies; }
            set { selectedCurrencies = value; OnPropertyChanged("SelectedCurrencies"); }
        }
        public ObservableCollection<CurrenciesRate> _Rate
        {
            get { return new ObservableCollection<CurrenciesRate>(dbContext.CurrenciesRate.ToList()); }
            set
            {
                OnPropertyChanged("_CurrenciesRate");
            }
        }
        private CurrenciesRate selectedCurrenciesRate;
        public CurrenciesRate SelectedCurrenciesRate
        {
            get { return selectedCurrenciesRate; }
            set { selectedCurrenciesRate = value; OnPropertyChanged("SelectedCurrenciesRate"); }
        }

        public ObservableCollection<UserCurrencies> _Amount
        {
            get { return new ObservableCollection<UserCurrencies>(dbContext.UserCurrencies.Where(s => s.User.Id == mainWindowViewModel.Member).ToList()); }
            set
            {
                OnPropertyChanged("_Amount");
            }
        }
        private UserCurrencies selectedUserCurrencies;
        public UserCurrencies SelectedUserCurrencies
        {
            get { return selectedUserCurrencies; }
            set { selectedUserCurrencies = value; OnPropertyChanged("SelectedUserCurrencies"); }
        }
      
        private RelayCommand sendButtonCommand;
        public RelayCommand SendButtonCommand
        {
            get
            {
                return sendButtonCommand ??
                  (sendButtonCommand = new RelayCommand(obj =>
                  {
                      mainWindowViewModel.Send();

                  }));
            }
        }
        private RelayCommand receiveButtonCommand;
        public RelayCommand ReceiveButtonCommand
        {
            get
            {
                return receiveButtonCommand ??
                  (receiveButtonCommand = new RelayCommand(obj =>
                  {
                      Keys user = new Keys();
                      using (WalletDbContext db = new WalletDbContext())
                      {
                          var data = (from i in db.Keys select i).ToList();
                          foreach (var item in data)
                          {
                              if (item.Id == mainWindowViewModel.Member)
                              {
                                  user = item;
                              }
                          }
                      }
                      MessageBox.Show("Ваш публичный ключ : " +user.PublicKey );
                  }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

}
