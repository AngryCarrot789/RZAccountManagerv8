using System.Windows;
using RZAccountManagerv8.Core.Views;

namespace RZAccountManagerv8.Dialogs.Accounts {
    /// <summary>
    /// Interaction logic for NewAccountWindow.xaml
    /// </summary>
    public partial class NewAccountWindow : Window, IView {
        public NewAccountWindow() {
            this.InitializeComponent();
        }

        public void CloseView(bool result) {
            throw new System.NotImplementedException();
        }
    }
}
