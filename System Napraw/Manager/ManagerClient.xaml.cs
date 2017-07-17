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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace System_Napraw.Manager
{
    /// <summary>
    /// Interaction logic for ManagerClients.xaml
    /// </summary>
    public partial class ManagerClients : UserControl
    {
        private refreshRequestWindow refresher;
        private WindowMode mode;
        public List<Client> clients;

        public ManagerClients()
        {
            InitializeComponent();
            //TODO: Make sure that instantion of this object is updated. Do some tests
            dataGrid.ItemsSource = clients;
            refreshGird();
        }

        public void refreshGird()
        {
            dataGrid.ItemsSource = ClientDAO.GetClientList();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
           ManageClient addClient = new ManageClient(WindowMode.ADD,this);                                         
           addClient.Show();

        }

        private void findButton_Click(object sender, RoutedEventArgs e)
        {
            ManageClient searchClient = new ManageClient(WindowMode.CHOOSE,this);
            searchClient.Show();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            if(dataGrid.SelectedItem != null)
            {
                ManageClient editClient = new ManageClient(WindowMode.EDIT,this);
                editClient.Show();
            }
        }

        private void showObjectsButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                ShowObjects showObj = new ShowObjects(this);
                showObj.Show();
            }
            
        }
    }
}
