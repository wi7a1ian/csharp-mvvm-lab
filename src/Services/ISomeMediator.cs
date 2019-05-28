using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MvvmSampleApp.Services
{
    public class FontChangedEventArgs : EventArgs
    {
        public string Direction { get; set; }
        public int FontSize { get; set; }
    }

    public interface IFontTransformationMediator
    {
        event EventHandler<FontChangedEventArgs> SomethingChanged;
        ICommand ChangeMainFontCommand { get; set; }

        void OnChangeDirection(string direction);
        void OnChangeFontSize(int size);
    }
}
