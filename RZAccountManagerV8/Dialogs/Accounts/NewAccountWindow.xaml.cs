using System.Windows;
using System.Windows.Threading;
using RZAccountManagerV8.Core.Views;

namespace RZAccountManagerV8.Dialogs.Accounts {
    /// <summary>
    /// Interaction logic for NewAccountWindow.xaml
    /// </summary>
    public partial class NewAccountWindow : Window, IView {
        public NewAccountWindow() {
            this.InitializeComponent();
        }

        public void CloseView(bool result) {
            this.DialogResult = result;
            this.Close();
        }

        public void SelectAccountNameBox() {
            this.Dispatcher.Invoke(() => {
                this.AccountNameBox.Focus();
                this.AccountNameBox.SelectAll();
            }, DispatcherPriority.Loaded);
        }
    }
}
