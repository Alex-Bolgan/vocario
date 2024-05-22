using ReCallVocabulary.Pages;

namespace ReCallVocabulary
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(DictionaryView), typeof(DictionaryView));

        }

    }
}
