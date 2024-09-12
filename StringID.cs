using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirmaSpedycyjna
{
    public class StringID
    {
        public string ID{get; set;}
        public string String{get; set;}
        public StringID(string ID, string String) {
            this.ID = ID;
            this.String = String;
        }

        public StringID() 
        {
            ID = "";
            String = "";
        }
    }

    
}
