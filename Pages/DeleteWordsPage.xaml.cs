using ReCallVocabulary.Data_Access;

namespace ReCallVocabulary.Pages;

public partial class DeleteWordsPage : ContentPage
{
    private StatsService _statsService;
    private PhraseService _phraseService;
    List<Phrase> PhraseList { get; set; }

    public List<object> SelectedItems { get; set; } = new List<object>();

    public DeleteWordsPage(DbContextManager dbContextManager, StatsService statsService, PhraseService phraseService)
    {
        PhraseList = dbContextManager.CurrentDictionaryContext.Phrases.ToList();
        _statsService = statsService;
        _phraseService = phraseService;

        InitializeComponent();
        dictView.ItemsSource = PhraseList;
        dictView.SelectedItems = SelectedItems;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        if (SelectedItems is not null)
        {
            _phraseService.RemoveRange(SelectedItems.Cast<Phrase>().ToList());
        }

        _statsService.UpdateAddedNumber();
    }
}