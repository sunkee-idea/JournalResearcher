using System;
using System.Collections.Generic;
using System.Text;

namespace JournalResearcher.DataAccess.Cores
{
    public class CounterModel<T>
    {
        public int Total { get; set; }
        public IEnumerable<T> Items { get; set; }
        
    }
}
