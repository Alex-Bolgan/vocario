using ReCallVocabulary.Data_Access;

namespace ReCallVocabulary.Pages;

public partial class DictionaryOptionsPage : ContentPage
{
    private readonly DictionaryContext dictionaryContext;

    public DictionaryOptionsPage(DbContextManager dbContextManager)
    {
        dictionaryContext = dbContextManager.CurrentDictionaryContext;
        InitializeComponent();
    }
    private async void DeleteWordsButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(DeleteWordsPage));
    }

    private async void AddWordsButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddWordsPage));
    }

    private async void ShareDictionary_Clicked(object sender, EventArgs e)
    {
        await Share.Default.RequestAsync(new ShareFileRequest
        { File = new ShareFile(dictionaryContext.MyPath) });
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
            File.Delete(dictionaryContext.MyPath);
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