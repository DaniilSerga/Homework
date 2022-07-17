using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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

namespace Homework.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для BankStatement.xaml
    /// </summary>
    public partial class BankStatement : Page
    {
        SqlConnection _sqlConnection = Model.Connection.GetConnection();

        public BankStatement()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            FillWalletStatus();
            FillTransactionsStatus();

            void FillWalletStatus()
            {
                if (_sqlConnection.State is ConnectionState.Closed)
                {
                    _sqlConnection.Open();
                }

                try
                {
                    SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Wallet", _sqlConnection);

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);

                    DataTable dataTable = new DataTable("WalletStatus");

                    dataAdapter.Fill(dataTable);

                    WalletStatus.ItemsSource = dataTable.DefaultView;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    _sqlConnection.Close();
                }
            }

            void FillTransactionsStatus()
            {
                if (_sqlConnection.State is ConnectionState.Closed)
                {
                    _sqlConnection.Open();
                }

                try
                {
                    SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Transactions", _sqlConnection);

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);

                    DataTable dataTable = new DataTable("TransactionsStatus");

                    dataAdapter.Fill(dataTable);

                    TransactionsStatus.ItemsSource = dataTable.DefaultView;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    _sqlConnection.Close();
                }
            }
        }
    }
}
