using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShibaReader.Events
{
    public class UpdateImportJobEvent : IObservable<int>
    {
        private static UpdateImportJobEvent instance = null;
        private List<IObserver<int>> observers = new List<IObserver<int>>();
        private int progress = 0;
        public int Progress 
        { 
            get
            {
                return progress;
            }
            set 
            {
                progress = value;
                foreach (var observer in observers)
                {
                    observer.OnNext(value);
                }
            }
        }

        public void TaskComplete()
        {
            progress = 0;
            foreach (var observer in observers)
            {
                observer.OnCompleted();
            }
        }
        private UpdateImportJobEvent() { }

        public static UpdateImportJobEvent getInstance() 
        {
            instance = instance ?? new UpdateImportJobEvent();
            return instance;
        }

        public IDisposable Subscribe(IObserver<int> observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
                observer.OnNext(Progress);
            }
            return new UnsubscribeUpdateImportJobEvent(observers, observer);
        }
    }
}
