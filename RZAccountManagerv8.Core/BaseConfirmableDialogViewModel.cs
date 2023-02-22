using System.Windows.Input;
using RZAccountManagerV8.Core.Views;

namespace RZAccountManagerV8.Core {
    public abstract class BaseConfirmableDialogViewModel : BaseDialogViewModel {
        public ICommand ConfirmCommand { get; }
        public ICommand CancelCommand { get; }

        protected BaseConfirmableDialogViewModel(IView view) : base(view) {
            this.ConfirmCommand = new RelayCommand(this.ConfirmAction);
            this.CancelCommand = new RelayCommand(this.CancelAction);
        }

        public virtual void ConfirmAction() {
            this.View.CloseView(true);
        }

        public virtual void CancelAction() {
            this.View.CloseView(false);
        }
    }
}