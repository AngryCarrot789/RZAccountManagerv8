using System.Threading.Tasks;
using System.Windows;
using RZAccountManagerV8.Core.Dialogs.Accounts;

namespace RZAccountManagerV8.Dialogs.Accounts {
    public class AccountDialogService : IAccountDialogService {
        public async Task<NewAccountDialogResult> ShowNewAccountDialogAsync() {
            return await Application.Current.Dispatcher.InvokeAsync(this.ShowNewAccountDialog);
        }

        public NewAccountDialogResult ShowNewAccountDialog() {
            NewAccountWindow window = new NewAccountWindow();
            NewAccountViewModel vm = new NewAccountViewModel(window);
            window.DataContext = vm;
            window.SelectAccountNameBox();

            if (window.ShowDialog() != true) {
                return new NewAccountDialogResult(false);
            }

            return new NewAccountDialogResult() {
                Name = vm.Name,
                Email = vm.Email,
                Username = vm.Username,
                Password = vm.Password,
                DateOfBirth = vm.DateOfBirth
            };
        }
    }
}