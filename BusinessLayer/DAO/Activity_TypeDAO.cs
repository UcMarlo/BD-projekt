using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DAO
{
    public static class Activity_TypeDAO
    {

        public static List<Activity_Type> GetAllActivityTypes() {
            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
                return ctx.Activity_Type.ToList();
            }
        }


        public static bool AddActivityType(Activity_Type actvitiy)
        {
            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
                try
                {
                    ctx.Activity_Type.Add(actvitiy);
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

        public static bool editActivityType(Activity_Type activityType)
        {
            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
                try
                {
                    Activity_Type baseActivityType = ctx.Activity_Type.FirstOrDefault(m => m.act_type == activityType.act_type);
                    baseActivityType = activityType;
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

    }
}
