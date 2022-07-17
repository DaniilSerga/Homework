using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using Homework.MVVM.Model;

namespace Homework.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для BankStatement.xaml
    /// </summary>
    public partial class BankStatement : Page
    {
        public BankStatement()
        {
            InitializeComponent();
        }

        // Fills DataGrid when page is loaded
        private void Page_Loaded(object sender, RoutedEventArgs e) => FillDataGrid();

        // Fills DataGrid with information from database
        private void FillDataGrid()
        {
            SqlConnection _sqlConnection = Model.Connection.GetConnection();

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
