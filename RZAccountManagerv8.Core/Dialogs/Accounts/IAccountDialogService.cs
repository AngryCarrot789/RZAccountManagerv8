using System.Threading.Tasks;

namespace RZAccountManagerv8.Core.Dialogs.Accounts {
    public interface IAccountDialogService {
        Task<NewAccountDialogResult> ShowNewAccountDialogAsync();
        
        NewAccountDialogResult ShowNewAccountDialog();
    }
}