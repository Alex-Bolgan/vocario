namespace ReCallVocabulary.Pages;
using ReCallVocabulary.Data_Access;
public partial class PhraseViewPage : ContentPage
{
    public int Id { get; set; }
    public string? Term { get; set;}
    public string? Definition { get; set; }
    public string[]? Synonyms { get; set; }
    public string[]? Tags { get; set; }
    public DateOnly CreationDate { get; set; }
    public PhraseViewPage(int id)
	{
		InitializeComponent();
        Phrase phrase = Model.GetPhraseById(id);
        Id = id;
        Term = phrase.Term;
        Definition = phrase.Definition;
        Synonyms = phrase.Synonyms;
        Tags = phrase.Tags;
        CreationDate = phrase.CreationDate;
	}
    private void Editor_Completed(object sender, EventArgs e)
    {
        Model.UpdatePhrase(new Phrase { Id=this.Id,Term=this.Term, 
            Definition = this.Definition, Synonyms=this.Synonyms,Tags=this.Tags});
    }
}