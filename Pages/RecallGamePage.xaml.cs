using ReCallVocabulary.Data_Access;
using System.ComponentModel;
namespace ReCallVocabulary.Pages;

public partial class RecallGamePage : ContentPage, INotifyPropertyChanged
{
    private readonly bool isOnlyRecent;
    private string term;
    private string definition;
    private int countWithThresholds = 1;
    private int totalCount = 0;
    private int firstThresholdId;
    private int secondThresholdId;
    private int maxId;
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
        InitializeComponent();

        if (Model.IsEmpty())
        {
            Definition = "Oops. It seems you have an empty dictionary";
            definitionLabel.IsVisible = true;
            StopButton.Text = "Return to main menu";
        }
        else
        {
            this.isOnlyRecent = MainPage.IsOnlyRecent;
            termLabel.IsVisible = definitionLabel.IsVisible= true;

            DateOnly firstThresholdDate = DateOnly.FromDateTime(DateTime.Now).AddDays(-SettingsPage.firstThreshold * 7);
            firstThresholdId = Model.GetFirstIdWithDate(firstThresholdDate);
            generatingMethod = GenerateWith1Threshold;
            if (SettingsPage.secondThreshold != 0)
            {
                DateOnly secondThresholdDate = DateOnly.FromDateTime(DateTime.Now).AddDays(-SettingsPage.secondThreshold * 30);
                secondThresholdId = Model.GetFirstIdWithDate(secondThresholdDate);
                generatingMethod = GenerateWith2Thresholds;
            }
            maxId = Model.GetMaxId();
            BindingContext = this;

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

    }
    private void StopButton_Clicked(object sender, EventArgs e)
    {
        Definition = $"Congrats! You recalled {totalCount}";
        Term = "";
        Navigation.PopAsync();
    }
    void OnTapGestureRecognizerTappedRecent(object sender, TappedEventArgs args)
    {
        if (termLabel.IsVisible)
        {
            do
            {
                countWithThresholds = 1;
                randomNumber = random.Next(firstThresholdId, maxId + 1);
            } while (!Model.PhraseExists(randomNumber));
            randomNumber = random.Next(firstThresholdId, maxId+1);
            Definition = Model.GetPhraseById(randomNumber).Definition;
            Term = Model.GetPhraseById(randomNumber).Term;
            termLabel.IsVisible = false;
        }
        else
        {
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
            termLabel.IsVisible = true;
        }
    }
    private int GenerateWith1Threshold()
    {
        if (countWithThresholds == 3)
        {
            randomNumber = random.Next(firstThresholdId, maxId);
            countWithThresholds = 1;
        }
        else
        {
            randomNumber = random.Next(1, maxId+1);
            countWithThresholds++;
        }
        return randomNumber;
    }
    private int GenerateWith2Thresholds()
    {
        if (countWithThresholds == 3)
        {
            randomNumber = random.Next(firstThresholdId, maxId);
            countWithThresholds = 1;
        }
        else if(countWithThresholds==2)
        {
            randomNumber = random.Next(secondThresholdId, firstThresholdId);
            countWithThresholds++;
        }
        else
        {
            randomNumber = random.Next(1, maxId);
            countWithThresholds++;
        }
        return randomNumber;
    }
}