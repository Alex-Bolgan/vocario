using ReCallVocabulary.Data_Access;

namespace ReCallVocabulary.Pages;
public partial class AddWordsPage : ContentPage
{
    private readonly DictionaryContext activeContext = (App.ActiveContext ?? throw new ArgumentNullException(nameof(activeContext)));

    private readonly List<string> tagList = PhraseService.GetTags();

    public AddWordsPage()
    {
        InitializeComponent();
        Tags.ItemsSource = tagList;
    }

    private void TagEntry_Focused(object sender, FocusEventArgs e)
    {
        Tags.IsVisible = AddTagButton.IsVisible = true;
    }

    private void AddTagButton_Clicked(object sender, EventArgs e)
    {
        TagEntry.Text += " ";
        tagList.Add(TagEntry.Text.Split(" ").Last());
    }

    private void Tags_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        string? item;

        if (e.CurrentSelection.Count > 0 && (item = e.CurrentSelection[0] as string) is not null)
        {
            TagEntry.Text += " " + item;
        }
    }

    private async void AddButton_Clicked(object sender, EventArgs e)
    {
        activeContext.Database.EnsureCreated();
        if (!String.IsNullOrWhiteSpace(phraseEntry.Text) && !String.IsNullOrWhiteSpace(definitionEntry.Text))
        {
            await activeContext.Phrases.AddAsync(new Phrase
            {
                Term = phraseEntry.Text,
                Definition = definitionEntry.Text,
                Synonyms = synonymsEntry.Text?.Split(" "),
                Tags = TagEntry.Text?.Split(" ")
            });
        }

        await activeContext.SaveChangesAsync();
    }

    protected override void OnDisappearing()
    {
        StatsService.UpdateAddedNumber();
    }
}