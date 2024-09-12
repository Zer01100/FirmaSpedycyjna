using System.Collections.ObjectModel;

namespace FirmaSpedycyjna;

public partial class KlienciStrona : ContentPage
{
    public ObservableCollection<Klient> KierowcyList { get; set; } = new ObservableCollection<Klient>();
    private DatabaseService _databaseService;
    public KlienciStrona()
	{
        _databaseService = new DatabaseService(this);
        InitializeComponent();
        BindingContext = this;
        LoadData();
    }

    public void LoadData()
    {
        KierowcyList.Clear();
        var query = "SELECT * FROM Klienci";
        string[] queryResult = _databaseService.ExecuteSelectQuery(query);


        foreach (var rowData in queryResult)
        {
            var klient = new Klient(rowData);
            KierowcyList.Add(klient);
        }
    }

    public void LoadData(string OrderBy)
    {
        KierowcyList.Clear();
        var query = "SELECT * FROM Klienci" + OrderBy;
        string[] queryResult = _databaseService.ExecuteSelectQuery(query);


        foreach (var rowData in queryResult)
        {
            var klient = new Klient(rowData);
            KierowcyList.Add(klient);
        }
    }

    private void OnLabelTapped(object sender, EventArgs e)
    {
        var label = sender as Label;
        if (label != null)
        {
            switch (label.Text)
            {
                case "ID":
                    LoadData(" ORDER BY IDKlienta");
                    break;
                case "Nazwa":
                    LoadData(" ORDER BY Nazwa");
                    break;
                case "Adres":
                    LoadData(" ORDER BY Adres");
                    break;
                case "Numer Telefonu":
                    LoadData(" ORDER BY NumerTelefonu");
                    break;
                case "Adres Email":
                    LoadData(" ORDER BY AdresEmail");
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

        var selectedKlient = grid.BindingContext as Klient;
        if (selectedKlient == null) return;

        var id = selectedKlient.Id;
        var imie = selectedKlient.Nazwa;

        var action = await DisplayActionSheet("Wybierz akcjê", "Anuluj", null, "Edytuj", "Usuñ");

        if (action == "Edytuj")
        {
            await Navigation.PushModalAsync(new EdytujKlienciStrona(selectedKlient, false));
        }
        else if (action == "Usuñ")
        {
            var confirm = await DisplayAlert("Potwierdzenie", "Czy na pewno chcesz usun¹æ ten rekord?", "Tak", "Nie");
            if (confirm)
            {
                string query = "DELETE FROM Klienci WHERE IdKlienta = " + id;
                _databaseService.ExecuteGeneralQuery(query);

                LoadData();
            }
        }
    }

    private async void OnDodajKlientaClicked(object sender, EventArgs e)
    {
        Klient nowyKlient = new Klient();
        await Navigation.PushModalAsync(new EdytujKlienciStrona(nowyKlient, true));
    }

    private void OnChangeToKlienciClicked(object sender, EventArgs e)
    {
        LoadData();
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