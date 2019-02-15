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
using WpfApp.ViewModels;

namespace WpfApp
{
    public partial class WindowOrders : Window
    {
        public WindowOrders()
        {
            InitializeComponent();
        }
        
        // helper that makes it easy to get to member ViewModel
        
        private MemberViewModel MemberViewModel
        {
            get { return (Application.Current as App).MemberViewModel; }
        }

        // called when window has been loaded

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // gets member model and set data context.

            var memberModel = MemberViewModel.CurrentMember;
            DataContext = memberModel;

            Title = "Orders for: " + memberModel.CompanyName;

            listViewOrders.SelectedIndex = 0;
        }
    }
}
