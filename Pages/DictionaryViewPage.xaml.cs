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
        Phrase item = e.CurrentSelection[0] as Phrase;
        if (item is not null)
        {
            await Navigation.PushAsync(new PhraseViewPage(item.Id));
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
}