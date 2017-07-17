using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DataLayer.DAO;

namespace DataLayer
{
    public static class RequestDAO
    {
      
        public static List<Request> GetAllRequests()
        {
            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
                return ctx.Request.Include(c => c.Object).Include(c => c.Activity).ToList();
            }

        }

        public static List<Request> GetRequestsByPersonelID(int perID)
        {
            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
                Guid IDguid = ToGuid(perID);
                return ctx.Request.Where(m => m.Personel.id_person == IDguid).ToList();
            }

        }
        public static List<Request> GetRequestsByPersonelID(Guid perID)
        {
            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
                return ctx.Request.Where(m => m.Personel.id_person == perID).ToList();
            }

        }

        public static List<Request> GetRequestsByManagerID(int manID)
        {
            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
                Guid IDguid = ToGuid(manID);
                return ctx.Request.Where(m => m.id_manager == IDguid).ToList();
            }

        }
        public static List<Request> GetRequestsByManagerID(Guid manID)
        {
            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
                return ctx.Request.Where(m => m.id_manager == manID).ToList();
            }

        }

        public static List<Request> GetRequestsByObjectID(int objectID)
        {
            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
                Guid IDguid = ToGuid(objectID);
                return ctx.Request.Where(m => m.id_object == IDguid).ToList();
            }

        }
        public static List<Request> GetRequestsByObjectID(Guid objectID)
        {
            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
                return ctx.Request.Where(m => m.id_object == objectID).ToList();
            }

        }
     

        public static bool AddRequest(Request request)
        {

            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {

                try
                {

                    request.id_request = Guid.NewGuid();

                    //ctx.Entry(request.Personel).State = System.Data.Entity.EntityState.Unchanged;                 
                    //ctx.Entry(request.Object).State = System.Data.Entity.EntityState.Unchanged;
                    //ctx.Entry(request.Object.Client).State = System.Data.Entity.EntityState.Unchanged;
                    //ctx.Entry(request.Object.Object_Type).State = System.Data.Entity.EntityState.Unchanged;
                    //ctx.Entry(request).State = System.Data.Entity.EntityState.Detached;
                    var temp_request = ctx.Request.Create();
                    temp_request.date_fin_can = request.date_fin_can;
                    temp_request.date_reg = request.date_reg;
                    temp_request.description = request.description;
                    temp_request.id_manager = request.id_manager;
                    temp_request.id_object = request.id_object;
                    temp_request.id_request = request.id_request;
                    temp_request.result = request.result;
                    temp_request.status = request.status;
                    ctx.Request.Add(temp_request);
                  
                    ctx.SaveChanges();
                    return true;

                }
                catch (Exception ex)
                {
                    //TODO: jak coś nie pykło to dodaj logowanie EX
                    Console.WriteLine(ex.StackTrace);
                    Console.WriteLine("end");
                    return false;
                }
            }
             
            
        }

        public static bool EditRequest(Request request)
        {
            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
                try
                {
                    request.Personel = null;
                    request.Object = null;
                    ctx.Entry(request).State = EntityState.Modified;
                    ctx.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }

        public static List<Request> getRequestByDate(DateTime beginDate, DateTime endDate)
        {

            try
            {       
             
                 return DAOsContext.context.Request.Where(a => a.date_reg > beginDate && a.date_reg < endDate).ToList();

                }
                catch (Exception)
                {

                    return new List<Request>();
                }
            
         
        }

        public static bool addActivityToRequest(Activity activity, Request request)
        {
            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
                try
                {
                    Activity baseActivity = ctx.Activity.FirstOrDefault(m => m.id_activity == activity.id_activity);
                    baseActivity.id_request = request.id_request;
                    ctx.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
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
        #endregion
    }
}
