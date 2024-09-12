namespace FirmaSpedycyjna;

public partial class EdytujKierowcaStrona : ContentPage
{
    private Kierowca _kierowca;
    private DatabaseService _databaseService;
    private bool EditOrCreate = false;
    public EdytujKierowcaStrona(Kierowca kierowca, bool EditOrCreate)
    {

        _databaseService = new DatabaseService(this);
        InitializeComponent();
        this.EditOrCreate = EditOrCreate;

        _kierowca = kierowca;
        IdEntry.Text = _kierowca.Id;
        ImieEntry.Text = _kierowca.Imie;
        NazwiskoEntry.Text = _kierowca.Nazwisko;
        NumerTelefonuEntry.Text = _kierowca.NumerTelefonu;
        NumerPrawaJazdyEntry.Text = _kierowca.NumerPrawaJazdy;
        KategoriaPicker.SelectedItem = "C";
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        _kierowca.Imie = ImieEntry.Text;
        _kierowca.Nazwisko = NazwiskoEntry.Text;
        _kierowca.NumerTelefonu = NumerTelefonuEntry.Text;
        _kierowca.NumerPrawaJazdy = NumerPrawaJazdyEntry.Text;
        _kierowca.Kategoria = KategoriaPicker.SelectedItem as string;


        if (EditOrCreate)
        {
            string query = "INSERT INTO Kierowcy (Imie, Nazwisko, NumerTelefonu, NumerPrawaJazdy, UprawnieniaKierowcy) VALUES (" +
               $"'{_kierowca.Imie}', '{_kierowca.Nazwisko}', '{_kierowca.NumerTelefonu}', '{_kierowca.NumerPrawaJazdy}', '{_kierowca.Kategoria}')";
            _databaseService.ExecuteGeneralQuery(query);
            await Navigation.PopModalAsync();
        }
        else
        {

            string query = "UPDATE Kierowcy SET " +
               $"Imie = '{_kierowca.Imie}', " +
               $"Nazwisko = '{_kierowca.Nazwisko}', " +
               $"NumerTelefonu = '{_kierowca.NumerTelefonu}', " +
               $"NumerPrawaJazdy = '{_kierowca.NumerPrawaJazdy}', " +
               $"UprawnieniaKierowcy = '{_kierowca.Kategoria}' " +
               $"WHERE IdKierowcy = {_kierowca.Id}";
            _databaseService.ExecuteGeneralQuery(query);
            await Navigation.PopModalAsync();

        }
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}

