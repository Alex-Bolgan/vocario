namespace ReCallVocabulary.Pages;
using ReCallVocabulary.Data_Access;
using System.ComponentModel;

public partial class PhraseViewPage : ContentPage, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    private Phrase CurrentPhrase { get; set; }
    public string Term
    {
        get => CurrentPhrase.Term;
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
        get => CurrentPhrase.Synonyms;
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
    public string[] Tags
    {
        get => CurrentPhrase.Tags;
        set
        {
            CurrentPhrase.Tags = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Tags"));
        }
    }
    public PhraseViewPage(int id)
	{
        CurrentPhrase = Model.GetPhraseById(id);
        BindingContext = this;
        InitializeComponent();
	}

    private void SaveChangesButton_Clicked(object sender, EventArgs e)
    {
        Model.UpdatePhrase(CurrentPhrase);
    }
}