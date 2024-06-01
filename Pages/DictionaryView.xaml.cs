using ReCallVocabulary.Data_Access;

namespace ReCallVocabulary.Pages;

public partial class DictionaryView : ContentPage
{
    List<Phrase> PhraseList { get; set; } = App.ActiveContext.Phrases.ToList();

    public DictionaryView()
	{
		InitializeComponent();
        dictView.ItemsSource = PhraseList;
    }

    private async void dictView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        Phrase item = e.Item as Phrase;
        await Navigation.PushAsync(new PhraseViewPage(item.Id));
    }
}