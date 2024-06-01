using ReCallVocabulary.Data_Access;

namespace ReCallVocabulary.Pages;
public partial class AddWordsPage : ContentPage
{
    private DictionaryContext? activeContext = App.ActiveContext;

    public AddWordsPage()
    {
        InitializeComponent();

    }


    private async void AddButton_Clicked(object sender, EventArgs e)
    {
        activeContext.Database.EnsureCreated();
        if (!String.IsNullOrWhiteSpace(phraseEntry.Text) && !String.IsNullOrWhiteSpace(definitionEntry.Text))
            await activeContext.Phrases.AddAsync(new Phrase
            {
                Term = phraseEntry.Text,
                Definition = definitionEntry.Text,
                Synonyms = synonymsEntry.Text?.Split(" "),
                Tags = tagsEntry.Text?.Split(" ")
            });
        await activeContext.SaveChangesAsync();
    }
}