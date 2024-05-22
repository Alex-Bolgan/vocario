namespace ReCallVocabulary.Pages;
using ReCallVocabulary.Data_Access;
public partial class ChooseDictionary : ContentPage
{
    private readonly List<DatabaseName> DatabaseNames = new List<DatabaseName>();
    DictionaryContext activeContext = App.Services.GetService<DictionaryContext>();

    public ChooseDictionary()
	{
        InitializeComponent();
        string currentDBName = Path.GetFileNameWithoutExtension(activeContext.MyPath);
        List<string> fullPathNames = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "*.*", SearchOption.TopDirectoryOnly)
            .Where(s => s.EndsWith(".db", StringComparison.OrdinalIgnoreCase))
            .ToList();
        for (int i = 0;i<fullPathNames.Count;++i)
        {
            fullPathNames[i] = Path.GetFileNameWithoutExtension(fullPathNames[i]);
            if (currentDBName == fullPathNames[i])
            {
                DatabaseNames.Add(new DatabaseName { Name = fullPathNames[i] , FontAttribute =FontAttributes.Bold});
            }
            else
                DatabaseNames.Add(new DatabaseName { Name = fullPathNames[i], FontAttribute = FontAttributes.None });
        }
        chooseDictionaryView.ItemsSource = DatabaseNames;

    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        Button buttonSender = (Button)sender;
        activeContext = new DictionaryContext(buttonSender.Text+".db");
    }

    private async void AddDictionaryButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddDictionaryPage());
    }
}
public class DatabaseName { 
    public string Name { get; set; }
    public FontAttributes FontAttribute { get; set; } = FontAttributes.None;
}