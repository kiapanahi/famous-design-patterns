using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using WinFormsApp.Models;
using WinFormsApp.Presenters;
using WinFormsApp.Views;

namespace WinFormsApp
{
    /// <summary>
    /// Main window for Windows Forms application. Most business logic resides 
    /// in this window as it responds to local control events, menu events, and 
    /// closed dialog events. This is usually the preferred model, unless the 
    /// child windows have significant processing requirements, then they handle 
    /// that themselves. 
    /// </summary>
    /// <remarks>
    /// All communications required for this application runs via the Service layer. 
    /// The application uses the Model View Presenter design pattern. Each of these
    /// reside in its own Visual Studio project.
    /// 
    /// MV Patterns: MVP design pattern is used throughout this WinForms application.
    /// </remarks>
    public partial class FormMain : Form, IMembersView, IOrdersView
    {
        private MembersPresenter _membersPresenter;
        private OrdersPresenter _ordersPresenter;

        /// <summary>
        /// Default form constructor. 
        /// </summary>
        public FormMain()
        {
            InitializeComponent();

            // Create two Presenters. Note: the form is the view. 
            _membersPresenter = new MembersPresenter(this);
            _ordersPresenter = new OrdersPresenter(this);
        }

        /// <summary>
        /// Displays login dialog box and loads member list in treeview.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var form = new FormLogin())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    labelAnnouncement.Visible = false;

                    _membersPresenter.Display();

