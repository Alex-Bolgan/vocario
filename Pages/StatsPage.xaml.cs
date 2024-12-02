using Microcharts;
using Microcharts.Maui;
using ReCallVocabulary.Data_Access;
using SkiaSharp;
namespace ReCallVocabulary.Pages;

public partial class StatsPage : ContentPage
{
    private StatsContext context = App.statsContext ?? 
                           throw new ArgumentNullException(nameof(context));
	public StatsPage()
	{
		InitializeComponent();
        var addedNumberColor = SKColor.Parse("#FF6347");
        var uniqueRecalledNumberColor = SKColor.Parse("#4169E1");

        var addedNumbersEntries = context.StatsRecords.Select(stat => new Microcharts.ChartEntry(stat.AddedNumber)
        {
            Label = stat.Date.ToString("MM/dd/yyyy"),
            ValueLabel = stat.AddedNumber.ToString(),
            Color = addedNumberColor
        }).ToList();

        var recalledNumberEntries = context.StatsRecords.Select(stat => new Microcharts.ChartEntry(stat.UniqueRecalledNumber)
        {
            Label = stat.Date.ToString("MM/dd/yyyy"),
            ValueLabel = stat.UniqueRecalledNumber.ToString(),
            Color = uniqueRecalledNumberColor
        }).ToList();

        if (addedNumbersEntries.Count > 0 && recalledNumberEntries.Count > 0)
        {
            AddedChart.Chart = new LineChart()
            {
                Entries = addedNumbersEntries.Concat(recalledNumberEntries),
                LineMode = LineMode.Straight,
                LineSize = 4,
                PointMode = PointMode.Circle,
                PointSize = 8
            };

            RecalledChart.Chart = new LineChart()
            {
                Entries = addedNumbersEntries.Concat(recalledNumberEntries),
                LineMode = LineMode.Straight,
                LineSize = 4,
                PointMode = PointMode.Circle,
                PointSize = 8
            };
        }
        else
        {
            textLabel.Text = "Oops. It seems you have words to recall";
        }
    }
}