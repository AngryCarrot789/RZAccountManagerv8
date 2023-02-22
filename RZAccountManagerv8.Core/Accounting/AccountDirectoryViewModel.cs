namespace RZAccountManagerv8.Core.Accounting {
    public class AccountDirectoryViewModel : BaseViewModel {
        private string name;
        public string Name {
            get => this.name;
            set => RaisePropertyChanged(ref this.name, value);
        }
    }
}