using DBCleaner.Models;

namespace DBCleaner.Helpers
{
    internal static class UtilitiesHelper
    {
        internal static TimeSpan ConvertTimeSpanDataModelToTimeSpan(TimeSpanDataModel timeSpanDataModel) =>
            new TimeSpan(timeSpanDataModel.Days, timeSpanDataModel.Hours, timeSpanDataModel.Minutes,
                timeSpanDataModel.Seconds, timeSpanDataModel.Milliseconds);
    }
}
