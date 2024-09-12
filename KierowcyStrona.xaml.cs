using System.Collections.ObjectModel;

namespace FirmaSpedycyjna;

public partial class KierowcyStrona : ContentPage
{
    public ObservableCollection<Kierowca> KierowcyList { get; set; } = new ObservableCollection<Kierowca>();
    private DatabaseService _databaseService;
    public KierowcyStrona()
    {
        _databaseService = new DatabaseService(this);
        InitializeComponent();
        BindingContext = this;
        LoadData();
    }

    public void LoadData()
    {
        KierowcyList.Clear();
        var query = "SELECT IdKierowcy, Imie, Nazwisko, NumerTelefonu, NumerPrawaJazdy, UprawnieniaKierowcy FROM Kierowcy";
        string[] queryResult = _databaseService.ExecuteSelectQuery(query);


        foreach (var rowData in queryResult)
        {
            var kierowca = new Kierowca(rowData);
            KierowcyList.Add(kierowca);
        }
    }

    public void LoadData(string OrderBy)
    {
        KierowcyList.Clear();
        var query = "SELECT IdKierowcy, Imie, Nazwisko, NumerTelefonu, NumerPrawaJazdy, UprawnieniaKierowcy FROM Kierowcy" + OrderBy;
        string[] queryResult = _databaseService.ExecuteSelectQuery(query);


        foreach (var rowData in queryResult)
        {
            var kierowca = new Kierowca(rowData);
            KierowcyList.Add(kierowca);
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
                    LoadData(" ORDER BY IDKierowcy");
                    break;
                case "Imiê":
                    LoadData(" ORDER BY Imie");
                    break;
                case "Nazwisko":
                    LoadData(" ORDER BY Nazwisko");
                    break;
                case "Numer Telefonu":
                    LoadData(" ORDER BY NumerTelefonu");
                    break;
                case "Numer Prawa Jazdy":
                    LoadData(" ORDER BY NumerPrawaJazdy");
                    break;
                case "Kategoria":
                    LoadData(" ORDER BY UprawnieniaKierowcy");
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

        var selectedKierowca = grid.BindingContext as Kierowca;
        if (selectedKierowca == null) return;

        var id = selectedKierowca.Id;
        var imie = selectedKierowca.Imie;

        var action = await DisplayActionSheet("Wybierz akcjê", "Anuluj", null, "Edytuj", "Usuñ");

        if (action == "Edytuj")
        {
            await Navigation.PushModalAsync(new EdytujKierowcaStrona(selectedKierowca, false));
        }
        else if (action == "Usuñ")
        {
            var confirm = await DisplayAlert("Potwierdzenie", "Czy na pewno chcesz usun¹æ ten rekord?", "Tak", "Nie");
            if (confirm)
            {
                string query = "DELETE FROM Kierowcy WHERE IdKierowcy = " + id;
                _databaseService.ExecuteGeneralQuery(query);

                LoadData();
            }
        }
    }

    private async void OnDodajKierowceClicked(object sender, EventArgs e)
    {
        Kierowca nowyKierowca = new Kierowca();
        await Navigation.PushModalAsync(new EdytujKierowcaStrona(nowyKierowca, true));
    }

    private async void OnChangeToKlienciClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new KlienciStrona(), animated: false);
    }

    private async void OnChangeToZamowieniaClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ZamowieniaStrona(), animated: false);
    }

    private void OnChangeToKierowcyClicked(object sender, EventArgs e)
    {
        LoadData();
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