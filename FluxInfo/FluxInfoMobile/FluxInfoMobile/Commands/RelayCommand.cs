using System;
using System.Windows.Input;

namespace FluxInfoMobile.Commands
{
    public class RelayCommand<T> : ICommand
    {
        private static bool CanExecute(T parameter)
        {
            return true;
        }

        readonly Action<T> mExecute;

        readonly Func<T, bool> mCanExecute;

        public RelayCommand(Action<T> execute, Func<T, bool> canExecute = null)
        {
            mExecute = execute ?? throw new ArgumentNullException(nameof(execute));
            mCanExecute = canExecute ?? CanExecute;
        }

        public bool CanExecute(object parameter)
        {
            return mCanExecute(TranslateParameter(parameter));
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            mExecute(TranslateParameter(parameter));
        }

        T TranslateParameter(object parameter)
        {
            T value = default(T);

            value = (T)parameter;

            return value;
        }
    }

    public class RelayCommand : RelayCommand<object>
    {
        public RelayCommand(Action execute, Func<bool> canExecute = null)
            : base(obj => execute(), (canExecute == null ? null : new Func<object, bool>(obj => canExecute())))
        {
        }
    }
}
