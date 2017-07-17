using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DataLayer.DAO
{
    public static class PersonelDAO
    {
        #region GETTERS
        public static List<Personel> GetPersonelList()
        {
            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
                return ctx.Personel.ToList();
            }
            
        }

        public static Personel GetPersonelByID(int ID)
        {
            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
                Guid IDguid = ToGuid(ID);
                return ctx.Personel.FirstOrDefault(m => m.id_person == IDguid);
            }

        }
        public static Personel GetPersonelByID(Guid ID)
        {
            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
                return ctx.Personel.FirstOrDefault(m => m.id_person == ID);
            }

        }
        #endregion

        public static bool AddPersonel(Personel personel)
        {

            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
                try
                {
                    personel.id_person = Guid.NewGuid();
                    personel.password = PassEncryption.Encrypt(personel.password, true);

                    if (ctx.Personel.Where(a => a.email == personel.email).Count() == 0)
                    {
                        if (ctx.Personel.Where( a => a.first_name == personel.first_name && a.second_name == personel.second_name).Count() == 0)
                        {
                            

                            ctx.Personel.Add(personel);
                            ctx.SaveChanges();
                            return true;
                        }
                    }


                    return false;

                }
                catch (Exception ex)
                {
                    //TODO: jak coś nie pykło to dodaj logowanie EX
                    return false;
                }


            }
        }

        public static bool UpdatePersonelData(Personel personel)
        {
            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
                try
                {

              
                    personel.password = PassEncryption.Encrypt(personel.password, true);
                    ctx.Entry(personel).State = EntityState.Modified;
                    ctx.SaveChanges();
                    return true;

                }
                catch (Exception)
                {

                    return false;
                }

            }
            return false;
        }

        public static Personel getPersonByLoginAndPassword(string login, string password)
        {

            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
                try
                {
                    // DAOsContext.context.Configuration.LazyLoadingEnabled = false;
                    var person = ctx.Personel.Where(a => a.email == login).Include(c => c.Activity.Select(b => b.Request.Object)).Include(c => c.Activity.Select(b => b.Request.Personel)).Include(c => c.Request).First();

                    var pass = PassEncryption.Decrypt(person.password, true);

                    if (pass == password)
                    {
                        return person;
                    }

                }
                catch (Exception ex)
                {
                    return null;

                }
                return null;

            }
        }

        public static int getAdminsInDb()
        {
            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
               return ctx.Personel.Where(a => a.role == Role.Admin).Count();
            }
        }

        public static List<Personel> getUsersByRole(Role role)
        {
            using (System_NaprawEntities ctx = new System_NaprawEntities())
            {
                return ctx.Personel.Where(a => a.role == role).ToList();
            }
        }

        public static Personel decryptPass(Personel personel)
        {
            personel.password = PassEncryption.Decrypt(personel.password, true);
            return personel;
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
