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
    public partial class WindowMember : Window
    {
        // new member flag
        
        public bool IsNewMember { get; set; }

        private string originalEmail;
        private string originalCompany;
        private string originalCity;
        private string originalCountry;
       
        public WindowMember()
        {
            InitializeComponent();
        }

        // helper to get member ViewModel.
        
        private MemberViewModel MemberViewModel
        {
            get { return (Application.Current as App).MemberViewModel; }
        }


        // load new or existing record

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CommandModel commandModel;

            // New member.
            if (IsNewMember)
            {
                this.DataContext = MemberViewModel.NewMemberModel;
                Title = "Add new member";

                commandModel = MemberViewModel.AddCommandModel;

                // display hint message
                LabelNewMessage1.Visibility = Visibility.Visible;
                LabelNewMessage2.Visibility = Visibility.Visible;
            }
            else
            {
                this.DataContext = MemberViewModel.CurrentMember;

                // save off original values. due to binding viewmodel is changed immediately when editing.
                // so, when canceling we have these values to restore original state.
                // suggestion: could be implemented as Memento pattern.

                originalEmail = MemberViewModel.CurrentMember.Email;
                originalCompany = MemberViewModel.CurrentMember.CompanyName;
                originalCity = MemberViewModel.CurrentMember.City;
                originalCountry = MemberViewModel.CurrentMember.Country;

                Title = "Edit member";

                commandModel = MemberViewModel.EditCommandModel;
            }

            textBoxEmail.Focus();

            // the command helps determine whether save button is enabled or not

            buttonSave.Command = commandModel.Command;
            buttonSave.CommandParameter = this.DataContext;
            buttonSave.CommandBindings.Add(new CommandBinding(commandModel.Command, commandModel.OnExecute, commandModel.OnCanExecute));
        }

        // save button was clicked

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            Close();
        }

        // cancel button was clicked

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            // restore viewmodel to original values

            if (!IsNewMember)
            {
                MemberViewModel.CurrentMember.Email = originalEmail;
                MemberViewModel.CurrentMember.CompanyName = originalCompany;
                MemberViewModel.CurrentMember.City = originalCity;
                MemberViewModel.CurrentMember.Country = originalCountry;
            }
        }
    }
}
