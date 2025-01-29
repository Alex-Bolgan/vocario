using Microsoft.EntityFrameworkCore;
using ReCallVocabulary.Data_Access;

namespace ReCallVocabulary
{
    public partial class App : Application
    {
        public App(MainPage page)
        {
            InitializeComponent();

            MainPage = page;
        }
    }
}
