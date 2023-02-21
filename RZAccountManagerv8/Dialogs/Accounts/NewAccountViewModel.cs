using System;
using RZAccountManagerv8.Core;
using RZAccountManagerv8.Core.Views;

namespace RZAccountManagerv8.Dialogs.Accounts {
    public class NewAccountViewModel : BaseConfirmableDialogViewModel {
        private string name;
        private string email;
        private string username;
        private string password;
        private DateTime dateOfBirth;

        public string Name {
            get => this.name;
            set => this.RaisePropertyChanged(ref this.name, value);
        }

        public string Email {
            get => this.email;
            set => this.RaisePropertyChanged(ref this.email, value);
        }

        public string Username {
            get => this.username;
            set => this.RaisePropertyChanged(ref this.username, value);
        }

        public string Password {
            get => this.password;
            set => this.RaisePropertyChanged(ref this.password, value);
        }

        public DateTime DateOfBirth {
            get => this.dateOfBirth;
            set => this.RaisePropertyChanged(ref this.dateOfBirth, value);
        }

        public NewAccountViewModel(IView view) : base(view) {

        }

        public override async void ConfirmAction() {
            if (string.IsNullOrEmpty(this.Name)) {
                await IoC.MessageDialogs.ShowDialogAsync("No account name", "You have not given the account a name");
                return;
            }

            base.ConfirmAction();
        }
    }
}