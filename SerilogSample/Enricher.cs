using System;
using Serilog.Core;
using Serilog.Events;

namespace SerilogSample
{
    public class Enricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty("RequestId", getRequestid()));
        }

        private static string getRequestid()
        {
            double id = Math.Ceiling(Convert.ToDouble(new Random().Next(1,1000)*1000000000));

            return id.ToString() + "";
        }
    }
}