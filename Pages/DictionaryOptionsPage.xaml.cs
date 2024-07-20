using ReCallVocabulary.Data_Access;

namespace ReCallVocabulary.Pages;

public partial class DictionaryOptionsPage : ContentPage
{
    private DictionaryContext? activeContext = App.ActiveContext;

    public DictionaryOptionsPage()
    {
        InitializeComponent();
    }
    private async void DeleteWordsButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new DeleteWordsPage());
    }

    private async void AddWordsButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddWordsPage());
    }

    private async void ShareDictionary_Clicked(object sender, EventArgs e)
    {
        await Share.Default.RequestAsync(new ShareFileRequest
        { File = new ShareFile(activeContext.MyPath) });
    }

    private void GenerateDefinitionsButton_Clicked(object sender, EventArgs e)
    {
        //TODO: secondary
    }

    private async void ChooseDictionaryButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ChooseDictionary());
    }

    private async void DeleteDictionaryButton_Clicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Confirm deletion", "Are you sure you want to delete your dictionary? ", "Yes", "No");
        if (answer)
        {
            File.Delete(activeContext.MyPath);
        }
    }

    private void MergeWithButton_Clicked(object sender, EventArgs e)
    {
        //TODO: secondary
    }

    private async void GetDictionaryFromDevice_Clicked(object sender, EventArgs e)
    {
        await Controls.DbFilePicker.PickAndMoveDb();
    }
}