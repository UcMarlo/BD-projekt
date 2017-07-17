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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataLayer;
using System_Napraw.Manager;

namespace System_Napraw
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public enum WindowMode { EDIT, SHOW, ADD, CHOOSE };

    public partial class MainWindow : Window
    {

        private Control clientControl;
        private Personel preson;

        public MainWindow(Personel preson)
        {
            InitializeComponent();
            Console.WriteLine("test");
            UserControlPanel.Children.Clear();
            UserControlPanel.Children.Add(clientControl = new ManagerClients());
            ClientsControl.Background = Brushes.LightBlue;
            RequestsControl.Background = Brushes.LightGray;
            logoutButton.Background = Brushes.LightGray;
            this.preson = preson;
        }

    

      

        private void ActivitesControl_Click(object sender, RoutedEventArgs e)
        {

            UserControlPanel.Children.Clear();
            UserControlPanel.Children.Add(clientControl = new ManagerClients());
        }

        private void ClientsControl_Click(object sender, RoutedEventArgs e)
        {
            UserControlPanel.Children.Clear();
            UserControlPanel.Children.Add(clientControl = new ManagerClients());
            ClientsControl.Background = Brushes.LightBlue;
            RequestsControl.Background = Brushes.LightGray;
            logoutButton.Background = Brushes.LightGray;

        }

        private void RequestsControl_Click(object sender, RoutedEventArgs e)
        {
            UserControlPanel.Children.Clear();
            UserControlPanel.Children.Add(clientControl = new ManagerRequest());
            ClientsControl.Background = Brushes.LightGray;
            RequestsControl.Background = Brushes.LightBlue;
            logoutButton.Background = Brushes.LightGray;
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            ClientsControl.Background = Brushes.LightGray;
            RequestsControl.Background = Brushes.LightGray;
            logoutButton.Background = Brushes.LightBlue;

            var count = App.Current.Windows.Count;

            for (int i = 0; i < count; i++)
            {
                if (App.Current.Windows[i].Title != "MainWindow")
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
