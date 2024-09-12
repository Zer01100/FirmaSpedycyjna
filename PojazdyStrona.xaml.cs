using System.Collections.ObjectModel;

namespace FirmaSpedycyjna;

public partial class PojazdyStrona : ContentPage
{
    public ObservableCollection<Pojazd> PojazdyList { get; set; } = new ObservableCollection<Pojazd>();
    private DatabaseService _databaseService;
    public PojazdyStrona()
	{
        _databaseService = new DatabaseService(this);
        InitializeComponent();
        BindingContext = this;
        LoadData();
    }

    public void LoadData()
    {
        PojazdyList.Clear();
        var query = "SELECT * FROM Pojazdy";
        string[] queryResult = _databaseService.ExecuteSelectQuery(query);


        foreach (var rowData in queryResult)
        {
            var pojazd = new Pojazd(rowData);
            PojazdyList.Add(pojazd);
        }
    }
    public void LoadData(string OrderBy)
    {
        PojazdyList.Clear();
        var query = "SELECT * FROM Pojazdy" + OrderBy;
        string[] queryResult = _databaseService.ExecuteSelectQuery(query);


        foreach (var rowData in queryResult)
        {
            var pojazd = new Pojazd(rowData);
            PojazdyList.Add(pojazd);
        }
    }

    private void OnLabelTapped(object sender, EventArgs e)
    {
        if (sender is Label label)
        {
            switch (label.Text)
            {
                case "ID":
                    LoadData(" ORDER BY IDPojazdu");
                    break;
                case "Numer Rejestracyjny":
                    LoadData(" ORDER BY NumerRejestracyjny");
                    break;
                case "Wymagane Uprawnienia":
                    LoadData(" ORDER BY WymaganeUprawnienia");
                    break;
                case "Typ Pojazdu":
                    LoadData(" ORDER BY TypPojazdu");
                    break;
                case "Maksymalna Masa":
                    LoadData(" ORDER BY MaxMasa");
                    break;
                case "Maksymalna D£ugoœæ":
                    LoadData(" ORDER BY MaxDlugosc");
                    break;
                case "Maksymalna Szerokoœæ":
                    LoadData(" ORDER BY MaxSzerokosc");
                    break;
                case "Maksymalna Wysokoœæ":
                    LoadData(" ORDER BY MaxWysokosc");
                    break;
                default:
                    break;
            }
        }
    }

    private void OnLabelPointerEntered(object sender, PointerEventArgs e)
    {
        var label = sender as Label;
        if (label != null)
        {
            label.TextColor = Color.FromRgb(162, 168, 168);
        }
    }

    private void OnLabelPointerExited(object sender, PointerEventArgs e)
    {
        var label = sender as Label;
        if (label != null)
        {
            label.TextColor = Color.FromRgb(142, 148, 148);
        }
    }

    private async void OnGridTapped(object sender, EventArgs e)
    {
        var grid = sender as Grid;
        if (grid == null) return;

        var selectedPojazd = grid.BindingContext as Pojazd;
        if (selectedPojazd == null) return;

        var id = selectedPojazd.Id;

        var action = await DisplayActionSheet("Wybierz akcjê", "Anuluj", null, "Edytuj", "Usuñ");

        if (action == "Edytuj")
        {
            await Navigation.PushModalAsync(new EdytujPojazdyStrona(selectedPojazd, false));
        }
        else if (action == "Usuñ")
        {
            var confirm = await DisplayAlert("Potwierdzenie", "Czy na pewno chcesz usun¹æ ten rekord?", "Tak", "Nie");
            if (confirm)
            {
                string query = "DELETE FROM Pojazdy WHERE IdPojazdu = " + id;
                _databaseService.ExecuteGeneralQuery(query);

                LoadData();
            }
        }
    }

    private async void OnDodajPojazdClicked(object sender, EventArgs e)
    {
        Pojazd nowyPojazd = new Pojazd();
        await Navigation.PushModalAsync(new EdytujPojazdyStrona(nowyPojazd, true));
    }

    private async void OnChangeToKlienciClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new KlienciStrona(), animated: false);
    }

    private async void OnChangeToZamowieniaClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ZamowieniaStrona(), animated: false);
    }

    private async void OnChangeToKierowcyClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new KierowcyStrona(), animated: false);
    }

    private async void OnChangeToPrzejazdyClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PrzejazdyStrona(), animated: false);
    }

    private void OnChangeToPojazdyClicked(object sender, EventArgs e)
    {
        LoadData();
    }

    private async void OnChangeToNaczepyClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NaczepyStrona(), animated: false);
    }

    private void OnPointerEntered(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button != null)
        {
            button.BackgroundColor = Color.FromArgb("#2b2d2d");
        }
    }

    private void OnPointerExited(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button != null)
        {
            button.BackgroundColor = Color.FromArgb("#383b3b");
        }
    }

    private void OnPointerGridEntered(object sender, EventArgs e)
    {
        var grid = sender as Grid;
        if (grid != null)
        {
            grid.BackgroundColor = Color.FromArgb("#3a3c3c");
        }
    }

    private void OnPointerGridExited(object sender, EventArgs e)
    {
        var grid = sender as Grid;
        if (grid != null)
        {
            grid.BackgroundColor = Color.FromArgb("#181919");
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadData();
    }
}