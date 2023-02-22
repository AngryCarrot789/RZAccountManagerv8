using System;
using RZAccountManagerV8.Core;
using RZAccountManagerV8.Core.Accounting;

namespace RZAccountManagerV8 {
    public class MainViewModel : BaseViewModel {
        public AccountManagerViewModel AccountManager { get; }

        public MainViewModel() {
            this.AccountManager = new AccountManagerViewModel();
            this.AccountManager.Navigator.CurrentFolder.CreateAccount("Gmail", "mygmail@gmail.com", "Joe Ronimo", "Sus1011", DateTime.Now - TimeSpan.FromDays(200), "some info here lolol");
            this.AccountManager.Navigator.CurrentFolder.CreateAccount("Gmail", "mygmail@gmail.com", "Joe Ronimo", "Sus1011", DateTime.Now - TimeSpan.FromDays(200), "some info here lolol");
            this.AccountManager.Navigator.CurrentFolder.CreateAccount("Steam", "lololololol@hotmail.com", "MrYahooUser", "password", DateTime.Now - TimeSpan.FromDays(2000), null);
            AccountDirectoryViewModel fodler = this.AccountManager.Navigator.CurrentFolder.CreateSubFolder("My Folder 1");
            fodler.CreateAccount("Gmail inner", "mygmail@gmail.com", "Joe Ronimo", "Sus1011", DateTime.Now - TimeSpan.FromDays(200), "some info here lolol");
            fodler.CreateAccount("Gmail inner 2", "ok@gmail.com", "Joe dd", "Sus1011", DateTime.Now - TimeSpan.FromDays(200), "some info here lolol");
            fodler.CreateSubFolder("Inner folder");

            AccountDirectoryViewModel fodler2 = this.AccountManager.Navigator.CurrentFolder.CreateSubFolder("My Folder 1");
            fodler2.CreateAccount("Gmail inner3", "mygmail@gmail.com", "Joe Ronimo", "Sus1011", DateTime.Now - TimeSpan.FromDays(200), "some info here lolol");
            fodler2.CreateAccount("Gmail inner4", "ok@gmail.com", "Joe dd", "Sus1011", DateTime.Now - TimeSpan.FromDays(200), "some info here lolol");
            fodler2.CreateSubFolder("Inner folder 2");
        }
    }
}