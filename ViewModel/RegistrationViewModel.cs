using CryptoWallet_3.Model;
using CryptoWallet_3.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Acces = CryptoWallet_3.Model.Acces;

namespace CryptoWallet_3.ViewModel
{
    public class RegistrationViewModel : INotifyPropertyChanged
    {
        WalletDbContext dbContext = new WalletDbContext();
        Random random = new Random();

        public RegistrationViewModel(MainWindowViewModel mainWindowViewModel)
        {
            dbContext = new WalletDbContext();
            this.mainWindowViewModel = mainWindowViewModel;
            if (dbContext.Access.Count() == 0)
                SelectedAccess = new Acces();
            else
                SelectedAccess = _Access.FirstOrDefault();
            if (dbContext.Users.Count() == 0)
                SelectedUsers = new Users();
            else
                SelectedUsers = _Users.FirstOrDefault();
        }

        public ObservableCollection<Users> _Users
        {
            get { return new ObservableCollection<Users>(dbContext.Users.ToList()); }
            set
            {
                OnPropertyChanged("_Users");
            }
        }

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
        private Users selectedUsers;
        public Users SelectedUsers
        {
            get { return selectedUsers; }
            set { selectedUsers = value; OnPropertyChanged("SelectedUsers"); }
        }


        RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                    
                      using (WalletDbContext dbContext = new WalletDbContext())
                      {
                          try
                          {
                              Keys keys = new Keys { PrivateKey = random.Next(100000, 999999).ToString(), PublicKey = random.Next(100000, 999999).ToString() };
                              dbContext.Keys.Add(keys);
                              dbContext.SaveChanges();

                              Acces acces = new Acces { Login = selectedAccess.Login.ToString(), Password = selectedAccess.Password.ToString() };
                              dbContext.Access.Add(acces);
                              dbContext.SaveChanges();

                              Users users = new Users { Name = selectedUsers.Name, Surname = selectedUsers.Surname, Access = acces, Keys = keys, AccessId = acces.Id, KeysId = keys.Id };
                              dbContext.Users.Add(users);

                              dbContext.SaveChanges();

                          }
                          catch (DbEntityValidationException ex)
                          {
                              string message = "";
                              foreach (var e in ex.EntityValidationErrors.ToList())
                                  message += e.ToString();
                          }
                      }
                      MessageBox.Show(selectedUsers.Name+", регистрация прошла успешно. Свои публичный и приватный ключи можете узнать на странице данные пользователя");
                      mainWindowViewModel.Access();
                  }));
            }
        }



        private MainWindowViewModel mainWindowViewModel;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
