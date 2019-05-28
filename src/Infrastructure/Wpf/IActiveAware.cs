using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmSampleApp.Infrastructure.Wpf
{
    public interface IActiveAware
    {
        bool IsActive { get; }
        void Activate();
        void Deactivate();
    }
}
