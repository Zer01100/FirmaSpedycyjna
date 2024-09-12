namespace FirmaSpedycyjna;

public partial class EdytujNaczepyStrona : ContentPage
{
    private Naczepa _naczepa;
    private DatabaseService _databaseService;
    private bool EditOrCreate = false;
    public EdytujNaczepyStrona(Naczepa naczepa, bool EditOrCreate)
	{
        _databaseService = new DatabaseService(this);
        InitializeComponent();
        this.EditOrCreate = EditOrCreate;

        _naczepa = naczepa;
        IdEntry.Text = _naczepa.Id;
        NumerRejestracyjnyEntry.Text = _naczepa.NumerRejestracyjny;
        RodzajNaczepyEntry.Text = _naczepa.RodzajNaczepy;
        DlugoscNaczepyEntry.Text = _naczepa.DlugoscNaczepy;
        MaxMasaEntry.Text = _naczepa.MaxMasa;
        MaxDlugoscEntry.Text = _naczepa.MaxDlugosc;
        MaxSzerokoscEntry.Text = _naczepa.MaxSzerokosc;
        MaxWysokoscEntry.Text = _naczepa.MaxWysokosc;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        _naczepa.NumerRejestracyjny = NumerRejestracyjnyEntry.Text;
        _naczepa.RodzajNaczepy = RodzajNaczepyEntry.Text;
        _naczepa.DlugoscNaczepy = DlugoscNaczepyEntry.Text.Replace(',', '.');
        _naczepa.MaxMasa = MaxMasaEntry.Text.Replace(',','.');
        _naczepa.MaxDlugosc = MaxDlugoscEntry.Text.Replace(',', '.');
        _naczepa.MaxSzerokosc = MaxSzerokoscEntry.Text.Replace(',', '.');
        _naczepa.MaxWysokosc = MaxWysokoscEntry.Text.Replace(',', '.');

        if (EditOrCreate)
        {
            string query = "INSERT INTO Naczepy (NumerRejestracyjny, RodzajNaczepy, DlugoscNaczepy, MaxMasa, MaxDlugosc, MaxSzerokosc, MaxWysokosc) VALUES (" +
               $"'{_naczepa.NumerRejestracyjny}', '{_naczepa.RodzajNaczepy}', '{_naczepa.DlugoscNaczepy}', '{_naczepa.MaxMasa}', '{_naczepa.MaxDlugosc}', '{_naczepa.MaxSzerokosc}', '{_naczepa.MaxWysokosc}')";
            _databaseService.ExecuteGeneralQuery(query);
            await Navigation.PopModalAsync();
        }
        else
        {

            string query = "UPDATE Naczepy SET " +
               $"NumerRejestracyjny = '{_naczepa.NumerRejestracyjny}', " +
               $"RodzajNaczepy = '{_naczepa.RodzajNaczepy}', " +
               $"DlugoscNaczepy = '{_naczepa.DlugoscNaczepy}', " +
               $"MaxMasa = {_naczepa.MaxMasa}, " +
               $"MaxDlugosc = {_naczepa.MaxDlugosc}, " +
               $"MaxSzerokosc = {_naczepa.MaxSzerokosc}, " +
               $"MaxWysokosc = {_naczepa.MaxWysokosc} " +
               $"WHERE IdNaczepy = {_naczepa.Id}";

            _databaseService.ExecuteGeneralQuery(query);
            await Navigation.PopModalAsync();
        }

        
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}