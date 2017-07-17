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
using System_Napraw.Manager.Showing;

namespace System_Napraw.Manager
{
    /// <summary>
    /// Interaction logic for ManagerSelectClientForObject.xaml
    /// </summary>
    public partial class ShowObjects : Window
    {
        private ManageRequest manageRequest;
        private ManagerClients managerClients;

        public Client owner { get; set; }
        public Object_Type object_type { get; set; }

        public ShowObjects(ManagerClients manager_clients)
        {
            InitializeComponent();
            this.managerClients = manager_clients;
            this.ownerButton.IsEnabled = false;
            try
            {          
                this.owner = (Client)this.managerClients.dataGrid.SelectedItem;
                this.ownerTextBox.Text = this.owner.clientString;
                refreshGird();
                bindCombo();
            }
            catch (Exception)
            {
                this.Close();
            }
        }

        public ShowObjects(ManageRequest manageRequest)
        {
            this.manageRequest = manageRequest;
            InitializeComponent();
            refreshGird();
            bindCombo();
        }

        public void refreshGird()
        {
            if (owner!=null)
            {
                var list = owner.Object;
                dataGrid.ItemsSource = list;
            }
            else
            {
                var list = ObjectDAO.GetObjectList();
                dataGrid.ItemsSource = list;
            }

        }

        private void bindCombo()
        {
            var objectTypes = Object_TypeDAO.getAllObjectTypes();
            objectType.DataContext = objectTypes;
            objectType.ItemsSource = objectTypes;
            objectType.DisplayMemberPath = "type_name";
            objectType.SelectedValuePath = "type_code";
        }



        private void chooseButton_Click(object sender, RoutedEventArgs e)
        {
            var my_object = (DataLayer.Object)dataGrid.SelectedItem;
            if (dataGrid.SelectedItem!=null)
            {
                this.manageRequest.my_object = my_object;
                this.manageRequest.objectNameText.Text = my_object.name;
            }
            this.Close();
            
        }

        private void newObjButton_Click(object sender, RoutedEventArgs e)
        {
            ManageObject objWindow = new ManageObject(WindowMode.ADD,this);
            objWindow.Show();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            var my_object = dataGrid.SelectedItem;

            if (my_object!=null)
            {
                
                ManageObject objWindow = new ManageObject(WindowMode.EDIT,(DataLayer.Object)my_object,this);
                objWindow.Show();
            }

       
        }

        private void ownerButton_Click(object sender, RoutedEventArgs e)
        {
            ShowClients clientsWin = new ShowClients(this);
            clientsWin.Show();

        }

        internal void Show(Request request)
        {
            throw new NotImplementedException();
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            List<DataLayer.Object> temp_list;
            this.object_type = (Object_Type) objectType.SelectedItem;
            
            if (this.object_type!=null)
            {
               temp_list = ObjectDAO.GetObjectListByType(object_type);
            }
            else
            {
                temp_list = ObjectDAO.GetObjectList();
            }
            if (owner!=null)
            {
               dataGrid.ItemsSource =  temp_list.Where(a => a.Client.id_client == owner.id_client);
            }
            else
            {
                dataGrid.ItemsSource = temp_list;
            }

        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }


}
