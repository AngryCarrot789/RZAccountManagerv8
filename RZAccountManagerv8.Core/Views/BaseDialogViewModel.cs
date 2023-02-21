namespace RZAccountManagerv8.Core.Views {
    public abstract class BaseDialogViewModel : BaseViewModel {
        public IView View { get; }

        protected BaseDialogViewModel(IView view) {
            this.View = view;
        }
    }
}