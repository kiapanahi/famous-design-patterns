using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace WinFormsApp
{
    /// <summary>
    /// Form to display app information and copyright notice.
    /// </summary>
    public partial class FormAbout : Form
    {
        /// <summary>
        /// Constructor of the About Window
        /// </summary>
        public FormAbout()
        {
            InitializeComponent();
        }

        private void FormAbout_Load(object sender, EventArgs e)
        {
            labelCopyright.Text = "Copyright © " + DateTime.Now.Year + ", Data && Object Factory, LLC. All Rights Reserved";
        }

        /// <summary>
        /// Closes about dialog box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
