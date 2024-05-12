using CryptoWallet_3.Model;
using CryptoWallet_3.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Access = CryptoWallet_3.View.Access;

namespace CryptoWallet_3.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private WalletDbContext dbContext;
        private AccessViewModel accessViewModel;
        private RegistrationViewModel registrationViewModel;
        private CryptoWalletViewModel cryptoWalletViewModel;
        private HistoryViewModel historyViewModel;
        private UserDataViewModel userDataViewModel;
        private SendViewModel sendViewModel;
        private Page send;
        private Page registration;
        private Page cryptoWallet;
        private Page access;
        private Page history;
        private Page userData;

        private Page currentPage;
        public Page CurrentPage
        {
            get { return currentPage; }
            set { currentPage = value; OnPropertyChanged("CurrentPage"); }
        }

        private int member;
        public int Member
        {
            get { return member; }
            set {member = value; }
        }
        //public Acces UserAcces
        //{
        //    get { return accessViewModel.SelectedAccess; }
        //}

        public void Registration()
        {
            CurrentPage = registration;
        }
        public void Send()
        {
            CurrentPage = send;
        }
        public void CryptoWallet()
        {
            CurrentPage = cryptoWallet;
        }
        public void Access()
        {
            CurrentPage = access;
        }
        public void UserData()
        {
            CurrentPage = userData;
        }
        public void History()
        {
            CurrentPage = history;
        }




        private RelayCommand registrationCommand;
        public RelayCommand RregistrationCommamd
        {
            get
            {
                return registrationCommand ??
                  (registrationCommand = new RelayCommand(obj =>
                  {
                      Registration();
                  }));
            }
        }

        private RelayCommand cryptoWalletCommand;
        public RelayCommand CryptoWalletCommamd
        {
            get
            {
                return cryptoWalletCommand ??
                  (cryptoWalletCommand = new RelayCommand(obj =>
                  {
                      CryptoWallet();
                  }));
            }
        }

        private RelayCommand accessCommand;
        public RelayCommand AccessCommamd
        {
            get
            {
                return accessCommand ??
                  (accessCommand = new RelayCommand(obj =>
                  {
                      Access();
                  }));
            }
        }
        private RelayCommand historyCommand;
        public RelayCommand HistoryCommamd
        {
            get
            {
                return historyCommand ??
                  (historyCommand = new RelayCommand(obj =>
                  {
                      History();
                  }));
            }
        }
        private RelayCommand userDataCommand;
        public RelayCommand UserDataCommamd
        {
            get
            {
                return userDataCommand ??
                  (userDataCommand = new RelayCommand(obj =>
                  {
                      UserData();
                  }));
            }
        }

        public MainWindowViewModel()
        {
            dbContext = new WalletDbContext();
            //dbContext.Database.Delete();
            //dbContext.Database.Create();
            LoadPages();
        }

        public void LoadPages()
        {
            sendViewModel = new SendViewModel(this);
            send = new Send() { DataContext = sendViewModel };
            registrationViewModel = new RegistrationViewModel(this);
            registration = new Registration() { DataContext = registrationViewModel };
            cryptoWalletViewModel = new CryptoWalletViewModel(this);
            cryptoWallet = new CryptoWallet() { DataContext = cryptoWalletViewModel };
            accessViewModel = new AccessViewModel(this);
            access = new Access() { DataContext = accessViewModel };
            historyViewModel = new HistoryViewModel(this);
            history = new History() { DataContext = historyViewModel };
            userDataViewModel = new UserDataViewModel(this);
            userData = new UserData() { DataContext = userDataViewModel };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
