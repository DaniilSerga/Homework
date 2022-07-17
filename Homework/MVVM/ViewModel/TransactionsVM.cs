using Azure;
using Homework.MVVM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Homework.MVVM.ViewModel
{
    internal class TransactionsVM : INotifyPropertyChanged
    {
        private static DateTime _transactionDate;
        private static decimal _transactionAmount;
        private static int _operation;
        private static string _commentary;

        #region Properties
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
        #endregion
        
        // Creates new transaction
        public static void CreateTransaction()
        {
            switch (_operation)
            {
                case 0: 
                    Transaction.Put(_transactionAmount, _commentary);
                    break;
                case 1: 
                    Transaction.Withdraw(_transactionAmount, _commentary);
                    break;
                default: 
                    MessageBox.Show("Operation index is out of range.");
                    return;
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
