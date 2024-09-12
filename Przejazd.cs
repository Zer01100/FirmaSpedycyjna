using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirmaSpedycyjna
{
    public class Przejazd
    {
        public string Id { get; set; }
        public string IdZamowienia { get; set; }
        public string Pojazd { get; set; }
        public string Naczepa { get; set; }
        public string Kierowca { get; set; }
        public string DlugoscPrzejazdu { get; set; }
        public string CzasPrzejazdu { get; set; }
        public string CzasPracyKierowcy { get; set; }
        public Przejazd(string rowData)
        {
            var columns = rowData.Split('\t');


            Id = columns[0].Trim();
            IdZamowienia = columns[1].Trim();
            Pojazd = columns[2].Trim();
            Naczepa = columns[3].Trim();
            Kierowca = columns[4].Trim() + " " + columns[5].Trim();
            DlugoscPrzejazdu = columns[6].Trim();
            CzasPrzejazdu = columns[7].Trim();
            CzasPracyKierowcy = columns[8].Trim();
        }

        public Przejazd()
        {
            Id = "";
            IdZamowienia = "";
            Pojazd = "";
            Naczepa = "";
            Kierowca = "";
            DlugoscPrzejazdu = "";
            CzasPrzejazdu = "";
            CzasPracyKierowcy = "";
        }
    }
}
