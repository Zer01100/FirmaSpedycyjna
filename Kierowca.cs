namespace FirmaSpedycyjna
{
    public class Kierowca
    {
        public string Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string NumerTelefonu { get; set; }
        public string NumerPrawaJazdy { get; set; }
        public string Kategoria { get; set; }
        public Kierowca(string rowData)
        {
            var columns = rowData.Split('\t');


            Id = columns[0].Trim();
            Imie = columns[1].Trim();
            Nazwisko = columns[2].Trim();
            NumerTelefonu = columns[3].Trim();
            NumerPrawaJazdy = columns[4].Trim();
            Kategoria = columns[5].Trim();

        }

        public Kierowca()
        {
            Id = "-";
            Imie = "";
            Nazwisko = "";
            NumerTelefonu = "";
            NumerPrawaJazdy = "";
            Kategoria = "";
        }
    }
}
