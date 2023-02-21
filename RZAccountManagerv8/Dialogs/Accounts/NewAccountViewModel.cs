using RZAccountManagerv8.Core;
using RZAccountManagerv8.Core.Accounting;

namespace RZAccountManagerv8.Dialogs.Accounts {
    public class NewAccountViewModel : BaseViewModel {
        private string name;
        private string email;
        private string username;
        private string password;

        public string Name {
            get => this.name;
            set => RaisePropertyChanged(ref this.name, value);
        }

        public string Email {
            get => this.email;
            set => RaisePropertyChanged(ref this.email, value);
        }

        public string Username {
            get => this.username;
            set => RaisePropertyChanged(ref this.username, value);
        }

        public string Password {
            get => this.password;
            set => RaisePropertyChanged(ref this.password, value);
        }
    }
}