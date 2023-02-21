namespace RZAccountManagerv8.Core.Views {
    public abstract class BaseDialogResult {
        /// <summary>
        /// True if the user confirmed the UI action, otherwise false to indicate the user cancelled the action
        /// </summary>
        public bool IsSuccess { get; set; }
    }
}