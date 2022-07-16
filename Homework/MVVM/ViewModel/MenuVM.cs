using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Homework.MVVM.Model;

namespace Homework.MVVM.ViewModel
{
    internal class MenuVM : INotifyPropertyChanged
    {
        private decimal _balance = Wallet.GetCurrentBalance();
        private DateTime _recentBalanceRefresh = Wallet.GetRecentBalanceRefresh();

        public decimal Balance
        {
            get => _balance;
            set
            {
                _balance = value;
                OnPropertyChanged("Balance");
            }
        }

        public DateTime RecentBalanceRefresh
        {
            get => _recentBalanceRefresh;
            set
            {
                _recentBalanceRefresh = value;
                OnPropertyChanged("RecentBalanceRefresh");
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
