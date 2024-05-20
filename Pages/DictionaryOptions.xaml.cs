using ReCallVocabulary.Data_Access;

namespace ReCallVocabulary.Pages;

public partial class DictionaryOptions : ContentPage
{
	public DictionaryOptions()
	{
		InitializeComponent();
	}
    private void DeleteWordsButton_Clicked(object sender, EventArgs e)
    {
    }

    private async void AddWordsButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddPage());

    }

    private async void ShareDictionary_Clicked(object sender, EventArgs e)
    {
        await Share.Default.RequestAsync(new ShareFileRequest
        { File = new ShareFile(App.Services.GetService<DictionaryContext>().MyPath) });
    }

    private void GenerateDefinitionsButton_Clicked(object sender, EventArgs e)
    {

    }

    private void ChooseDictionaryButton_Clicked(object sender, EventArgs e)
    {

    }

    private void DeleteDictionaryButton_Clicked(object sender, EventArgs e)
    {

    }
}