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
using System_Napraw.InterfaceHelper;

namespace System_Napraw.Manager
{
    /// <summary>
    /// Interaction logic for ManagerAddActivity.xaml
    /// </summary>
    public partial class ManageActivity : Window
    {
        public Activity activity;

        public Personel personel;

        public Request request;

        private List<Activity_Type> activityTypes;

        private WindowMode windowMode;
        private ShowActivities showActivities;
        private Personel loggedInUser;
        private Worker worker;

        public ManageActivity()
        {
            InitializeComponent();
        }


        public ManageActivity(WindowMode windowMode, Activity activity,ShowActivities showActivities)
        {
            InitializeComponent();
            personel = loggedInUser;
            this.activity = activity;
            this.personel = PersonelDAO.GetPersonelByID(activity.id_personel);
            this.showActivities = showActivities;
            this.windowMode = windowMode;
            bindComobos();
            PrepareWindow();
        }


        public ManageActivity(WindowMode windowMode, Activity activity, Personel personel, Request request)
        {
            InitializeComponent();
            bindComobos();

            mapObjectToWindow(activity);
            this.windowMode = windowMode;
            this.request = request;
            this.personel = personel;
            this.activity = activity;
            PrepareWindow();
        }

        public ManageActivity(WindowMode windowMode,Request request, ShowActivities showActivities)
        {
            this.request = request;
            this.windowMode = windowMode;
            this.showActivities = showActivities;
            InitializeComponent();      
            bindComobos();
            PrepareWindow();
        }

        
        //implement editing activty by worker
        public ManageActivity(WindowMode windowMode, Activity activity, Personel loggedInUser, Worker worker)
        {
            InitializeComponent();
            
            this.windowMode = WindowMode.CHOOSE;
            this.activity = activity;
            this.personel = loggedInUser;
            //rezultat i date wykonania
            this.worker = worker;
            PrepareActivityToEditByWorker();
        }

        private void PrepareActivityToEditByWorker()
        {
            bindComobos();
            this.presonelButton.IsEnabled = false;
            this.presonelButton.Visibility = Visibility.Hidden;
            this.descriptionText.IsEnabled = false;
            this.currentWorker.Visibility = Visibility.Hidden;
            this.statusBox.SelectedItem = activity.status;
            this.statusBox.IsEnabled = false;
            activityComboBox.SelectedItem = activityTypes.Where(a => a.act_type == activity.Activity_Type.act_type);
            activityComboBox.IsEnabled = false;
            mapObjectToWindow(activity);
        }

        private void RefreshPersonelTextBlock()
        {
            currentWorker.Text = personel.first_name + " " + personel.second_name;
        }
        
        private void SetPersonelAsBlank()
        {
            personel = null;
            currentWorker.Text = "Brak Wykonanwcy";
        }

        #region CONSTRUCTOR STUFF

        private void PrepareWindow()
        {
            switch (windowMode)
            {
                case WindowMode.ADD:
                    PrepareWindowToAdd();
                    break;
                case WindowMode.EDIT:
                    PrepareWindowToEdit();
                    break;
                case WindowMode.SHOW:
                    PrepareWindowToShow();
                    break;
            }
        }

        //TODO: Make sure that this casting is correct & combobox index is also ok
        private void PrepareWindowToShow()
        {
            mapObjectToWindow(activity);
            statusBox.SelectedIndex = (int)activity.status;
            statusBox.IsEnabled = false;
            presonelButton.Content = "Pokaż wykonawce";
            descriptionText.IsEnabled = false;
            activityComboBox.SelectedIndex = activityTypes.TakeWhile(t => t.act_type == activity.act_type).Count();
        }

        private void PrepareWindowToEdit()
        {
            mapObjectToWindow(activity);
        }

        private void PrepareWindowToAdd()
        {
            this.activity = new Activity();
            beginDate.SelectedDate = DateTime.Now;
        }

        #endregion

        #region PERSONEL BUTTON STUFF

        private void ExecutePersonelButton()
        {
            switch (windowMode)
            {
                case WindowMode.ADD:
                    DoPersonelAdd();
                    break;
                case WindowMode.EDIT:
                    DoPersonelEdit();
                    break;
                case WindowMode.SHOW:
                    DoPersonelShow();
                    break;
            }
        }

        private void DoPersonelAdd()
        {
            ShowPersonel personelShow = new ShowPersonel(WindowMode.ADD, this);
            personelShow.Show();
        }

        private void DoPersonelShow()
        {
            mapObjectToWindow(activity);
        }

        private void DoPersonelEdit()
        {
            mapObjectToWindow(activity);  
        }

        #endregion

        #region ACCEPT BUTTON STUFF

