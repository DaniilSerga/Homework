using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Homework.MVVM.Model
{
    internal class Wallet : INotifyPropertyChanged
    {
        private static readonly SqlConnection _sqlConnection = Connection.SqlConnection;

        // TODO Gets current balance
        public static decimal GetCurrentBalance()
        {
            if (_sqlConnection.State is ConnectionState.Closed)
            {
                _sqlConnection.Open();
            }

            List<decimal> balances = new List<decimal>();

            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT Balance FROM Wallet ORDER BY RefreshDate DESC", _sqlConnection);

                SqlDataReader dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        balances.Add(dataReader.GetDecimal(0));
                    }
                }

                dataReader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                _sqlConnection.Close();
            }

            return balances[0];
        }

        // Updates wallet balance
        public static void UpdateBalance(decimal amount, int operation)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Transaction amount annot be negative.");
            }

            if (operation < 0 || operation > 1)
            {
                throw new ArgumentOutOfRangeException(nameof(operation), "Operation cannot be negative and be grater than 1.");
            }

            decimal balance = GetCurrentBalance();

            if (_sqlConnection.State is ConnectionState.Closed)
            {
                _sqlConnection.Open();
            }

            try
            {
                if ((balance == 0 || amount > balance) && operation == 1)
                {
                    throw new ArgumentException("Your balance is too low for this operation.", nameof(amount));
                }

                SqlCommand sqlCommand = new SqlCommand("INSERT INTO Wallet VALUES (@Balance, @RefreshDate)", _sqlConnection);
                sqlCommand.Parameters.AddWithValue("Balance", operation == 0 ? balance + amount : balance - amount);
                sqlCommand.Parameters.AddWithValue("RefreshDate", DateTime.Now);

                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        // Gets recent balance refresh date
        public static DateTime GetRecentBalanceRefresh()
        {
            if (_sqlConnection.State is ConnectionState.Closed)
            {
                _sqlConnection.Open();
            }

            List<DateTime> balanceRefreshes = new List<DateTime>();

            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT RefreshDate FROM Wallet ORDER BY RefreshDate DESC", _sqlConnection);

                SqlDataReader dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        balanceRefreshes.Add(dataReader.GetDateTime(0));
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

            return balanceRefreshes[0];
        }

        #region INotifyPropertyChanged realization
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        #endregion
    }
}
