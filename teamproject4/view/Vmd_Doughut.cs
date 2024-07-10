using LiveChartsCore;
using LiveChartsCore.SkiaSharpView.Extensions;
namespace teamproject4.view
{
    public partial class ViewMode
    {
        // you can convert any array, list or IEnumerable<T> to a pie series collection:
        public IEnumerable<ISeries> Series { get; set; } =
            new[] { 2, 4, 1, 4, 3 }.AsPieSeries((value, series) =>
            {
                series.MaxRadialColumnWidth = 60;
            });
    }
}
