using System.Threading.Tasks;
using RZAccountManagerV8.Core.Utils;

namespace RZAccountManagerV8.Core.Dialogs.Messages {
    public interface IMessageDialogService {
        Task ShowDialogAsync(string caption, string message);
        Task ShowDialogAsync(string message);

        /// <summary>
        /// Shows a YES|NO|CANCEL dialog
        /// </summary>
        /// <param name="caption">The dialog's title bar</param>
        /// <param name="message">The dialog's center message/content/body</param>
        /// <returns>True if the user clicked YES/OK, false if the user clicked NO or null if the user clicked cancel</returns>
        Task<Tristate> ShowYesNoCancelDialogAsync(string caption, string message, Tristate defaultOption = Tristate.None);

        Task<bool> ShowYesNoDialogAsync(string caption, string message, bool defaultOption = false);

        Task<bool> ShowOkCancelDialogAsync(string caption, string message, bool defaultOption = false);
    }
}