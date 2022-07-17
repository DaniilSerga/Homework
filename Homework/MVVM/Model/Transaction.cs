using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Homework.MVVM.Model
{
    public class Transaction
    {
        private static readonly SqlConnection _sqlConnection = Connection.GetConnection();

        // Withdraws money from account
        public static void Withdraw(decimal amount, string commentary)
        {
            if (string.IsNullOrEmpty(commentary))
            {
                throw new ArgumentNullException(nameof(commentary), "Commentary was null or empty.");
            }

            if (amount < 0)
            {
                throw new ArgumentException("Transaction amount cannot be less than 0.");
            }

            if (_sqlConnection.State is ConnectionState.Closed)
            {
                _sqlConnection.Open();
            }

            try
            {
                // Balance, RefreshDate
                if (Wallet.UpdateBalance(amount, 1) == false)
                {
                    return;
                }

                SqlCommand sqlCommand = new SqlCommand("INSERT INTO Transactions VALUES (@TransactionDate, @Amount, @Operation, @Commentary)", _sqlConnection);
                sqlCommand.Parameters.AddWithValue("TransactionDate", DateTime.Now);
                sqlCommand.Parameters.AddWithValue("Amount", amount);
                sqlCommand.Parameters.AddWithValue("Operation", "Withdraw");
                sqlCommand.Parameters.AddWithValue("Commentary", commentary);

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
        public static void Put(decimal amount, string commentary)
        {
            if (string.IsNullOrEmpty(commentary))
            {
                throw new ArgumentNullException(nameof(commentary), "Commentary was null or empty.");
            }

            if (amount < 0)
            {
                throw new ArgumentException("Transaction amount cannot be less than 0.");
            }

            if (_sqlConnection.State is ConnectionState.Closed)
            {
                _sqlConnection.Open();
            }

            try
            {
                Wallet.UpdateBalance(amount, 0);

                SqlCommand sqlCommand = new SqlCommand("INSERT INTO Transactions VALUES (@TransactionDate, @Amount, @Operation, @Commentary)", _sqlConnection);
                sqlCommand.Parameters.AddWithValue("TransactionDate", DateTime.Now);
                sqlCommand.Parameters.AddWithValue("Amount", amount);
                sqlCommand.Parameters.AddWithValue("Operation", "Put");
                sqlCommand.Parameters.AddWithValue("Commentary", commentary);

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
    }
}
