using System.Collections.ObjectModel;
using System.IO;
using RZAccountManagerV8.Core.Dialogs.Accounts;
using RZAccountManagerV8.Core.Navigator;
using RZAccountManagerV8.Core.Utils;

namespace RZAccountManagerV8.Core.Accounting {
    public class AccountManagerViewModel : BaseViewModel {
        public RelayCommand CreateNewAccountCommand { get; }
        public RelayCommand DeleteSelectedAccountCommand { get; }

        public FileNavigatorViewModel Navigator { get; }

        public ObservableCollection<BaseNavItemViewModel> RightList { get; }

        private bool isDetailViewerOpen;
        public bool IsDetailViewerOpen {
            get => this.isDetailViewerOpen;
            set => this.RaisePropertyChanged(ref this.isDetailViewerOpen, value);
        }

        private bool isSecondAccountListOpen;
        public bool IsSecondAccountListOpen {
            get => this.isSecondAccountListOpen;
            set => this.RaisePropertyChanged(ref this.isSecondAccountListOpen, value);
        }

        public AccountManagerViewModel() {
            this.CreateNewAccountCommand = new RelayCommand(this.CreateNewAccountAction);
            this.DeleteSelectedAccountCommand = new RelayCommand(this.DeleteSelectedAccountAction, () => this.Navigator.SelectedItem != null);
            this.RightList = new ObservableCollection<BaseNavItemViewModel>();
            this.Navigator = new FileNavigatorViewModel(this);
            this.Navigator.OnSelectedItemChanged += (old, item) => {
                this.DeleteSelectedAccountCommand.RaiseCanExecuteChanged();
                if (item != null) {
                    if (item is AccountDirectoryViewModel) {
                        this.IsDetailViewerOpen = false;
                        this.IsSecondAccountListOpen = true;
                    }
                    else { // convenience
                        this.IsSecondAccountListOpen = false;
                        this.IsDetailViewerOpen = true;
                    }
                }
            };
        }

        public async void CreateNewAccountAction() {
            NewAccountDialogResult result = await IoC.AccountDialogs.ShowNewAccountDialogAsync();
            if (result.Result) {
                this.Navigator.CurrentFolder.CreateAccount(result.Name, result.Email, result.Username, result.Password, result.DateOfBirth, "");
            }
        }

        public void DeleteSelectedAccountAction() {
            this.DeleteSelectedAccountAction(false);
        }

        public void DeleteSelectedAccountAction(bool skipUi) {
            this.DeleteAccount(this.Navigator.SelectedItem, skipUi);
        }

        public async void DeleteAccount(BaseNavItemViewModel account, bool skipUi) {
            if (account != null && account.Parent != null) {
                if (account is AccountDirectoryViewModel dir) {
                    if (!skipUi) {
                        string readable = dir.Children.JoinToStringReadable(", ", (x) => x is AccountViewModel acc ? acc.Name : ((AccountDirectoryViewModel) x).Name);
                        if (!await IoC.MessageDialogs.ShowOkCancelDialogAsync("Delete this folder?", $"Are you sure you want to delete {dir.Name}? It contains {dir.Children.Count} items {(readable.Length > 0 ? $"({readable})" : "")}")) {
                            return;
                        }
                    }
                }
                else if (!skipUi && !await IoC.MessageDialogs.ShowOkCancelDialogAsync("Delete this account?", $"Are you sure you want to delete {((AccountViewModel) account).Name}?")) {
                    return;
                }

                this.Navigator.Remove(account);
            }
        }
    }
}