using ReCallVocabulary.Data_Access;

namespace ReCallVocabulary.Pages;
public partial class AddWordsPage : ContentPage
{
    private DictionaryContext? _activeContext;

    public AddWordsPage()
    {
        InitializeComponent();
        _activeContext = App.Services.GetService<DictionaryContext>();

    }


    private async void AddButton_Clicked(object sender, EventArgs e)
    {
        _activeContext.Database.EnsureCreated();
        if (!String.IsNullOrWhiteSpace(phraseEntry.Text) && !String.IsNullOrWhiteSpace(definitionEntry.Text))
            await _activeContext.Phrases.AddAsync(new Phrase
            {
                Term = phraseEntry.Text,
                Definition = definitionEntry.Text,
                Synonyms = synonymsEntry.Text?.Split(" ")
            });
        await _activeContext.SaveChangesAsync();
    }
}