﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTRevo.DataAccess.EF6;
using GTRevo.Infrastructure.ReadModel;

namespace GTRevo.Infrastructure.Notifications.Model
{
    [TablePrefix(NamespacePrefix = "REV", ColumnPrefix = "NBF")]
    public class NotificationBuffer : ReadModelBase
    {
        public NotificationBuffer(Guid id, Guid governorId, Guid pipelineId)
        {
            Id = id;
            GovernorId = governorId;
            PipelineId = pipelineId;
        }

        protected NotificationBuffer()
        {
        }

        public Guid Id { get; private set; }
        public Guid PipelineId { get; private set; }
        public Guid GovernorId { get; set; }
    }
}