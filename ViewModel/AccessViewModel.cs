using CryptoWallet_3.Model;
using CryptoWallet_3.View;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Linq;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Acces = CryptoWallet_3.Model.Acces;

namespace CryptoWallet_3.ViewModel
{
    public class AccessViewModel : INotifyPropertyChanged
    {
        WalletDbContext dbContext = new WalletDbContext();
        private bool isAccess = false;

        public ObservableCollection<Acces> _Access
        {
            get { return new ObservableCollection<Acces>(dbContext.Access.ToList()); }
            set
            {
                OnPropertyChanged("_Access");
            }
        }

        private Acces selectedAccess;
        public Acces SelectedAccess
        {
            get { return selectedAccess; }
            set { selectedAccess = value; OnPropertyChanged("SelectedAccess"); }
        }

        private RelayCommand entryCommand;
        public RelayCommand EntryCommand
        {
            get
            {
                return entryCommand ??
                  (entryCommand = new RelayCommand(obj =>
                  {
                      using (WalletDbContext db = new WalletDbContext())
                      {
                          var data = (from i in db.Access select i).ToList();
                          foreach (var item in data)
                          {
                              if (item.Login == selectedAccess.Login.ToString() && item.Password == selectedAccess.Password.ToString())
                              {
                                  isAccess = true;
                                  break;
                              }
                          }
                          if (isAccess)
                          {
                              mainWindowViewModel.Member = selectedAccess.Id;
                              mainWindowViewModel.CryptoWallet();
                          }

                          else
                              MessageBox.Show("Неправильный логин или пароль!", "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Error);

                          isAccess = false;
                      }

                  }));
            }
        }


        private MainWindowViewModel mainWindowViewModel;
        public AccessViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
            dbContext = new WalletDbContext();
            if (dbContext.Access.Count() == 0)
                SelectedAccess = new Acces();
            else
                SelectedAccess = _Access.FirstOrDefault();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
