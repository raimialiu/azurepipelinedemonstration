using System;
using Serilog.Core;
using Serilog.Events;

namespace SerilogSample
{
    public class TimeStamp : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty("TimeStampId", getTimestampId()));
        }

        private string getTimestampId()
        {
            return "" + Math.Ceiling(Convert.ToDecimal(DateTime.Now.Millisecond * 1000000)).ToString();
        }
    }
}