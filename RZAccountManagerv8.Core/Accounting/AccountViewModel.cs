using System;

namespace RZAccountManagerv8.Core.Accounting {
    public class AccountViewModel : BaseViewModel {
        private string name;
        private string email;
        private string username;
        private string password;
        private DateTime dateOfBirth;
        private DateTime timeLastModified;
        private string customInfo;

        /// <summary>
        /// The name of this account
        /// </summary>
        public string Name {
            get => this.name;
            set => this.RaisePropertyChanged(ref this.name, value);
        }

        /// <summary>
        /// This account's email address
        /// </summary>
        public string Email {
            get => this.email;
            set => this.RaisePropertyChanged(ref this.email, value);
        }

        /// <summary>
        /// This account's username
        /// </summary>
        public string Username {
            get => this.username;
            set => this.RaisePropertyChanged(ref this.username, value);
        }

        /// <summary>
        /// This account's password
        /// </summary>
        public string Password {
            get => this.password;
            set => this.RaisePropertyChanged(ref this.password, value);
        }

        /// <summary>
        /// The date that this account was created
        /// </summary>
        ///
        public DateTime DateOfBirth {
            get => this.dateOfBirth;
            set => this.RaisePropertyChanged(ref this.dateOfBirth, value);
        }

        public DateTime TimeCreated { get; set; }

        public DateTime TimeLastModified {
            get => this.timeLastModified;
            set => this.RaisePropertyChanged(ref this.timeLastModified, value);
        }

        public string CustomInfo {
            get => this.customInfo;
            set => this.RaisePropertyChanged(ref this.customInfo, value);
        }

        public AccountManagerViewModel Manager { get; }

        public AccountViewModel(AccountManagerViewModel manager) {
            this.Manager = manager;
        }
    }
}