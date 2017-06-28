using MegaBuy.Calls.Callers;
using MegaBuy.MegaBuyCorporation;

namespace MegaBuy.Reports
{
    public sealed class WorkReportData
    {
        public double HoursWorkedActual { get; set; }
        public double HoursWorkedExpected { get; set; }
        public int CallsAnsweredActual { get; set; }
        public int CallsAnsweredExpected { get; set; }
        public double AvgCallRatingActual { get; set; }
        public double AvgCallRatingExpected { get; set; }
        
        public CallerFeedback BestFeedback { get; set; }
        public CallerFeedback WorstFeedback { get; set; }

        public MegaBuyPerformanceRating PerformanceRating { get; set; }
    }
}
