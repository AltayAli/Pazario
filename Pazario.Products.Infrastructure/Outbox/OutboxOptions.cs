using System;
using System.Collections.Generic;
using System.Text;

namespace Pazario.Products.Infrastructure.Outbox
{
    public class OutboxOptions
    {
        public int IntervalInSeconds { get; set; }
        public int BatchSize { get; set; }
    }
}
