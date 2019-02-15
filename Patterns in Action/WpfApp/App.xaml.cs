using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfApp.ViewModels;

namespace WpfApp
{
    
    // interaction logic for App.xaml

    public partial class App : Application
    {
        
        // gets memberview model from MainWindow.
        
        public MemberViewModel MemberViewModel
        {
            get
            {
                var window = MainWindow as WindowMain;
                return window.ViewModel;
            }
        }
    }
}
