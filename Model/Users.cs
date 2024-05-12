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
    public class Users : INotifyPropertyChanged
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
        private string surname;
        public string Surname
        {
            get { return surname; }
            set { surname = value; OnPropertyChanged("Surname"); }
        }

        private int accessId;
        public int AccessId
        {
            get { return accessId; }
            set { accessId = value; OnPropertyChanged("AccessId"); }
        }

        private Acces access;
        [Required]
        public Acces Access
        {
            get { return access; }
            set { access = value; OnPropertyChanged("Access"); }
        }

        private int keysId;
        public int KeysId
        {
            get { return keysId; }
            set { keysId = value; OnPropertyChanged("KeysId"); }
        }
        private Keys keys;
        [Required]
        public Keys Keys
        {
            get { return keys; }
            set { keys = value; OnPropertyChanged("Keys"); }
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
