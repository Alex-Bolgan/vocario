using ReCallVocabulary.Data_Access;
using ReCallVocabulary.Pages;
namespace ReCallVocabulary
{
    public partial class MainPage : ContentPage
    {
        private DictionaryContext activeContext = App.ActiveContext ??
        throw new ArgumentNullException(nameof(activeContext));
        public static bool IsOnlyRecent { get; set; } = false;
        public MainPage()
        {
            InitializeComponent();
            if (!Directory.Exists(Path.GetDirectoryName(activeContext.MyPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(activeContext.MyPath)!);
            }

            if (!File.Exists(activeContext.MyPath))
            {
                var myFile = File.Create(activeContext.MyPath);
                myFile.Close();
            }
            activeContext?.Database.EnsureCreated();
        }
        private void Recall_Clicked(object sender, EventArgs e)
        {
            IsOnlyRecent = false;
            Navigation.PushAsync(new RecallGamePage());
        }
        private void RecallRecent_Clicked(object sender, EventArgs e)
        {
            IsOnlyRecent = true;
            Navigation.PushAsync(new RecallGamePage());
        }
        private async void Addwords_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Pages.AddWordsPage());
        }
        private async void SeeDictionary_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(DictionaryViewPage));
        }
    }
}
