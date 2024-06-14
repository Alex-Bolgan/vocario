using ReCallVocabulary.Data_Access;
using System.ComponentModel;
using System.Globalization;
namespace ReCallVocabulary.Pages;

public partial class RecallGamePage : ContentPage, INotifyPropertyChanged
{
    private readonly string datesListFile = SettingsPage.datesListFile;
    private readonly bool isOnlyRecent;
    private string term;
    private string definition;
    private int countWithThresholds = 1;
    private int totalCount = 0;
    private int firstPriorityId = Model.GetMinId();
    private int secondPriorityId = 0;
    private int endId = Model.GetMaxId();
    int randomNumber;
    Random random = new Random();

    private Func<int> generatingMethod;

    public event PropertyChangedEventHandler PropertyChanged;

    public string Term
    {
        get => term;
        set
        {
            term = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Term"));
        }
    }
    public string Definition
    {
        get => definition;
        set
        {
            definition = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Definition"));
        }
    }
    public RecallGamePage()
    {
        if (datesListFile is null)
        {
#if WINDOWS
        datesListFile = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Dates_list.txt");
#elif ANDROID
            datesListFile = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Dates_list.txt");
#endif        
        }

        InitializeComponent();
        BindingContext = this;

        bool isValidDate = false;
        DateTime tempDate0 = new();
        DateTime tempDate1 = new();
        DateTime tempDate2 = new();

        if (Model.IsEmpty())
        {
            Definition = "Oops. It seems you have an empty dictionary";
            definitionLabel.IsVisible = true;
            StopButton.IsVisible = false;
            ToMainMenuButton.IsVisible = true;
        }
        if (File.Exists(datesListFile))
        {
            var fileContent = File.ReadAllText(datesListFile).Split(" ");

            if (fileContent.Length == 2)
            {
                if (DateTime.TryParseExact(fileContent[0], "dd.MM.yyyy", CultureInfo.InvariantCulture,
                           DateTimeStyles.None, out tempDate0) && 
                           DateTime.TryParseExact(fileContent[1], "dd.MM.yyyy", CultureInfo.InvariantCulture,
                           DateTimeStyles.None, out tempDate1))
                {
                    firstPriorityId = Model.GetFirstIdWithDate(tempDate0);
                    endId = Model.GetFirstIdWithDate(tempDate1);
                    isValidDate = true;
                }
            }
            else if (fileContent.Length == 3)
            {
                if (DateTime.TryParseExact(fileContent[0], "dd.MM.yyyy", CultureInfo.InvariantCulture,
                           DateTimeStyles.None, out tempDate0) 
                    && DateTime.TryParseExact(fileContent[1], "dd.MM.yyyy", CultureInfo.InvariantCulture,
                           DateTimeStyles.None, out tempDate1) 
                    && DateTime.TryParseExact(fileContent[2], "dd.MM.yyyy", CultureInfo.InvariantCulture,
                           DateTimeStyles.None, out tempDate2))
                {
                    firstPriorityId = Model.GetFirstIdWithDate(tempDate0);
                    secondPriorityId = Model.GetFirstIdWithDate(tempDate1);
                    endId = Model.GetFirstIdWithDate(tempDate2);
                    isValidDate = true;
                }
            }
        }

        if (isValidDate)
        {
            this.isOnlyRecent = MainPage.IsOnlyRecent;
            termLabel.IsVisible = definitionLabel.IsVisible = true;

            generatingMethod = GenerateWith1Threshold;
            if (secondPriorityId != 0)
            {
                generatingMethod = GenerateWith2Thresholds;
            }

            if (this.isOnlyRecent)
            {
                tapRecognizer.Tapped += OnTapGestureRecognizerTappedRecent;
                OnTapGestureRecognizerTappedRecent(this, new TappedEventArgs(""));
            }
            else
            {
                tapRecognizer.Tapped += OnTapGestureRecognizerTapped;
                OnTapGestureRecognizerTapped(this, new TappedEventArgs(""));
            }
        }
        else
        {
            var myFile = File.Create(datesListFile);
            myFile.Close();
            DateTime firstStartDate = Model.GetPhraseById(firstPriorityId).CreationDate;
            DateTime endDate = Model.GetPhraseById(endId).CreationDate;
            File.WriteAllText(datesListFile, $"{firstStartDate,0:dd.MM.yyyy} {endDate,0:dd.MM.yyyy}");
        }
    }

    private void StopButton_Clicked(object sender, EventArgs e)
    {
        Definition = $"Congrats! You recalled {totalCount}!";
        Term = "";
        StopButton.IsVisible = false;
        ToMainMenuButton.IsVisible = true;
    }
    void OnTapGestureRecognizerTappedRecent(object sender, TappedEventArgs args)
    {
        if (termLabel.IsVisible)
        {
            do
            {
                randomNumber = random.Next(firstPriorityId, endId + 1);
            } while (!Model.PhraseExists(randomNumber));

            randomNumber = random.Next(firstPriorityId, endId + 1);
            Definition = Model.GetPhraseById(randomNumber).Definition;
            Term = Model.GetPhraseById(randomNumber).Term;
            termLabel.IsVisible = false;
        }
        else
        {
            totalCount++;
            termLabel.IsVisible = true;
        }
    }
    private void OnTapGestureRecognizerTapped(object? sender, TappedEventArgs e)
    {
        if (termLabel.IsVisible)
        {
            do
            {
                countWithThresholds = 1;
                randomNumber = generatingMethod();
            } while (!Model.PhraseExists(randomNumber));

            Definition = Model.GetPhraseById(randomNumber).Definition;
            Term = Model.GetPhraseById(randomNumber).Term;
            termLabel.IsVisible = false;
        }
        else
        {
            totalCount++;
            termLabel.IsVisible = true;
        }
    }
    private int GenerateWith1Threshold()
    {
        if (countWithThresholds == 3)
        {
            randomNumber = random.Next(firstPriorityId, endId);
            countWithThresholds = 1;
        }
        else
        {
            randomNumber = random.Next(1, endId + 1);
            countWithThresholds++;
        }
        return randomNumber;
    }
    private int GenerateWith2Thresholds()
    {
        if (countWithThresholds == 3)
        {
            randomNumber = random.Next(firstPriorityId, endId);
            countWithThresholds = 1;
        }
        else if (countWithThresholds == 2)
        {
            randomNumber = random.Next(secondPriorityId, firstPriorityId);
            countWithThresholds++;
        }
        else
        {
            randomNumber = random.Next(1, endId);
            countWithThresholds++;
        }
        return randomNumber;
    }

    private async void ToMainMenuButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}