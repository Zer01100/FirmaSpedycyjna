using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirmaSpedycyjna
{
    public class Pojazd
    {
        public string Id { get; set; }
        public string NumerRejestracyjny { get; set; }
        public string Uprawnienia { get; set; }
        public string Typ { get; set; }
        public string MaxMasa { get; set; }
        public string MaxDlugosc { get; set; }
        public string MaxSzerokosc { get; set; }
        public string MaxWysokosc { get; set; }
        
        public Pojazd(string rowData)
        {
            var columns = rowData.Split('\t');


            Id = columns[0].Trim();
            NumerRejestracyjny = columns[1].Trim();
            Uprawnienia = columns[2].Trim();
            Typ = columns[3].Trim();
            MaxMasa = columns[4].Trim();
            MaxDlugosc = columns[5].Trim();
            MaxSzerokosc = columns[6].Trim();
            MaxWysokosc = columns[7].Trim();
        }

        public Pojazd()
        {
            Id = "";
            NumerRejestracyjny = "";
            Uprawnienia = "";
            Typ = "";
            MaxMasa = "";
            MaxDlugosc = "";
            MaxSzerokosc = "";
            MaxWysokosc = "";
        }
    }
}

