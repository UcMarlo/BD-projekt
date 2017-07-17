using DataLayer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BusinessLayer
{
    public class NullableVaueConverter : IValueConverter
    {

  
     #region IValueConverter Members
  
     public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
     {
         return value;
     }
  
     public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
     {
            if (string.IsNullOrEmpty(value.ToString()))
            {
                var temp = new Personel();
                temp.first_name = "Brak";
                temp.second_name = " Wykonawcy";
                return temp;
            }
  
         return value;
     }
  
     #endregion


    }
}
