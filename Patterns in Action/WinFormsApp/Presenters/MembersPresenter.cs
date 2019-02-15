using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinFormsApp.Views;


namespace WinFormsApp.Presenters
{
    /// <summary>
    /// Members Presenter class.
    /// </summary>
    /// <remarks>
    /// MV Patterns: MVP design pattern.
    /// </remarks>
    public class MembersPresenter : Presenter<IMembersView>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="view">The view.</param>
        public MembersPresenter(IMembersView view)
            : base(view)
        {
        }

        /// <summary>
        /// Displays a list of members that are retrieved from model.
        /// </summary>
        public void Display()
        {
            View.Members = Model.GetMembers("CompanyName ASC");
        }
    }
}
