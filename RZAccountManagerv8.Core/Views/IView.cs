namespace RZAccountManagerv8.Core.Views {
    /// <summary>
    /// The base class for views, which is typically passed to a ViewModel, in order to access a close function while passing a custom DialogResult
    /// </summary>
    /// <typeparam name="TDialogResult">The type of Dialog Result to pass to the view</typeparam>
    public interface IView {
        void CloseView(bool result);
    }
}