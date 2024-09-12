namespace FirmaSpedycyjna;

public partial class EdytujPojazdyStrona : ContentPage
{
    private Pojazd _pojazd;
    private DatabaseService _databaseService;
    private bool EditOrCreate = false;
    public EdytujPojazdyStrona(Pojazd pojazd, bool EditOrCreate)
	{
        _databaseService = new DatabaseService(this);
        InitializeComponent();
        this.EditOrCreate = EditOrCreate;

        _pojazd = pojazd;
        IdEntry.Text = _pojazd.Id;
        NumerRejestracyjnyEntry.Text = _pojazd.NumerRejestracyjny;
        KategoriaPicker.SelectedItem = _pojazd.Uprawnienia;
        TypPicker.SelectedItem = _pojazd.Typ;
        MaxMasaEntry.Text = _pojazd.MaxMasa;
        MaxDlugoscEntry.Text = _pojazd.MaxDlugosc;
        MaxSzerokoscEntry.Text = _pojazd.MaxSzerokosc;
        MaxWysokoscEntry.Text = _pojazd.MaxWysokosc;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        _pojazd.NumerRejestracyjny = NumerRejestracyjnyEntry.Text;
        _pojazd.Uprawnienia = KategoriaPicker.SelectedItem as string;
        _pojazd.Typ = TypPicker.SelectedItem as string;
        _pojazd.MaxMasa = MaxMasaEntry.Text;
        _pojazd.MaxDlugosc = MaxDlugoscEntry.Text;
        _pojazd.MaxSzerokosc = MaxSzerokoscEntry.Text;
        _pojazd.MaxWysokosc = MaxWysokoscEntry.Text;

        if (EditOrCreate)
        {
            string query = "INSERT INTO Pojazdy (NumerRejestracyjny, WymaganeUprawnienia, TypPojazdu, MaxMasa, MaxDlugosc, MaxSzerokosc, MaxWysokosc) VALUES (" +
               $"'{_pojazd.NumerRejestracyjny}', '{_pojazd.Uprawnienia}', '{_pojazd.Typ}', '{_pojazd.MaxMasa}', '{_pojazd.MaxDlugosc}', '{_pojazd.MaxSzerokosc}', '{_pojazd.MaxWysokosc}')";
            _databaseService.ExecuteGeneralQuery(query);
            await Navigation.PopModalAsync();
        }
        else
        {

            string query = "UPDATE Pojazdy SET " +
               $"NumerRejestracyjny = '{_pojazd.NumerRejestracyjny}', " +
               $"WymaganeUprawnienia = '{_pojazd.Uprawnienia}', " +
               $"TypPojazdu = '{_pojazd.Typ}', " +
               $"MaxMasa = {_pojazd.MaxMasa}, " +
               $"MaxDlugosc = {_pojazd.MaxDlugosc}, " +
               $"MaxSzerokosc = {_pojazd.MaxSzerokosc}, " +
               $"MaxWysokosc = {_pojazd.MaxWysokosc} " +
               $"WHERE IdPojazdu = {_pojazd.Id}";

            _databaseService.ExecuteGeneralQuery(query);
            await Navigation.PopModalAsync();
        }

       
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}