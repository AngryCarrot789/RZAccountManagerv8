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

        public ICommand DeleteSelectedAccountCommand { get; }
        public RelayCommandParam<bool> DeleteSelectedAccountSkippableCommand { get; }

        public AccountManagerViewModel() {
            this.Accounts = new ObservableCollection<AccountViewModel>();
            this.NewAccountCommand = new RelayCommand(this.CreateNewAccountAction);
            this.DeleteSelectedAccountCommand = new RelayCommand(this.DeleteSelectedAccountNonSkippableAction);
            this.DeleteSelectedAccountSkippableCommand = new RelayCommandParam<bool>(this.DeleteSelectedAccountSkippableAction);
        }

        public async void CreateNewAccountAction() {
            NewAccountDialogResult result = await IoC.AccountDialogs.ShowNewAccountDialogAsync();
            if (result.Result) {
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

        public void CreateAccount(string name, string email, string username, string password) {
            AccountViewModel vm = new AccountViewModel(this) {
                Name = name,
                Email = email,
                Username = username,
                Password = password
            };

            this.Accounts.Add(vm);
        }

        public void DeleteSelectedAccountNonSkippableAction() {
            this.DeleteSelectedAccountSkippableAction(false);
        }

        public async void DeleteSelectedAccountSkippableAction(bool skip) {
            if (this.SelectedAccount != null) {
                if (skip || await IoC.MessageDialogs.ShowYesNoDialogAsync("Delete this account?", $"Are you sure you want to delete {this.SelectedAccount.Name}?")) {
                    this.DeleteSelectedAccountAction();
                }
            }
        }

        private void DeleteSelectedAccountAction() {
            if (this.SelectedAccount == null) {
                return;
            }

            this.RemoveAccount(this.SelectedAccount);
        }

        public async void DeleteAccount(AccountViewModel account, bool skip) {
            if (skip || await IoC.MessageDialogs.ShowYesNoDialogAsync("Delete this account?", $"Are you sure you want to delete {this.SelectedAccount.Name}?")) {
                this.RemoveAccount(account);
            }
        }

        public void RemoveAccount(AccountViewModel account) {
            int index = this.Accounts.IndexOf(account);
            if (index != -1) {
                bool selectLast = this.SelectedAccount == account;
                this.Accounts.Remove(account);
                if (selectLast && this.Accounts.Count > 0) {
                    if (index > 0)
                        index--;

                    if (index >= 0 && index < this.Accounts.Count) {
                        this.SelectedAccount = this.Accounts[index];
                    }
                }
            }
            else if (account == this.SelectedAccount) {
                this.SelectedAccount = null;
                if (this.Accounts.Count > 0) {
                    this.SelectedAccount = this.Accounts[0];
                }
            }
        }
    }
}