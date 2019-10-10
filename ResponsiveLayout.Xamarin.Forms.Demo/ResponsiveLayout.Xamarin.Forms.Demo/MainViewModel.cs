using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace ResponsiveLayout.Xamarin.Forms.Demo
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ResponsiveLayoutMode _selectedLayoutMode;

        public ResponsiveLayoutMode SelectedLayoutMode
        {
            get => _selectedLayoutMode;
            set => SetProperty(ref _selectedLayoutMode, value);
        }

        public IList<ResponsiveLayoutMode> LayoutModes { get; } = Enum.GetValues(typeof(ResponsiveLayoutMode)).Cast<ResponsiveLayoutMode>().ToList();



        private string _firstEntry;

        public string FirstEntry
        {
            get => _firstEntry;
            set => SetProperty(ref _firstEntry, value);
        }

        private string _secondEntry;

        public string SecondEntry
        {
            get => _secondEntry;
            set => SetProperty(ref _secondEntry, value);
        }

        private string _thirdEntry;

        public string ThirdEntry
        {
            get => _thirdEntry;
            set => SetProperty(ref _thirdEntry, value);
        }

        private string _fourthEntry;

        public string FourthEntry
        {
            get => _fourthEntry;
            set => SetProperty(ref _fourthEntry, value);
        }


        public MainViewModel()
        {
            FirstEntry = "Content of first Entry";
            SecondEntry = "Content of second Entry";
            ThirdEntry = "Content of third Entry";
            FourthEntry = "Content of fourth Entry";
            SelectedLayoutMode = ResponsiveLayoutMode.Width;
        }


        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
