using System.Windows.Input;

namespace RZAccountManagerV8.Core.Accounting {
    public class BaseNavItemViewModel : BaseViewModel {
        public AccountManagerViewModel Manager { get; }

        private AccountDirectoryViewModel parent;
        public AccountDirectoryViewModel Parent {
            get => this.parent;
            set => this.RaisePropertyChanged(ref this.parent, value);
        }

        public ICommand DeleteSelfCommand { get; }

        public BaseNavItemViewModel(AccountManagerViewModel manager) {
            this.Manager = manager;
            this.DeleteSelfCommand = new RelayCommand(this.DeleteSelfAction);
        }

        private void DeleteSelfAction() {
            this.Manager.DeleteAccount(this, false);
        }

        public virtual void OnRemovedFromFolder() {

        }

        public virtual void OnAddedToFolder() {

        }
    }
}