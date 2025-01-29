using ReCallVocabulary.Data_Access;

namespace ReCallVocabulary.Pages;

public partial class DeleteWordsPage : ContentPage
{
    List<Phrase> PhraseList { get; set; }

    public List<object> SelectedItems { get; set; } = new List<object>();

    public DeleteWordsPage(DbContextManager dbContextManager)
    {
        PhraseList = dbContextManager.CurrentDictionaryContext.Phrases.ToList();
        InitializeComponent();
        dictView.ItemsSource = PhraseList;
        dictView.SelectedItems = SelectedItems;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        if (SelectedItems is not null)
        {
            PhraseService.RemoveRange(SelectedItems.Cast<Phrase>().ToList());
        }

        StatsService.UpdateAddedNumber();
    }
}