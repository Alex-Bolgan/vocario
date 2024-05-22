using ReCallVocabulary.Data_Access;
using ReCallVocabulary.Pages;
namespace ReCallVocabulary
{
    public partial class MainPage : ContentPage
    {
        private DictionaryContext? activeContext = App.ActiveContext;
        public MainPage()
        {
            InitializeComponent();
            activeContext?.Database.EnsureCreated();
        }
        private void Recall_Clicked(object sender, EventArgs e)
        {
            //TODO
        }
        private void RecallRecent_Clicked(object sender, EventArgs e)
        {
            //TODO
        }
        private async void Addwords_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Pages.AddWordsPage());
        }
        private async void SeeDictionary_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(DictionaryView));
        }
    }
}
