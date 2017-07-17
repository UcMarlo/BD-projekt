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

namespace System_Napraw.Manager.Showing
{
    /// <summary>
    /// Interaction logic for ShowClients.xaml
    /// </summary>
    public partial class ShowClients : Window
    {
        private WindowMode cHOOSE;
        private ManageObject manageObject;
        private ShowObjects showObjects;

        public ShowClients()
        {
            InitializeComponent();
        }

        public ShowClients(ManageObject manageObject, WindowMode cHOOSE)
        {
            InitializeComponent();
            this.manageObject = manageObject;
            this.cHOOSE = cHOOSE;
            refreshClientGrid();
        }

        public ShowClients(ShowObjects showObjects)
        {
            this.showObjects = showObjects;
            InitializeComponent();
            refreshClientGrid();
        }

        public void refreshClientGrid()
        {
            var clients = ClientDAO.GetClientList();

            if (clients != null)
            {
                dataGrid.ItemsSource = clients;
            }

            
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            ManageClient addActWin = new ManageClient(WindowMode.EDIT, this);
            addActWin.Show();
        }

        private void newButton_Click(object sender, RoutedEventArgs e)
        {
            ManageClient addClientWin = new ManageClient(WindowMode.ADD,this);
            addClientWin.Show();
        }

        private void showButton_Click(object sender, RoutedEventArgs e)
        {
            ManageClient addActWin = new ManageClient(WindowMode.SHOW, this);
            addActWin.Show();
        }

        private void chooseButton_Click(object sender, RoutedEventArgs e)
        {

            if (dataGrid.SelectedItem != null)
            {
                if (manageObject!=null)
                {
                    this.manageObject.client = (Client)dataGrid.SelectedItem;
                    this.manageObject.ownerTextBlock.Text = this.manageObject.client.first_name + this.manageObject.client.second_name;
                }
                else if (showObjects!=null)
                {
                    if (dataGrid.SelectedItem != null)
                    {
                        this.showObjects.owner = (Client)dataGrid.SelectedItem;
                        this.showObjects.ownerTextBox.Text = this.showObjects.owner.first_name + this.showObjects.owner.second_name;
                    }
                }
                else if (showObjects!=null)
                {
                    showObjects.owner = (Client)dataGrid.SelectedItem;
                }
               
                this.Close();
            }
        }

        private void applyFilter_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = ClientDAO.getClientsByNameAndSecondName(nameFilter.Text, secondNameFilter.Text);
        }

        private void cancelFilter_Click(object sender, RoutedEventArgs e)
        {
            refreshClientGrid();
            nameFilter.Text = String.Empty;
            secondNameFilter.Text = String.Empty;
        }
    }
}
