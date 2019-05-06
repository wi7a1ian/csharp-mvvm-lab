using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmSampleApp.Core
{
    public interface IItemsRepository
    {
        IEnumerable<string> GetItems(int count);
    }
}
