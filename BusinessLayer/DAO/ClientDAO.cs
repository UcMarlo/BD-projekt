using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace DataLayer.DAO
{
    public static class ClientDAO
    {

        #region GETTERS
        public static List<Client> GetClientList()
        {
            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
                return ctx.Client.Include(c => c.Adress).Include(c => c.Object).Include(c => c.Object.Select(a => a.Client)).
                    Include(c => c.Object.Select(a => a.Object_Type))
                    .ToList();
            }
        }

        public static Client GetClientByID(int ID)
        {
            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
                Guid IDguid = ToGuid(ID);
                return ctx.Client.FirstOrDefault(m => m.id_client == IDguid);
            }

        }

        public static Client GetClientByID(Guid ID)
        {
            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
                return ctx.Client.FirstOrDefault(m => m.id_client == ID);
            }

        }
        #endregion

        public static bool AddClient(Client client)
        {
            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
                try
                {
                
                    ctx.Client.Add(client);
                    ctx.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
        }
        
        public static List<Client> getClientsByNameAndSecondName(string name, string second_name)
        {

               return  DAOsContext.context.Client.Where(c => c.first_name.Contains(name) && c.second_name.Contains(second_name)).ToList();
           

        }

        public static bool EditClient(Client client)
        {
            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
                try
                {

                    client.Object = null;                   
                    
                    ctx.Entry(client).State = System.Data.Entity.EntityState.Modified;
                    ctx.Entry(client.adressList.First()).State = EntityState.Modified;
                    ctx.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public static List<Client> SearchQuery(Client query)
        {

            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
                List<Client> queryResult = ctx.Client.Where(m =>
                m.id_client.ToString().Contains(query.id_client.ToString()) ||
                m.first_name.Contains(query.first_name) ||
                m.second_name.Contains(query.second_name) ||
                m.phone.Contains(query.phone)
                ).ToList();
                return queryResult;
            }

        }
        #region PRIVATE
        private static Guid ToGuid(int value)
        {
            byte[] bytes = new byte[16];
            BitConverter.GetBytes(value).CopyTo(bytes, 0);
            return new Guid(bytes);
        }

        public static List<Client> SearchQuery(Client query, Adress adressQuery)
        {

            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
                List<Adress> adresses = AdressDAO.getAdressByQuery(adressQuery);
                List<Client> clients = new List<Client>();
                foreach (Adress adress in adresses)
                {
                    clients.Add(ctx.Client.FirstOrDefault(m => m.id_client == adress.id_client));
                }
                return clients;
            }
        }
        #endregion
    }
}
