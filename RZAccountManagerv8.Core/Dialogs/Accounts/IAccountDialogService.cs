using System.Threading.Tasks;

namespace RZAccountManagerV8.Core.Dialogs.Accounts {
    public interface IAccountDialogService {
        Task<NewAccountDialogResult> ShowNewAccountDialogAsync();
        
        NewAccountDialogResult ShowNewAccountDialog();
    }
}