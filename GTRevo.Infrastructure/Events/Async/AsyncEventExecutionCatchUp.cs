﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTRevo.Core.Core;
using GTRevo.Core.Core.Lifecycle;

namespace GTRevo.Infrastructure.Events.Async
{
    public class AsyncEventExecutionCatchUp : IApplicationStartListener
    {
        private readonly IEventSourceCatchUp[] eventSourceCatchUps;
        private readonly IAsyncEventQueueManager asyncEventQueueManager;
        private readonly Func<IAsyncEventQueueBacklogWorker> asyncEventQueueBacklogWorkerFunc;

        public AsyncEventExecutionCatchUp(IEventSourceCatchUp[] eventSourceCatchUps,
            IAsyncEventQueueManager asyncEventQueueManager,
            Func<IAsyncEventQueueBacklogWorker> asyncEventQueueBacklogWorkerFunc)
        {
            this.eventSourceCatchUps = eventSourceCatchUps;
            this.asyncEventQueueManager = asyncEventQueueManager;
            this.asyncEventQueueBacklogWorkerFunc = asyncEventQueueBacklogWorkerFunc;
        }

        public void OnApplicationStarted()
        {
            Task.Run(InitializeAsync).GetAwaiter().GetResult();
        }

        private async Task InitializeAsync()
        {
            await CatchUpEventSourcesAsync();
            await RunBackloggedQueuesAsync();
        }

        private async Task CatchUpEventSourcesAsync()
        {
            foreach (var eventSourceCatchUp in eventSourceCatchUps)
            {
                await eventSourceCatchUp.CatchUpAsync();
            }
        }
        
        private async Task RunBackloggedQueuesAsync()
        {
            var backloggedQueueNames = await asyncEventQueueManager.GetNonemptyQueueNamesAsync();

            await Task.WhenAll(backloggedQueueNames.Select(queueName =>
                Task.Factory.StartNewWithContext(() =>
                {
                    var asyncEventQueueBacklogWorker = asyncEventQueueBacklogWorkerFunc();
                    return asyncEventQueueBacklogWorker.RunQueueBacklogAsync(queueName);
                }))); //using new Task because we want a new context (parallelization on ASP.NET 4 + fresh DI lifetime scope)
        }
    }
}