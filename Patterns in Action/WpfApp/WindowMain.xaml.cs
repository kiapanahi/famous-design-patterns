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
using WpfApp.ViewModels;

namespace WpfApp
{
    public partial class WindowMain : Window
    {
        // the member viewmodel.
        
        public MemberViewModel ViewModel { private set; get; }

        public WindowMain()
        {
            InitializeComponent();

            // create viewmodel and set data context

            ViewModel = new MemberViewModel(new Provider());
            DataContext = ViewModel;
        }

        
        // double clicking on member rectangle opens Orders dialog

        private void MemberListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ViewOrdersCommand_Executed(sender, null);
        }

        
        // hitting Enter key also opens Orders dialog
        // hitting Del key deletes item

        private void MemberListBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ViewOrdersCommand_Executed(sender, null);
            }
            else if (e.Key == Key.Delete)
            {
                if (ViewModel.CurrentMember == null)
                    MessageBox.Show("Please select a member first");
                else
                    DeleteCommand_Executed(null, null);
            }
        }

        #region Menu Command handlers

       
        // checks if login command can execute

        private void LoginCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !ViewModel.IsLoaded;
        }

        
        // executes login command. Opens login dialog and loads members

        private void LoginCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var window = new WindowLogin();
            window.Owner = this; // This will center dialog in owner window

            if (window.ShowDialog() == true)
            {
                TextBlockAnnouncement.Visibility = Visibility.Collapsed;

                Cursor = Cursors.Wait;
                ViewModel.LoadMembers();
                Cursor = Cursors.Arrow;

                CommandManager.InvalidateRequerySuggested();
            }
        }

        
        // checks if logout command can execute

        private void LogoutCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ViewModel.IsLoaded;
        }

        
        // executes logout command. Unload members

        private void LogoutCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ViewModel.UnloadMembers();
            TextBlockAnnouncement.Visibility = Visibility.Visible;

            CommandManager.InvalidateRequerySuggested();
        }

        // executes exit command. Shutdown application

        private void ExitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        
        // checks if add-member command can execute

        private void AddCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ViewModel.CanAdd;
        }

        
        // executes add-member command. Opens member dialog

        private void AddCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var window = new WindowMember();
            window.Owner = this;
            window.IsNewMember = true;

            if (window.ShowDialog() == true)
            {
                this.MemberListBox.ScrollIntoView(ViewModel.CurrentMember);
                CommandManager.InvalidateRequerySuggested();
            }
        }

        
        // checks if edit-member command can execute

        private void EditCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ViewModel.CanEdit;
        }

        
        // execute edit-member command. Opens member dialog

        private void EditCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var window = new WindowMember();
            window.Owner = this;

            if (window.ShowDialog() == true)
            {
                CommandManager.InvalidateRequerySuggested();
            }
        }

        
        // checks if delete-member command can execute

        private void DeleteCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ViewModel.CanDelete;
        }

        
        // executes delete-member command

        private void DeleteCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ViewModel.DeleteCommandModel.OnExecute(this, e);

            if (!e.Handled)
            {
                string name = ViewModel.CurrentMember != null ? ViewModel.CurrentMember.CompanyName : "member";
                MessageBox.Show("Cannot delete " + name + " because they have existing orders.", "Delete Member");
            }

            CommandManager.InvalidateRequerySuggested();
        }

        
        // checks if view-orders command can execute

        private void ViewOrdersCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ViewModel.CanViewOrders;
        }

        // execute view-orders command. Opens orders dialog

        private void ViewOrdersCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var window = new WindowOrders();
            window.Owner = this;

            window.ShowDialog();

            CommandManager.InvalidateRequerySuggested();
        }

        
        // executes How-do-I menu command

        private void HowDoICommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("How do I help is not implemented", "How Do I");
        }

        
        // Executes help index command

        private void IndexCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Help index is not implemented", "Index");
        }

        
        // executes about command. Opens about box

        private void AboutCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var window = new WindowAbout();
            window.Owner = this;
            window.ShowDialog();
        }

        #endregion
    }
}
