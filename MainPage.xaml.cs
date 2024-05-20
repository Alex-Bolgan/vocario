using ReCallVocabulary.Data_Access;
namespace ReCallVocabulary
{
    public partial class MainPage : ContentPage
    {
        internal DictionaryContext? _activeContext;
        public MainPage()
        {
            InitializeComponent();
            
            _activeContext = App.Services.GetService<DictionaryContext>();
            _activeContext?.Database.EnsureCreated();
        }
        private void Recall_Clicked(object sender, EventArgs e)
        {
            //TODO
        }
        private void RecallRecent_Clicked(object sender, EventArgs e)
        {
            //TODO
        }
        private void Addwords_Clicked(object sender, EventArgs e)
        {
            //TODO
        }
        private void SeeDictionary_Clicked(object sender, EventArgs e)
        {
            //TODO
        }
    }
}
