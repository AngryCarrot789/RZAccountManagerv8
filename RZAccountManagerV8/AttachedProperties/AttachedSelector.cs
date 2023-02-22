using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace RZAccountManagerV8.AttachedProperties {
    public static class AttachedSelector {
        public static readonly DependencyProperty DoubleClickCommandProperty =
            DependencyProperty.RegisterAttached(
                "DoubleClickCommand",
                typeof(ICommand),
                typeof(AttachedSelector),
                new FrameworkPropertyMetadata(null, PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if (d is Selector selector) {
                selector.PreviewMouseDoubleClick -= SelectorOnPreviewMouseDoubleClick;
                if (e.NewValue != null) {
                    selector.PreviewMouseDoubleClick += SelectorOnPreviewMouseDoubleClick;
                }
            }
        }

        public static void SetDoubleClickCommand(DependencyObject element, ICommand value) {
            element.SetValue(DoubleClickCommandProperty, value);
        }

        public static ICommand GetDoubleClickCommand(DependencyObject element) {
            return (ICommand) element.GetValue(DoubleClickCommandProperty);
        }

        private static void SelectorOnPreviewMouseDoubleClick(object sender, MouseButtonEventArgs e) {
            if (sender is Selector selector) {
                ICommand command = GetDoubleClickCommand(selector);
                if (command == null) {
                    return;
                }

                object selected = selector.SelectedItem;
                FrameworkElement element;
                if (selected == null) {
                    return;
                }

                if (selected is FrameworkElement) {
                    element = (FrameworkElement) selected;
                }
                else if (selector.ItemContainerGenerator.ContainerFromItem(selected) is FrameworkElement elem) {
                    element = elem;
                }
                else {
                    return;
                }

                if (element.IsMouseOver) {
                    if (command.CanExecute(selected)) {
                        command.Execute(selected);
                    }
                }
            }
        }
    }
}