        private void DoAction()
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
                case WindowMode.CHOOSE:
                    DoWorkerEdit();
                    break;
            }
        }

        private void DoWorkerEdit()
        {
            if (IsAllRequiredItemsFilled())
            {
                MapGUIToObject();
                ActivityDAO.editActivity(activity);
            }
            this.worker.refreshGird();
            this.Close();
        }

        private void DoShow()
        {
            this.Close();
        }

        private void DoEdit()
        {
            if (IsAllRequiredItemsFilled())
            {
                MapGUIToObject();
                ActivityDAO.editActivity(activity);
                this.showActivities.refreshGird();
                this.Close();
            }

        }

        private void DoAdd()
        {
            if (IsAllRequiredItemsFilled())
            {
                MapGUIToObject();
                ActivityDAO.AddActivityToRequest(activity, request,(Activity_Type)activityComboBox.SelectedItem);
                this.showActivities.refreshGird();
                this.Close();
            }

        }

        
        private void MapGUIToObject()
        {
            activity.status = (Status)this.statusBox.SelectedItem;
            activity.result = resultText.Text;
            activity.date_reg = beginDate.DisplayDate;
            activity.date_fin = endDate.DisplayDate;
            activity.description = descriptionText.Text;
            activity.Activity_Type = (Activity_Type)activityComboBox.SelectedItem;
            if(personel != null)
            {
                activity.id_personel = personel.id_person;
            }
        }

        #endregion
        
        private bool IsAllRequiredItemsFilled()
        {
            return (this.statusBox.SelectedValue != null &&
                (beginDate.DisplayDate != null)&& 
                (statusBox.SelectedValue != null) &&
                (activityComboBox.SelectedValue != null));
        }


        #region DOMINIK'S CODE

        private void bindComobos()
        {
           
            activityTypes = Activity_TypeDAO.GetAllActivityTypes();
            activityComboBox.DataContext = activityTypes;
            activityComboBox.ItemsSource = activityTypes;
            activityComboBox.DisplayMemberPath = "act_name";
            activityComboBox.SelectedValuePath = "act_type";
            
        }

        private void mapObjectToWindow(Activity activity)
        {
            
            if (activity.date_reg != null)
            {
                beginDate.SelectedDate = activity.date_reg;
            }
          
            endDate.SelectedDate = (DateTime)activity.date_fin;
            statusBox.SelectedItem = activity.status;
            activityComboBox.SelectedItem = activityTypes.Where(a => a.act_type == activity.act_type).First();
            resultText.Text = activity.result;
            descriptionText.Text = activity.description;
            currentWorker.Text = activity.Personel.first_name + " "+ activity.Personel.second_name;

        }

        public void ShowActivity()
        {
            foreach (Control ctrl in UserControlPanel.Children)
                    ctrl.IsEnabled = false;
            buttonAccept.Visibility = Visibility.Hidden;
        }


        private Activity addActivity()
        {
            

            var status = (Status)this.statusBox.SelectedValue;

            if (status != null)
            {
                activity.status = status;
            }
            else
                return null;

            

            var description = descriptionText.Text;

            if (!(String.IsNullOrWhiteSpace(description)))
            {
                activity.description = description;
            }
            else
                return null;
            try
            {
                var dateEnd = this.endDate.DisplayDate;


                activity.date_fin = dateEnd;
            }
            catch (Exception ex)
            {

            }

            var dateBegin = this.beginDate.DisplayDate;

            if ((dateBegin != null))
            {
                activity.date_reg = dateBegin;
            }
            else
                return null;
            if (personel != null)
            {
                activity.Personel = personel;
            }
            else
                return null;

            var result = this.resultText.Text;

            if (!(String.IsNullOrWhiteSpace(result)))
            {
                activity.result = result;
            }
            else
                return null;

        

            var activityType = (Activity_Type)this.activityComboBox.SelectedItem;

            if (activityType!=null)
            {
                activity.act_type = activityType.act_type;
                activity.Activity_Type = activityType;
            }
            else
                return null;

            try
            {
                //if (ActivityDAO.AddActivity(act))
                //    this.Close();
                //else
                    errorText.Text = "Wystapil blad prosze sprawdzić pola";
            }
            catch (Exception)
            {

               
            }
            if (this.showActivities.request!=null)
            {
                activity.Request = this.showActivities.request;
                activity.id_request = this.showActivities.request.id_request;
            }

            return activity;
        }

        public void refreshPersonData()
        {
            currentWorker.Text = activity.Personel.first_name + " " + activity.Personel.second_name;
        }
   
        #endregion

        #region BUTTONS

        private void presonelButton_Click(object sender, RoutedEventArgs e)
        {
            ExecutePersonelButton();
            //TODO: Delete this line if its necessary
          //  this.Close();
        }

        private void buttonAccept_Click(object sender, RoutedEventArgs e)
        {
            DoAction();
            //TODO: Delete this line if its necessary
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

#endregion

    }
}
