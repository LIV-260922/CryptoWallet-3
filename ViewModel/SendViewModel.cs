using CryptoWallet_3.Model;
using CryptoWallet_3.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.Entity;
using System.Security.Cryptography;


namespace CryptoWallet_3.ViewModel
{
    public class SendViewModel : INotifyPropertyChanged
    {
        WalletDbContext dbContext = new WalletDbContext();

        private MainWindowViewModel mainWindowViewModel;
        public SendViewModel(MainWindowViewModel mainWindowViewModel)
        {
            dbContext = new WalletDbContext();
            this.mainWindowViewModel = mainWindowViewModel;
            if (dbContext.Keys.Count() == 0)
                SelectedKeys = new Keys();
            else
                SelectedKeys = _Keys.FirstOrDefault();
            if (dbContext.Currencies.Count() == 0)
                SelectedCurrencies = new Currencies();
            else
                SelectedCurrencies = _Currencies.FirstOrDefault();
            if (dbContext.UserCurrencies.Count() == 0)
                SelectedUserCurrencies = new UserCurrencies();
            else
                SelectedUserCurrencies = _UserCurrencies.FirstOrDefault();

        }

        public ObservableCollection<Keys> _Keys
        {
            get { return new ObservableCollection<Keys>(dbContext.Keys.ToList()); }
            set
            {
                OnPropertyChanged("_Keys");
            }
        }
        public ObservableCollection<UserCurrencies> _UserCurrencies
        {
            get { return new ObservableCollection<UserCurrencies>(dbContext.UserCurrencies.ToList()); }
            set
            {
                OnPropertyChanged("_UserCurrencies");
            }
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
        private UserCurrencies selectedUserCurrencies;
        public UserCurrencies SelectedUserCurrencies
        {
            get { return selectedUserCurrencies; }
            set { selectedUserCurrencies = value; OnPropertyChanged("SelectedUserCurrencies"); }
        }
        private Keys selectedKeys;
        public Keys SelectedKeys
        {
            get { return selectedKeys; }
            set { selectedKeys = value; OnPropertyChanged("SelectedKeys"); }
        }
        RelayCommand sendCommand;
        public RelayCommand SendCommand
        {
            get
            {
                return sendCommand ??
                  (sendCommand = new RelayCommand(obj =>
                  {
                      using (WalletDbContext dbContext = new WalletDbContext())
                      {
                          Users sendUser = new Users();
                          sendUser = (from i in dbContext.Users.Include("Access").Include("Keys") where i.Access.Id == mainWindowViewModel.Member select i).FirstOrDefault();
                          if (sendUser.Keys.PrivateKey == selectedKeys.PrivateKey)
                          {
                              Users receiveUser = new Users();
                              var dataUser = (from i in dbContext.Users.Include("Access").Include("Keys") select i).ToList();
                              foreach (var item in dataUser)
                              {
                                  if (item.Keys.PublicKey.ToString() == selectedKeys.PublicKey.ToString())
                                  {
                                      receiveUser = item;
                                  }
                              }
                              Currencies currencies = new Currencies();
                              var dataCurrencies = (from i in dbContext.Currencies select i).ToList();
                              foreach (var item in dataCurrencies)
                              {
                                  if (item.Name == selectedCurrencies.Name.ToString())
                                  {
                                      currencies = item;
                                  }
                              }
                              decimal amount = selectedUserCurrencies.AmountCurrencies;
                              Operation op = new Operation();
                              var dataOperation = (from i in dbContext.Operation select i).ToList();
                              foreach (var item in dataOperation)
                              {
                                  if (item.Id==2)
                                  {
                                      op = item;
                                  }
                              }
                              var dataSend = (from i in dbContext.UserCurrencies select i).ToList();
                              foreach (var item in dataSend)
                              {
                                  if (item.User == sendUser && item.Currency == currencies)
                                  {
                                      item.AmountCurrencies -= amount;
                                      dbContext.SaveChanges();
                                  }
                              }
                              var dataReceive = (from i in dbContext.UserCurrencies select i).ToList();
                              int count = 0;
                              foreach (var item in dataReceive)
                              {
                                  if (item.User == receiveUser && item.Currency == currencies)
                                  {
                                          item.AmountCurrencies += amount;
                                          dbContext.SaveChanges();
                                      count++;
                                      break;
                                  }
                              }
                              if (count>0)
                              {
                                  UserCurrencies uC = new UserCurrencies { User = receiveUser, Currency = currencies, AmountCurrencies = amount };
                                  dbContext.UserCurrencies.Add(uC);
                                  dbContext.SaveChanges();
                                  count = 0;
                              }                            
                              Transaction sendTransaction = new Transaction { Amount = amount,Operation=op, Date = DateTime.Now, Recipient = receiveUser.Name+" "+receiveUser.Surname,Users = sendUser, Currencies = currencies};
                              dbContext.Transaction.Add(sendTransaction);
                              dbContext.SaveChanges();
                          }
                          MessageBox.Show("ssssv");
                      }
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
