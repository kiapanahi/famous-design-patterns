using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace WpfApp.Converters
{
    
    // Member Image converter utility. Converts a member id to an image path.
    
    public class MemberImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string size = (string)parameter;

            int id = int.Parse(value.ToString());
            if (id > 91) id = 0; // new members are getting the default silhouette icon

            return "Images/Members/" + size + "/" + id + ".jpg";
        }           

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
