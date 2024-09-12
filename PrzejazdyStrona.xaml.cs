using System.Collections.ObjectModel;

namespace FirmaSpedycyjna;

public partial class PrzejazdyStrona : ContentPage
{
    public ObservableCollection<Przejazd> PrzejazdyList { get; set; } = new ObservableCollection<Przejazd>();
    private DatabaseService _databaseService;
    public PrzejazdyStrona()
	{
        _databaseService = new DatabaseService(this);
        InitializeComponent();
        BindingContext = this;
        LoadData();
    }

    public void LoadData()
    {
        PrzejazdyList.Clear();
        var query = "SELECT P.IDDostawy, P.IDZamowienia, PO.NumerRejestracyjny, N.NumerRejestracyjny, K.Imie, K.Nazwisko, P.DlugoscPrzejazdu, P.CzasPrzejazdu, P.CzasPracyKierowcy FROM Przejazdy AS P LEFT JOIN Pojazdy AS PO ON PO.IDPojazdu = P.IDPojazdu LEFT JOIN Naczepy AS N ON N.IDNaczepy = P.IDNaczepy LEFT JOIN Kierowcy AS K ON K.IDKierowcy = P.IDKierowcy";
        string[] queryResult = _databaseService.ExecuteSelectQuery(query);


        foreach (var rowData in queryResult)
        {
            var przejazd = new Przejazd(rowData);
            PrzejazdyList.Add(przejazd);
        }
    }

    public void LoadData(string OrderBy)
    {
        PrzejazdyList.Clear();
        var query = "SELECT P.IDDostawy, P.IDZamowienia, PO.NumerRejestracyjny, N.NumerRejestracyjny, K.Imie, K.Nazwisko, P.DlugoscPrzejazdu, P.CzasPrzejazdu, P.CzasPracyKierowcy FROM Przejazdy AS P LEFT JOIN Pojazdy AS PO ON PO.IDPojazdu = P.IDPojazdu LEFT JOIN Naczepy AS N ON N.IDNaczepy = P.IDNaczepy LEFT JOIN Kierowcy AS K ON K.IDKierowcy = P.IDKierowcy" + OrderBy;
        string[] queryResult = _databaseService.ExecuteSelectQuery(query);


        foreach (var rowData in queryResult)
        {
            var przejazd = new Przejazd(rowData);
            PrzejazdyList.Add(przejazd);
        }
    }

    private void OnLabelTapped(object sender, EventArgs e)
    {
        if (sender is Label label)
        {
            switch (label.Text)
            {
                case "ID":
                    LoadData(" ORDER BY IDDostawy");
                    break;
                case "ID Zamówienia":
                    LoadData(" ORDER BY IDZamowienia");
                    break;
                case "Pojazd":
                    LoadData(" ORDER BY PO.NumerRejestracyjny");
                    break;
                case "Naczepa":
                    LoadData(" ORDER BY N.NumerRejestracyjny");
                    break;
                case "Kierowca":
                    LoadData(" ORDER BY Imie, Nazwisko");
                    break;
                case "D³ugoœæ Trasy":
                    LoadData(" ORDER BY DlugoscPrzejazdu");
                    break;
                case "Czas Jazdy":
                    LoadData(" ORDER BY CzasPrzejazdu");
                    break;
                case "Czas Pracy Kierowcy":
                    LoadData(" ORDER BY CzasPracyKierowcy");
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

        var selectedPrzejazd = grid.BindingContext as Przejazd;
        if (selectedPrzejazd == null) return;

        var id = selectedPrzejazd.Id;

        var action = await DisplayActionSheet("Wybierz akcjê", "Anuluj", null, "Edytuj", "Usuñ");

        if (action == "Edytuj")
        {
            await Navigation.PushModalAsync(new EdytujPrzejazdyStrona(selectedPrzejazd, false));
        }
        else if (action == "Usuñ")
        {
            var confirm = await DisplayAlert("Potwierdzenie", "Czy na pewno chcesz usun¹æ ten rekord?", "Tak", "Nie");
            if (confirm)
            {
                string query = "DELETE FROM Przejazdy WHERE IdDostawy = " + id;
                _databaseService.ExecuteGeneralQuery(query);

                LoadData();
            }
        }
    }


    private async void OnDodajZamowienieClicked(object sender, EventArgs e)
    {
        Przejazd nowyPrzejazd = new Przejazd();
        await Navigation.PushModalAsync(new EdytujPrzejazdyStrona(nowyPrzejazd, true));
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

    private void OnChangeToPrzejazdyClicked(object sender, EventArgs e)
    {
        LoadData();
    }

    private async void OnChangeToPojazdyClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PojazdyStrona(), animated: false);
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
