using ReCallVocabulary.Data_Access;

namespace ReCallVocabulary.Pages;

public partial class DictionaryView : ContentPage
{
    private DictionaryContext? _activeContext;

    public DictionaryView()
	{
		InitializeComponent();
        _activeContext = App.Services.GetService<DictionaryContext>();
        dictView.ItemsSource = _activeContext?.Phrases.ToList();
    }
}