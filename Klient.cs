using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirmaSpedycyjna
{
    public class Klient
    {
        public string Id { get; set; }
        public string Nazwa { get; set; }
        public string Adres { get; set; }
        public string NumerTelefonu { get; set; }
        public string AdresEmail { get; set; }
        public Klient(string rowData)
        {
            var columns = rowData.Split('\t');


            Id = columns[0].Trim();
            Nazwa = columns[1].Trim();
            Adres = columns[2].Trim();
            NumerTelefonu = columns[3].Trim();
            AdresEmail = columns[4].Trim();
        }

        public Klient()
        {
            Id = "";
            Nazwa = "";
            Adres = "";
            NumerTelefonu = "";
            AdresEmail = "";
        }
    }
}
