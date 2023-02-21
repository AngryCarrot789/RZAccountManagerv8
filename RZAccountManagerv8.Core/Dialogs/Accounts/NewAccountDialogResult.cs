using System;
using RZAccountManagerv8.Core.Views;

namespace RZAccountManagerv8.Core.Dialogs.Accounts {
    public class NewAccountDialogResult : BaseDialogResult {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }

        public NewAccountDialogResult() {
        }

        public NewAccountDialogResult(bool result) : base(result) {
        }
    }
}