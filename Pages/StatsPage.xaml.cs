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
        SKColor color = SKColor.Parse("#266489");
        var addedNumbersEntries = context.StatsRecords.Select(stat => new Microcharts.ChartEntry(stat.AddedNumber)
        {
            Label = stat.Date.ToString("MM/dd/yyyy"),
            ValueLabel = stat.AddedNumber.ToString(),
            Color = color
        }).ToList();

        var recalledNumberEntries = context.StatsRecords.Select(stat => new Microcharts.ChartEntry(stat.AddedNumber)
        {
            Label = stat.Date.ToString("MM/dd/yyyy"),
            ValueLabel = stat.RecalledNumber.ToString(),
            Color = color
        }).ToList();

        ChartView.Chart = new LineChart()
        {
            Entries = addedNumbersEntries.Concat(recalledNumberEntries)
        };
    }
}