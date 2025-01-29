using Microcharts;
using Microcharts.Maui;
using ReCallVocabulary.Data_Access;
using SkiaSharp;
namespace ReCallVocabulary.Pages;

public partial class StatsPage : ContentPage
{
    private StatsContext context;
	public StatsPage(DbContextManager contextManager)
    {
        context = contextManager.CurrentStatsContext;

		InitializeComponent();
        SKColor color = SKColor.Parse("#266489");
        var addedNumbersEntries = context.StatsRecords.Select(stat => new Microcharts.ChartEntry(stat.AddedNumber)
        {
            Label = stat.Date.ToString("MM/dd/yyyy"),
            ValueLabel = stat.AddedNumber.ToString(),
            Color = color
        }).ToList();

        var recalledNumberEntries = context.StatsRecords.Select(stat => new Microcharts.ChartEntry(stat.UniqueRecalledNumber)
        {
            Label = stat.Date.ToString("MM/dd/yyyy"),
            ValueLabel = stat.UniqueRecalledNumber.ToString(),
            Color = color
        }).ToList();

        if (addedNumbersEntries.Count > 0 && recalledNumberEntries.Count > 0)
        {
            ChartView.Chart = new LineChart()
            {
                Entries = addedNumbersEntries.Concat(recalledNumberEntries)
            };
        }
        else
        {
            textLabel.Text = "Oops. It seems you have words to recall";
        }
    }
}