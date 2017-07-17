using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DataLayer
{
    //TODO: maybe change int ito Guid or long type
    public static class ActivityDAO
    {
        #region GETTERS

        public static List<Activity> GetAllActivities()
        {
            using( System_NaprawEntities ctx = new System_NaprawEntities())
            {
                return ctx.Activity.Include(c => c.Personel).Include(c => c.Request).Include(c => c.Activity_Type).ToList();
            }
            
        }

        //<summary>
        //get activity by activity ID
        //<summary>
        public static Activity GetActivityByActID(int actID)
        {
            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
                Guid IDguid = ToGuid(actID);
                return ctx.Activity.FirstOrDefault(m => m.id_activity == IDguid);
            }
        }

        public static Activity GetActivityByActID(Guid actID)
        {
            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
                return ctx.Activity.FirstOrDefault(m => m.id_activity == actID);
            }
        }

        //<summary>
        //get activites by Request ID
        //<summary>
        public static List<Activity> GetActivitesByReqID(int reqID)
        {
            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
                Guid IDguid = ToGuid(reqID);
                return ctx.Activity.Where(m => m.id_request == IDguid).ToList();
            }
        }

        public static List<Activity> GetActivitesByReqID(Guid reqID)
        {
            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
                var part1 = ctx.Activity.Where(m => m.id_request == reqID).Include(c => c.Personel).Include(c => c.Request).Include(c => c.Activity_Type).ToList();
                var part2 = ctx.Activity.Where(m => m.id_request == reqID).Include(c => c.Request).Include(c => c.Activity_Type).ToList();
                return part2;
            }
        }

        //<summary>
        //get activites by Personel ID
        //<summary>
        public static List<Activity> GetActivitesByPersonelID(int perID)
        {
            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
                Guid guidID = ToGuid(perID);
                return ctx.Activity.Where(m => m.id_personel == guidID).ToList();
            }
        }

        public static List<Activity> GetActivitesByPersonelID(Guid perID)
        {
            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
                return ctx.Activity.Where(m => m.id_personel == perID).ToList();
            }
        }

        #endregion

        //<summary>
        //adds activity to DB and assigns it to request
        //THIS DOES NOT ADDS REQUEST TO DB!
        //<summary>
        public static bool AddActivityToRequest(Activity activity, Request request, Activity_Type type)
        {
            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
                try
                {
                    Request baseRequest = ctx.Request.FirstOrDefault(m => m.id_request == request.id_request);
                    if(baseRequest == null)
                    {
                        return false;
                    }
                    activity.id_activity = Guid.NewGuid();
                    activity.id_request = baseRequest.id_request;
                    activity.act_type = type.act_type;
                    activity.Activity_Type = null;
                    activity.Personel = null;
                    activity.Request = null;
                    activity.id_personel = null;                
                    ctx.Entry(activity).State = EntityState.Detached;                    
                    ctx.Activity.Add(activity);
                    ctx.SaveChanges();
                    return true;
                }
                catch (Exception ex) 
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                    return false;
                }
            }
        }

        public static bool AddActivity(Activity actvitiy)
        {
            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
                try
                {
                    ctx.Entry(actvitiy.Personel).State = System.Data.Entity.EntityState.Unchanged;
                    ctx.Entry(actvitiy.Request).State = System.Data.Entity.EntityState.Unchanged;
                    ctx.Entry(actvitiy.Activity_Type).State = System.Data.Entity.EntityState.Unchanged;               
                    
                    ctx.Activity.Add(actvitiy);
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

        public static bool ChangeWorkerInActivity(Personel personel,Activity activity)
        {
            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
                try
                {
                    Activity baseActivity = ctx.Activity.FirstOrDefault(m => m.id_activity == activity.id_activity);
                    baseActivity.id_personel = personel.id_person;
                    ctx.SaveChanges();
                    return true;
                }
                catch(Exception ex)
                {
                    return false;
                }
            }
        }

        //Im not sure if this is right
        public static bool ChangeTypeInActivity(Activity_Type type, Activity activity)
        {
            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
                try
                {
                    Activity baseActivity = ctx.Activity.FirstOrDefault(m => m.id_activity == activity.id_activity);
                    baseActivity.Activity_Type = type;
                    ctx.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public static bool editActivity(Activity activity)
        {
            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
                try
                {
                    activity.Personel = null;
                    activity.Request = null;
                    activity.Activity_Type = null;
                    ctx.Entry(activity).State = EntityState.Modified;
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

        public static List<Activity> getActivitesByStatus(Status status, Request request)
        {
            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
                 return ctx.Activity.Where(a => a.status == status && a.Request.id_request == request.id_request).Include(c => c.Personel).Include(c => c.Request).Include(c => c.Activity_Type).ToList();
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
