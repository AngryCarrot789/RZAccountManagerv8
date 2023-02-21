using System.Windows;
using System.Windows.Threading;
using RZAccountManagerv8.Core.Views;

namespace RZAccountManagerv8.Dialogs.Accounts {
    /// <summary>
    /// Interaction logic for NewAccountWindow.xaml
    /// </summary>
    public partial class NewAccountWindow : Window, IView {
        public NewAccountWindow() {
            this.InitializeComponent();
            this.Dispatcher.Invoke(() => {
                this.AccountNameBox.Focus();
                this.AccountNameBox.SelectAll();
            }, DispatcherPriority.Loaded);
        }

        public void CloseView(bool result) {
            this.DialogResult = result;
            this.Close();
        }
    }
}
