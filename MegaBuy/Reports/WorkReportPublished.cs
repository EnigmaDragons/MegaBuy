
namespace MegaBuy.Reports
{
    public sealed class WorkReportPublished
    {
        public WorkReportData Report { get; }

        public WorkReportPublished(WorkReportData report)
        {
            Report = report;
        }
    }
}
