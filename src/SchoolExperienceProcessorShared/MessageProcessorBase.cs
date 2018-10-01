using System;
using System.Threading.Tasks;
using SchoolExperienceEvents.Dto;

namespace SchoolExperienceProcessorShared
{
    public abstract class MessageProcessorBase<T> : IMessageProcessor
    {
        public async Task ProcessAsync(INotificationEvent message)
        {
            await ProcessAsync((T)message);
        }

        protected abstract Task ProcessAsync(T message);

        #region IDisposable Support
        private bool _isDisposed = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                _isDisposed = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
