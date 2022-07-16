﻿using System;
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
        private static SqlConnection _sqlConnection = Connection.SqlConnection;

        // TODO Gets current balance
        public static decimal GetCurrentBalance()
        {
            if (_sqlConnection.State is ConnectionState.Closed)
            {
                _sqlConnection.Open();
            }

            decimal balance = 0;

            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT Balance FROM Wallet ORDER BY RefreshDate DESC", _sqlConnection);

                SqlDataReader dataReader = sqlCommand.ExecuteReader();

                balance = dataReader.GetDecimal(0);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            MessageBox.Show($"{balance}");
            return balance;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}