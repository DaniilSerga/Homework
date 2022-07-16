using Homework.MVVM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Homework.MVVM.ViewModel
{
    internal class TransactionsVM : INotifyPropertyChanged
    {
        private DateTime _transactionDate;
        private decimal _transactionAmount;
        private string _operation;

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

        public ICommand PutCommand { get; }
        public ICommand WithdrawCommand { get; }

        // TODO Fix Commands
        public TransactionsVM()
        {
            PutCommand = new RelayCommand(_ => Put());
            WithdrawCommand = new RelayCommand(_ => Transaction.Withdraw(TransactionAmount));
        }

        private void Put() => Transaction.Put(TransactionAmount);
        #region INotifyPropertyChanged realization
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        #endregion
    }
}
