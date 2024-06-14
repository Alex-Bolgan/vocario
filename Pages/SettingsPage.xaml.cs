using ReCallVocabulary.Data_Access;
using System.ComponentModel;

namespace ReCallVocabulary.Pages;

public partial class SettingsPage : ContentPage, INotifyPropertyChanged
{
    public static string datesListFile;

    public DateTime FirstStartDate { get; set; } = DateTime.Now;

    public DateTime SecondStartDate { get; set; } = DateTime.Now;

    private DateTime MinDate { get; set; } = Model.GetPhraseById(Model.GetMinId()).CreationDate;

    private DateTime FirstMaxDate { get; set; } = DateTime.Now;

    public DateTime EndDate { get; set; } = DateTime.Now;

    public SettingsPage()
    {
        InitializeComponent();
#if WINDOWS
        datesListFile = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Dates_list.txt");
#elif ANDROID
        datesListFile = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Dates_list.txt");
#endif
        bool isValidDate = false;
        DateTime tempDate0 = new();
        DateTime tempDate1 = new();
        DateTime tempDate2 = new();

        if (File.Exists(datesListFile))
        {
            var fileContent = File.ReadAllText(datesListFile).Split(" ");

            if (fileContent.Length == 2)
            {
                if (DateTime.TryParse(fileContent[0], out tempDate0) && DateTime.TryParse(fileContent[1], out tempDate1))
                {
                    FirstStartDate = tempDate0;
                    EndDate = tempDate1;
                    isValidDate = true;
                }
            }
            else if (fileContent.Length == 3)
            {
                if (DateTime.TryParse(fileContent[0], out tempDate0) && DateTime.TryParse(fileContent[1], out tempDate1)
    && DateTime.TryParse(fileContent[2], out tempDate2))
                {
                    SecondPrioritySwitch.IsToggled = true;
                    FirstStartDate = tempDate0;
                    SecondStartDate = tempDate1;
                    EndDate = tempDate2;
                    isValidDate = true;
                }
            }
        }

        if (!isValidDate)
        {
            var myFile = File.Create(datesListFile);
            myFile.Close();
            EndDate = FirstStartDate = DateTime.Now;
            File.WriteAllText(datesListFile, $"{FirstStartDate,0:dd.MM.yyyy} {EndDate,0:dd.MM.yyyy}");
        }

        FirstFormDatePicker.Date = FirstStartDate;
        SecondFormDatePicker.Date = SecondStartDate;
        EndFormDatePicker.Date = EndDate;
    }
    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        if (SecondPrioritySwitch.IsToggled == true)
        {
            File.WriteAllText(datesListFile, $"{FirstStartDate,0:dd.MM.yyyy} {SecondStartDate,0:dd.MM.yyyy} {EndDate,0:dd.MM.yyyy}");
        }
        else
        {
            File.WriteAllText(datesListFile, $"{FirstStartDate,0:dd.MM.yyyy} {EndDate,0:dd.MM.yyyy}");
        }
    }

    private void FirstFormDatePicker_DateSelected(object sender, DateChangedEventArgs e)
    {
        FirstStartDate = e.NewDate;
    }

    private void SecondFormDatePicker_DateSelected(object sender, DateChangedEventArgs e)
    {
        SecondStartDate = e.NewDate;
    }

    private void EndFormDatePicker_DateSelected(object sender, DateChangedEventArgs e)
    {
        EndDate = e.NewDate;
        SecondEndDate.IsEnabled = true;
        SecondEndDate.Date = EndDate;
        SecondEndDate.IsEnabled = false;
    }
}