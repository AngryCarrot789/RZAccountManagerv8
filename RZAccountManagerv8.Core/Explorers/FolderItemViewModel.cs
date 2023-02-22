using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data.SqlTypes;
using RZAccountManagerv8.Core.Accounting;

namespace RZAccountManagerv8.Core.Explorers {
    public class FolderItemViewModel : FileItemViewModel {
        public override FileType Type { get; }

        public ObservableCollection<FileItemViewModel> Children { get; }

        private bool isEmpty;
        public bool IsEmpty {
            get => this.isEmpty;
            set => this.RaisePropertyChanged(ref this.isEmpty, value);
        }

        public FolderItemViewModel() : this(FileType.Folder) {

        }

        public FolderItemViewModel(object data) : this(data, FileType.Folder) {

        }

        public FolderItemViewModel(object data, FileType type) : this(type) {
            this.Data = data;
        }

        public FolderItemViewModel(FileType type) {
            this.Type = type;
            this.Children = new ObservableCollection<FileItemViewModel>();
            this.Children.CollectionChanged += this.OnChildrenChanged;
            this.isEmpty = true;
        }

        protected virtual void OnChildrenChanged(object sender, NotifyCollectionChangedEventArgs e) {
            if (e.OldItems != null) {
                foreach (FileItemViewModel item in e.OldItems) {
                    item.OnRemovedFromFolder();
                    item.Parent = null;
                }
            }

            if (e.NewItems != null) {
                foreach (FileItemViewModel item in e.NewItems) {
                    item.Parent = this;
                    item.OnAddedToFolder();
                }
            }

            this.IsEmpty = this.Children.Count == 0;
        }

        protected override bool CanExpandTreeItem() {
            return this.Children.Count > 0;
        }

        public void Remove(FileItemViewModel item) {
            this.Children.Remove(item);
        }

        public FileItemViewModel GetFileForDataObject(object data) {
            if (this.Data == data) {
                return this;
            }

            foreach (FileItemViewModel file in this.Children) {
                if (file is FolderItemViewModel) {
                    return ((FolderItemViewModel) file).GetFileForDataObject(data);
                }
                else if (file.Data == data) {
                    return file;
                }
            }

            return null;
        }

        public FolderItemViewModel CreateSubFolder() {
            FolderItemViewModel item = new FolderItemViewModel(FileType.Folder);
            this.Children.Add(item);
            return item;
        }

        public FolderItemViewModel CreateSubFolder(object data) {
            FolderItemViewModel item = new FolderItemViewModel(data, FileType.Folder);
            this.Children.Add(item);
            return item;
        }

        public FileItemViewModel CreateFile() {
            FileItemViewModel item = new FileItemViewModel();
            this.Children.Add(item);
            return item;
        }

        public FileItemViewModel CreateFile(object data) {
            FileItemViewModel item = new FileItemViewModel(data);
            this.Children.Add(item);
            return item;
        }
    }
}