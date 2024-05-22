namespace ReCallVocabulary.Pages;
using ReCallVocabulary.Data_Access;

public partial class AddDictionaryPage : ContentPage
{
	public AddDictionaryPage()
	{
		InitializeComponent();
	}

    private async void addDictionaryButton_Clicked(object sender, EventArgs e)
    {
		File.Create(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
			newDictionaryNameEntry.Text+".db"));
		await Navigation.PopAsync();
    }
}