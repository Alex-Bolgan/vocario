namespace ReCallVocabulary.Pages;

public partial class SettingsPage : ContentPage
{
	public static int firstThreshold { get; set; }=2;
	public static int secondThreshold { get; set; }
    int sliderIncrement = 2;
	public SettingsPage()
	{
		InitializeComponent();
	}

    private void FirstThresholdSlider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
            Slider slider = (Slider)sender;

            double relativeValue = slider.Value - slider.Minimum;

            if (relativeValue % sliderIncrement == 0)
            {
                firstThresholdLabel.Text = slider.Value.ToString();
            }
    }
    private void SecondThresholdSlider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        Slider slider = (Slider)sender;

        double relativeValue = slider.Value - slider.Minimum;

        if (relativeValue % sliderIncrement == 0)
        {

            secondThresholdLabel.Text = slider.Value.ToString();
        }
    }
}