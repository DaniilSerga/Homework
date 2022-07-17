using System;
using System.Windows;

namespace Homework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Shows starting page
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowFrame.Navigate(new MVVM.View.MainPage());
        }

        // Shuts down the application
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        // Minimizes window size
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
