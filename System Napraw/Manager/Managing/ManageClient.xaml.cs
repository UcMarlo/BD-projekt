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
    /// Interaction logic for ManagerAddClient.xaml
    /// </summary>
    /// 
    public partial class ManageClient : Window
    {
        private refreshRequestWindow refresher;
        private Client client;
        private Adress adress;
        private WindowMode mode;
        private ManagerClients managerClients;
        
        private ShowClients showClients;

        public ManageClient(WindowMode mode, refreshRequestWindow refresher, Client client)
        {
            this.refresher = refresher;
            InitializeComponent();
            this.mode = mode;
            switchMode();

           
        }

        public ManageClient(WindowMode mode, ManagerClients managerClients)
        {
            InitializeComponent();
            this.mode = mode;
            this.managerClients = managerClients;
            this.client = (Client)managerClients.dataGrid.SelectedItem;
          
                switchMode();
            
           // else
               // this.Close();
          
           
            
        }


        public ManageClient(WindowMode mode, ShowClients showClients) 
        {
            InitializeComponent();
            this.showClients = showClients;
            this.mode = mode;
            try
            {

          
            this.client = (Client)showClients.dataGrid.SelectedItem;
            }
            catch (Exception)
            {
                this.Close();
            }

            switchMode();
        }

        private void switchMode()
        {
            switch (this.mode)
            {
                case WindowMode.ADD:
                    break;
                //case WindowMode.CHOOSE:
                //    break;
                case WindowMode.EDIT:
                    PrepareClientToEdit();
                    break;
                case WindowMode.SHOW:
                    ShowClient();
                    break;
            }
        }

        internal void Show(ShowClients showClients, WindowMode aDD)
        {
            throw new NotImplementedException();
        }

        private void PrintClient()
        {
            nameText.Text = client.first_name;
            secondNameText.Text = client.second_name;
            phoneText.Text = client.phone;
            Adress adress = AdressDAO.GetAdressByClientID(client.id_client);
            adressText.Text = adress.adress1;
            cityText.Text = adress.city;
            zipCodeText.Text = adress.postcode;
        }

        private void SearchClient()
        {

        }

        public void PrepareClientToEdit()
        {
            foreach (Control ctrl in UserControlPanel.Children)
                ctrl.IsEnabled = true;
            acceptButton.Visibility = Visibility.Visible;
            PrintClient();
        }

        public void ShowClient()
        {
            foreach (Control ctrl in UserControlPanel.Children)
                ctrl.IsEnabled = false;
            acceptButton.Visibility = Visibility.Hidden;
            PrintClient();
        }

        private bool addClientToDataBase()
        {
            if (mode == WindowMode.ADD)
            {
                client = new Client();

                client.id_client = Guid.NewGuid();
            }
           

            var name = this.nameText.Text;

            if (!(String.IsNullOrWhiteSpace(name)))
            {
                client.first_name = name;
            }
            else
                return false;

            var last_name = this.secondNameText.Text;

            if (!(String.IsNullOrWhiteSpace(last_name)))
            {
                client.second_name = last_name;
            }
            else
                return false;

            var phone = this.phoneText.Text;

            if (!(String.IsNullOrWhiteSpace(phone)))
            {
                client.phone = phone;
            }
            else
                return false;

            var adress = this.adressText.Text;

            if (!(String.IsNullOrWhiteSpace(adress)))
            {

            }
            else
                return false;

            if (mode==WindowMode.ADD)
            {
                client.Adress =  this.getAdressFromWindow(client) ;
            }
            else
            {
                client.Adress = this.getAdressFromWindow(client);
            }
        



           

            //MessageBox.Show("Wystapil blad prosze sprawdzić pola");
            return true;


        }

        private ICollection<Adress> getAdressFromWindow(Client client)
        {
            Adress adress;

            if (mode==WindowMode.ADD)
            {
                 adress = new Adress();

                adress.id_adress = Guid.NewGuid();

                client.Adress = new List<Adress>() { adress };

            }
            else
            {
               adress  = client.Adress.First();
            }


            var street = this.adressText.Text;


            adress.adress1 = street;

            var city = this.cityText.Text;

            if (!(String.IsNullOrWhiteSpace(city)))
            {
                adress.city = city;
            }

            var zipCode = this.zipCodeText.Text;

            if (!(String.IsNullOrWhiteSpace(zipCode)))
            {
                adress.postcode = zipCode;
            }

            adress.Client = client;



            return client.Adress;




        }


        private void doAcceptButton()
        {
            switch (this.mode)
            {
                case WindowMode.ADD:
                    performAddUser();                   
                    break;
                case WindowMode.CHOOSE:
                    performSearchQuery();
                    break;
                case WindowMode.EDIT:
                    performEditUser();
                    break;
                case WindowMode.SHOW:
                    //do nothing
                    break;
            }
        }

        private bool isAllRequiredFieldsFilled()
        {
            return UserControlPanel.Children.OfType<TextBox>().Any(tb => string.IsNullOrEmpty(tb.Text)); 
        }


        private void assignTextBoxToDTO()
        {
            client.first_name = nameText.Text;
            client.second_name = secondNameText.Text;
            client.phone = phoneText.Text;

            adress.adress1 = adressText.Text;
            adress.city = cityText.Text;
            adress.postcode = zipCodeText.Text;
        }

        private void performAddUser()
        {
            
                addClientToDataBase();
            if (ClientDAO.AddClient(client))
            {
                if (showClients != null)
                {
                    showClients.refreshClientGrid();

                }
                else
                    this.managerClients.refreshGird();                            
            }
            // ClientDAO.AddClient(client);

            this.Close();
            
        }

        private void performEditUser()
        {
            addClientToDataBase();
            ClientDAO.EditClient(client);
            if (showClients != null)
            {
                showClients.refreshClientGrid();

            }
            else
                managerClients.refreshGird();          
           
                this.Close();            
        }

        private void performSearchQuery()
        {
            Client query = new Client();
            query.first_name = nameText.Text;
            query.second_name = secondNameText.Text;
            query.phone = phoneText.Text;
            
            // if those fields are not empty
            if(!String.IsNullOrEmpty(adressText.Text) || !String.IsNullOrEmpty(cityText.Text)|| !String.IsNullOrEmpty(zipCodeText.Text))
            {
                Adress adressQuery = new Adress();
                adressQuery.adress1 = adressText.Text;
                adressQuery.city = cityText.Text;
                adressQuery.postcode = zipCodeText.Text;
                List<Client> clients = ClientDAO.SearchQuery(query,adressQuery);
                managerClients.clients = clients;
            }
            else
            {
                List<Client> clients = ClientDAO.SearchQuery(query);
                managerClients.clients = clients;
            }
            this.Close();            
        }

        //TODO: Prepare switchcase for WindowMode
        private void acceptButton_Click(object sender, RoutedEventArgs e)
        {
            doAcceptButton();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
