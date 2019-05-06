using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmSampleApp.Core;

namespace MvvmSampleApp.Infrastructure
{
    public class ItemsRepository : IItemsRepository
    {
        private int prevStartIndex = 0;

        public IEnumerable<string> GetItems(int count)
        {
            foreach(var item in Enumerable.Range(prevStartIndex, count))
            {
                //prevStartIndex += 1;
                yield return $"Item #{item}";
            }
            prevStartIndex += count;
        }
    }
}
