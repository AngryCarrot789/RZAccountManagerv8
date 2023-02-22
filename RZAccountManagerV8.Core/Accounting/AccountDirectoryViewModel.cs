using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace RZAccountManagerV8.Core.Accounting {
    public class AccountDirectoryViewModel : BaseNavItemViewModel {
        private string name;
        public string Name {
            get => this.name;
            set => this.RaisePropertyChanged(ref this.name, value);
        }

        public DateTime TimeCreated { get; set; }

        private DateTime timeLastModified;
        public DateTime TimeLastModified {
            get => this.timeLastModified;
            set => this.RaisePropertyChanged(ref this.timeLastModified, value);
        }

        private bool isEmpty;
        public bool IsEmpty {
            get => this.isEmpty;
            set => this.RaisePropertyChanged(ref this.isEmpty, value);
        }

        public ObservableCollection<BaseNavItemViewModel> Children { get; }

        public AccountDirectoryViewModel(AccountManagerViewModel manager) : base(manager) {
            this.Children = new ObservableCollection<BaseNavItemViewModel>();
            this.Children.CollectionChanged += this.ChildrenOnCollectionChanged;
        }

        private void ChildrenOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            if (e.OldItems != null) {
                foreach (BaseNavItemViewModel item in e.OldItems) {
                    item.OnRemovedFromFolder();
                    item.Parent = null;
                }
            }

            if (e.NewItems != null) {
                foreach (BaseNavItemViewModel item in e.NewItems) {
                    item.Parent = this;
                    item.OnAddedToFolder();
                }
            }

            this.IsEmpty = this.Children.Count == 0;
        }

        public void Remove(BaseNavItemViewModel selected) {
            this.Children.Remove(selected);
        }

        public AccountDirectoryViewModel CreateSubFolder(string name) {
            AccountDirectoryViewModel dir = new AccountDirectoryViewModel(this.Manager) {
                Name = name, Parent = this
            };

            this.Children.Add(dir);
            return dir;
        }

        public AccountViewModel CreateAccount(string name, string email, string username, string password, DateTime dateOfBirth, string customInfo) {
            AccountViewModel account = new AccountViewModel(this.Manager) {
                Name = name,
                Email = email,
                Username = username,
                Password = password,
                DateOfBirth = dateOfBirth,
                CustomInfo = customInfo,
                Parent = this
            };

            this.Children.Add(account);
            return account;
        }
    }
}