namespace ReCallVocabulary.Pages;
using ReCallVocabulary.Data_Access;
using System.ComponentModel;

public partial class PhraseViewPage : ContentPage, INotifyPropertyChanged
{
    new public event PropertyChangedEventHandler? PropertyChanged;

    private readonly List<string> tagList = Model.GetTags();

    private Phrase CurrentPhrase { get; set; }
    public string Term
    {
        get => CurrentPhrase.Term!;
        set
        {
            CurrentPhrase.Term = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Term)));
        }
    }
    public string Definition
    {
        get => CurrentPhrase.Definition;
        set
        {
            CurrentPhrase.Definition = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Definition)));
        }
    }
    public string[] Synonyms
    {
        get => CurrentPhrase.Synonyms!;
        set
        {
            CurrentPhrase.Synonyms = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Synonyms"));
        }
    }
    public DateTime CreationDate
    {
        get => CurrentPhrase.CreationDate;
    }
    public string Tags
    {
        get
        {
            if(CurrentPhrase.Tags is not null)
            {
                return string.Join(' ', CurrentPhrase.Tags);
            }
            else
            {
                return string.Empty;
            }
        }
        set
        {
            CurrentPhrase.Tags = value.Split(' ');
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Tags"));
        }
    }

    public PhraseViewPage(Phrase phrase)
	{
        CurrentPhrase = phrase;
        BindingContext = this;
        InitializeComponent();
        tags.ItemsSource = tagList;
	}

    private void SaveChangesButton_Clicked(object sender, EventArgs e)
    {
        Model.UpdatePhraseAsync(CurrentPhrase);
    }

    private void TagEntry_Focused(object sender, FocusEventArgs e)
    {
        tags.IsVisible = AddTagButton.IsVisible = true;
    }

    private void AddTagButton_Clicked(object sender, EventArgs e)
    {
        TagEntry.Text += " ";
        tagList.Add(TagEntry.Text.Split(" ").Last());
    }

    private void tags_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        string? item;

        if (e.CurrentSelection.Count > 0 && (item = e.CurrentSelection[0] as string) is not null)
        {
            Tags += " " + item;
        }
    }
}