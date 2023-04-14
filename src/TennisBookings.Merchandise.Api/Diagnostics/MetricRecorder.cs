
namespace TennisBookings.Merchandise.Api.Diagnostics
{
    public class MetricRecorder : IMetricRecorder
    {
        public void RecordMetric(string name, int increment, string[] tags)
        {
            // does nothing for this demo
        }
    }
}
