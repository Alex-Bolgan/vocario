using ReCallVocabulary.Data_Access;

namespace ReCallVocabulary.Pages;
using ReCallVocabulary.Data_Access;
public partial class AddPage : ContentPage
{
    private DictionaryContext _activeContext;

    public AddPage()
	{
		InitializeComponent();
        _activeContext = App.Services.GetService<DictionaryContext>();

    }


    private async void AddButton_Clicked(object sender, EventArgs e)
    {
        if (String.IsNullOrWhiteSpace(phraseEntry.Text) && String.IsNullOrWhiteSpace(definitionEntry.Text))
            await _activeContext.Phrases.AddAsync(new Phrase {Term=phraseEntry.Text,
                Definition=definitionEntry.Text,Synonyms=synonymsEntry.Text.Split(" ") });
    }
}