using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShibaReader.Events
{
    class UnsubscribeUpdateImportJobEvent : IDisposable
    {
        private List<IObserver<int>> _observers;
        private IObserver<int> _observer;

        public UnsubscribeUpdateImportJobEvent(List<IObserver<int>> _observers, IObserver<int> _observer)
        {
            this._observers = _observers;
            this._observer = _observer;
        }
        public void Dispose()
        {
            if (_observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }
}
