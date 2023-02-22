using System.Windows;
using RZAccountManagerV8.Core;
using RZAccountManagerV8.Core.Dialogs.Accounts;
using RZAccountManagerV8.Core.Dialogs.Messages;
using RZAccountManagerV8.Dialogs.Accounts;
using RZAccountManagerV8.Dialogs.Message;

namespace RZAccountManagerV8 {
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
