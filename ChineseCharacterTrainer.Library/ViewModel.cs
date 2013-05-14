using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace ChineseCharacterTrainer.Library
{
    public class ViewModel : IViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                return;
            }

            var body = propertyExpression.Body as MemberExpression;
            if (body == null)
            {
                var msg = "Invalid member expression: " + propertyExpression.Body;
                throw new ArgumentException(msg);
            }

            RaisePropertyChanged(body.Member.Name);
        }
    }
}