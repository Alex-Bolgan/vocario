using CommunityToolkit.Maui.Views;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ReCallVocabulary.Controls;
using ReCallVocabulary.Data_Access;

namespace ReCallVocabulary.Pages;

public partial class ChoosePriorityPopup : Popup
{
    private readonly bool isOnlyRecent;

    public DateTime FirstStartDate { get; set; } = DateTime.Now;

    public DateTime? SecondStartDate { get; set; } = null;

    private DateTime MinDate { get; set; } = PhraseService.GetPhraseById(PhraseService.GetMinId()).CreationDate;

    private DateTime FirstMaxDate { get; set; } = DateTime.Now;

    public DateTime EndDate { get; set; } = DateTime.Now;

    public ChoosePriorityPopup(bool isOnlyRecent)
    {
        this.isOnlyRecent = isOnlyRecent;
        InitializeComponent();
        Layout.HeightRequest = 400;
        Layout.WidthRequest = 400;

        DateTime[] dates = DateFileHandler.GetDates();

        if (dates[0] == DateTime.MinValue)
        {
            FirstStartDate = dates[1];
            EndDate = dates[2];
        }
        else
        {
            SecondPrioritySwitch.IsToggled = true;
            FirstStartDate = dates[1];
            SecondStartDate = dates[0];
            EndDate = dates[2];
        }

        FirstFormDatePicker.Date = FirstStartDate;

        if (SecondStartDate is null)
        {
            SecondFormDatePicker.Date = (DateTime)FirstStartDate;
        }
        else
        {
            SecondFormDatePicker.Date = (DateTime)SecondStartDate;
        }
        EndFormDatePicker.Date = EndDate;
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

    private async void StartGameButton_OnClicked(object? sender, EventArgs e)
    {
        if (SecondPrioritySwitch.IsToggled)
        {
            DateFileHandler.WriteDates(new DateTime[] { SecondStartDate ?? default, FirstStartDate,EndDate});
        }
        else
        {
            DateFileHandler.WriteDates(new DateTime[] {FirstStartDate, EndDate });
        }

        await Application.Current.MainPage.Navigation.PushAsync(new RecallGamePage(isOnlyRecent));
        this.Close();
    }
}