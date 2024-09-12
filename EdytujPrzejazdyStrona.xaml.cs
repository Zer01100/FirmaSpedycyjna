using System.Collections.ObjectModel;
using System.Formats.Tar;

namespace FirmaSpedycyjna;

public partial class EdytujPrzejazdyStrona : ContentPage
{
    private Przejazd _przejazd;
    private DatabaseService _databaseService;
    private bool EditOrCreate = false;
    public ObservableCollection<StringID> ZamowieniaList { get; set; } = new ObservableCollection<StringID>();
    public StringID WybraneZamowienie { get; set; } = new StringID();
    public ObservableCollection<StringID> PojazdyList { get; set; } = new ObservableCollection<StringID>();
    public StringID WybranyPojazd { get; set; } = new StringID();
    public ObservableCollection<StringID> NaczepyList { get; set; } = new ObservableCollection<StringID>();
    public StringID WybranaNaczepa { get; set; } = new StringID();
    public ObservableCollection<StringID> KierowcyList { get; set; } = new ObservableCollection<StringID>();
    public StringID WybranyKierowca { get; set; } = new StringID();
    public EdytujPrzejazdyStrona(Przejazd przejazd, bool EditOrCreate)
    {
        _databaseService = new DatabaseService(this);
        InitializeComponent();
        this.EditOrCreate = EditOrCreate;

        LoadData();

        BindingContext = this;

        _przejazd = przejazd;
        IdEntry.Text = _przejazd.Id;
        ZamowieniaPicker.SelectedItem = ZamowieniaList.FirstOrDefault(k => k.ID == _przejazd.Id);
        PojazdPicker.SelectedItem = PojazdyList.FirstOrDefault(k => k.ID == _przejazd.Pojazd);
        NaczepaPicker.SelectedItem = NaczepyList.FirstOrDefault(k => k.ID == _przejazd.Naczepa);
        KierowcyPicker.SelectedItem = KierowcyList.FirstOrDefault(k => k.ID == _przejazd.Kierowca);
        DlogoscEntry.Text = _przejazd.DlugoscPrzejazdu;
        CzasEntry.Text = _przejazd.CzasPrzejazdu;
        CzasPEntry.Text = _przejazd.CzasPracyKierowcy;
    }

    private void LoadData()
    {
        LoadZamowienia();
        LoadPojazdy();
        LoadNaczepy();
        LoadKierowcy();
    }

    private void LoadZamowienia()
    {
        var query = "SELECT Z.IDZamowienia, Z.DataZamowienia, Z.AdresPoczatkowy, Z.AdresDocelowy, Z.Towar, Z.Masa, Z.Dlugosc, Z.Szerokosc, Z.Wysokosc, Z.Status, Z.IDKlienta, K.Nazwa FROM Zamowienia AS Z LEFT JOIN Klienci AS K ON K.IDKlienta = Z.IDKlienta";
        string[] queryResult = _databaseService.ExecuteSelectQuery(query);

        foreach (var rowData in queryResult)
        {
            var columns = rowData.Split('\t');
            string ID = columns[0];
            string Reszta = $"{columns[0]} {columns[1]} {columns[4]} {columns[2]} {columns[11]}";
            ZamowieniaList.Add(new StringID(ID, Reszta));
        }
    }

    private void LoadPojazdy()
    {
        var query = "SELECT IDPojazdu, NumerRejestracyjny FROM Pojazdy";
        string[] queryResult = _databaseService.ExecuteSelectQuery(query);

        foreach (var rowData in queryResult)
        {
            var columns = rowData.Split('\t');
            string ID = columns[0];
            string Reszta = columns[1];
            PojazdyList.Add(new StringID(ID, Reszta));
        }
    }

    private void LoadNaczepy()
    {
        var query = "SELECT IDNaczepy, NumerRejestracyjny FROM Naczepy";
        string[] queryResult = _databaseService.ExecuteSelectQuery(query);

        foreach (var rowData in queryResult)
        {
            var columns = rowData.Split('\t');
            string ID = columns[0];
            string Reszta = columns[1];
            NaczepyList.Add(new StringID(ID, Reszta));
        }
    }

    private void LoadKierowcy()
    {
        var query = "SELECT IDKierowcy, Imie, Nazwisko FROM Kierowcy";
        string[] queryResult = _databaseService.ExecuteSelectQuery(query);

        foreach (var rowData in queryResult)
        {
            var columns = rowData.Split('\t');
            string ID = columns[0];
            string Reszta = $"{columns[1]} {columns[2]}";
            KierowcyList.Add(new StringID(ID, Reszta));
        }
    }


    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (WybraneZamowienie == null || WybranyPojazd == null || WybranaNaczepa == null || WybranyKierowca == null)
        {
            await DisplayAlert("B³¹d", "Wszystkie pola musz¹ byæ wybrane.", "OK");
        }
        else { 
        _przejazd.Id = IdEntry.Text;
        _przejazd.DlugoscPrzejazdu = string.IsNullOrEmpty(DlogoscEntry.Text) ? "0" : DlogoscEntry.Text.Replace(',', '.');
        _przejazd.CzasPrzejazdu = string.IsNullOrEmpty(CzasEntry.Text) ? "0" : CzasEntry.Text.Replace(',', '.');
        _przejazd.CzasPracyKierowcy = string.IsNullOrEmpty(CzasPEntry.Text) ? "0" : CzasPEntry.Text.Replace(',', '.');

            if (EditOrCreate)
            {
                string query = "INSERT INTO Przejazdy (IDZamowienia, IDPojazdu, IDNaczepy, IDKierowcy, DlugoscPrzejazdu, CzasPrzejazdu, CzasPracyKierowcy) VALUES (" +
                   $"'{WybraneZamowienie.ID}', '{WybranyPojazd.ID}', '{WybranaNaczepa.ID}', '{WybranyKierowca.ID}', {_przejazd.DlugoscPrzejazdu}, {_przejazd.CzasPrzejazdu}, {_przejazd.CzasPracyKierowcy})";
                _databaseService.ExecuteGeneralQuery(query);
                await Navigation.PopModalAsync();
            }
            else
            {
                string query = "UPDATE Przejazdy SET " +
                   $"IDZamowienia = '{WybraneZamowienie.ID}', " +
                   $"IDPojazdu = '{WybranyPojazd.ID}', " +
                   $"IDNaczepy = '{WybranaNaczepa.ID}', " +
                   $"IDKierowcy = '{WybranyKierowca.ID}', " +
                   $"DlugoscPrzejazdu = {_przejazd.DlugoscPrzejazdu}, " +
                   $"CzasPrzejazdu = {_przejazd.CzasPrzejazdu}, " +
                   $"CzasPracyKierowcy = {_przejazd.CzasPracyKierowcy} " +
                   $"WHERE IdDostawy = {_przejazd.Id}";
                _databaseService.ExecuteGeneralQuery(query);
                await Navigation.PopModalAsync();
            }
        }
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}