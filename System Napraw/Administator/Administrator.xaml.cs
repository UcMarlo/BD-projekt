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
using System_Napraw.InterfaceHelper;

namespace System_Napraw
{
    /// <summary>
    /// Interaction logic for Administrator.xaml
    /// </summary>
    public partial class Administrator : Window
    {
        public Administrator()
        {

                 

            InitializeComponent();
        }

        public void refreshGrid()
        {
            dataGrid.ItemsSource = PersonelDAO.GetPersonelList();
        }

      

        private void addNewWorker_Click(object sender, RoutedEventArgs e)
        {
            //GenerateForm genrator = new GenerateForm();
            //genrator.GenerateWindow(typeof(Personel));

            AddPersonel personelWin = new AddPersonel(WindowMode.ADD,null,this);            
            personelWin.Show();


        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            if (role.SelectedItem == null)
            {
                dataGrid.ItemsSource = PersonelDAO.GetPersonelList();
            }
            else
                dataGrid.ItemsSource = PersonelDAO.getUsersByRole((Role)role.SelectedItem);

        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            var person = (Personel)dataGrid.SelectedItem;
            if (person!=null)
            {
                AddPersonel personelWin = new AddPersonel(WindowMode.EDIT, person,this);
                personelWin.Show();
            }
         

          
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {

            var count = App.Current.Windows.Count;

            for (int i = 0; i < count; i++)
            {
                if (App.Current.Windows[i].Title != "Administrator")
                {
                    App.Current.Windows[i].Close();
                }



            }

            Login loginWindow = new Login();
            loginWindow.Show();
            this.Close();
        }
    }
}
