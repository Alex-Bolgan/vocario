using ReCallVocabulary.Data_Access;

namespace ReCallVocabulary.Pages;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class DictionaryViewPage : ContentPage
{
    List<Phrase> PhraseList { get; set; } = App.ActiveContext.Phrases.ToList();

    public DictionaryViewPage()
    {
        InitializeComponent();
        int wordNumber = Model.GetTotalNumber();
        this.Title = $"{Path.GetFileNameWithoutExtension(File.ReadAllText(App.fileWithCurrentDBName))} ({wordNumber} words)";
        dictView.ItemsSource = PhraseList;
    }

    private async void dictView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Phrase item;

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
            searchResultTags.ItemsSource = Model.SearchTags(searchBar.Text);
            dictView.IsVisible = false;
            searchResultTags.IsVisible = true;
            searchResults.IsVisible = true;
        }
        else
        {
            dictView.IsVisible = true;
            dictView.ItemsSource = PhraseList;
            searchResultTags.IsVisible = false;
            searchResults.IsVisible = false;
        }
    }

    private void searchResultTags_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        string tag = e.CurrentSelection[0] as string;

        if (tag is not null)
        {
            searchResults.ItemsSource = Model.SearchPhrasesWithTag(tag);
            searchResultTags.IsVisible = false;
        }
    }
}