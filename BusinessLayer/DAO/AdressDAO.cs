using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DAO
{
    public static class AdressDAO
    {
        #region GETTERS
        public static Adress GetAdressByClientID(int cliID)
        {
            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
                Guid IDguid = ToGuid(cliID);
                return ctx.Adress.FirstOrDefault(m => m.id_client == IDguid);
            }

        }
        #endregion
        public static Adress GetAdressByClientID(Guid cliGuid)
        {
            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
                return ctx.Adress.FirstOrDefault(m => m.id_client == cliGuid);
            }

        }

        public static List<Adress> getAdressByQuery(Adress query)
        {

            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
                List<Adress> queryResult = ctx.Adress.Where(m =>
                m.id_client.ToString().Contains(query.id_client.ToString()) ||
                m.adress1.Contains(query.adress1) ||
                m.adress1.Contains(query.city) ||
                m.postcode.Contains(query.postcode)
                ).ToList();
                return queryResult;
            }
        }

        public static bool AddAdress(Adress adress,Client client)
        {

            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
                try
                {
                    adress.id_adress = new Guid();
                    adress.id_client = client.id_client;
                    ctx.Adress.Add(adress);
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

        public static bool EditAdress(Adress adress)
        {
            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
                try
                {
                    Adress baseAdress = ctx.Adress.FirstOrDefault(m => m.id_adress == adress.id_adress);
                    baseAdress = adress;
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
