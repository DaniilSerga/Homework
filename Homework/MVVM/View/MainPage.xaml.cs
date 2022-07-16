using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Homework.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        // Loads pages in different stream
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Thread loadPages = new Thread(LoadPages);
            
            loadPages.Start();
        }

        // Preloads pages
        private void LoadPages()
        {
            Dispatcher.Invoke(DispatcherPriority.Render, (ThreadStart)delegate
            {
                MainMenuFrame.Navigate(new Menu());
                TransactionsFrame.Navigate(new Transactions());
                BankStatementFrame.Navigate(new BankStatement());
            });
        }
    }
}
