using ReCallVocabulary.Data_Access;

namespace ReCallVocabulary.Pages;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class DictionaryViewPage : ContentPage
{
    List<Phrase> PhraseList { get; set; } = [.. (App.ActiveContext ??
        throw new ArgumentNullException(nameof(PhraseList))).Phrases];

    private readonly List<string> tagList = Model.GetTags();

    public DictionaryViewPage()
    {
        InitializeComponent();
        int wordNumber = Model.GetTotalNumber();
        this.Title = $"{Path.GetFileNameWithoutExtension(File.ReadAllText(App.FileWithCurrentDBName))} ({wordNumber} words)";
        dictView.ItemsSource = PhraseList;
        searchResultTags.ItemsSource = Model.GetTags();
    }

    private async void dictView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Phrase? item;

        if (e.CurrentSelection.Count > 0 && (item = e.CurrentSelection[0] as Phrase) is not null)
        {
            await Navigation.PushAsync(new PhraseViewPage(item));
            dictView.SelectedItem = null;
        }
    }
    
    private void OnTextChanged(object sender, EventArgs e)
    {
        SearchBar searchBar = (SearchBar)sender;
        if (!String.IsNullOrWhiteSpace(searchBar.Text))
        {
            searchResults.ItemsSource = Model.SearchPhrases(searchBar.Text);
            dictView.IsVisible = false;
            searchResults.IsVisible = true;
        }
        else
        {
            dictView.IsVisible = true;
            dictView.ItemsSource = PhraseList;
            searchResults.IsVisible = false;
        }
    }

    private void SearchResultTags_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        string? tag;

        if (e.CurrentSelection.Count > 0 && (tag = e.CurrentSelection[0] as string) is not null)
        {
            searchResults.ItemsSource = Model.SearchPhrasesWithTag(tag);
            searchResults.IsVisible = true;
            dictView.IsVisible = false;
        }
        else
        {
            searchResults.IsVisible = false;
            dictView.IsVisible = true;
        }
    }
}