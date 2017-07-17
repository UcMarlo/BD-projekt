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

namespace System_Napraw.Administator
{
    /// <summary>
    /// Interaction logic for AddPersonel.xaml
    /// </summary>
    public partial class AddPersonel : Window
    {

        WindowMode currentMode;
        public Personel person { get; set; }
        public Administrator adminWin { get; set; }

        public AddPersonel(WindowMode mode, Personel person, Administrator admin)
        {
            InitializeComponent();
            currentMode = mode;
            this.adminWin = admin;
            try
            {
                this.person = person;
                if (mode == WindowMode.EDIT)
                {
                    populateValuesForEdit(person);
                }

               // userRole.ItemsSource = Enum.GetValues(typeof(Role)).Cast<Role>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
               
            }

            var parent = (Window)this.Parent;
           
            


        }   

        private void populateValuesForEdit(Personel person)
        {
            person = PersonelDAO.decryptPass(person);

            Email.Text = person.email;
            firstName.Text = person.first_name;
            
            passWord.Password = person.password;

            lastName.Text = person.second_name;
            userRole.SelectedValue = person.role;
            retireDate.SelectedDate = person.retire_date;

        }

        private void acceptButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentMode==WindowMode.ADD)
            {
                person = new Personel();
                person.id_person = Guid.NewGuid();
            }
           
           


            
            person.email = Email.Text;
            person.first_name = firstName.Text;
            person.password = passWord.Password;
        
            
            person.second_name = lastName.Text;       
            person.role = (Role)userRole.SelectedValue;

            try
            {
                person.retire_date = retireDate.SelectedDate;
            }
            catch (Exception)
            {
                person.retire_date = null;
                
            }

            if (currentMode == WindowMode.ADD)
            {
                PersonelDAO.AddPersonel(person);
            }
            else if(currentMode == WindowMode.EDIT)
                PersonelDAO.UpdatePersonelData(person);
            if (adminWin!=null)
            {
                adminWin.refreshGrid();
            }
          
          

            this.Close();
            
            

        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
