using BusinessLayer;
using DataLayer;
using DataLayer.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System_Napraw.Administator;

namespace System_Napraw
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {

        public Login()
        {
            if (PersonelDAO.getAdminsInDb() == 0)
            {
                AddPersonel addPersonel = new AddPersonel(WindowMode.ADD,null,null);
                addPersonel.Show();
                this.Hide();
            }
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {



            var login = loginForm.Text;
            var pass = passForm.Password;
            if ( login != String.Empty)
            {
                if (pass != String.Empty)
                {
                    var preson = PersonelDAO.getPersonByLoginAndPassword(login, pass);

                    if (preson != null)
                    {
                        if (!(preson.retire_date < DateTime.Now))
                        {                          

                            if (preson.role == (Role.Admin))
                            {
                                Administrator adminWin = new Administrator();

                                adminWin.Show();
                                this.Close();

                            }
                            else if (preson.role == (Role.Manager))
                            {
                                MainWindow mangWin = new MainWindow(preson);
                                mangWin.Show();
                                this.Close();

                            }
                            else if (preson.role == (Role.Worker))
                            {
                                Worker mangWin = new Worker(preson);
                                mangWin.Show();
                                this.Close();

                            }
                        }
                    }
                    CurrentUserUtils.loggedInUser = preson;





                }
            }
        }
    }
}
