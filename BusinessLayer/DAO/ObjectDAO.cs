using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace DataLayer.DAO
{
    public static class ObjectDAO
    {
        #region GETTERS
        public static List<Object> GetObjectList()
        {
            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                return ctx.Object.Include(c => c.Client).Include(c => c.Object_Type).ToList();
            }

        }
        public static Object GetObjectByID(int ID)
        {
            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
                Guid IDguid = ToGuid(ID);
                return ctx.Object.FirstOrDefault(m => m.id_object == IDguid);
            }

        }
        public static Object GetObjectByID(Guid ID)
        {
            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
                return ctx.Object.FirstOrDefault(m => m.id_object == ID);
            }

        }

        public static List<Object> GetObjectListByClientID(int cliID)
        {
            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
                Guid IDguid = ToGuid(cliID);
                return ctx.Object.Where(m => m.id_client == IDguid).ToList();
            }

        }
        public static List<Object> GetObjectListByClientID(Guid cliID)
        {
            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
                return ctx.Object.Where(m => m.id_client == cliID).ToList();
            }

        }
        public static List<Object> GetObjectListByType(Object_Type type)
        {
           
             return   DAOsContext.context.Object.Where(m => m.Object_Type.type_code == type.type_code).ToList();
          

        }
        #endregion

        public static bool AddObject(Object obj, Object_Type type, Client client)
        {
            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
                try
                {
                    obj.id_object = Guid.NewGuid(); 
                
                    Client baseClient = ctx.Client.FirstOrDefault(m => m.id_client == client.id_client);
                    if((baseClient == null))
                    {
                        return false;
                    }
                    obj.id_client = client.id_client;
                    obj.obj_type = type.type_code;
                    ctx.Entry(type).State = System.Data.Entity.EntityState.Unchanged;
                    ctx.Object.Add(obj);
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



        #region PRIVATE
        private static Guid ToGuid(int value)
        {
            byte[] bytes = new byte[16];
            BitConverter.GetBytes(value).CopyTo(bytes, 0);
            return new Guid(bytes);
        }

        public static void editObject(Object obj, Object_Type objType, Client client)
        {
            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
                try
                {
                    obj.Object_Type = null;
                    obj.id_client = client.id_client;
                    obj.Client = null;
                    ctx.Entry(obj).State = EntityState.Modified;
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
           
            }
        }
        #endregion
    }
}
