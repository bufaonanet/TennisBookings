namespace TennisBookings.Merchandise.Api.Diagnostics
{
    public interface IMetricRecorder
    {
        void RecordMetric(string name, int increment = 1, string[] tags = null);
    }
}
