using ReCallVocabulary.Data_Access;
using System.ComponentModel;
namespace ReCallVocabulary.Pages;

public partial class RecallGamePage : ContentPage, INotifyPropertyChanged
{
    private List<int> phraseNumberList = new();

    private string term = string.Empty;

    private string definition = string.Empty;

    private int countWithThresholds = 1;

    private int firstPriorityId = Model.GetMinId();

    private int secondPriorityId = 0;

    private int endId = Model.GetMaxId();

    int randomNumber;

    Random random = new Random();

    private Func<int> generatingMethod;

    new public event PropertyChangedEventHandler? PropertyChanged;

    public string Term
    {
        get => term;
        set
        {
            term = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Term)));
        }
    }

    public string Definition
    {
        get => definition;
        set
        {
            definition = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Definition)));
        }
    }

    public RecallGamePage(bool isOnlyRecent)
    {
        InitializeComponent();
        BindingContext = this;

        if (Model.IsEmpty())
        {
            Definition = "Oops. It seems you have an empty dictionary. Try adding some words first";
            definitionLabel.IsVisible = true;
            StopButton.IsVisible = false;
            ToMainMenuButton.IsVisible = true;
        }

        DateTime[] dates = DateFileHandler.GetDates();

        //MinValue is default value
        if (dates[0] == DateTime.MinValue)
        {
            firstPriorityId = Model.GetFirstIdWithDate(dates[1]);
        }
        else
        {
            firstPriorityId = Model.GetFirstIdWithDate(dates[1]);
            secondPriorityId = Model.GetFirstIdWithDate(dates[0]);
        }

        endId = Model.GetMaxIdWithDate(dates[2]);

        termLabel.IsVisible = definitionLabel.IsVisible = true;

        generatingMethod = GenerateWith1Threshold;
        if (secondPriorityId != 0)
        {
            generatingMethod = GenerateWith2Thresholds;
        }

        if (isOnlyRecent)
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

    private void StopButton_Clicked(object sender, EventArgs e)
    {
        definitionLabel.IsVisible = false;
        termLabel.IsVisible = false;

        int uniqueCount = phraseNumberList.Distinct().Count();
        StatsService.UpdateRecalledNumber(phraseNumberList.Count,uniqueCount, DateTime.Now);
        Term = $"Congrats! You recalled {uniqueCount} ({phraseNumberList.Count} with duplicates)!";

        answerLabel.Text = "";
        StopButton.IsVisible = false;
        ToMainMenuButton.IsVisible = true;
    }

     void OnTapGestureRecognizerTappedRecent(object? sender, TappedEventArgs args)
     {
        if (termLabel.IsVisible)
        {
            do
            {
                randomNumber = random.Next(firstPriorityId, endId + 1);
            } while (!Model.PhraseExists(randomNumber));

            Phrase newPhrase = Model.GetPhraseById(randomNumber);
            Definition = newPhrase.Definition;
            Term = newPhrase.Term;
            termLabel.IsVisible = false;
            tagView.ItemsSource = newPhrase.Tags;
        }
        else
        {
            phraseNumberList.Add(randomNumber);
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

            phraseNumberList.Add(randomNumber);
            Phrase newPhrase = Model.GetPhraseById(randomNumber);
            Definition = newPhrase.Definition;
            Term = newPhrase.Term;
            tagView.ItemsSource = newPhrase.Tags;
            termLabel.IsVisible = false;
        }
        else
        {
            phraseNumberList.Add(randomNumber);
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