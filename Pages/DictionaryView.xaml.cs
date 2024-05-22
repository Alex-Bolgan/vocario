using ReCallVocabulary.Data_Access;

namespace ReCallVocabulary.Pages;

public partial class DictionaryView : ContentPage
{
    private DictionaryContext? activeContext = App.ActiveContext;

    public DictionaryView()
	{
		InitializeComponent();
        dictView.ItemsSource = activeContext?.Phrases.ToList();
    }
}