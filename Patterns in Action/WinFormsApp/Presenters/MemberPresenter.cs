using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinFormsApp.Models;
using WinFormsApp.Views;

namespace WinFormsApp.Presenters
{
    /// <summary>
    /// Member Presenter class.
    /// </summary>
    /// <remarks>
    /// MV Patterns: MVP design pattern.
    /// </remarks>
    public class MemberPresenter : Presenter<IMemberView>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="view">The view</param>
        public MemberPresenter(IMemberView view)
            : base(view)
        {
        }

        /// <summary>
        /// Gets member from model and sets values in the view.
        /// </summary>
        /// <param name="memberId">Member to display</param>
        public void Display(int memberId)
        {
            if (memberId <= 0) return;

            var member = Model.GetMember(memberId);

            View.MemberId = member.MemberId;
            View.Email = member.Email;
            View.CompanyName = member.CompanyName;
            View.City = member.City;
            View.Country = member.Country;
        }

        /// <summary>
        /// Saves a member by getting view data and sending it to model.
        /// </summary>
        /// <returns>Number of records affected.</returns>
        public void Save()
        {
            var member = new MemberModel
            {
                MemberId = View.MemberId,
                Email = View.Email,
                CompanyName = View.CompanyName,
                City = View.City,
                Country = View.Country,
            };

            if (member.MemberId == 0)
                Model.AddMember(member);
            else
                Model.UpdateMember(member);
        }

        /// <summary>
        /// Deletes a member by calling into model.
        /// </summary>
        /// <param name="memberId">The member to delete</param>
        /// <returns>Number of records affected.</returns>
        public void Delete(int memberId)
        {
            Model.DeleteMember(memberId);
        }
    }
}
