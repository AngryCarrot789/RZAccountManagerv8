using System.Windows;
using RZAccountManagerv8.Core;
using RZAccountManagerv8.Core.Dialogs.Accounts;
using RZAccountManagerv8.Core.Dialogs.Messages;
using RZAccountManagerv8.Dialogs.Accounts;
using RZAccountManagerv8.Dialogs.Message;

namespace RZAccountManagerv8 {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        private void App_OnStartup(object sender, StartupEventArgs e) {
            IoC.Instance.Register<IMessageDialogService>(new MessageDialogService());
            IoC.Instance.Register<IAccountDialogService>(new AccountDialogService());

            this.MainWindow = new MainWindow();
            this.MainWindow.Show();
        }
    }
}
