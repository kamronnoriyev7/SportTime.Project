namespace SportTime.Pages;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
    }

    private async void OnSearchClicked(object sender, EventArgs e)
    {
        string sportType = SportTypePicker.SelectedItem?.ToString();
        string sortOption = SortPicker.SelectedItem?.ToString();

        if (string.IsNullOrEmpty(sportType) || string.IsNullOrEmpty(sortOption))
        {
            await DisplayAlert("Xatolik", "Iltimos, barcha maydonlarni tanlang.", "OK");
            return;
        }

        // Natija sahifasiga yuboramiz
        await Navigation.PushAsync(new ResultPage(sportType, sortOption));
    }
}
