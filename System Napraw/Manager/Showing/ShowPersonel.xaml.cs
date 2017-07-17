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
    /// Interaction logic for ManagerSelectWorkerForActivity.xaml
    /// </summary>
    public partial class ShowPersonel : Window
    {
        private ManageActivity manageActivity;

        public ShowPersonel()
        {
            InitializeComponent();
            initGird();
        }

        public ShowPersonel(WindowMode windowStyle)
        {

            InitializeComponent();
            initGird();

            if (windowStyle == WindowMode.ADD)
                editButton.Visibility = Visibility.Hidden;

            

        }

        public ShowPersonel(WindowMode windowStyle, ManageActivity manageActivity) : this(windowStyle)
        {
            this.manageActivity = manageActivity;
            InitializeComponent();
            initGird();
        }

        private void initGird()
        {
            dataGrid.ItemsSource = PersonelDAO.getUsersByRole(DataLayer.Role.Worker);
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {

            Personel person = (Personel)dataGrid.SelectedItem;
            
            this.Close();
        }

        private void chooseButton_Click(object sender, RoutedEventArgs e)
        {
            this.manageActivity.personel = (Personel)dataGrid.SelectedItem;
            this.manageActivity.activity.Personel = (Personel)dataGrid.SelectedItem;
            this.manageActivity.refreshPersonData();          
            this.Close();
        }
    }
}
