using RZAccountManagerv8.Core;
using RZAccountManagerv8.Core.Accounting;
using RZAccountManagerv8.Core.Explorers;

namespace RZAccountManagerv8 {
    public class MainViewModel : BaseViewModel {
        public AccountManagerViewModel AccountManager { get; }

        public MainViewModel() {
            this.AccountManager = new AccountManagerViewModel();
            this.AccountManager.CreateAccount("Gmail", "mygmail@gmail.com", "Joe Ronimo", "Sus1011");
            this.AccountManager.CreateAccount("Steam", "lololololol@hotmail.com", "MrYahooUser", "password");
            FolderItemViewModel folder = this.AccountManager.Root.CreateSubFolder(new AccountDirectoryViewModel() {Name = "A"});
            folder.CreateSubFolder(new AccountDirectoryViewModel() {Name = "B"});
            folder.CreateFile(new AccountViewModel() {Name = "A's file 1", Email = "Joe@h.com"});
            folder.CreateFile(new AccountViewModel() {Name = "A's file 2"});
        }
    }
}