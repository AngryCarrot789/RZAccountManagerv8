using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using RZAccountManagerv8.Core.Dialogs.Accounts;

namespace RZAccountManagerv8.Core.Accounting {
    public class AccountManagerViewModel : BaseViewModel {
        public ObservableCollection<AccountViewModel> Accounts { get; }

        private AccountViewModel selectedAccount;
        public AccountViewModel SelectedAccount {
            get => this.selectedAccount;
            set => this.RaisePropertyChanged(ref this.selectedAccount, value);
        }

        public ICommand NewAccountCommand { get; }

        public AccountManagerViewModel() {
            this.Accounts = new ObservableCollection<AccountViewModel>();
            this.NewAccountCommand = new RelayCommand(this.CreateNewAccountAction);
        }

        public async void CreateNewAccountAction() {
            NewAccountDialogResult result = await IoC.AccountDialogs.ShowNewAccountDialogAsync();
            if (result.IsSuccess) {
                AccountViewModel account = new AccountViewModel(this) {
                    Name = result.Name,
                    Email = result.Email,
                    Username = result.Username,
                    Password = result.Password,
                    DateOfBirth = result.DateOfBirth
                };

                this.Accounts.Add(account);
            }
        }

        public AccountViewModel GetAccountByName(string name) {
            return this.Accounts.FirstOrDefault(x => x.Name == name);
        }
    }
}