using RZAccountManagerv8.Core;
using RZAccountManagerv8.Core.Accounting;

namespace RZAccountManagerv8 {
    public class MainViewModel : BaseViewModel {
        public AccountManagerViewModel AccountManager { get; }

        public MainViewModel() {
            this.AccountManager = new AccountManagerViewModel();
            this.AccountManager.CreateAccount("Gmail", "mygmail@gmail.com", "Joe Ronimo", "Sus1011");
            this.AccountManager.CreateAccount("Steam", "lololololol@hotmail.com", "MrYahooUser", "password");
        }
    }
}