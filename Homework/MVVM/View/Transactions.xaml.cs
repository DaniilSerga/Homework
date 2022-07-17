using Homework.MVVM.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Homework.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для Transactions.xaml
    /// </summary>
    public partial class Transactions : Page
    {
        public Transactions()
        {
            InitializeComponent();
        }

        // I didn't have enough time to understand how bind button clicks, so that's what I've done
        private void Button_Click(object sender, RoutedEventArgs e) => TransactionsVM.CreateTransaction();
    }
}
