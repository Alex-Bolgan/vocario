using ReCallVocabulary.Data_Access;

namespace ReCallVocabulary.Pages;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class DictionaryViewPage : ContentPage
{
    List<Phrase> PhraseList { get; set; } = [.. (App.ActiveContext ??
        throw new ArgumentNullException(nameof(PhraseList))).Phrases];

    private readonly List<string> tagList = PhraseService.GetTags();

    public DictionaryViewPage()
    {
        InitializeComponent();
        int wordNumber = PhraseService.GetTotalNumber();
        this.Title = $"{Path.GetFileNameWithoutExtension(File.ReadAllText(App.FileWithCurrentDBName))} ({wordNumber} words)";
        DictView.ItemsSource = PhraseList;
        SearchResultTags.ItemsSource = PhraseService.GetTags();
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
            await Navigation.PushAsync(new PhraseViewPage(item));
            DictView.SelectedItem = null;
        }
    }
    
    private void OnTextChanged(object sender, EventArgs e)
    {
        SearchBar searchBar = (SearchBar)sender;
        if (!String.IsNullOrWhiteSpace(searchBar.Text))
        {
            SearchResults.ItemsSource = PhraseService.SearchPhrases(searchBar.Text);
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
            SearchResults.ItemsSource = PhraseService.SearchPhrasesWithTag(tag);
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