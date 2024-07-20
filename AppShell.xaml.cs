using ReCallVocabulary.Pages;

namespace ReCallVocabulary
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(DictionaryViewPage), typeof(DictionaryViewPage));
            Routing.RegisterRoute(nameof(DictionaryOptionsPage), typeof(DictionaryOptionsPage));
        }
    }
}
