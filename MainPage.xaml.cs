using ReCallVocabulary.Data_Access;
using ReCallVocabulary.Pages;
using CommunityToolkit.Maui.Views;
namespace ReCallVocabulary
{
    public partial class MainPage : ContentPage
    {
        private DictionaryContext dictionaryContext;
        
        private StatsContext statsContext;

        private StatsService statsService;

        private PhraseService phraseService;
        public MainPage()
        {
            statsContext = ServiceHelper.GetService<DbContextManager>().CurrentStatsContext;
            dictionaryContext = ServiceHelper.GetService<DbContextManager>().CurrentDictionaryContext;

            InitializeComponent();
            if (!Directory.Exists(Path.GetDirectoryName(dictionaryContext.MyPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(dictionaryContext.MyPath)!);
            }

            if (!File.Exists(dictionaryContext.MyPath))
            {
                var myFile = File.Create(dictionaryContext.MyPath);
                myFile.Close();
            }

            if (!Directory.Exists(Path.GetDirectoryName(statsContext.MyPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(statsContext.MyPath)!);
            }

            if (!File.Exists(statsContext.MyPath))
            {
                var myFile = File.Create(statsContext.MyPath);
                myFile.Close();
            }

            dictionaryContext.Database.EnsureCreated();
            statsContext.Database.EnsureCreated();
        }
        private async void Recall_Clicked(object sender, EventArgs e)
        {
            Popup popup = new ChoosePriorityPopup(false);
            await Application.Current.MainPage.ShowPopupAsync(popup);

            popup.Close();
        }
        private async void RecallRecent_Clicked(object sender, EventArgs e)
        {
            Popup popup = new ChoosePriorityPopup(true);
            await Application.Current.MainPage.ShowPopupAsync(popup);

            popup.Close();
        }
        private async void Addwords_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Pages.AddWordsPage());
        }
        private async void SeeDictionary_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Pages.DictionaryViewPage());
        }
    }
}
