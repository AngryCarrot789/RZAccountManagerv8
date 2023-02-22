using System.Windows.Input;

namespace RZAccountManagerv8.Core.Explorers {
    public class FileItemViewModel : BaseViewModel {
        private FolderItemViewModel parent;
        private bool isExpanded;
        private object data;

        public virtual FileType Type => FileType.File;

        /// <summary>
        /// This file's parent folder (see <see cref="IsRoot"/> instead of checking if this is null)
        /// </summary>
        public FolderItemViewModel Parent {
            get => this.parent;
            set => this.RaisePropertyChanged(ref this.parent, value);
        }

        /// <summary>
        /// Used to query if this item is expandable. Should only be true for folders
        /// </summary>
        public bool CanExpand => this.CanExpandTreeItem();

        /// <summary>
        /// Whether this item is expanded or not. Should only be true for folders
        /// </summary>
        public bool IsExpanded {
            get => this.isExpanded;
            set => this.RaisePropertyChanged(ref this.isExpanded, value);
        }

        public bool IsRoot => this.Type == FileType.Root || this.Parent == null;

        /// <summary>
        /// Custom data associated with this item
        /// </summary>
        public object Data {
            get => this.data;
            set => this.RaisePropertyChanged(ref this.data, value);
        }

        public ICommand DeleteSelfCommand { get; }

        public FileItemViewModel() {
            this.DeleteSelfCommand = new RelayCommand(DeleteSelfAction);
        }

        public virtual void DeleteSelfAction() {
            this.Parent?.Remove(this);
        }

        public FileItemViewModel(object data) {
            this.Data = data;
        }

        public virtual void OnRemovedFromFolder() {

        }

        public virtual void OnAddedToFolder() {

        }

        protected virtual bool CanExpandTreeItem() {
            return false;
        }

        public T GetData<T>() {
            return (T) this.Data;
        }
    }
}