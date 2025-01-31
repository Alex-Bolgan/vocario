namespace ReCallVocabulary.Pages;
using ReCallVocabulary.Data_Access;
public partial class ChooseDictionary : ContentPage
{
    private readonly List<DatabaseName> DatabaseNames = [];
    private DictionaryContext dictionaryContext;

    public ChooseDictionary(DbContextManager dbContextManager)
    {
        dictionaryContext = dbContextManager.CurrentDictionaryContext; 

        InitializeComponent();
        string currentDBName = Path.GetFileNameWithoutExtension(dictionaryContext.MyPath);
        List<string> fullPathNames = new List<string>();
#if ANDROID
        fullPathNames = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.Personal),
            "*.*", SearchOption.TopDirectoryOnly)
            .Where(s => s.EndsWith(".db", StringComparison.OrdinalIgnoreCase))
            .ToList();
#elif WINDOWS
        fullPathNames = Directory.GetFiles(
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),"ReCallVocabulary"),
            "*.*", SearchOption.TopDirectoryOnly)
            .Where(s => s.EndsWith(".db", StringComparison.OrdinalIgnoreCase))
            .ToList();
#endif
        for (int i = 0;i<fullPathNames.Count;++i)
        {
            fullPathNames[i] = Path.GetFileNameWithoutExtension(fullPathNames[i]);
            if (currentDBName == fullPathNames[i])
                DatabaseNames.Add(new DatabaseName { Name = fullPathNames[i] , FontAttribute =FontAttributes.Bold});
            else
                DatabaseNames.Add(new DatabaseName { Name = fullPathNames[i], FontAttribute = FontAttributes.None });
        }

        chooseDictionaryView.ItemsSource = DatabaseNames;

    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        Button buttonSender = (Button)sender;
        dictionaryContext = new DictionaryContext(buttonSender.Text+".db");
        await Navigation.PopAsync();
    }

    private async void AddDictionaryButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddDictionaryPage());
    }
}
public class DatabaseName {
    public required string Name { get; set; }
    public FontAttributes FontAttribute { get; set; } = FontAttributes.None;
}