using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Homework.MVVM.Model
{
    public class Transaction : INotifyPropertyChanged
    {
        private DateTime _transactionDate;
        private decimal _transactionAmount;
        private string _operation;
        private string _status;

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

        public string Operation
        {
            get => _operation;
            set
            {
                _operation = value;
                OnPropertyChanged("Operation");
            }
        }

        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged("Status");
            }
        }

        public Transaction(DateTime transactionDate, decimal transactionAmount, string operation, string status)
        {
            _transactionDate = transactionDate;
            _transactionAmount = transactionAmount;
            _operation = operation;
            _status = status;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
