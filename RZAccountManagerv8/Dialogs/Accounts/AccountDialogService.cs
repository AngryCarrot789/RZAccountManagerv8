using System.Threading.Tasks;
using System.Windows;
using RZAccountManagerv8.Core.Dialogs.Accounts;

namespace RZAccountManagerv8.Dialogs.Accounts {
    public class AccountDialogService : IAccountDialogService {
        public async Task<NewAccountDialogResult> ShowNewAccountDialogAsync() {
            await Application.Current.Dispatcher.InvokeAsync(this.ShowNewAccountDialog);
        }

        public NewAccountDialogResult ShowNewAccountDialog() {
            NewAccountWindow window = new NewAccountWindow();
            
        }
    }
}