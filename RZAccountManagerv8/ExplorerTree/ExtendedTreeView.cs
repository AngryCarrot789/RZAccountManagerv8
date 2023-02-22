using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using RZAccountManagerv8.Core.Explorers;

namespace RZAccountManagerv8.ExplorerTree {
    public class ExtendedTreeView : TreeView, ITreeView {
        public static readonly DependencyProperty ExplorerProperty =
            DependencyProperty.Register(
                "Explorer",
                typeof(ExplorerViewModel),
                typeof(ExtendedTreeView),
                new PropertyMetadata(null, PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((ExplorerViewModel) e.NewValue).TreeView = (ExtendedTreeView) d;
        }

        public ExplorerViewModel Explorer {
            get => (ExplorerViewModel) this.GetValue(ExplorerProperty);
            set => this.SetValue(ExplorerProperty, value);
        }

        public ExtendedTreeView() {

        }

        protected override void OnSelectedItemChanged(RoutedPropertyChangedEventArgs<object> e) {
            base.OnSelectedItemChanged(e);
            if (e.Handled) {
                return;
            }

            if (e.NewValue is FileItemViewModel file) {
                this.Explorer.OnTreeSelectItem(file);
            }
        }

        protected override void OnMouseDoubleClick(MouseButtonEventArgs e) {
            base.OnMouseDoubleClick(e);
            if (e.Handled) {
                return;
            }

            object selected = base.SelectedItem;
            if (selected is FileItemViewModel file) {
                if (this.ItemContainerGenerator.ContainerFromItem(selected) is UIElement ui && ui.IsMouseOver) {
                    this.Explorer.OnTreeDoubleClickItem(file);
                }
            }
        }

        object ITreeView.SelectedItem {
            get => this.SelectedItem;
            set {
                if (value is FolderItemViewModel folder) {
                    for (FolderItemViewModel parent = folder.Parent; parent != null; parent = parent.Parent) {
                        if (!parent.IsExpanded && parent.CanExpand) {
                            parent.IsExpanded = true;
                        }
                    }

                    folder.IsExpanded = true;
                }
            }
        }
    }
}