                    Status = LoginStatus.LoggedIn;
                }
            }
        }

        /// <summary>
        /// Logoff user, empties datagridviews, and disables menus.
        /// </summary>
        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new LogoutPresenter(null).Logout();
            Status = LoginStatus.LoggedOut;

            labelAnnouncement.Visible = true;
        }

        /// <summary>
        /// Exits application.
        /// </summary>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Retrieves order data from the web service for the member currently selected.
        /// If, however, orders were retrieved previously, then these will be displayed. 
        /// The effect is that the client application speeds up over time. 
        /// </summary>
        private void treeViewMember_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Get selected member. Note: root node does not have a member record
            var member = treeViewMember.SelectedNode.Tag as MemberModel;
            if (member == null) return;

            // Check if orders were already retrieved for member
            if (member.Orders.Count > 0)
                BindOrders(member.Orders);
            else
            {
                this.Cursor = Cursors.WaitCursor;
                _ordersPresenter.Display(member.MemberId);

                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Databinds orders to dataGridView control. Private helper method.
        /// </summary>
        /// <param name="orders">Order list.</param>
        private void BindOrders(IList<OrderModel> orders)
        {
            if (orders == null) return;

            dataGridViewOrders.DataSource = orders;

            dataGridViewOrders.Columns["Member"].Visible = false;
            dataGridViewOrders.Columns["OrderDetails"].Visible = false;


            dataGridViewOrders.Columns["Freight"].DefaultCellStyle.Format = "C";
            dataGridViewOrders.Columns["Freight"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridViewOrders.Columns["RequiredDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewOrders.Columns["RequiredDate"].DefaultCellStyle.BackColor = Color.Cornsilk;
            dataGridViewOrders.Columns["RequiredDate"].DefaultCellStyle.Font = new Font(dataGridViewOrders.Font, FontStyle.Bold);

            dataGridViewOrders.Columns["OrderDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        /// <summary>
        /// Displays order details (line items) that are part of the currently 
        /// selected order. 
        /// </summary>
        private void dataGridViewOrders_SelectionChanged(object sender, EventArgs e)
        {
            dataGridViewOrderDetails.DataSource = null;
            if (dataGridViewOrders.SelectedRows.Count == 0) return;

            var row = dataGridViewOrders.SelectedRows[0];
            if (row == null) return;

            int orderId = int.Parse(row.Cells["OrderId"].Value.ToString());

            // Get member record from treeview control.
            var member = treeViewMember.SelectedNode.Tag as MemberModel;

            // Check for root node. It does not have a member record
            if (member == null) return;

            // Locate order record
            foreach (var order in member.Orders)
            {
                if (order.OrderId == orderId)
                {
                    if (order.OrderDetails.Count == 0) return;

                    dataGridViewOrderDetails.DataSource = order.OrderDetails;

                    dataGridViewOrderDetails.Columns["Order"].Visible = false;

                    dataGridViewOrderDetails.Columns["Discount"].DefaultCellStyle.Format = "C";
                    dataGridViewOrderDetails.Columns["UnitPrice"].DefaultCellStyle.Format = "C";
                    dataGridViewOrderDetails.Columns["Discount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridViewOrderDetails.Columns["UnitPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    dataGridViewOrderDetails.Columns["Quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dataGridViewOrderDetails.Columns["UnitPrice"].DefaultCellStyle.BackColor = Color.Cornsilk;

                    return;
                }
            }
        }

        /// <summary>
        /// Adds a new member.
        /// </summary>
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var form = new FormMember())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // Redisplay list of members
                    _membersPresenter.Display();
                }
            }
        }

        /// <summary>
        /// Edits an existing member record.
        /// </summary>
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Check if a node is selected (and not the root)
            if (treeViewMember.SelectedNode == null ||
                treeViewMember.SelectedNode.Text == "Members")
            {
                MessageBox.Show("No member is currently selected",
                            "Edit Member", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var form = new FormMember())
            {
                var member = treeViewMember.SelectedNode.Tag as MemberModel;
                form.MemberId = member.MemberId;

                if (form.ShowDialog() == DialogResult.OK)
                {
                    // Redisplay list of members
                    _membersPresenter.Display();
                }
            }
        }

        /// <summary>
        /// Deletes a member.
        /// </summary>
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Check if a node is selected (and not the root)
            if (treeViewMember.SelectedNode == null ||
                treeViewMember.SelectedNode.Text == "Members")
            {
                MessageBox.Show("No member is currently selected",
                            "Delete Member", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var member = treeViewMember.SelectedNode.Tag as MemberModel;

            try
            {
                if (member.Orders.Count > 0)
                {
                    MessageBox.Show("Cannot delete " + member.CompanyName + " because they have existing orders!", "Cannot Delete");
                    return;
                }

                // Execute delete
                new MemberPresenter(null).Delete(member.MemberId);

                // Remove node
                treeViewMember.Nodes[0].Nodes.Remove(treeViewMember.SelectedNode);
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message, "Delete failed");
            }
        }

        /// <summary>
        /// Selects clicked node and then displays context menu. The tree node selection
        /// is important here because this does not happen by default in this event. 
        /// </summary>
        private void treeViewMember_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                treeViewMember.SelectedNode =
                    treeViewMember.GetNodeAt(e.Location);

                contextMenuStripMember.Show((Control)sender, e.Location);
            }
        }

        /// <summary>
        /// Redirects login request to equivalent menu event handler.
        /// </summary>
        private void toolStripButtonLogin_Click(object sender, EventArgs e)
        {
            loginToolStripMenuItem_Click(this, null);
        }

        /// <summary>
        /// Redirects logout request to equivalent menu event handler.
        /// </summary>
        private void toolStripButtonLogout_Click(object sender, EventArgs e)
        {
            logoutToolStripMenuItem_Click(this, null);
        }

        /// <summary>
        /// Redirects add member request to equivalent menu event handler.
        /// </summary>
        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            if (addToolStripMenuItem.Enabled)
                addToolStripMenuItem_Click(this, null);
        }

        /// <summary>
        /// Redirects edit member request to equivalent menu event handler.
        /// </summary>
        private void toolStripButtonEdit_Click(object sender, EventArgs e)
        {
            if (editToolStripMenuItem.Enabled)
                editToolStripMenuItem_Click(this, null);
        }

        /// <summary>
        /// Redirects delete member request to equivalent menu event handler.
        /// </summary>
        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            if (deleteToolStripMenuItem.Enabled)
                deleteToolStripMenuItem_Click(this, null);
        }

        /// <summary>
        /// Opens the about dialog window.
        /// </summary>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormAbout();
            form.ShowDialog();
        }

        /// <summary>
        /// Help toolbutton clicked event handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show(" Help is not implemented... ", "Help");
        }

        /// <summary>
        /// Help menu item event handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void indexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(" Help is not implemented... ", "Help");
        }

        /// <summary>
        /// Adds a list of members to the left tree control.
        /// </summary>
        public IList<MemberModel> Members
        {
            set
            {
                var members = value;
                // Clear nodes under root of tree
                var root = treeViewMember.Nodes[0];
                root.Nodes.Clear();

                // Build the member tree
                foreach (var member in members)
                {
                    AddMemberToTree(member);
                }
            }
        }

        /// <summary>
        /// Private helper function that appends a member to the next node
        /// in the treeview control
        /// </summary>
        private TreeNode AddMemberToTree(MemberModel member)
        {
            var node = new TreeNode();
            node.Text = member.CompanyName + " (" + member.Country + ")";
            node.Tag = member;
            node.ImageIndex = 1;
            node.SelectedImageIndex = 1;
            this.treeViewMember.Nodes[0].Nodes.Add(node);

            return node;
        }

        #region IOrderView Members

        /// <summary>
        /// Databinds orders to dataGridView control
        /// </summary>
        public IList<OrderModel> Orders
        {
            set
            {
                // Unpack order transfer objects into order business objects.
                var orders = value;

                // Store orders for next time this member is selected.
                var member = treeViewMember.SelectedNode.Tag as MemberModel;
                member.Orders = orders;

                BindOrders(orders);
            }
        }
        #endregion

        #region Window State

        // Enumerates login status: Logged In or Logged Out.
        private enum LoginStatus
        {
            LoggedIn,
            LoggedOut
        }

        /// <summary>
        /// Central place where controls are enabled/disabled depending on
        /// logged in / logged out state.
        /// </summary>
        private LoginStatus Status
        {
            set
            {
                if (value == LoginStatus.LoggedIn)
                {
                    // Display tree expanded
                    treeViewMember.ExpandAll();

                    // Enable member add/edit/delete menu items.
                    this.addToolStripMenuItem.Enabled = true;
                    this.editToolStripMenuItem.Enabled = true;
                    this.deleteToolStripMenuItem.Enabled = true;

                    // Disable login menu
                    this.loginToolStripMenuItem.Enabled = false;
                }
                else
                {
                    // Clear nodes under root of tree
                    var root = treeViewMember.Nodes[0];
                    root.Nodes.Clear();

                    // Clear orders (this is databound, cannot touch rows)
                    dataGridViewOrders.DataSource = null;

                    // Clear order details (this is not databound)
                    dataGridViewOrderDetails.Rows.Clear();

                    // Disable member add/edit/delete menu items.
                    this.addToolStripMenuItem.Enabled = false;
                    this.editToolStripMenuItem.Enabled = false;
                    this.deleteToolStripMenuItem.Enabled = false;

                    // Disable login menu
                    this.loginToolStripMenuItem.Enabled = true;
                }
            }
        }

        #endregion
    }
}
