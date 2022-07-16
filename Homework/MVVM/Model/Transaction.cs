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
        private static readonly SqlConnection _sqlConnection = Connection.GetConnection();
        public static DateTime TransactionDate { get; set; }
        public static decimal TransactionAmount { get; set; }
        public static string Operation { get; set; }
        public static string Commentary { get; set; }

        public Transaction(DateTime transactionDate, decimal transactionAmount, string operation, string commentary)
        {
            TransactionDate = transactionDate;
            TransactionAmount = transactionAmount;
            Operation = operation;
            Commentary = commentary;
        }

        public Transaction() { }

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
                SqlCommand sqlCommand = new SqlCommand("INSERT INTO Transactions VALUES (@TransactionsDate, @Amount, '@Operation', N'@Commentary')", _sqlConnection);
                sqlCommand.Parameters.AddWithValue("@TransactionDate", DateTime.Now);
                sqlCommand.Parameters.AddWithValue("@Amount", amount);
                sqlCommand.Parameters.AddWithValue("@Operation", "Withdraw");
                sqlCommand.Parameters.AddWithValue("@Commentary", commentary);

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

        // TODO Fix database query
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
                SqlCommand sqlCommand = new SqlCommand("INSERT INTO Transactions VALUES (@TransactionsDate, @Amount, '@Operation', N'@Commentary')", _sqlConnection);
                sqlCommand.Parameters.AddWithValue("@TransactionDate", DateTime.Now);
                sqlCommand.Parameters.AddWithValue("@Amount", amount);
                sqlCommand.Parameters.AddWithValue("@Operation", "Put");
                sqlCommand.Parameters.AddWithValue("@Commentary", commentary);

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

        // Gets all transactions
        public static List<Transaction> GetAllTransactions()
        {
            if (_sqlConnection.State is ConnectionState.Closed)
            {
                _sqlConnection.Open();
            }

            List<Transaction> transactionsList = new List<Transaction>();

            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Transaction");
                SqlDataReader dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        transactionsList.Add(new Transaction(dataReader.GetDateTime(0), dataReader.GetDecimal(1), dataReader.GetString(2), dataReader.GetString(3)));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                _sqlConnection.Close();
            }

            return transactionsList;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
