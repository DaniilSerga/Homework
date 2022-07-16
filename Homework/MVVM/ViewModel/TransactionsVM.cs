using Azure;
using Homework.MVVM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Homework.MVVM.ViewModel
{
    internal class TransactionsVM : INotifyPropertyChanged
    {
        private static DateTime _transactionDate;
        private static decimal _transactionAmount;
        private static int _operation;
        private static string _commentary;

        public DateTime TransactionDate
        {
            get => _transactionDate;
            set
            {
                _transactionDate = value;
                OnPropertyChanged("TransactionDate");
            }
        }

        public decimal TransactionAmount
        {
            get => _transactionAmount;
            set
            {
                _transactionAmount = value;
                OnPropertyChanged("TransactionAmount");
            }
        }

        public int Operation
        {
            get => _operation;
            set
            {
                _operation = value;
                OnPropertyChanged("Operation");
            }
        }

        public string Commentary
        {
            get => _commentary;
            set
            {
                _commentary = value;
                OnPropertyChanged("Commentary");
            }
        }

        // Creates new transaction
        public static void CreateTransaction()
        {
            if (_operation < 0 || _operation > 1)
            {
                throw new ArgumentOutOfRangeException(nameof(_operation), "Operation index is out of range.");
            }

            try
            {
                if (_operation == 0)
                {
                    Transaction.Put(_transactionAmount, _commentary);
                }
                else if (_operation == 1)
                {
                    Transaction.Withdraw(_transactionAmount, _commentary);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
