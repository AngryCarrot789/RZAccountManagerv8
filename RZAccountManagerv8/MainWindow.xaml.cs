﻿using System.Windows;

namespace RZAccountManagerv8 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            this.InitializeComponent();
            this.DataContext = new MainViewModel();
        }
    }
}
