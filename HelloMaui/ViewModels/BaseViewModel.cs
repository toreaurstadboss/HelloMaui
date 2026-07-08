using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Text;

namespace HelloMaui.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T backingStore, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected bool SetProperty<T, TProp>(ref T backingStore, Expression<Func<T, TProp>> memberExpression, string propertyName)
        {
            if (!(memberExpression is MemberExpression member)) { 
                throw new ArgumentException("Expression must be a member expression", nameof(memberExpression));
            }

            var propertyInfo = member.Member as System.Reflection.PropertyInfo;
            if (propertyInfo is null)
            {
                throw new ArgumentException($"Expression must be a public instance based property of {typeof(T).Name}", nameof(memberExpression));
            }
            var propertyValue = (TProp)propertyInfo.GetValue(backingStore)!;

            //if (EqualityComparer<TProp>.Default.Equals(backingStore, propertyValue))
            //    return false;

            OnPropertyChanged(propertyName);
            return true;
        }


    }
}
