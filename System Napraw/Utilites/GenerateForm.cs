using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace System_Napraw.InterfaceHelper
{
    class GenerateForm
    {
        

        public Grid mainGird { get; set; }

        public StackPanel panel { get; set; }

        public void GenerateWindow(Type classType)
        {
            Window window = new Window();
         
            window.Width = 600;
            window.Height = 300;
            mainGird = new Grid();
             panel = new StackPanel();
            panel.Orientation = Orientation.Vertical;
            panel.Margin = new Thickness(10);
            window.Content = mainGird;
            mainGird.Children.Add(panel);
            var properties = classType.GetProperties().Where(m => m.PropertyType != typeof(Guid));

            foreach (var item in properties)
            {
                GenerateSimpleForm(item.PropertyType, item.Name.ToString());
            }

            buttonsBottom();

            window.Show();
        }
        


        public  void GenerateSimpleForm(Type formType , string name)
        {
            if (!(formType.GetInterfaces().Contains(typeof(IEnumerable)) && formType != typeof(String)))
            {


                Label label = new Label();
                label.Content = name;

                TextBox box = new TextBox();
                box.Width = 200;                
                panel.Children.Add(label);
                panel.Children.Add(box);
            }
            


          

        }


        public void buttonsBottom()
        {
            Button ok = new Button();
            ok.Width = 150;
            ok.Content = "OK";
            ok.Click += Ok_Click;
            Button cancel = new Button();
            cancel.Content = "Anuluj";
            cancel.Width = 150;
            ok.Margin = new Thickness(10);
            cancel.Margin = new Thickness(10);
            panel.Children.Add(ok);
            panel.Children.Add(cancel);
        }

        private void Ok_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
