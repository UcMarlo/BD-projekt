using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DAO
{
    public static class DAOsContext
    {

        public static System_NaprawEntities context { get; set; }

        public static void IniitContext()
        {
            if (context==null)
            {
                context = new System_NaprawEntities();
            }
        
        }

        public static void saveChanges()
        {
            context.SaveChanges();            
        }

    }
}
