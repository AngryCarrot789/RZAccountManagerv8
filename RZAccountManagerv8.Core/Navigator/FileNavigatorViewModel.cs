using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using RZAccountManagerV8.Core.Accounting;
using RZAccountManagerV8.Core.Utils;

namespace RZAccountManagerV8.Core.Navigator {
    public class FileNavigatorViewModel : BaseViewModel {
        public delegate void SelectedItemChanged(BaseNavItemViewModel oldItem, BaseNavItemViewModel newItem);
        public delegate void CurrentFolderChanged(AccountDirectoryViewModel oldDir, AccountDirectoryViewModel newDir);

        public event SelectedItemChanged OnSelectedItemChanged;
        public event SelectedItemChanged OnCurrentFolderChanged;

        public AccountDirectoryViewModel Root { get; }

        private AccountDirectoryViewModel currentFolder;
        public AccountDirectoryViewModel CurrentFolder {
            get => this.currentFolder;
            set {
                AccountDirectoryViewModel prev = this.currentFolder;
                AccountDirectoryViewModel targ = value ?? this.Root;
                if (prev == targ) {
                    return;
                }

                this.RaisePropertyChanged(ref this.currentFolder, targ);
                this.OnCurrentFolderChanged?.Invoke(prev, targ);
            }
        }

        private BaseNavItemViewModel selectedItem;
        public BaseNavItemViewModel SelectedItem {
            get => this.selectedItem;
            set {
                if (value == this.Root) {
                    throw new InvalidOperationException("Cannot set selected item to the root folder");
                }
                else if (value == this.selectedItem) {
                    return;
                }

                BaseNavItemViewModel prev = this.selectedItem;
                this.RaisePropertyChanged(ref this.selectedItem, value);
                this.OnSelectedItemChanged?.Invoke(prev, value);
            }
        }

        public List<AccountDirectoryViewModel> Backward { get; }
        public List<AccountDirectoryViewModel> Forward { get; }

        public ObservableCollection<AccountDirectoryViewModel> PathElements { get; }

        public RelayCommand GoBackwardCommand { get; }
        public RelayCommand GoForwardCommand { get; }

        public AccountManagerViewModel Manager { get; set; }

        public RelayCommandParam<BaseNavItemViewModel> NavigateCommand { get; }

        public FileNavigatorViewModel(AccountManagerViewModel manager) {
            this.Manager = manager;
            this.Root = new AccountDirectoryViewModel(manager);
            this.CurrentFolder = this.Root;
            this.Backward = new List<AccountDirectoryViewModel>();
            this.Forward = new List<AccountDirectoryViewModel>();
            this.PathElements = new ObservableCollection<AccountDirectoryViewModel>();
            this.GoBackwardCommand = new RelayCommand(this.OnGoBackAction, this.CanGoBack);
            this.GoForwardCommand = new RelayCommand(this.OnGoForwardAction, this.CanGoForward);
            this.NavigateCommand = new RelayCommandParam<BaseNavItemViewModel>(this.NavigateAction);
        }

        public void NavigateAction(BaseNavItemViewModel obj) {
            if (obj is AccountDirectoryViewModel dir) {
                this.Forward.Clear();
                this.Backward.Add(this.CurrentFolder);
                this.NavigateTo(dir);
            }
        }

        private bool CanGoBack() {
            return this.Backward.Count > 0;
        }

        private bool CanGoForward() {
            return this.Forward.Count > 0;
        }

        private void NavigateTo(AccountDirectoryViewModel accountDirectory) {
            if (accountDirectory == null) {
                this.CurrentFolder = this.Root;
                this.SelectedItem = null;
                this.PathElements.Clear();
            }
            else {
                this.PathElements.Clear();
                this.CurrentFolder = accountDirectory;
                this.SelectedItem = accountDirectory.Children.Count > 0 ? accountDirectory.Children[0] : null;
            }

            this.GoBackwardCommand.RaiseCanExecuteChanged();
            this.GoForwardCommand.RaiseCanExecuteChanged();
            this.CalculatePathElements();
        }

        public bool TryPopBack(out AccountDirectoryViewModel target) {
            if (this.Backward.Count < 1) {
                target = null;
                return false;
            }

            target = this.Backward[this.Backward.Count - 1];
            this.Backward.RemoveAt(this.Backward.Count - 1);
            return true;
        }

        public bool TryPopForward(out AccountDirectoryViewModel target) {
            if (this.Forward.Count < 1) {
                target = null;
                return false;
            }

            target = this.Forward[this.Forward.Count - 1];
            this.Forward.RemoveAt(this.Forward.Count - 1);
            return true;
        }

        public void OnGoBackAction() {
            if (this.TryPopBack(out AccountDirectoryViewModel dir)) {
                this.Forward.Add(this.CurrentFolder);
                this.NavigateTo(dir);
            }
        }

        public void OnGoForwardAction() {
            if (this.TryPopForward(out AccountDirectoryViewModel dir)) {
                this.Backward.Add(this.CurrentFolder);
                this.NavigateTo(dir);
            }
        }

        public void CalculatePathElements() {
            List<AccountDirectoryViewModel> dirs = new List<AccountDirectoryViewModel>();
            for (AccountDirectoryViewModel dir = this.CurrentFolder; dir != this.Root; dir = dir.Parent) {
                dirs.Add(dir);
            }

            dirs.Reverse();
            this.PathElements.Clear();
            this.PathElements.AddAll(dirs);
        }

        public void Remove(BaseNavItemViewModel account) {
            if (account == null) {
                return;
            }

            if (account is AccountDirectoryViewModel dir) {
                this.Backward.Remove(dir);
                this.Forward.Remove(dir);
                AccountDirectoryViewModel target = account.Parent;
                account.Parent.Remove(account);
                if (target == null) {
                    target = this.Root;
                }

                this.NavigateTo(target);
            }
            else {
                account.Parent.Remove(account);
            }
        }
    }
}