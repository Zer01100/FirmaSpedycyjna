namespace FirmaSpedycyjna
{
    public class Naczepa
    {

        public string Id { get; set; }
        public string NumerRejestracyjny { get; set; }
        public string RodzajNaczepy { get; set; }
        public string MaxMasa { get; set; }
        public string MaxDlugosc { get; set; }
        public string MaxSzerokosc { get; set; }
        public string MaxWysokosc { get; set; }
        public string DlugoscNaczepy { get; set; }
        public Naczepa(string rowData)
        {
            var columns = rowData.Split('\t');


            Id = columns[0].Trim();
            RodzajNaczepy = columns[1].Trim();
            MaxMasa = columns[2].Trim();
            MaxDlugosc = columns[3].Trim();
            MaxSzerokosc = columns[4].Trim();
            MaxWysokosc = columns[5].Trim();
            DlugoscNaczepy = columns[6].Trim();
            NumerRejestracyjny = columns[7].Trim();
        }

        public Naczepa()
        {
            Id = "";
            RodzajNaczepy = "";
            NumerRejestracyjny = "";
            MaxMasa = "";
            MaxDlugosc = "";
            MaxSzerokosc = "";
            MaxWysokosc = "";
            DlugoscNaczepy = "";
        }
    }
}
