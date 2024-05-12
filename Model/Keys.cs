using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CryptoWallet_3.Model
{
    public class Keys
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }
       
        private string privateKey;
        public string PrivateKey
        {
            get { return privateKey; }
            set { privateKey = value; OnPropertyChanged("PrivateKey"); }
        }
        private string publicKey;
        public string PublicKey
        {
            get { return publicKey; }
            set { publicKey = value; OnPropertyChanged("PublicKey"); }
        }

        private Users users;
        public Users Users
        {
            get { return users; }
            set { users = value; OnPropertyChanged("Users"); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
