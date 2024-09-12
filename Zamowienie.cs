using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirmaSpedycyjna
{
    public class Zamowienie
    {
        public string Id { get; set; }
        public string DataZamowienia { get; set; }
        public string AdresPoczatkowy { get; set; }
        public string AdresDocelowy { get; set; }
        public string Towar { get; set; }
        public string Masa { get; set; }
        public string Dlugosc { get; set; }
        public string Szerokosc { get; set; }
        public string Wysokosc { get; set; }
        public string Status { get; set; }
        public string IdKlienta { get; set; }
        public string NazwaKlienta { get; set; }
        public Zamowienie(string rowData)
        {
            var columns = rowData.Split('\t');


            Id = columns[0].Trim();
            DataZamowienia = columns[1].Trim();
            AdresPoczatkowy = columns[2].Trim();
            AdresDocelowy = columns[3].Trim();
            Towar = columns[4].Trim();
            Masa = columns[5].Trim();
            Dlugosc = columns[6].Trim();
            Szerokosc = columns[7].Trim();
            Wysokosc = columns[8].Trim();
            Status = columns[9].Trim();
            IdKlienta = columns[10].Trim();
            NazwaKlienta = columns[11].Trim();
        }

        public Zamowienie()
        {
            Id = "";
            DataZamowienia = "";
            AdresPoczatkowy = "";
            AdresDocelowy = "";
            Towar = "";
            Masa = "";
            Dlugosc = "";
            Szerokosc = "";
            Wysokosc = "";
            Status = "Złożone";
            NazwaKlienta = "";
            IdKlienta = "";
        }
    }
}
