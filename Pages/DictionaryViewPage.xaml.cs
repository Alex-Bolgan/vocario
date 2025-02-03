using ReCallVocabulary.Data_Access;

namespace ReCallVocabulary.Pages;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class DictionaryViewPage : ContentPage
{
    private PhraseService _phraseService;
    List<Phrase> PhraseList { get; set; }

    private readonly List<string> tagList;

    public DictionaryViewPage()
    {
        DbContextManager dbContextManager = ServiceHelper.GetService<DbContextManager>();
        PhraseList = dbContextManager.CurrentDictionaryContext.Phrases.ToList();
        _phraseService = ServiceHelper.GetService<PhraseService>();
        tagList = _phraseService.GetTags();

        InitializeComponent();
        int wordNumber = _phraseService.GetTotalNumber();
        this.Title = $"{Path.GetFileNameWithoutExtension(File.ReadAllText(dbContextManager.FileWithCurrentDBName))} ({wordNumber} words)";
        DictView.ItemsSource = PhraseList;
        SearchResultTags.ItemsSource = _phraseService.GetTags();
        SizeChanged += new EventHandler(ChangeDictViewSize);
    }

    private void ChangeDictViewSize(object? sender, EventArgs e)
    {
        DictView.HeightRequest =
            DeviceDisplay.Current.MainDisplayInfo.Height / DeviceDisplay.Current.MainDisplayInfo.Density;
    }

    private async void dictView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Phrase? item;

        if (e.CurrentSelection.Count > 0 && (item = e.CurrentSelection[0] as Phrase) is not null)
        {
            _phraseService.CurrentPhrase = item;
            await Navigation.PushAsync(new Pages.PhraseViewPage());
            DictView.SelectedItem = null;
        }
    }
    
    private void OnTextChanged(object sender, EventArgs e)
    {
        SearchBar searchBar = (SearchBar)sender;
        if (!String.IsNullOrWhiteSpace(searchBar.Text))
        {
            SearchResults.ItemsSource = _phraseService.SearchPhrases(searchBar.Text);
            DictView.IsVisible = false;
            SearchResults.IsVisible = true;
        }
        else
        {
            DictView.IsVisible = true;
            DictView.ItemsSource = PhraseList;
            SearchResults.IsVisible = false;
        }
    }

    private void SearchResultTags_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        string? tag;

        if (e.CurrentSelection.Count > 0 && (tag = e.CurrentSelection[0] as string) is not null)
        {
            SearchResults.ItemsSource = _phraseService.SearchPhrasesWithTag(tag);
            SearchResults.IsVisible = true;
            DictView.IsVisible = false;
        }
        else
        {
            SearchResults.IsVisible = false;
            DictView.IsVisible = true;
        }
    }
}