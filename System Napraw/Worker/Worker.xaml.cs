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
using DataLayer;
using System_Napraw.Manager;
using BusinessLayer;

namespace System_Napraw
{
    /// <summary>
    /// Interaction logic for Worker.xaml
    /// </summary>
    public partial class Worker : Window
    {
        private Personel preson;

        public Worker()
        {
            InitializeComponent();
        }

        public Worker(Personel preson)
        {
            InitializeComponent();

            this.preson = preson;

            dataGrid.ItemsSource = preson.Activity.Where(a => a.status == Status.Active);

           

        }

        public void refreshGird()
        {
            dataGrid.ItemsSource = preson.Activity.Where(a => a.status == Status.Active);
        }

        private void acceptActivty_Click(object sender, RoutedEventArgs e)
        {
            if(dataGrid.SelectedItem != null){ 
                Activity activity = (Activity)dataGrid.SelectedItem;
                activity.date_fin = DateTime.Now;
                activity.status = Status.Finished;
                ManageActivity manActWin = new ManageActivity(WindowMode.CHOOSE, activity, CurrentUserUtils.loggedInUser ,this);
                manActWin.ShowDialog();
            }
            
        }

        private void cancelActivty_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                Activity activity = (Activity)dataGrid.SelectedItem;
                activity.status = Status.Canceled;
                activity.date_fin = DateTime.Now;
                if (activity != null)
                {
                    ManageActivity manActWin = new ManageActivity(WindowMode.CHOOSE, activity, CurrentUserUtils.loggedInUser, this);
                    manActWin.ShowDialog();
                }
            }
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            var count = App.Current.Windows.Count;

            for (int i = 0; i < count; i++)
            {
                if (App.Current.Windows[i].Title != "Worker")
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
