using System.Windows.Input;
using RZAccountManagerv8.Core.Dialogs.Accounts;
using RZAccountManagerv8.Core.Explorers;

namespace RZAccountManagerv8.Core.Accounting {
    public class AccountManagerViewModel : ExplorerViewModel {
        public ICommand CreateNewAccountCommand { get; }
        public ICommand DeleteSelectedAccountCommand { get; }

        public AccountManagerViewModel() {
            this.CreateNewAccountCommand = new RelayCommand(this.CreateNewAccountAction);
            this.DeleteSelectedAccountCommand = new RelayCommand(this.DeleteSelectedAccountAction);
        }

        public async void CreateNewAccountAction() {
            FolderItemViewModel folder = this.SelectedFolderNearestParent;
            if (folder == null) {
                return;
            }

            NewAccountDialogResult result = await IoC.AccountDialogs.ShowNewAccountDialogAsync();
            if (result.Result) {
                AccountViewModel account = new AccountViewModel() {
                    Name = result.Name,
                    Email = result.Email,
                    Username = result.Username,
                    Password = result.Password,
                    DateOfBirth = result.DateOfBirth
                };

                folder.CreateFile(account);
            }
        }

        public void CreateAccount(string name, string email, string username, string password) {
            AccountViewModel vm = new AccountViewModel() {
                Name = name,
                Email = email,
                Username = username,
                Password = password
            };

            this.Root.CreateFile(vm);
        }

        public async void DeleteSelectedAccountAction() {
            FileItemViewModel selected = this.SelectedFile;
            if (selected != null && selected.Parent != null && this.TryGetDataFromContainer(selected, out AccountViewModel account)) {
                if (await IoC.MessageDialogs.ShowOkCancelDialogAsync("Delete this account?", $"Are you sure you want to delete {account.Name}?")) {
                    selected.Parent.Remove(selected);
                }
            }
        }

        public async void DeleteAccount(AccountViewModel account, bool skip) {
            FileItemViewModel item = this.GetFileForAccount(account);
            if (item != null && item.Parent != null) {
                if (skip || await IoC.MessageDialogs.ShowOkCancelDialogAsync("Delete this account?", $"Are you sure you want to delete {account.Name}?")) {
                    item.Parent.Remove(item);
                }
            }
        }

        public FileItemViewModel GetFileForAccount(AccountViewModel account) {
            return this.Root.GetFileForDataObject(account);
        }
    }
}