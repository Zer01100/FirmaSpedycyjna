using System.Collections.ObjectModel;

namespace FirmaSpedycyjna;

public partial class EdytujZamowieniaStrona : ContentPage
{
    private Zamowienie _zamowienie;
    private DatabaseService _databaseService;
    private bool EditOrCreate = false;
    public Klient WybranyKlient { get; set; }
    public ObservableCollection<Klient> Klienci { get; set; } = new ObservableCollection<Klient>();
    public EdytujZamowieniaStrona(Zamowienie zamowienie, bool EditOrCreate)
	{

        _databaseService = new DatabaseService(this);
        InitializeComponent();
        this.EditOrCreate = EditOrCreate;
        WybranyKlient = new Klient();

        var query = "SELECT * FROM Klienci";
        string[] queryResult = _databaseService.ExecuteSelectQuery(query);

        foreach (var rowData in queryResult)
        {
            var klient = new Klient(rowData);
            Klienci.Add(klient);
        }

        BindingContext = this;

        _zamowienie = zamowienie;
        IdEntry.Text = _zamowienie.Id;
        DataEntry.Text = _zamowienie.DataZamowienia;
        AdresPEntry.Text = _zamowienie.AdresPoczatkowy;
        AdresDEntry.Text = _zamowienie.AdresDocelowy;
        TowarEntry.Text = _zamowienie.Towar;
        MasaEntry.Text = _zamowienie.Masa;
        DlugoscEntry.Text = _zamowienie.Dlugosc;
        SzerokoscEntry.Text = _zamowienie.Szerokosc;
        WysokoscEntry.Text = _zamowienie.Wysokosc;
        StatusPicker.SelectedItem = _zamowienie.Status;
        KlientPicker.SelectedItem = Klienci.FirstOrDefault(k => k.Nazwa == _zamowienie.NazwaKlienta);

    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (WybranyKlient == null)
        {
            await DisplayAlert("B³¹d", "Nie wybrano klienta.", "OK");
            return;
        }
        else
        { 
        _zamowienie.DataZamowienia = DataEntry.Text;
        _zamowienie.AdresPoczatkowy = AdresPEntry.Text;
        _zamowienie.AdresDocelowy = AdresDEntry.Text;
        _zamowienie.Towar = TowarEntry.Text;
        _zamowienie.Masa = string.IsNullOrEmpty(MasaEntry.Text) ? "0" : MasaEntry.Text.Replace(',', '.');
        _zamowienie.Dlugosc = string.IsNullOrEmpty(DlugoscEntry.Text) ? "0" : MasaEntry.Text.Replace(',', '.');
        _zamowienie.Szerokosc = string.IsNullOrEmpty(SzerokoscEntry.Text) ? "0" : MasaEntry.Text.Replace(',', '.');
        _zamowienie.Wysokosc = string.IsNullOrEmpty(WysokoscEntry.Text) ? "0" : MasaEntry.Text.Replace(',', '.');
        _zamowienie.Status = StatusPicker.SelectedItem as string;

            if (EditOrCreate)
            {
                string query = "INSERT INTO Zamowienia (DataZamowienia, AdresPoczatkowy, AdresDocelowy, Towar, Masa, Dlugosc, Szerokosc, Wysokosc, Status, IDKlienta) VALUES (" +
                   $"'{_zamowienie.DataZamowienia}', '{_zamowienie.AdresPoczatkowy}', '{_zamowienie.AdresDocelowy}', '{_zamowienie.Towar}', {_zamowienie.Masa}, {_zamowienie.Dlugosc}, {_zamowienie.Szerokosc}, {_zamowienie.Wysokosc}, '{_zamowienie.Status}', {WybranyKlient.Id})";

                _databaseService.ExecuteGeneralQuery(query);
                await Navigation.PopModalAsync();
            }
            else
            {
                string query = "UPDATE Zamowienia SET " +
                   $"DataZamowienia = '{_zamowienie.DataZamowienia}', " +
                   $"AdresPoczatkowy = '{_zamowienie.AdresPoczatkowy}', " +
                   $"AdresDocelowy = '{_zamowienie.AdresDocelowy}', " +
                   $"Towar = '{_zamowienie.Towar}', " +
                   $"Masa = {_zamowienie.Masa}, " +
                   $"Dlugosc = {_zamowienie.Dlugosc}, " +
                   $"Szerokosc = {_zamowienie.Szerokosc}, " +
                   $"Wysokosc = {_zamowienie.Wysokosc}, " +
                   $"Status = '{_zamowienie.Status}', " +
                   $"IDKlienta = {WybranyKlient.Id} " +
                   $"WHERE IdZamowienia = {_zamowienie.Id}";



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