using ReCallVocabulary.Data_Access;

namespace ReCallVocabulary.Pages;

public partial class SettingsPage : ContentPage
{
	private DateOnly endDate;

	public static readonly string datesListFile = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Dates list.txt");

    public DateOnly FirstStartDate { get; set; }
	public DateOnly SecondStartDate { get; set; }
	public DateOnly MinDate { get; set; } = Model.GetPhraseById(Model.GetMinId()).CreationDate;
	public DateOnly FirstMaxDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
	public DateOnly EndDate { 
		get=>endDate; 
		set {
			endDate = value;
			SecondEndDate.Date = new DateTime(endDate,new TimeOnly()); 
		}
	}
	public SettingsPage()
	{
		InitializeComponent();

		bool isValidDate = true;
		DateOnly tempDate0 = new();
		DateOnly tempDate1 = new();
		DateOnly tempDate2 = new();

		if(!File.Exists(datesListFile))
		{
			isValidDate = false;
		}
		else
		{
			var fileContent = File.ReadAllText(datesListFile).Split(" ");

			if (fileContent.Length == 2)
			{
				if (DateOnly.TryParse(fileContent[0],out tempDate0) && DateOnly.TryParse(fileContent[1], out tempDate1))
				{
					FirstStartDate = tempDate0;
					endDate = tempDate1;
					isValidDate = true;
				}
			}
			else if(fileContent.Length == 3)
			{
                if (DateOnly.TryParse(fileContent[0], out tempDate0) && DateOnly.TryParse(fileContent[1], out tempDate1)
    && DateOnly.TryParse(fileContent[2], out tempDate2))
                {
                    FirstStartDate = tempDate0;
                    SecondStartDate = tempDate1;
                    endDate = tempDate2;
                    isValidDate = true;
                }
            }
		}

        if (!isValidDate)
        {
            var myFile = File.Create(datesListFile);
            myFile.Close();
			SecondStartDate = FirstStartDate = DateOnly.FromDateTime(DateTime.Now);
            File.WriteAllText(datesListFile, $"{FirstStartDate} {FirstStartDate}");
        }
    }
}