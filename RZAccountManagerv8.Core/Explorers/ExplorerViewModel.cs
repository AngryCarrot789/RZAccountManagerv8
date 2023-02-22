using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RZAccountManagerv8.Core.Explorers {
    public class ExplorerViewModel : BaseViewModel {
        private readonly Dictionary<object, List<FileItemViewModel>> dataToContainerMap;
        private bool isUpdatingSelection;

        private FolderItemViewModel currentFolder;
        public FolderItemViewModel CurrentFolder {
            get => this.currentFolder;
            set => this.RaisePropertyChanged(ref this.currentFolder, value);
        }

        private FileItemViewModel selectedFile;
        public FileItemViewModel SelectedFile {
            get => this.selectedFile;
            set => this.RaisePropertyChanged(ref this.selectedFile, value);
        }

        public FolderItemViewModel Root { get; }

        public FolderItemViewModel SelectedFolderNearestParent {
            get {
                FileItemViewModel selected = this.SelectedFile;
                if (selected == null) {
                    return null;
                }
                else if (!(selected is FolderItemViewModel)) {
                    if ((selected = selected.Parent) == null) {
                        return null;
                    }
                }

                return (FolderItemViewModel) selected;
            }
        }

        public ITreeView TreeView { get; set; }

        public ExplorerViewModel() {
            this.dataToContainerMap = new Dictionary<object, List<FileItemViewModel>>(128); // data -> list<file> in case data is shared between files
            this.Root = new FolderItemViewModel();

        }

        public void OnTreeSelectItem(FileItemViewModel file) {
            if (this.isUpdatingSelection) {
                return;
            }

            this.isUpdatingSelection = true;
            try {
                if (file != null) {
                    if (file is FolderItemViewModel folder) {
                        this.OnClickFolder(folder);
                    }
                    else {
                        this.OnClickFile(file);
                    }
                }
                else {
                    this.CurrentFolder = null;
                    this.SelectedFile = null;
                }
            }
            finally {
                this.isUpdatingSelection = false;
            }
        }

        public void OnTreeDoubleClickItem(FileItemViewModel file) {
            if (file != null && !(file is FolderItemViewModel)) {
                this.OnUseFileItem(file);
            }
        }

        public virtual void OnClickFolder(FolderItemViewModel folder) {
            if (folder != null) {
                this.CurrentFolder = folder;
                if (folder.Children.Count > 0) {
                    this.SelectedFile = folder.Children[0];
                }
            }
            else {
                this.CurrentFolder = null;
                this.SelectedFile = null;
            }
        }

        public virtual void OnClickFile(FileItemViewModel file) {
            if (this.CurrentFolder == null || this.CurrentFolder != file.Parent) {
                this.CurrentFolder = file.Parent;
            }

            this.SelectedFile = file;
        }

        public virtual void OnUseFolderItem(FolderItemViewModel folder) {

        }

        public virtual void OnUseFileItem(FileItemViewModel folder) {

        }

        public void SetDataForContainer(FileItemViewModel file, object data) {
            if (file == null) {
                throw new ArgumentNullException(nameof(file), "File cannot be null");
            }

            List<FileItemViewModel> list;
            if (data == null) {
                if (file.Data != null) {
                    if (this.dataToContainerMap.TryGetValue(file.Data, out list)) {
                        list.Remove(file);
                    }
                }

                file.Data = null;
            }
            else {
                file.Data = data;
                if (!this.dataToContainerMap.TryGetValue(data, out list)) {
                    this.dataToContainerMap[data] = list = new List<FileItemViewModel>();
                }

                if (!list.Contains(file)) {
                    list.Add(file);
                }
            }
        }

        public bool TryGetDataFromContainer<T>(FileItemViewModel file, out T value) where T : class {
            if (file != null && file.Data is T val) {
                value = val;
                return true;
            }
            else {
                value = null;
                return false;
            }
        }

        public T GetDataFromContainer<T>(FileItemViewModel file) where T : class {
            return (T) file.Data;
        }

        public bool TryGetContainerFromData(object data, out FileItemViewModel file) {
            if (data != null && this.dataToContainerMap.TryGetValue(data, out List<FileItemViewModel> list)) {
                foreach (FileItemViewModel nextFile in list) {
                    if (nextFile.Data == data) {
                        file = nextFile;
                        return true;
                    }
                }
            }

            file = null;
            return false;
        }

        public FileItemViewModel GetContainerFromData(object data) {
            foreach (FileItemViewModel file in this.dataToContainerMap[data]) {
                if (file.Data == data) {
                    return file;
                }
            }

            throw new Exception("No such container for data: " + data);
        }
    }
}