using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MvvmSampleApp.Services
{
    public class SimpleVmMediator : ISomeMediator
    {
        public ICommand SomeCommand { get; set; }

        public event EventHandler<SomethingChangedEventArgs> SomethingChanged;

        public void OnChange(string what)
        {
            SomethingChanged?.Invoke(this, new SomethingChangedEventArgs { Something = what });
        }

    }
}
