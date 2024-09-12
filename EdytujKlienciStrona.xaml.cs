namespace FirmaSpedycyjna;

public partial class EdytujKlienciStrona : ContentPage
{
    private Klient _klient;
    private DatabaseService _databaseService;
    private bool EditOrCreate = false;
    public EdytujKlienciStrona(Klient klient, bool EditOrCreate)
    {

        _databaseService = new DatabaseService(this);
        InitializeComponent();
        this.EditOrCreate = EditOrCreate;

        _klient = klient;
        IdEntry.Text = _klient.Id;
        NazwaEntry.Text = _klient.Nazwa;
        AdresEntry.Text = _klient.Adres;
        NumerTelefonuEntry.Text = _klient.NumerTelefonu;
        AdresEmailEntry.Text = _klient.AdresEmail;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        _klient.Nazwa = NazwaEntry.Text;
        _klient.Adres = AdresEntry.Text;
        _klient.NumerTelefonu = NumerTelefonuEntry.Text;
        _klient.AdresEmail = AdresEmailEntry.Text;

        if (EditOrCreate)
        {
            string query = "INSERT INTO Klienci (Nazwa, Adres, NumerTelefonu, AdresEmail) VALUES (" +
               $"'{_klient.Nazwa}', '{_klient.Adres}', '{_klient.NumerTelefonu}', '{_klient.AdresEmail}')";
            _databaseService.ExecuteGeneralQuery(query);
            await Navigation.PopModalAsync();
        }
        else
        {

            string query = "UPDATE Klienci SET " +
               $"Nazwa = '{_klient.Nazwa}', " +
               $"Adres = '{_klient.Adres}', " +
               $"NumerTelefonu = '{_klient.NumerTelefonu}', " +
               $"AdresEmail = '{_klient.AdresEmail}' " +
               $"WHERE IdKlienta = {_klient.Id}";
            _databaseService.ExecuteGeneralQuery(query);
            await Navigation.PopModalAsync();

        }
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}