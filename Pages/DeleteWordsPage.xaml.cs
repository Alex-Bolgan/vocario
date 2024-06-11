using ReCallVocabulary.Data_Access;

namespace ReCallVocabulary.Pages;

public partial class DeleteWordsPage : ContentPage
{
    List<Phrase> PhraseList { get; set; } = App.ActiveContext.Phrases.ToList();
    List<int> SelectedIdList { get; set; } = new List<int>();

    public DeleteWordsPage()
	{
		InitializeComponent();
        dictView.ItemsSource = PhraseList;

    }
    private void dictView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        Phrase item = e.Item as Phrase;
        Model.RemovePhrase(item);
    }
}