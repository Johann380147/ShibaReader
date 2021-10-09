using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShibaReader.Models
{
    class HistoryLinkedList : List<History>
    {
        public new int Count { get; private set; }
        public History Current { get; set; }

        public void AddAfter(History after, List<string> matchedJobs, string searchText, int currentIndex)
        {
            AddAfter(after, new History(matchedJobs, searchText, currentIndex));
        }

        public void AddAfter(History after, History node)
        {
            if (after == null)
            {
                Current = node;
                Count++;
                return;
            }
            Count += CalculateCountOffset(Current);
            after.Next = node;
            node.Previous = after;
            Current = node;
        }

        public History Previous()
        {
            History temp = Current;
            Current = temp?.Previous;
            return temp?.Previous;
        }

        public History Next()
        {
            History temp = Current;
            Current = temp?.Next;
            return temp?.Next;
        }

        public History PeekPrevious()
        {
            return Current?.Previous;
        }

        public History PeekNext()
        {
            return Current?.Next;
        }

        private int CalculateCountOffset(History history)
        {
            int count = 0;
            History node = history;
            while (node != null)
            {
                count--;
                node = node?.Next;
            }
            return count + 1;
        }
    }
}
