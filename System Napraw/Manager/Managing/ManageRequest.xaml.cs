using BusinessLayer;
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

namespace System_Napraw.Manager
{
    /// <summary>
    /// Interaction logic for ManagerAddRequest.xaml
    /// </summary>
    public partial class ManageRequest : Window
    {
        private refreshRequestWindow refresher;
        private WindowMode windowMode;
        public Request request { get; set; }

        public DataLayer.Object my_object { get; set; }

        public ManageRequest()
        {
            InitializeComponent();
        }

        public ManageRequest(WindowMode windowMode, refreshRequestWindow refresher, Request request = null)
        {
            InitializeComponent();
            this.refresher = refresher;
            this.windowMode = windowMode;
            this.request = request;
            initWindow();        
        }

        private void initWindow()
        {
            switch (windowMode)
            {
                case WindowMode.ADD:
                    prepareWindowToAdd();
                    break;
                case WindowMode.EDIT:
                    prepareWindowToEdit();
                    break;
                case WindowMode.SHOW:
                    ShowObject();
                    break;
                case WindowMode.CHOOSE:
                    break;
            }
        }


        private void prepareWindowToEdit()
        {
            my_object = ObjectDAO.GetObjectByID(request.id_object);
            mapObjectToGUI();
        }

        private void prepareWindowToAdd()
        {
            this.request = new Request();
            this.request.id_request = Guid.NewGuid();
            showActivitesButton.IsEnabled = false;
            beginDate.SelectedDate = DateTime.Now;
        }

        public void ShowObject()
        {
            foreach (Control ctrl in UserControlPanel.Children)
                ctrl.IsEnabled = false;
            AcceptButton.Visibility = Visibility.Hidden;
        }

        private void mapObjectToGUI()
        {
            beginDate.DisplayDate = (DateTime)request.date_reg;
            beginDate.SelectedDate = (DateTime)request.date_reg;
            if (request.date_fin_can != null)
            {
                endDate.DisplayDate = (DateTime)request.date_fin_can;
                endDate.SelectedDate = (DateTime)request.date_fin_can;
            }
            ResultTextBox.Text = request.result;
            DescriptionTextBox.Document.Blocks.Clear(); // FATHER WHY YOU HAVE FORSAKEN ME?
            DescriptionTextBox.Document.Blocks.Add(new Paragraph( new Run( request.description)));
            StatusComboBox.SelectedItem = request.status;
            objectNameText.Text = my_object.name;

            numberActivites.Text = request.Activity.Count().ToString();
        }

        private bool IsAllRequestedFilled()
        {
            return ((my_object != null) &&
                (beginDate.DisplayDate !=null));
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            DoAcceptButton();
        }

        private void DoAcceptButton()
        {
            switch (windowMode)
            {
                case WindowMode.ADD:
                    DoAdd();
                    break;
                case WindowMode.EDIT:
                    DoEdit();
                    break;
                case WindowMode.SHOW:
                    DoShow();
                    break;
                default:
                    this.Close();
                    break;
            }
        }

        private void DoShow()
        {
            this.Close();
        }

        private void DoEdit()
        {
            if (IsAllRequestedFilled() && parseRequestWindow())
            {
                RequestDAO.EditRequest(request);
                refresher();
                this.Close();
            }
        }

        private void DoAdd()
        {
            if (IsAllRequestedFilled())
            {
                saveRequest();
                refresher();
                this.Close();
            }
        }

        private void saveRequest()
        {

            if (parseRequestWindow())
            {
                RequestDAO.AddRequest(request);
            }
            
        }

        private bool parseRequestWindow()
        {          

            var result = ResultTextBox.Text;            
            request.result = result;
           
               

            var status = (Status)StatusComboBox.SelectedValue;
            if (status!=null)
            {
                request.status = status;
            }
            else
                return false;

            var description = new TextRange(DescriptionTextBox.Document.ContentStart, DescriptionTextBox.Document.ContentEnd).Text;
           
            request.description = description;
           

            request.id_manager = CurrentUserUtils.loggedInUser.id_person;
           // request.Personel = CurrentUserUtils.loggedInUser;

            if (my_object != null)
            {
              //  request.Object = my_object;
                request.id_object = my_object.id_object;

            }
            else
                return false;

            var begin_date = beginDate.SelectedDate;
            if (begin_date != null)
            {
                request.date_reg = (DateTime)begin_date;
            }
            else
                return false;

            try
            {
                           
            var fin_date = endDate.SelectedDate;
            
                request.date_fin_can = (DateTime)fin_date;
            }
            catch (Exception ex)
            {
                
            }


            return true;

        }


        private void ObjectTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void showActivitesButton_Click(object sender, RoutedEventArgs e)
        {
            //   ManagerShowActivities showActWin = new ManagerShowActivities();
            //showActWin.Show();
            ShowActivities showActivites = new ShowActivities(this.request,this);
            showActivites.Show();

        }

        private void selectObjectButton_Click(object sender, RoutedEventArgs e)
        {
            ShowObjects showObjWin = new ShowObjects(this);
            showObjWin.Show();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ResultTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void DescriptionTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
