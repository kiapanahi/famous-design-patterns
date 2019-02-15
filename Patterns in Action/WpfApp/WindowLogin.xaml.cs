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
using WpfApp.Models;

namespace WpfApp
{
    public partial class WindowLogin : Window
    {
        public WindowLogin()
        {
            InitializeComponent();
        }

        // link was clicked for credential info

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("For demonstration purposes please use\nEmail: debbie@company.com\nPassword: secret123", "Login credentials");
        }

        // OK buttons was clicked

        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            Cursor = Cursors.Wait;
            string email = emailBox.Text.Trim();
            string password = passwordBox.Password.Trim();

            try
            {
                var provider = new Provider();
                provider.Login(email, password);

                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Arrow;
                MessageBox.Show(ex.Message + " Please try again.", "Login failed");
            }
        }
    }
}
