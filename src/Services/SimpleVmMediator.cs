using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MvvmSampleApp.Services
{
    public class SimpleFontTransformationMediator : IFontTransformationMediator
    {
        public ICommand ChangeMainFontCommand { get; set; }

        public event EventHandler<FontChangedEventArgs> SomethingChanged;

        public void OnChangeDirection(string direction)
        {
            SomethingChanged?.Invoke(this, new FontChangedEventArgs { Direction = direction });
        }

        public void OnChangeFontSize(int size)
        {
            SomethingChanged?.Invoke(this, new FontChangedEventArgs { FontSize = size });
        }
    }
}
