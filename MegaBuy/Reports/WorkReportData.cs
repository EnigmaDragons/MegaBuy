using MegaBuy.Calls.Callers;

namespace MegaBuy.Reports
{
    public sealed class WorkReportData
    {
        public float HoursWorkedActual { get; set; }
        public float HoursWorkedExpected { get; set; }
        public int CallsAnsweredActual { get; set; }
        public int CallsAnsweredExpected { get; set; }
        public float AvgCallRatingActual { get; set; }
        public float AvgCallRatingExpected { get; set; }

        public CallerFeedback BestFeedback { get; set; }
        public CallerFeedback WorstFeedback { get; set; }
    }
}
