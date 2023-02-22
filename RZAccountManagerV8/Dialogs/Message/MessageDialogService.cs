using System.Threading.Tasks;
using System.Windows;
using RZAccountManagerV8.Core.Dialogs.Messages;
using RZAccountManagerV8.Core.Utils;

namespace RZAccountManagerV8.Dialogs.Message {
    public class MessageDialogService : IMessageDialogService {
        public async Task ShowDialogAsync(string caption, string message) {
            await Application.Current.Dispatcher.InvokeAsync(() => {
                MessageBox.Show(message, caption);
            });
        }

        public async Task ShowDialogAsync(string message) {
            await this.ShowDialogAsync("Information", message);
        }

        public async Task<Tristate> ShowYesNoCancelDialogAsync(string caption, string message, Tristate defaultOption = Tristate.None) {
            return await Application.Current.Dispatcher.InvokeAsync(() => {
                    MessageBoxResult def;
                    switch (defaultOption) {
                        case Tristate.True: def = MessageBoxResult.Yes; break;
                        case Tristate.False: def = MessageBoxResult.No; break;
                        default: def = MessageBoxResult.Cancel; break;
                    }

                    switch (MessageBox.Show(message, caption, MessageBoxButton.YesNoCancel, MessageBoxImage.Information, def)) {
                        case MessageBoxResult.Yes: return Tristate.True;
                        case MessageBoxResult.No: return Tristate.False;
                        default: return Tristate.None;
                    }
                }
            );
        }

        public async Task<bool> ShowYesNoDialogAsync(string caption, string message, bool defaultOption = false) {
            return await Application.Current.Dispatcher.InvokeAsync(() => {
                    MessageBoxResult def = defaultOption ? MessageBoxResult.Yes : MessageBoxResult.No;
                    switch (MessageBox.Show(message, caption, MessageBoxButton.YesNo, MessageBoxImage.Information, def)) {
                        case MessageBoxResult.Yes: return true;
                        default: return false;
                    }
                }
            );
        }

        public async Task<bool> ShowOkCancelDialogAsync(string caption, string message, bool defaultOption = false) {
            return await Application.Current.Dispatcher.InvokeAsync(() => {
                    MessageBoxResult def = defaultOption ? MessageBoxResult.OK : MessageBoxResult.Cancel;
                    switch (MessageBox.Show(message, caption, MessageBoxButton.OKCancel, MessageBoxImage.Information, def)) {
                        case MessageBoxResult.OK: return true;
                        default: return false;
                    }
                }
            );
        }
    }
}