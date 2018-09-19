using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace SchoolExperienceEvents.AzureServices.Implementation
{
    /// <summary>
    /// Implements an <see cref="IHostedService"/> to handle the basic start/stop functionality.
    /// </summary>
    /// <seealso cref="Microsoft.Extensions.Hosting.IHostedService" />
    /// <seealso cref="System.IDisposable" />
    public abstract class HostedService : IHostedService, IDisposable
    {
        /// <summary>
        /// The executing task.
        /// </summary>
        private Task _executingTask;

        /// <summary>
        /// The cancellation token source.
        /// </summary>
        protected CancellationTokenSource CancellationTokenSource { get; private set; }

        /// <inheritdoc />
        public Task StartAsync(CancellationToken cancellationToken)
        {
            CancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            _executingTask = ExecuteAsync(CancellationTokenSource.Token);

            // If the task is completed then return it,
            // this will bubble cancellation and failure to the caller
            if (_executingTask.IsCompleted)
            {
                return _executingTask;
            }

            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_executingTask != null)
            {
                CancellationTokenSource.Cancel();
                await Task.WhenAll(_executingTask, Task.Delay(Timeout.Infinite, cancellationToken));
                cancellationToken.ThrowIfCancellationRequested();
            }
        }

        protected abstract Task ExecuteAsync(CancellationToken cancellationToken);

        public virtual void Dispose()
        {
            CancellationTokenSource.Cancel();
        }
    }
}
