using DataLayer;
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

namespace System_Napraw.Manager
{
    public delegate void refreshRequestWindow();

    /// <summary>
    /// Interaction logic for ManagerRequest.xaml
    /// </summary>
    public partial class ManagerRequest : UserControl
    {
        private refreshRequestWindow refresher;

       private void refreshGrid()
        {
            requestGrid.ItemsSource = RequestDAO.GetAllRequests(); 


        }

        public ManagerRequest()
        {
            InitializeComponent();

            requestGrid.ItemsSource = RequestDAO.GetAllRequests();

             refresher = new refreshRequestWindow(refreshGrid);
        }

        

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            ManageRequest addReqWin = new ManageRequest(WindowMode.ADD, refresher);
            addReqWin.Show();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            ManageRequest addReqWin = new ManageRequest(WindowMode.EDIT, refresher);
            addReqWin.Show();
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            var end_date = DateTime.MaxValue;
            var begin_date = DateTime.MinValue;
            if (beginDate.SelectedDate != null)
            {
                begin_date = (DateTime)beginDate.SelectedDate;
            }

            if (endDate.SelectedDate != null)
            {
                end_date = (DateTime)endDate.SelectedDate;
            }

            if (begin_date < end_date)
            {
                requestGrid.ItemsSource = RequestDAO.getRequestByDate(begin_date, end_date);
            }
        }

        private void showActivites_Click(object sender, RoutedEventArgs e)
        {
            var req = (Request)requestGrid.SelectedItem;
            if (req != null)
            {
                ShowActivities showActivWin = new ShowActivities(req, this);
                showActivWin.Show();
            }
        }

        private void editButton_Click_1(object sender, RoutedEventArgs e)
        {
            if(requestGrid.SelectedItem != null){
                ManageRequest editRequest = new ManageRequest(WindowMode.EDIT, refresher, (Request)requestGrid.SelectedItem);
                editRequest.Show();
            }
        }
    }
}
