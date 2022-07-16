using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Homework.MVVM.Model
{
    public class Transaction : INotifyPropertyChanged
    {
        private static SqlConnection _sqlConnection = Connection.GetConnection();

        // Withdraws money from account
        public static void Withdraw(decimal amount)
        {
            if (_sqlConnection.State is ConnectionState.Closed)
            {
                _sqlConnection.Open();
            }

            try
            {
                SqlCommand sqlCommand = new SqlCommand("INSERT INTO Transactions VALUES (@TransactionsDate, @Amount, @Operation)", _sqlConnection);
                sqlCommand.Parameters.AddWithValue("@TransactionDate", DateTime.Now);
                sqlCommand.Parameters.AddWithValue("@Amount", amount);
                sqlCommand.Parameters.AddWithValue("@Operation", "Withdraw");

                sqlCommand.ExecuteNonQuery();

                MessageBox.Show($"Деньги в размере {amount}р. были успешно сняты со счёта!");
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

        // Puts money in account
        public static void Put(decimal amount)
        {
            if (_sqlConnection.State is ConnectionState.Closed)
            {
                _sqlConnection.Open();
            }

            try
            {
                SqlCommand sqlCommand = new SqlCommand("INSERT INTO Transactions VALUES (@TransactionsDate, @Amount, @Operation)", _sqlConnection);
                sqlCommand.Parameters.AddWithValue("@TransactionDate", DateTime.Now);
                sqlCommand.Parameters.AddWithValue("@Amount", amount);
                sqlCommand.Parameters.AddWithValue("@Operation", "Put");

                sqlCommand.ExecuteNonQuery();

                MessageBox.Show($"Деньги в размере {amount}р. были успешно положены на счёт!");
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
