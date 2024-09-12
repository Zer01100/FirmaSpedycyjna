using System.Collections.ObjectModel;

namespace FirmaSpedycyjna;

public partial class ZamowieniaStrona : ContentPage
{
    public ObservableCollection<Zamowienie> ZamowieniaList { get; set; } = new ObservableCollection<Zamowienie>();
    private DatabaseService _databaseService;
    public ZamowieniaStrona()
	{
        _databaseService = new DatabaseService(this);
        InitializeComponent();
        BindingContext = this;
        LoadData();
    }

    public void LoadData()
    {
        ZamowieniaList.Clear();
        var query = "SELECT Z.IDZamowienia, Z.DataZamowienia, Z.AdresPoczatkowy, Z.AdresDocelowy, Z.Towar, Z.Masa, Z.Dlugosc, Z.Szerokosc, Z.Wysokosc, Z.Status,Z.IDKlienta, K.Nazwa FROM Zamowienia AS Z LEFT JOIN Klienci AS K ON K.IDKlienta = Z.IDKlienta";
        string[] queryResult = _databaseService.ExecuteSelectQuery(query);


        foreach (var rowData in queryResult)
        {
            var zamowienie = new Zamowienie(rowData);
            ZamowieniaList.Add(zamowienie);
        }
    }

    public void LoadData(string OrderBy)
    {
        ZamowieniaList.Clear();
        var query = "SELECT Z.IDZamowienia, Z.DataZamowienia, Z.AdresPoczatkowy, Z.AdresDocelowy, Z.Towar, Z.Masa, Z.Dlugosc, Z.Szerokosc, Z.Wysokosc, Z.Status,Z.IDKlienta, K.Nazwa FROM Zamowienia AS Z LEFT JOIN Klienci AS K ON K.IDKlienta = Z.IDKlienta" + OrderBy;
        string[] queryResult = _databaseService.ExecuteSelectQuery(query);


        foreach (var rowData in queryResult)
        {
            var zamowienie = new Zamowienie(rowData);
            ZamowieniaList.Add(zamowienie);
        }
    }

    private void OnLabelTapped(object sender, EventArgs e)
    {
        if (sender is Label label)
        {
            switch (label.Text)
            {
                case "ID":
                    LoadData(" ORDER BY IDZamowienia");
                    break;
                case "Data Zamówienia":
                    LoadData(" ORDER BY DataZamowienia");
                    break;
                case "Adres Pocz¹tkowy":
                    LoadData(" ORDER BY AdresPoczatkowy");
                    break;
                case "Adres Docelowy":
                    LoadData(" ORDER BY AdresDocelowy");
                    break;
                case "Towar":
                    LoadData(" ORDER BY Towar");
                    break;
                case "Masa":
                    LoadData(" ORDER BY Masa");
                    break;
                case "D³ugoœæ":
                    LoadData(" ORDER BY Dlugosc");
                    break;
                case "Szerokoœæ":
                    LoadData(" ORDER BY Szerokosc");
                    break;
                case "Wysokoœæ":
                    LoadData(" ORDER BY Wysokosc");
                    break;
                case "Status":
                    LoadData(" ORDER BY Status");
                    break;
                case "Zamawiaj¹cy":
                    LoadData(" ORDER BY IDKlienta");
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

        var selectedZamowienie = grid.BindingContext as Zamowienie;
        if (selectedZamowienie == null) return;

        var id = selectedZamowienie.Id;

        var action = await DisplayActionSheet("Wybierz akcjê", "Anuluj", null, "Edytuj", "Usuñ");

        if (action == "Edytuj")
        {
            await Navigation.PushModalAsync(new EdytujZamowieniaStrona(selectedZamowienie, false));
        }
        else if (action == "Usuñ")
        {
            var confirm = await DisplayAlert("Potwierdzenie", "Czy na pewno chcesz usun¹æ ten rekord?", "Tak", "Nie");
            if (confirm)
            {
                string query = "DELETE FROM Zamowienia WHERE IdZamowienia = " + id;
                _databaseService.ExecuteGeneralQuery(query);

                LoadData();
            }
        }
    }

    private async void OnDodajZamowienieClicked(object sender, EventArgs e)
    {
        Zamowienie noweZamowienie = new Zamowienie();
        await Navigation.PushModalAsync(new EdytujZamowieniaStrona(noweZamowienie, true));
    }

    private async void OnChangeToKlienciClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new KlienciStrona(), animated: false);
    }

    private  void OnChangeToZamowieniaClicked(object sender, EventArgs e)
    {
        LoadData();
    }

    private async void OnChangeToKierowcyClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new KierowcyStrona(), animated: false);
    }

    private async void OnChangeToPrzejazdyClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PrzejazdyStrona(), animated: false);
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