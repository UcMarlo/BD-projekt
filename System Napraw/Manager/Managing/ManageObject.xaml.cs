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
    /// Interaction logic for ManagerAddObject.xaml
    /// </summary>
    public partial class ManageObject : Window
    {
        private WindowMode mode;
        public DataLayer.Object obj;
        List<Object_Type> objectTypeList;
        List<String> objTypeCBList;
        private Object_Type objType;
        private ShowObjects showObjects;
        

        public Client client { get; internal set; }

        public ManageObject(WindowMode mode)
        {
            obj = new DataLayer.Object();
            InitializeComponent();
            bindObjectTypeComobo();          
        }

        public void Refresh()
        {
            objectTypeList = Object_TypeDAO.GetObjectTypeList();
            objectTypeComboBox.ItemsSource = objectTypeList;
            objectTypeComboBox.DisplayMemberPath = "type_name";
            objectTypeComboBox.SelectedValuePath = "type_code";        

        }
    
        public ManageObject(WindowMode mode, DataLayer.Object obj, ShowObjects show_obj)
        {
            this.showObjects = show_obj;
             
            this.obj = obj;
            InitializeComponent();
            bindObjectTypeComobo();
            if (mode != WindowMode.ADD)
            {
                if (mode == WindowMode.SHOW)
                    mapObjectToWindow();
                    ShowObject();
                if (mode==WindowMode.EDIT)
                {
                    mapObjectToWindow();
                   // ShowObject();
                }
                    //Wyswietlanie danych obiektu object
            }
       
            
        }

        public ManageObject(WindowMode mode, ShowObjects showObjects) : this(mode)
        {
            this.showObjects = showObjects;
            this.mode = mode;
            InitializeComponent();
            bindObjectTypeComobo();
        }

        private void bindObjectTypeComobo()
        {
            objectTypeList = Object_TypeDAO.getAllObjectTypes();
            objectTypeComboBox.ItemsSource = objectTypeList;
            objectTypeComboBox.DisplayMemberPath = "type_name";
            objectTypeComboBox.SelectedValuePath = "type_code";
        }

        private void mapObjectToWindow()
        {
                ownerTextBlock.Text = obj.Client.clientString;
                objectTypeComboBox.SelectedItem = objectTypeList.Where(c => c.type_code == obj.obj_type).First();
                objectNameTextBox.Text = obj.name;
        }

        public void ShowObject()
        {
            foreach (Control ctrl in UserControlPanel.Children)
                    ctrl.IsEnabled = false;
           // acceptButton.Visibility = Visibility.Hidden;
        }


        private bool parseWindow()
        {
            if ((String.IsNullOrWhiteSpace(objectNameTextBox.Text)))
            {
                return false;
            }

            obj.name = objectNameTextBox.Text;

            objType = (Object_Type)objectTypeComboBox.SelectedItem;

            obj.Object_Type = objType;

            if (objType==null)
            {
                return false;
            }
            if (obj==null)
            {
                return false;
            }
            return true;

        }

        private void switchWindowMode()
        {
            switch (mode)
            {
                case WindowMode.EDIT:
                    doEditClick();
                    break;
                case WindowMode.SHOW:
                    break;
                case WindowMode.ADD:
                    doAddClick();
                    break;
                case WindowMode.CHOOSE:
                    break;
                default:
                    break;
            }
        }

        private void doEditClick()
        {
            if (parseWindow())
            {
                ObjectDAO.editObject(obj, objType, client);
            }
        }

        private void doAddClick()
        {
            if (parseWindow())
            {

                ObjectDAO.AddObject(obj, objType, client);
            }
        }

        private void acceptButton_Click(object sender, RoutedEventArgs e)
        {

            switchWindowMode();
           
            this.showObjects.refreshGird();
            this.Close();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void chooseOwnerButton_Click(object sender, RoutedEventArgs e)
        {
            //ManagerClients managerWindow = new ManagerClients();
            //managerWindow.Show();
            ShowClients clientsWindow = new ShowClients(this, WindowMode.CHOOSE);
            clientsWindow.Show();
        }

        private void newObjectTypeButton_Click(object sender, RoutedEventArgs e)
        {
            ManagerAddObject_type managerAddObject_type = new ManagerAddObject_type(this);
            managerAddObject_type.Show();
        }

    }
}
