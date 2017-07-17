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

namespace System_Napraw.Manager
{
    /// <summary>
    /// Interaction logic for ShowActivities.xaml
    /// </summary>
    public partial class ShowActivities : Window
    {
        public ManageRequest manageRequest;
        public Request request;     
        private ManagerRequest managerRequest;

        private List<String> comboItems;

        public Status currentStatus { get; set; }

        public ShowActivities()
        {
            InitializeComponent();
            refreshGird();
        }

        public ShowActivities(Request request, ManageRequest manageRequest)
        {
            this.request = request;
            this.manageRequest = manageRequest;

            InitializeComponent();
            initCombos();
            refreshGird();
        }

        public ShowActivities(Request request, ManagerRequest managerRequest)
        {
            InitializeComponent();
            
            this.request = request;
            this.managerRequest = managerRequest;
            initCombos();
            refreshGird();
        }

        public void refreshGird()
        {
            try
            {

          
            dataGrid.ItemsSource = ActivityDAO.GetActivitesByReqID(request.id_request);
            }
            catch (Exception ex)
            {
                this.Close();
               
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                ManageActivity addActWin = new ManageActivity(WindowMode.EDIT, (Activity) dataGrid.SelectedItem, this);
                addActWin.Show();
            }
        }      

        private void showButton_Click(object sender, RoutedEventArgs e)
        {
            ManageActivity addActWin = new ManageActivity(WindowMode.SHOW,request,this);
            addActWin.Show();
        }

        private void createActivity_Click(object sender, RoutedEventArgs e)
        {
            ManageActivity addActWin = new ManageActivity(WindowMode.ADD,request,this);
            addActWin.Show();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            if(typeCombo.SelectedItem != null) {
                currentStatus = getComboValue(typeCombo.SelectedItem.ToString());
            }
            if (request!=null && !typeCombo.SelectedItem.Equals(" "))
            {
                dataGrid.ItemsSource = ActivityDAO.getActivitesByStatus(currentStatus,request);
            } else
            {
                refreshGird();
            }
            
        }

        private void initCombos()
        {
            comboItems = new List<String>();
            comboItems.Add("Aktywne");
            comboItems.Add("Anulowane");
            comboItems.Add("Zakonczone");
            comboItems.Add("W trakcie");
            comboItems.Add(" ");
            typeCombo.ItemsSource = comboItems;
        }

        private Status getComboValue(String input)
        {
            if (input == "Aktywne")
                return Status.Active;
            if (input == "Anulowane")
                return Status.Canceled;
            if (input == "Zakonczone")
                return Status.Finished;
            if (input == "W trakcie")
                return Status.Progress;
            //on default
            return Status.Active;
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
