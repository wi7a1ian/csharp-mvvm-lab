using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MvvmSampleApp.Services
{
    public class SomethingChangedEventArgs : EventArgs
    {
        public string Something { get; set; }
    }

    public interface ISomeMediator
    {
        event EventHandler<SomethingChangedEventArgs> SomethingChanged;
        ICommand SomeCommand { get; set; }

        void OnChange(string what);
    }
}
