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
    /// Interaction logic for ManagerAddObject_type.xaml
    /// </summary>
    public partial class ManagerAddObject_type : Window
    {
        ManageObject manageObject;
        public ManagerAddObject_type(ManageObject manageObject)
        {
            this.manageObject = manageObject;
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            if(!(string.IsNullOrWhiteSpace(CodeTextBox.Text) && string.IsNullOrWhiteSpace(NameTextBox.Text)))
            {
                Object_Type objectType = new Object_Type();
                objectType.type_name = NameTextBox.Text;
                objectType.type_code = CodeTextBox.Text;
                Object_TypeDAO.AddType(objectType);
                manageObject.Refresh();
                this.Close();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
