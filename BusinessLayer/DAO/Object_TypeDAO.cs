using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//TODO:another getter and add to DB method
namespace DataLayer
{
    public static class Object_TypeDAO
    {

        #region GETTERS
        public static List<Object_Type> GetObjectTypeList()
        {
            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
                return ctx.Object_Type.ToList();
            }

        }

        #endregion

        public static bool AddType(Object_Type object_Type)
        {
            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
                try
                {                    
                    ctx.Object_Type.Add(object_Type);
                    ctx.SaveChanges();
                    return true;

                }
                catch (Exception ex)
                {
                    //TODO: jak coś nie pykło to dodaj logowanie EX
                    return false;
                }


            }
        }

        public static List<Object_Type> getAllObjectTypes()
        {
            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
                return ctx.Object_Type.ToList();
            }
        }
    }
}
