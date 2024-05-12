using CryptoWallet_3.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CryptoWallet_3.ViewModel
{
    public class UserDataViewModel : INotifyPropertyChanged
    {

        WalletDbContext dbContext = new WalletDbContext();

        public ObservableCollection<Acces> _Access
        {
            get { return new ObservableCollection<Acces>(dbContext.Access.ToList()); }
            set
            {
                OnPropertyChanged("_Access");
            }
        }
        public ObservableCollection<Users> _Users
        {
            get { return new ObservableCollection<Users>(dbContext.Users.ToList()); }
            set
            {
                OnPropertyChanged("_Users");
            }
        }
        public ObservableCollection<Keys> _Keys
        {
            get { return new ObservableCollection<Keys>(dbContext.Keys.ToList()); }
            set
            {
                OnPropertyChanged("_Keys");
            }
        }



        private Acces selectedAccess;
        public Acces SelectedAccess
        {
            get { return selectedAccess; }
            set { selectedAccess = value; OnPropertyChanged("SelectedAccess"); }
        }
        private Users selectedUsers;
        public Users SelectedUsers
        {
            get { return selectedUsers; }
            set { selectedUsers = value; OnPropertyChanged("SelectedUsers"); }
        }
        private Keys selectedKeys;
        public Keys SelectedKeys
        {
            get { return selectedKeys; }
            set { selectedKeys = value; OnPropertyChanged("SelectedKeys"); }
        }

        private RelayCommand upDataCommand;
        public RelayCommand UpDataCommand
        {
            get
            {
                return upDataCommand ??
                  (upDataCommand = new RelayCommand(obj =>
                  {
                      using (WalletDbContext db = new WalletDbContext())
                      {
                          var data = (from i in db.Access select i).ToList();
                          foreach (var item in data)
                          {
                              if (item.Login == selectedAccess.Login.ToString() && item.Password == selectedAccess.Password.ToString())
                              {
                                  mainWindowViewModel.CryptoWallet();
                              }
                          }
                      }

                  }));
            }
        }
        private MainWindowViewModel mainWindowViewModel;
        public UserDataViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
            dbContext = new WalletDbContext();
            if (dbContext.Access.Count() == 0)
                SelectedAccess = new Acces();
            else
                SelectedAccess = _Access.FirstOrDefault();
            if (dbContext.Keys.Count() == 0)
                SelectedKeys = new Keys();
            else
                SelectedKeys = _Keys.FirstOrDefault();
            if (dbContext.Users.Count() == 0)
                SelectedUsers = new Users();
            else
                SelectedUsers = _Users.FirstOrDefault();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
