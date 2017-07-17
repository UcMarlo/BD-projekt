using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class ClientQuery
    {

        public System.Guid id_client { get; set; }
        public string first_name { get; set; }
        public string second_name { get; set; }
        public string phone { get; set; }

        public System.Guid id_adress { get; set; }
        public string adress1 { get; set; }
        public string city { get; set; }
        public string postcode { get; set; }
        


    }
}
