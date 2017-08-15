﻿using System.Collections.Generic;
using PushSharp.Apple;

namespace GTRevo.Infrastructure.Notifications.Channels.Push
{
    public interface IApnsBrokerDispatcher
    {
        void QueueNotification(ApnsNotification notification);
        void QueueNotifications(IEnumerable<ApnsNotification> notifications);
    }
}
