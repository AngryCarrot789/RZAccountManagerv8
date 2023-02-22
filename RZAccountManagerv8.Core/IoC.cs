using RZAccountManagerV8.Core.Dialogs.Accounts;
using RZAccountManagerV8.Core.Dialogs.Messages;

namespace RZAccountManagerV8.Core {
    public static class IoC {
        public static SimpleIoC Instance { get; } = new SimpleIoC();
        public static IAccountDialogService AccountDialogs => Instance.Provide<IAccountDialogService>();
        public static IMessageDialogService MessageDialogs => Instance.Provide<IMessageDialogService>();
    }
}
